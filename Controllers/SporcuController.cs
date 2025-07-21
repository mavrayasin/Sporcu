using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sporcu.Business.Abstract;
using Sporcu.Data;
using Sporcu.Entity;
using Sporcu.UnitOfWork;

namespace Sporcu.Controllers
{
    public class SporcuController(ISporcuService _sporcuService) : Controller
    {

        // GET: TblSporcus
        public async Task<IActionResult> Index()
        {
            return View(await _sporcuService.GetAllSporcuAsync());
        }

        // GET: TblSporcus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var tblSporcu = await _sporcuService.GetSporcuByIdAsync(Convert.ToInt32(id));

            return View(tblSporcu);
        }

        // GET: TblSporcus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblSporcus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TcKimlikNo,AdSoyad,DogumTarihi,Cinsiyet,IlId,IlceId,LisansBaslangic,LisansBitis,AktifMi")] TblSporcu tblSporcu)
        {
            await _sporcuService.AddSporcuAsync(tblSporcu);
            return RedirectToAction(nameof(Index));
        }

        // GET: TblSporcus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tblSporcu = await _sporcuService.GetSporcuByIdAsync(id ?? 0);
            return View(tblSporcu);
        }

        // POST: TblSporcus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TcKimlikNo,AdSoyad,DogumTarihi,Cinsiyet,IlId,IlceId,LisansBaslangic,LisansBitis,AktifMi")] TblSporcu tblSporcu)
        {

            var tblSporcuResult = await _sporcuService.UpdateSporcuAsync(tblSporcu);
            return View(tblSporcuResult);
        }

        // GET: TblSporcus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var tblSporcu = await _sporcuService.GetSporcuByIdAsync(id ?? 0);

            return View(tblSporcu);
        }

        // POST: TblSporcus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sporcuService.DeleteSporcuAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SporcuRapor()
        {
            var sporcuRapor = await _sporcuService.GetSporcuRapor();
            return View(sporcuRapor);
        }
        public async Task<IActionResult> SporcuSayisiRapor()
        {
            var sporcuSayisiRapor = await _sporcuService.GetSporcuSayisiRapor();
            return View(sporcuSayisiRapor);

        }
    }
}
