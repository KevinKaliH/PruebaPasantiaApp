using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModel
{
    public class FacturaVM
    {
        public long NumeroFact { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Total { get; set; }
    }
}
