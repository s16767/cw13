using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cw13.Models
{
    public class OrderConfectionery
    {
        [Key, Column(Order = 0)]
        public int IdConfectionery { get; set; }
        public Confectionery Confectionery { get; set; }

        [Key, Column(Order = 0)]
        public int IdOrder { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
