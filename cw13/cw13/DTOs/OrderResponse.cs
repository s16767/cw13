using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw13.DTOs
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public List<string> Names { get; set; }
    }
}
