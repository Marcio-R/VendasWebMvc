using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Models;
using VendasWebMvc.Service;
using VendasWebMvc.Service.Exceptions;

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
            if (!ModelState.IsValid)
            {
                var departamento = _departamentoService.GetDepartamentos();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
                return View(viewModel);
            }
            _vendedoorService.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vend = _vendedoorService.GetId(id.Value);
            if (vend == null)
            {
                return NotFound();
            }
            return View(vend);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _vendedoorService.Remover(id);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vend = _vendedoorService.GetId(id.Value);
            if (vend == null)
            {
                return NotFound();
            }
            return View(vend);
        }
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var obj = _vendedoorService.GetId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            List<Departamento> departamentos = _departamentoService.GetDepartamentos();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {

                var departamento = _departamentoService.GetDepartamentos();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
                return View(viewModel);

            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _vendedoorService.Atualizar(vendedor);
                return RedirectToAction(nameof(Index));

            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        private object Error()
        {
            throw new NotImplementedException();
        }
    }
}
