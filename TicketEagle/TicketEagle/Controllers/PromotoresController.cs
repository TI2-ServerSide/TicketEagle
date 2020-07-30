using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketEagle.Data;
using TicketEagle.Models;

namespace TicketEagle.Controllers
{
    public class PromotoresController : Controller
    {
        private readonly TEDbContext _context;

        public PromotoresController(TEDbContext context)
        {
            _context = context;
        }

        //[Authorize(Roles="Promotor, Admin")]
        // GET: Promotores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promotor.ToListAsync());
        }

        // GET: Promotores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotor = await _context.Promotor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (promotor == null)
            {
                return NotFound();
            }

            return View(promotor);
        }

        // GET: Promotores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promotores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Email,Password,Foto")] Promotor promotor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promotor);
        }

        // GET: Promotores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotor = await _context.Promotor.FindAsync(id);
            if (promotor == null)
            {
                return NotFound();
            }
            return View(promotor);
        }

        // POST: Promotores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Email,Password,Foto")] Promotor promotor)
        {
            if (id != promotor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotorExists(promotor.ID))
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
            return View(promotor);
        }

        // GET: Promotores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotor = await _context.Promotor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (promotor == null)
            {
                return NotFound();
            }

            return View(promotor);
        }

        // POST: Promotores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotor = await _context.Promotor.FindAsync(id);
            _context.Promotor.Remove(promotor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromotorExists(int id)
        {
            return _context.Promotor.Any(e => e.ID == id);
        }
    }
}
