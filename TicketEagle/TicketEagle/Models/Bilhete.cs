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

        [RegularExpression("[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]",ErrorMessage = "Deve Inserir um email valido")]
        public string email { get; set; }

        public string Descrição { get; set; }
        
        public DateTime DataCompra{ get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        //FK para o utiizador
        [ForeignKey(nameof(UserID))]
        public int IDFK { get; set; }
        public virtual Utilizador UserID { get; set; }
    }
}
