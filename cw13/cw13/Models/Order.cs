using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw13.Models
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        [Required]
        public DateTime DateOfAdmission { get; set; }
        public DateTime DateOfRealization { get; set; }
        [MaxLength(300)]
        public string Comments { get; set; }

        public int IdClient { get; set; }
        public Client Client { get; set; }
        public int IdEmployee { get; set; }
        public Employee Employee { get; set; }

        public List<OrderConfectionery> OrderConfectioneries { get; set; }
    }
}
