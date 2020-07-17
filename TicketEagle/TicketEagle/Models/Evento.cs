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
        }

        [Key]
        public int EvId { get; set; }

        public DateTime Data { get; set; }

        //FK para o local
        [ForeignKey(nameof(Local))]
        public int LocalFK { get; set; }
        public virtual Local Local { get; set; }

        public virtual ICollection<Bilhete> Bilhete { get; set; }
    }
}
