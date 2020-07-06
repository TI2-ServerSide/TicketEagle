using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int UserID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Foto { get; set; }

        public virtual ICollection<Bilhete> ListaBilhetes { get; set; }
      
    }

  
}
