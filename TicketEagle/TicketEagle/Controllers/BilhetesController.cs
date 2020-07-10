using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketEagle.Data;
using TicketEagle.Models;

namespace TicketEagle.Controllers
{
    public class BilhetesController : Controller
    {
        private readonly TicketEagleContext _context;

        public BilhetesController(TicketEagleContext context)
        {
            _context = context;
        }

        // GET: Bilhetes
        public async Task<IActionResult> Index()
        {
            var ticketEagleContext = _context.Bilhete.Include(b => b.UserID);
            return View(await ticketEagleContext.ToListAsync());
        }

        // GET: Bilhetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilhete = await _context.Bilhete
                .Include(b => b.UserID)
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (bilhete == null)
            {
                return NotFound();
            }

            return View(bilhete);
        }

        // GET: Bilhetes/Create
        public IActionResult Create()
        {
            ViewData["IDFK"] = new SelectList(_context.Set<Utilizador>(), "UserID", "UserID");
            return View();
        }

        // POST: Bilhetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,email,Descrição,DataCompra,IDFK,Preco")] Bilhete bilhete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bilhete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDFK"] = new SelectList(_context.Set<Utilizador>(), "UserID", "UserID", bilhete.IDFK);
            return View(bilhete);
        }

        // GET: Bilhetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilhete = await _context.Bilhete.FindAsync(id);
            if (bilhete == null)
            {
                return NotFound();
            }
            ViewData["IDFK"] = new SelectList(_context.Set<Utilizador>(), "UserID", "UserID", bilhete.IDFK);
            return View(bilhete);
        }

        // POST: Bilhetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,email,Descrição,DataCompra,IDFK,Preco")] Bilhete bilhete)
        {
            if (id != bilhete.TicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bilhete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BilheteExists(bilhete.TicketID))
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
            ViewData["IDFK"] = new SelectList(_context.Set<Utilizador>(), "UserID", "UserID", bilhete.IDFK);
            return View(bilhete);
        }

        // GET: Bilhetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilhete = await _context.Bilhete
                .Include(b => b.UserID)
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (bilhete == null)
            {
                return NotFound();
            }

            return View(bilhete);
        }

        // POST: Bilhetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bilhete = await _context.Bilhete.FindAsync(id);
            _context.Bilhete.Remove(bilhete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BilheteExists(int id)
        {
            return _context.Bilhete.Any(e => e.TicketID == id);
        }
    }
}
