using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Departamento>> GetDepartamentos()
        {
            return await Context.Departamento.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
