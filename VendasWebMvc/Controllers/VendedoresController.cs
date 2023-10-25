using Microsoft.AspNetCore.Mvc;
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
    }
}
