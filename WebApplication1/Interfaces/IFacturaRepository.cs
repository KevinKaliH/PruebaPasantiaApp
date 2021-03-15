using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IFacturaRepository
    {
        public long Create(Factura factura);

        public int AddDetail(DetalleFactura detalleFactura);
        public IEnumerable<Factura> GetAll_Facturas();
    }
}
