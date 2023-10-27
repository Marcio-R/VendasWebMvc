using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Models;
using VendasWebMvc.Service;

namespace VendasWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedoorService;

        public VendedoresController(VendedorService vendedoorService)
        {
            _vendedoorService = vendedoorService;
        }
        public IActionResult Index()
        {
            var vendedores = _vendedoorService.TodosVendedores();
            return View(vendedores);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            _vendedoorService.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}
