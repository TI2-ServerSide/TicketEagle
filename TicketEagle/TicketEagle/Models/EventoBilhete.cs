using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class EventoBilhete
    {

        //FK para o bilhete
        [Key]
        [ForeignKey(nameof(TicketID))]
        public int TicketFK { get; set; }
        public virtual Bilhete TicketID { get; set; }


        //FK para o local
        [ForeignKey(nameof(EventoID))]
        public int EventoFK { get; set; }
        public virtual Evento EventoID { get; set; }

    }
}
