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
    public class EventosController : Controller
    {
        private readonly TEDbContext _context;

        public EventosController(TEDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var ticketEagleContext = _context.Evento.Include(e => e.Local).Include(e => e.TicketID);
            return View(await ticketEagleContext.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Local)
                .Include(e => e.TicketID)
                .FirstOrDefaultAsync(m => m.EventoID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["LocalFK"] = new SelectList(_context.Set<Local>(), "ID", "ID");
            ViewData["TicketFK"] = new SelectList(_context.Bilhete, "TicketID", "TicketID");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoID,Data,TicketFK,LocalFK")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalFK"] = new SelectList(_context.Set<Local>(), "ID", "ID", evento.LocalFK);
            ViewData["TicketFK"] = new SelectList(_context.Bilhete, "TicketID", "TicketID", evento.TicketFK);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["LocalFK"] = new SelectList(_context.Set<Local>(), "ID", "ID", evento.LocalFK);
            ViewData["TicketFK"] = new SelectList(_context.Bilhete, "TicketID", "TicketID", evento.TicketFK);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoID,Data,TicketFK,LocalFK")] Evento evento)
        {
            if (id != evento.EventoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.EventoID))
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
            ViewData["LocalFK"] = new SelectList(_context.Set<Local>(), "ID", "ID", evento.LocalFK);
            ViewData["TicketFK"] = new SelectList(_context.Bilhete, "TicketID", "TicketID", evento.TicketFK);
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Local)
                .Include(e => e.TicketID)
                .FirstOrDefaultAsync(m => m.EventoID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            _context.Evento.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.EventoID == id);
        }
    }
}
