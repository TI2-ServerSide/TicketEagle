using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class Evento
    {
        public Evento()
        {
            Bilhete = new HashSet<Bilhete>();
            Promotor = new HashSet<PromotorEvento>();
        }

        [Key]
        public int EvId { get; set; }

        public string Titulo { get; set; }

        public DateTime Data { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        //FK para o local
        [ForeignKey(nameof(Local))]
        public int LocalFK { get; set; }
        public virtual Local Local { get; set; }

        public virtual ICollection<Bilhete> Bilhete { get; set; }

        public virtual ICollection<PromotorEvento> Promotor { get; set; }
    }
}
