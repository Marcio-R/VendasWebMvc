using VendasWebMvc.Data;
using VendasWebMvc.Models;

namespace VendasWebMvc.Service
{
    public class DepartamentoService
    {
        private readonly VendasWebMvcContext Context;

        public DepartamentoService(VendasWebMvcContext context)
        {
            Context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            return Context.Departamento.OrderBy(x => x.Nome).ToList();
        }
    }
}
