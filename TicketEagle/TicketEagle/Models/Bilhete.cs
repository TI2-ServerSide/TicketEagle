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
        public int TicketID { get; set; }

        public string email { get; set; }

        public string Descrição { get; set; }

        public DateTime DataCompra{ get; set; }


        //FK para o utiizador
        [ForeignKey(nameof(UserID))]
        public int IDFK { get; set; }
        public virtual Utilizador UserID { get; set; }
    }
}
