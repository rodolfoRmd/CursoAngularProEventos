using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;
        public EventoService(IEventoPersist eventoPersist, IMapper mapper)
        {
            this._mapper = mapper;
            _eventoPersist = eventoPersist;

        }
        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var eventoDomain = _mapper.Map<Evento>(model);

                _eventoPersist.Add(eventoDomain);
                if (await _eventoPersist.SaveChangesAsync()){
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(eventoDomain.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                   
                else
                    return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoID, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoID, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                var eventoDomain = _mapper.Map<Evento>(model);

                _eventoPersist.Update(eventoDomain);
                if (await _eventoPersist.SaveChangesAsync()){
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(eventoDomain.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                   
                }
                    
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
                if (evento == null) throw new Exception("Evento para delete não encontrado");

                _eventoPersist.Delete(evento);
                return await _eventoPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                 var eventosDto = _mapper.Map<EventoDto[]>(eventos);
                return eventosDto;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                var eventosDto = _mapper.Map<EventoDto[]>(eventos);
                return eventosDto;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null) return null;
                var eventoDto = _mapper.Map<EventoDto>(evento);

                return eventoDto;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}