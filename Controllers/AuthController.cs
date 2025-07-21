using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Sporcu.Data;
using Sporcu.Dtos;
using Sporcu.Entity;
using Sporcu.Helpers;
using System.Text.Json;

namespace Sporcu.Controllers
{
    public class AuthController : Controller
    {
        private readonly SporcuTakipDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        public AuthController(SporcuTakipDbContext context, IConfiguration configuration,IJwtTokenHelper jwtTokenHelper)
        {
            _context = context;
            _configuration = configuration;
            _jwtTokenHelper = jwtTokenHelper;
        }

        // GET: Auth/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Email kontrolü
            if (_context.UserSporcus.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("", "Bu email zaten kayıtlı.");
                return View(model);
            }

            // Şifre hashleme
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new UserSporcu
            {
                Username = model.Username,
                PasswordHash = passwordHash,
            };

            _context.UserSporcus.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }
        // GET: Auth/Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Kullanıcıyı veritabanından al
            var user = await _context.UserSporcus.FirstOrDefaultAsync(u => u.Username == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Geçersiz giriş.");
                return View();
            }

            // reCAPTCHA doğrulama
            var response = Request.Form["g-recaptcha-response"];
            var client = new HttpClient();
            var secret = _configuration["RecaptchaSettings:SecretKey"];
            var verify = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={response}", null);
            var json = await verify.Content.ReadAsStringAsync();
            var captcha = JsonSerializer.Deserialize<RecaptchaResponse>(json);

            if (!captcha.success)
            {
                ModelState.AddModelError("", "reCAPTCHA doğrulaması başarısız.");
                return View();
            }

            // JWT token oluştur
            var token = _jwtTokenHelper.GenerateToken(user);

            // HttpOnly cookie ekle
            Response.Cookies.Append("X-Access-Token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15) // Örnek 15 dk
            });

            return RedirectToAction("Index", "Sporcu");
        }
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            return RedirectToAction("Login");
        }
    }
}
