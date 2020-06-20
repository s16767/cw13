using cw13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw13.DTOs
{
    public class OrderRequest
    {
        public DateTime DateOfAdmission { get; set; }
        public string Comments { get; set; }
        public List<ConfectioneryRequest> Confectioneries { get; set; }
    }
}
