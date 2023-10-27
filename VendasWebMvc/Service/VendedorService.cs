using VendasWebMvc.Data;
using VendasWebMvc.Models;

namespace VendasWebMvc.Service
{
    public class VendedorService
    {
        private readonly VendasWebMvcContext Context;

        public VendedorService(VendasWebMvcContext context)
        {
            Context = context;
        }

        public List<Vendedor> TodosVendedores()
        {
            return Context.Vendedor.ToList();
        }
        public void Insert(Vendedor vendedor)
        {
            vendedor.Departamento = Context.Departamento.First();
            Context.Add(vendedor);
            Context.SaveChanges();
        }
    }
}
