using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            var ticketEagleContext = _context.Evento.Include(e => e.Local);
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
                .FirstOrDefaultAsync(m => m.EvId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        [Authorize(Roles="Promotor,Admin")]
        public IActionResult Create()
        {
            ViewData["LocalFK"] = new SelectList(_context.Local, "ID", "NomeLocal");
            ViewData["PromotorEvento"] = new SelectList(_context.Set<Promotor>(), "ID", "ID");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvId,Titulo,Data,Preco,LocalFK")] Evento evento,PromotorEvento prEv)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();

                //relacionar evento ao promotor que o criou
                prEv.EventoFK = _context.Evento.Where(b => b.Titulo == evento.Titulo).Select(b => b.EvId).FirstOrDefault();
                prEv.PromotorFK = _context.Promotor.Where(b => b.Nome == User.Identity.Name).Select(b => b.ID).FirstOrDefault();
                _context.Add(prEv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalFK"] = new SelectList(_context.Local, "ID", "NomeLocal", evento.LocalFK);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        [Authorize(Roles = "Promotor,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            //verificar se o utilizador é o proprio promotor do evento para poder edita-lo
           int ip = _context.Promotor.Where(p => p.Nome == User.Identity.Name).Select(p => p.ID).FirstOrDefault();
           int ip2= _context.PromotorEvento.Where(p => p.EventoFK == id).Select(p => p.PromotorFK).FirstOrDefault();

            if (id == null)
            {
                return NotFound();
            }

            if (ip != ip2)
            {
                //nao deixa editar se nao for o proprio promotor
                return StatusCode((int)System.Net.HttpStatusCode.Unauthorized, "Acesso a este Evento Nao Autorizado");
            }
            
            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["LocalFK"] = new SelectList(_context.Local, "ID", "NomeLocal", evento.LocalFK);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EvId,Titulo,Data,Preco,LocalFK")] Evento evento)
        {
            if (id != evento.EvId)
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
                    if (!EventoExists(evento.EvId))
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
            ViewData["LocalFK"] = new SelectList(_context.Local, "ID", "ID", evento.LocalFK);
            return View(evento);
        }

        [Authorize(Roles = "Promotor,Admin")]
        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Local)
                .FirstOrDefaultAsync(m => m.EvId == id);
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

        public IActionResult Comprar()
        {
            ViewData["EventoFK2"] = new SelectList(_context.Set<Evento>(), "EvId", "EvId");
            ViewData["IDFK"] = new SelectList(_context.Set<Utilizador>(), "UserID", "UserID");
            return View();
        }

        // POST: Bilhetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilizador")]
        public async Task<IActionResult> Comprar(Bilhete bilhete, [Bind("EvId,Local,Titulo,Preco")]Evento evento)
        {

            if (ModelState.IsValid)
            {

                //var ev = _context.Evento.Where(e => e.EvId == evento.EvId).Select(b=>b.Preco).FirstOrDefault();
                var ev = await _context.Evento.FindAsync(evento.EvId);

                //encontrar nome do local
                var loc = _context.Local.Where(b => b.ID == evento.LocalFK).Select(b => b.NomeLocal).FirstOrDefault();

                //encontrar o ID Utilizador do user autenticado
                bilhete.IDFK = _context.Utilizador.Where(b => b.Nome == User.Identity.Name).Select(b => b.UserID).FirstOrDefault();
                bilhete.email = User.Identity.Name;
                bilhete.DataCompra = DateTime.Now;

                //recebe preco do evento atraves do nome do select
                if (ev.Preco == 0)
                {
                    var p = Request.Form["pr"];
                    bilhete.Preco = decimal.Parse(Request.Form["pr"]);
                }
                else
                {
                    bilhete.Preco = ev.Preco;
                }

                bilhete.Descrição = string.Concat(loc, ev.Titulo);
                bilhete.EventoFK2 = evento.EvId;
                _context.Add(bilhete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoFK2"] = new SelectList(_context.Set<Evento>(), "EvId", "EvId", bilhete.EventoFK2);
            ViewData["IDFK"] = new SelectList(_context.Set<Utilizador>(), "UserID", "UserID", bilhete.IDFK);
            // ViewData["conf"] = "<script>alert('Change succesfully');</script>";
            //return View(bilhete);
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.EvId == id);
        }
    }
}
