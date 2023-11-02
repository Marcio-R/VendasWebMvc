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
            if (id != vendedor.Id)
            {
                return BadRequest();
            }
            try
            {
                _vendedoorService.Atualizar(vendedor);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException)
            {

                return NotFound();
            }
            catch (DbConcurrencyException)
            {

                return BadRequest();
            }
        }
    }
}
