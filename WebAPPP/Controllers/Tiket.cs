using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
namespace WebAPPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tiket
    {
        [HttpPost("CreateTicket")]
        public IActionResult CreateTicket([FromBody]dynamic Ticket)
        {
           
            var resultado = Ticket;
            return resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ticket"></param>
        /// <returns></returns>
        [HttpGet("ReadTicket")]
        public IActionResult ReadTicket([FromBody]dynamic Ticket)
        {
            var resultado = Ticket;
            return resultado;
        }
    }
}
