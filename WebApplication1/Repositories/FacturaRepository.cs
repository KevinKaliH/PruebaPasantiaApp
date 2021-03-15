using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly dbIngemanContext db;
        public FacturaRepository(dbIngemanContext _db)
        {
            db = _db;
        }

        public int AddDetail(DetalleFactura detalleFactura)
        {
            Factura fact = db.Facturas.SingleOrDefault(x => x.NumeroFact == detalleFactura.NumeroFact);
            fact.DetalleFacturas.Add(detalleFactura);

            return db.SaveChanges();
        }

        public long Create(Factura factura)
        {
            db.Facturas.Add(factura);
            db.SaveChanges();

            return factura.NumeroFact;
        }

        public IEnumerable<Factura> GetAll_Facturas()
        {
            return db.Facturas
                .OrderByDescending(x => x.NumeroFact)
                .ToList();
        }
    }
}
