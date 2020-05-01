using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class Bilhete
    {
        [Key]
        public int ID { get; set; }

        public string NomeEvento { get; set; }

        public string Descrição { get; set; }

        [ForeignKey("Local")]
        public int LocalFK { get; set; }

        public string Data { get; set; }


    }
}
