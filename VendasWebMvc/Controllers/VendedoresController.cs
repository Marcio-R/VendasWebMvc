using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Models;
using VendasWebMvc.Service;

namespace VendasWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedoorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedoorService, DepartamentoService departamentoService)
        {
            _vendedoorService = vendedoorService;
            _departamentoService = departamentoService;
        }

        public IActionResult Index()
        {
            var vendedores = _vendedoorService.TodosVendedores();
            return View(vendedores);
        }

        public IActionResult Create()
        {
            var departamentos = _departamentoService.GetDepartamentos();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
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
