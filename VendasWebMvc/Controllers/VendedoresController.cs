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

        public async Task<IActionResult> Index()
        {
            var vendedores = await _vendedoorService.TodosVendedores();
            return View(vendedores);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.GetDepartamentos();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                var departamentos = await _departamentoService.GetDepartamentos();
                var viewModel = new VendedorFormViewModel { Departamentos = departamentos, Vendedor = vendedor };
                return View(viewModel);
            }
            await _vendedoorService.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vend = await _vendedoorService.GetId(id.Value);
            if (vend == null)
            {
                return NotFound();
            }
            return View(vend);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendedoorService.Remover(id);
                return RedirectToAction(nameof(Index));

            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Error),new {message = ex.Message});
            }

        }
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vend = await _vendedoorService.GetId(id.Value);
            if (vend == null)
            {
                return NotFound();
            }
            return View(vend);
        }
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var obj = await _vendedoorService.GetId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            List<Departamento> departamentos = await _departamentoService.GetDepartamentos();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {

                var departamento = await _departamentoService.GetDepartamentos();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
                return View(viewModel);

            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _vendedoorService.Atualizar(vendedor);
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
