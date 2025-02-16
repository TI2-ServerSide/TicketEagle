﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketEagle.Areas.Identity.Data;
using TicketEagle.Models;

namespace TicketEagle.Data
{
    public class TEDbContext : IdentityDbContext<TicketEagleUser>
    {
        public TEDbContext(DbContextOptions<TEDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PromotorEvento>()
            .HasKey(e => new { e.PromotorFK, e.EventoFK });

            builder.Entity<PromotorEvento>()
                .HasOne(su => su.Promotor)
                .WithMany(s => s.PromotorEvento)
                .HasForeignKey(su => su.PromotorFK);

            builder.Entity<PromotorEvento>()
                .HasOne(su => su.EvId)
                .WithMany(s => s.PromotorEvento)
                .HasForeignKey(su => su.EventoFK);


            //email tem que ser uniq
            builder.Entity<TicketEagleUser>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
    
   public DbSet<TicketEagle.Models.Bilhete> Bilhete { get; set; }

   public DbSet<TicketEagle.Models.Evento> Evento { get; set; }

    public DbSet<TicketEagle.Models.Utilizador> Utilizador { get; set; }

   public DbSet<TicketEagle.Models.Promotor> Promotor { get; set; }

    public DbSet<TicketEagle.Models.Local> Local { get; set; }

   public DbSet<TicketEagle.Models.PromotorEvento> PromotorEvento { get; set; }

    }
    }

    
