using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using VendasWebMvc.Service.Exceptions;

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
            Context.Add(vendedor);
            Context.SaveChanges();
        }
        public Vendedor GetId(int id)
        {
            return Context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(obj => obj.Id == id);
        }
        public void Remover(int id)
        {
            var obj = Context.Vendedor.Find(id);
            Context.Vendedor.Remove(obj);
            Context.SaveChanges();
        }
        public void Atualizar(Vendedor obj)
        {
            if (!Context.Vendedor.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                Context.Update(obj);
                Context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
