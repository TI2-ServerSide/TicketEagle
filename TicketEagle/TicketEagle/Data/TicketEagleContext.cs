using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketEagle.Models;

namespace TicketEagle.Data
{
    public class TicketEagleContext : DbContext
    {
        public TicketEagleContext (DbContextOptions<TicketEagleContext> options)
            : base(options)
        {
        }

        public DbSet<TicketEagle.Models.Bilhete> Bilhete { get; set; }

        public DbSet<TicketEagle.Models.Evento> Evento { get; set; }

        public DbSet<TicketEagle.Models.Utilizador> Utilizador { get; set; }

        public DbSet<TicketEagle.Models.Promotor> Promotor { get; set; }

        public DbSet<TicketEagle.Models.Local> Local { get; set; }
    }
}
