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

        public async Task<List<Vendedor>> TodosVendedores()
        {
            return await Context.Vendedor.ToListAsync();
        }
        public async Task Insert(Vendedor vendedor)
        {
            Context.Add(vendedor);
            await Context.SaveChangesAsync();
        }
        public async Task<Vendedor> GetId(int id)
        {
            return await Context.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task Remover(int id)
        {
            try
            {

                var obj = await Context.Vendedor.FindAsync(id);
                Context.Vendedor.Remove(obj);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Atualizar(Vendedor obj)
        {
            bool hasAny = await Context.Vendedor.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                Context.Update(obj);
                await Context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
