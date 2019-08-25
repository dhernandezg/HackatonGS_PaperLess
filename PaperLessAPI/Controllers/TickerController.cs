using DTO.PaperLessAPI;
using PaperLessBussinesLogic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace PaperLessAPI.Controllers
{

    [AllowAnonymous]
    [RoutePrefix("API/Ticket")]
    public class TickerController : ApiController
    {
        private PaperLessModel Modelo = new PaperLessModel();
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(RequestCreateTicket request)
        {
            if (request == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            if (Modelo.CrearTicket(request) > 0)
            { 
                
                return Ok();

            }
            return Unauthorized();
        }
        [HttpGet]
        [Route("GetTicket/{IdTicket}")]
        public HttpResponseMessage GetTicket(long IdTicket)
        {
            if (IdTicket <= 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            string fileName = string.Format("Operation_{0}.pdf", IdTicket);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Modelo.ObtenerTicket(IdTicket))
            };
            result.Content.Headers.ContentDisposition =
            new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        {
            FileName = string.Format("TicketDigital_{0}.pdf", IdTicket)
        };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
    }
}
