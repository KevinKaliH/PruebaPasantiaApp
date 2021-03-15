using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class FacturaController : Controller
    {
        private readonly IFacturaRepository facturaRepository;

        public FacturaController(IFacturaRepository _facturaRepository)
        {
            facturaRepository = _facturaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        //Peticiones con ajax
        [HttpGet]
        public JsonResult GetAllFacturas()
        {
            IEnumerable<FacturaVM> list = facturaRepository.GetAll_Facturas().Select(e => new FacturaVM
            {
                NumeroFact = e.NumeroFact,
                Impuesto = e.Impuesto,
                FechaRegistro = e.FechaRegistro,
                Subtotal = e.Subtotal,
                Total = e.Total,
            });

            ResponseVM resp = new ResponseVM(true, "Enviando datos", list);

            return Json(resp);
        }

        [HttpPost]
        public JsonResult Create(FacturaVM facturaDto)
        {
            decimal imp = (decimal)(facturaDto.Impuesto / 100);
            facturaDto.Total =facturaDto.Subtotal + (imp * facturaDto.Subtotal);

            long result = facturaRepository.Create(new Factura 
            { 
                FechaRegistro = facturaDto.FechaRegistro,
                Impuesto = facturaDto.Impuesto,
                Subtotal = facturaDto.Subtotal,
                Total = facturaDto.Total
            });

            ResponseVM response = new ResponseVM(true, null, result);

            return Json(response);
        }

        [HttpPost]
        public JsonResult CreateDetails(DetalleFacturaVM detail)
        {
            int result = facturaRepository.AddDetail(new DetalleFactura
            {
                NumeroFact = detail.NumeroFact,
                Cantidad = detail.Cantidad,
                Precio = detail.Precio,
                IdArticulo = detail.IdArticulo,
                Impuesto = detail.Impuesto,
                Subtotal = detail.Subtotal,
                Total = detail.Total
            });
            
            return Json(result);
        }

    }
}
