using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketEagle.Models
{
    public class Utilizador
    {
        public Utilizador()
        {
            ListaBilhetes = new HashSet<Bilhete>();
        }

        [Key]
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Password { get; set; }

        public string Foto { get; set; }

        public virtual ICollection<Bilhete> ListaBilhetes { get; set; }
      
    }

  
}
