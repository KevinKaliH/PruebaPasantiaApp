using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModel
{
    public class DetalleFacturaVM
    {
        public long Id { get; set; }
        public long NumeroFact { get; set; }
        public long IdArticulo { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Total { get; set; }

    }
}
