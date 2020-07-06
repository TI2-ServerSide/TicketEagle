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
        [Key]
        public int EventoID { get; set; }

        public DateTime Data { get; set; }

        //FK para o bilhete
        [ForeignKey(nameof(TicketID))]
        public int TicketFK { get; set; }
        public virtual Bilhete TicketID { get; set; }


        //FK para o local
        [ForeignKey(nameof(Local))]
        public int LocalFK { get; set; }
        public virtual Local Local { get; set; }

    }
}
