using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class Local
    {
        [Key]
        public int ID { get; set; }

        public string NomeLocal { get; set; }

        public string Descrição { get; set; }
        
        public int Capacidade { get; set; }
        
        public string Foto { get; set; }

    }
}
