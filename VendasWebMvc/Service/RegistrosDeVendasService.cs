using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Models;

namespace VendasWebMvc.Service
{
    public class RegistrosDeVendasService
    {
        private readonly VendasWebMvcContext Context;

        public RegistrosDeVendasService(VendasWebMvcContext context)
        {
            Context = context;
        }

        public async Task<List<RegistrosDeVendas>> EncontrarPorData(DateTime? minDate, DateTime? maxDate)
        {
            
            var query = from obj in Context.RegistrosDeVenda select obj;

            if (minDate.HasValue)
            {
                query = query.Where(x => x.Data >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                query = query.Where(x => x.Data <= maxDate.Value);
            }

                 return await query
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();


        }


    }
}
