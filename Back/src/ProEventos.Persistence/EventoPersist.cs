using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist :  IEventoPersist
    {
        private readonly ProEventosContext _contexto;
        public EventoPersist(ProEventosContext contexto)
        {
            _contexto = contexto;

        }
        public void Add(Evento entity)
        {
           _contexto.Add(entity);
        }

        public void Update(Evento entity) 
        {
            _contexto.Update(entity);
        }
        public void Delete(Evento entity) 
        {
           _contexto.Remove(entity);
        }

        public void DeleteRange(Evento[] entityArr) 
        {
            _contexto.RemoveRange(entityArr);
        }
        public async Task<bool> SaveChangesAsync()
        {
           return (await _contexto.SaveChangesAsync())>0;
        }


        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes=false)
        {
            IQueryable<Evento> query = _contexto.Eventos
                                  .Include(x=>x.Lotes)
                                  .Include(x=>x.RedesSociais);
                    
            if (includePalestrantes)
                query.Include(x=>x.PalestranteEventos).ThenInclude(x=>x.Palestrante);

            query = query.OrderBy(x=>x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes=false)
        {
            IQueryable<Evento> query = _contexto.Eventos
                                  .Include(x=>x.Lotes)
                                  .Include(x=>x.RedesSociais);
                    
            if (includePalestrantes)
                query.Include(x=>x.PalestranteEventos).ThenInclude(x=>x.Palestrante);
                
            query = query.OrderBy(x=>x.Id).Where(x=>x.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes=false)
        {
           IQueryable<Evento> query = _contexto.Eventos.AsNoTracking()//NECESSÁRIO PARA NÃO BLOQUEAR 
                                  .Include(x=>x.Lotes)
                                  .Include(x=>x.RedesSociais);
                    
            if (includePalestrantes)
                query.Include(x=>x.PalestranteEventos).ThenInclude(x=>x.Palestrante);
                
           return await query.FirstOrDefaultAsync(x=>x.Id==eventoId);

            
        }



   
    }
}