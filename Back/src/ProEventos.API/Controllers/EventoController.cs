using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
         public IEnumerable<Evento> _eventos =  new Evento[]{
                new Evento() {
                EventoID= 1,
                 Tema = "Angular",
                 Local = "SP",
                 Lote = "1º Lote",
                QtdePessoas = 10,
                DataEvento = DateTime.Now.AddDays(2).ToString(),
                ImagemURL = "foto.png"
            },
            new Evento() {
                EventoID= 2,
                 Tema = "c#",
                 Local = "SP",
                 Lote = "1º Lote",
                QtdePessoas = 10,
                DataEvento = DateTime.Now.AddDays(2).ToString(),
                ImagemURL = "foto.png"
            }
            };

        public EventoController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
         
            return _eventos;
        }

          [HttpGet("{id}")]
        public IEnumerable<Evento> GetByID(int id)
        {
         
            return _eventos.Where(x=>x.EventoID==id);
        }
        

       
    }
}
