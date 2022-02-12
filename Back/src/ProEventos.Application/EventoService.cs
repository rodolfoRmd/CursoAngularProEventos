using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IEventoPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _eventoPersist.Add(model);
                if (await _eventoPersist.SaveChangesAsync())
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                else
                    return null;
            }
            catch (Exception ex)  
            {

                throw new Exception(ex.Message);
            }
        }

          public async Task<Evento> UpdateEvento(int eventoID, Evento model)
        {
           try
           {
               var evento = await _eventoPersist.GetEventoByIdAsync(eventoID,false);
               if(evento==null) return null;

               model.Id = evento.Id;

                _eventoPersist.Update(model);
                if (await _eventoPersist.SaveChangesAsync())
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                else
                    return null;
           }
           catch (Exception ex)
           {
               
                 throw new Exception(ex.Message);
           }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                if(evento==null) throw new Exception("Evento para delete não encontrado");

                _eventoPersist.Delete(evento);
                return await _eventoPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
           try
           {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(eventos==null) return null; 

                return eventos;
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
           try
           {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema,includePalestrantes);
                if(eventos==null) return null; 

                return eventos;
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
             try
           {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId,includePalestrantes);
                if(evento==null) return null; 

                return evento;
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
        }

      
    }
}