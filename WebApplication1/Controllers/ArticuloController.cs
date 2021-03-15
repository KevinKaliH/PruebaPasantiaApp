using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly IArticulosRepository artRepository;

        public ArticuloController(IArticulosRepository _articuloRepositoy)
        {
            artRepository = _articuloRepositoy;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewData["message"] = null;

            return View();
        }

        [HttpPost]
        public IActionResult Create(ArticuloVM dto)
        {
            ViewData["message"] = null;

            if (!ModelState.IsValid)
                return View(dto);

            Articulo articulo = new Articulo
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Costo = dto.Costo,
                Activo = dto.Activo
            };

            bool result = artRepository.Create_Articulo(articulo);

            if (!result)
            {
                ViewData["message"] = "El código de articulo ya existe, Ingresar uno nuevo!!";
                return View(dto);
            }

            return RedirectToAction("Index", "Articulo");
        }

        public IActionResult Edit(int id)
        {
            ViewData["message"] = null;

            Articulo data = artRepository.GetArticulo(id);

            ArticuloVM dto = new ArticuloVM
            {
                Id = data.Id,
                Activo = data.Activo,
                Codigo = data.Codigo,
                Descripcion = data.Descripcion,
                Costo = data.Costo,
                Precio = data.Precio
            };

            return View(dto);
        }
        
        [HttpPost]
        public IActionResult Edit(ArticuloVM dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            Articulo articulo = artRepository.GetArticulo(dto.Id);
            articulo.Codigo = dto.Codigo;
            articulo.Descripcion = dto.Descripcion;
            articulo.Precio = dto.Precio;
            articulo.Costo = dto.Costo;
            articulo.Activo = dto.Activo;
            
            bool result = artRepository.Update_Articulo(articulo);

            if (!result)
            {
                ViewData["message"] = "El Código le pertenece a otro registro, Ingresar uno nuevo!!";
                return View(dto);
            }

            return RedirectToAction("Index", "Articulo");
        }

        //Peticiones con Ajax
        [HttpGet]
        public JsonResult GetAllArticles(bool isActive)
        {
            IEnumerable<ArticuloVM> list = artRepository.GetAll_Articulos(isActive).Select(e => new ArticuloVM
            {
                Id = e.Id,
                Activo = e.Activo,
                Codigo = e.Codigo,
                Descripcion = e.Descripcion,
                Costo = e.Costo,
                Precio = e.Precio
            });

            ResponseVM resp = new ResponseVM(true, "Enviando datos", list);

            return Json(resp);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            bool result = await artRepository.CambiarEstado(id);

            var response = new
            {
                success = true,
                message = "El producto a sido Actualizado"
            };

            return Json(response);
        }
    
        [HttpGet]
        public JsonResult SearchArticle(string codigo)
        {
            IEnumerable<ArticuloVM> list = artRepository.GetArticulosByCodigo(codigo).Select(e => new ArticuloVM
            {
                Id = e.Id,
                Activo = e.Activo,
                Codigo = e.Codigo,
                Descripcion = e.Descripcion,
                Costo = e.Costo,
                Precio = e.Precio
            });

            ResponseVM resp;
            if (list != null)
            {

                resp = new ResponseVM(true,null, list);
            }
            else
                resp = new ResponseVM(false, null, list);

            return Json(resp);
        }
    }
}
