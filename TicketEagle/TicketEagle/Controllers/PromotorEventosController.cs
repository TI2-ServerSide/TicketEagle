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
    public class PromotorEventosController : Controller
    {
        private readonly TEDbContext _context;

        public PromotorEventosController(TEDbContext context)
        {
            _context = context;
        }

        // GET: PromotorEventos
        public async Task<IActionResult> Index()
        {
            var ticketEagleContext = _context.PromotorEvento.Include(p => p.EvId).Include(p => p.Promotor);
            return View(await ticketEagleContext.ToListAsync());
        }

        // GET: PromotorEventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotorEvento = await _context.PromotorEvento
                .Include(p => p.EvId)
                .Include(p => p.Promotor)
                .FirstOrDefaultAsync(m => m.PromotorFK == id);
            if (promotorEvento == null)
            {
                return NotFound();
            }

            return View(promotorEvento);
        }

        // GET: PromotorEventos/Create
        public IActionResult Create()
        {
            ViewData["EventoFK"] = new SelectList(_context.Evento, "EvId", "EvId");
            ViewData["PromotorFK"] = new SelectList(_context.Promotor, "ID", "ID");
            return View();
        }

        // POST: PromotorEventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromotorFK,EventoFK")] PromotorEvento promotorEvento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotorEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoFK"] = new SelectList(_context.Evento, "EvId", "EvId", promotorEvento.EventoFK);
            ViewData["PromotorFK"] = new SelectList(_context.Promotor, "ID", "ID", promotorEvento.PromotorFK);
            return View(promotorEvento);
        }

        // GET: PromotorEventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotorEvento = await _context.PromotorEvento.FindAsync(id);
            if (promotorEvento == null)
            {
                return NotFound();
            }
            ViewData["EventoFK"] = new SelectList(_context.Evento, "EvId", "EvId", promotorEvento.EventoFK);
            ViewData["PromotorFK"] = new SelectList(_context.Promotor, "ID", "ID", promotorEvento.PromotorFK);
            return View(promotorEvento);
        }

        // POST: PromotorEventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromotorFK,EventoFK")] PromotorEvento promotorEvento)
        {
            if (id != promotorEvento.PromotorFK)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotorEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotorEventoExists(promotorEvento.PromotorFK))
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
            ViewData["EventoFK"] = new SelectList(_context.Evento, "EvId", "EvId", promotorEvento.EventoFK);
            ViewData["PromotorFK"] = new SelectList(_context.Promotor, "ID", "ID", promotorEvento.PromotorFK);
            return View(promotorEvento);
        }

        // GET: PromotorEventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotorEvento = await _context.PromotorEvento
                .Include(p => p.EvId)
                .Include(p => p.Promotor)
                .FirstOrDefaultAsync(m => m.PromotorFK == id);
            if (promotorEvento == null)
            {
                return NotFound();
            }

            return View(promotorEvento);
        }

        // POST: PromotorEventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotorEvento = await _context.PromotorEvento.FindAsync(id);
            _context.PromotorEvento.Remove(promotorEvento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromotorEventoExists(int id)
        {
            return _context.PromotorEvento.Any(e => e.PromotorFK == id);
        }
    }
}
