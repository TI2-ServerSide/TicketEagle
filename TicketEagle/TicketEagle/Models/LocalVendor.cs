using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class LocalVendor
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Vendedor))]
        public int VendedorFK { get; set; }

        public virtual Promotor Vendedor { get; set; }

        [Key]
        [Column(Order = 1)] 
        [ForeignKey(nameof(Local))]
        public int LocalFK { get; set; }

        public virtual Local Local { get; set; }

    }
}
