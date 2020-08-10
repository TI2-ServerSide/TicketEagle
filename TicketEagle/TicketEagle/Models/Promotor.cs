using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class Promotor
    {
        public Promotor()
        {
            PromotorEvento = new HashSet<PromotorEvento>();
        }

        [Key]
        public int ID { get; set; }

        public string Nome { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Foto{get; set; }

        public virtual ICollection<PromotorEvento> PromotorEvento { get; set; }

    }
}
