using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using VendasWebMvc.Service;

namespace VendasWebMvc.Controllers
{
    public class RegistrosDeVendasController : Controller
    {
        private readonly RegistrosDeVendasService RegistroServices;

        public RegistrosDeVendasController(RegistrosDeVendasService registroServices)
        {
            RegistroServices = registroServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> BuscaSimples(DateTime? minDate, DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await RegistroServices.EncontrarPorData(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> BuscaAgrupada()
        {
            return View();
        }
    }
}
