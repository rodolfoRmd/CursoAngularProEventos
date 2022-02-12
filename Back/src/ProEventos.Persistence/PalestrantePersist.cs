using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IGeralPersist<Palestrante>, IPalestrantePersist
    {
        private readonly ProEventosContext _contexto;
        public PalestrantePersist(ProEventosContext contexto)
        {
            _contexto = contexto;

        }
        public void Add(Palestrante entity) 
        {
           _contexto.Add(entity);
        }

        public void Update(Palestrante entity) 
        {
            _contexto.Update(entity);
        }
        public void Delete(Palestrante entity) 
        {
           _contexto.Remove(entity);
        }

        public void DeleteRange(Palestrante[] entityArr) 
        {
            _contexto.RemoveRange(entityArr);
        }
        public async Task<bool> SaveChangesAsync()
        {
           return (await _contexto.SaveChangesAsync())>0;
        }


       
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos =false)
        {
              IQueryable<Palestrante> query = _contexto.Palestrantes
                               .Include(x=>x.RedesSociais);
                                  
                    
            if (includeEventos)
                query.Include(x=>x.PalestranteEventos).ThenInclude(x=>x.Evento);

            query = query.OrderBy(x=>x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos=false)
        {
             IQueryable<Palestrante> query = _contexto.Palestrantes
                                  .Include(x=>x.RedesSociais);
                    
            if (includeEventos)
                query.Include(x=>x.PalestranteEventos).ThenInclude(x=>x.Evento);
                
            query = query.OrderBy(x=>x.Id).Where(x=>x.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }


        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
              IQueryable<Palestrante> query = _contexto.Palestrantes.AsNoTracking()
                                  .Include(x=>x.RedesSociais);
                    
            if (includeEventos)
                query.Include(x=>x.PalestranteEventos).ThenInclude(x=>x.Palestrante);
                
           return await query.FirstOrDefaultAsync(x=>x.Id==palestranteId);
        }


    }
}