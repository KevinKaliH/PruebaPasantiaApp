using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ArticulosRepository : IArticulosRepository
    {
        private readonly dbIngemanContext db;

        public ArticulosRepository(dbIngemanContext _db)
        {
            db = _db;
        }

        public Articulo GetArticulo(long id)
        {
            return db.Articulos.SingleOrDefault(x => x.Id == id);
        }

        public List<Articulo> GetArticulosByCodigo(string codigo)
        {
            return db.Articulos.Where(x => x.Codigo.StartsWith(codigo)).ToList();
        }

        public List<Articulo> GetAll_Articulos(bool isActive)
        {
            return db.Articulos
                .Where(x => x.Activo == isActive)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public async Task<bool> CambiarEstado(long id)
        {
            Articulo aux = this.GetArticulo(id);
            aux.Activo = !aux.Activo;

            int result = await db.SaveChangesAsync();
            return result == 1;
        }

        public bool Create_Articulo(Articulo articulo)
        {
            int result = db.Articulos.Where(x => x.Codigo == articulo.Codigo).Count();
            if (result > 0)
                return false;

            db.Articulos.Add(articulo);
            db.SaveChanges();

            return true;
        }

        public bool Update_Articulo(Articulo articulo)
        {
            Articulo aux = db.Articulos.Where(x => x.Codigo == articulo.Codigo).FirstOrDefault();
            if (aux != null)
            {
                bool result = aux.Id == articulo.Id;

                if (!result)
                    return false;
            }

            db.SaveChanges();
            return true;
        }
    }
}
