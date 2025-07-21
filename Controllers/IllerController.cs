using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sporcu.Data;
using Sporcu.Entity;

namespace Sporcu.Controllers
{
    public class IllerController : Controller
    {
        private readonly SporcuTakipDbContext _context;

        public IllerController(SporcuTakipDbContext context)
        {
            _context = context;
        }

        // GET: Iller
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblIls.ToListAsync());
        }

        // GET: Iller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblIl = await _context.TblIls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblIl == null)
            {
                return NotFound();
            }

            return View(tblIl);
        }

        // GET: Iller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Iller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad")] TblIl tblIl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblIl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblIl);
        }

        // GET: Iller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblIl = await _context.TblIls.FindAsync(id);
            if (tblIl == null)
            {
                return NotFound();
            }
            return View(tblIl);
        }

        // POST: Iller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad")] TblIl tblIl)
        {
            if (id != tblIl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblIl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblIlExists(tblIl.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblIl);
        }

        // GET: Iller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblIl = await _context.TblIls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblIl == null)
            {
                return NotFound();
            }

            return View(tblIl);
        }

        // POST: Iller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblIl = await _context.TblIls.FindAsync(id);
            if (tblIl != null)
            {
                _context.TblIls.Remove(tblIl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblIlExists(int id)
        {
            return _context.TblIls.Any(e => e.Id == id);
        }
    }
}
