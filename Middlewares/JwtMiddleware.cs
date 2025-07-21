using Sporcu.Helpers;

namespace Sporcu.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IJwtTokenHelper jwtTokenHelper)
        {
            var token = context.Request.Cookies["X-Access-Token"];

            if (!string.IsNullOrEmpty(token))
            {
                var principal = jwtTokenHelper.ValidateToken(token);
                if (principal == null)
                {
                    // Token geçersiz veya süresi dolmuş => cookie sil => login sayfasına yönlendir
                    context.Response.Cookies.Delete("X-Access-Token");
                    context.Response.Redirect("/Auth/Login");
                    return;
                }

                context.User = principal;
            }
         

                await _next(context);
        }
    }

}
