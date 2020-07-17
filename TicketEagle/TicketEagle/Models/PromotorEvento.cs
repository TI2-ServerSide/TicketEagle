using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class PromotorEvento
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Promotor))]
        public int PromotorFK { get; set; }

        public virtual Promotor Promotor { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey(nameof(EvId))]
        public int EventoFK { get; set; }

        public virtual Evento EvId { get; set; }
    }
}
