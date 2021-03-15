using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IArticulosRepository
    {
        public List<Articulo> GetAll_Articulos(bool isActive);

        public Articulo GetArticulo(long id);

        public Task<Boolean> CambiarEstado(long id);

        public bool Create_Articulo(Articulo articulo);

        public bool Update_Articulo(Articulo articulo);

        public List<Articulo> GetArticulosByCodigo(string codigo);
    }
}
