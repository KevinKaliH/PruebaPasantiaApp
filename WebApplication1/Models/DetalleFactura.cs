using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class DetalleFactura
    {
        public long Id { get; set; }
        public long NumeroFact { get; set; }
        public long IdArticulo { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Total { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual Factura NumeroFactNavigation { get; set; }
    }
}
