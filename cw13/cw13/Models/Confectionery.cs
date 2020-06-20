using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw13.Models
{
    public class Confectionery
    {
        [Key]
        public int IdConfectionery { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public float PriceForPiece { get; set; }
        [Required]
        [MaxLength(40)]
        public string Type { get; set; }
        [MaxLength(300)]
        public string Comments { get; set; }

        public List<OrderConfectionery> OrderConfectioneries { get; set; }
    }
}
