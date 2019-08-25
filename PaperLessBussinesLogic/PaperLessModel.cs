using AccesoDatos;
using CanalTransferencia.Enums;
using CanalTransferencia.Model;
using DTO.PaperLessAPI;
using ITextSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TicketGenerator;

namespace PaperLessBussinesLogic
{
    public class PaperLessModel
    {
        private SQLAccess DAO = new SQLAccess();
        public int CrearTicket(RequestCreateTicket request, string currentDomain = "")
        {
            int RowsAfected = 0;
            var ticket = new Ticket()
            {
                Descripcion = request.Description,
                IdTemplate = request.IdTemplate
            };
            RowsAfected += DAO.AddTicket(ticket);
            RowsAfected += DAO.AddValoresTicket(request.Valores != null ? request.Valores
                   .Select(v =>
                   {
                       return new ValoresTicket()
                       {
                           Clave = v.Key,
                           Valor = v.Value,
                           IdTicket = ticket.Id
                       };
                   }).ToArray() : new ValoresTicket[0]);
            if (!string.IsNullOrEmpty(currentDomain))
                SendURL(request, string.Format("{0}/GetTicket/{1}", currentDomain, ticket.Id));


            return RowsAfected;
        }

        public byte[] ObtenerTicket(long idTicket)
        {
            string NameConventions = string.Format("C:\\Windows\\Temp\\Ticket_{0}.pdf", idTicket);
            if (File.Exists(NameConventions))
                File.Delete(NameConventions);
            bool success = ITextWrapper.CreatePDF(TemplateConverter
                .FillTemplate(DAO.GetTickets()
                .Where(t => t.Id == idTicket)
                .Select(t => t.Template.Contenido).FirstOrDefault() ?? "", DAO.GetDictionaryFromIdTicket(idTicket)), NameConventions);
            if (success)
            {
                return File.ReadAllBytes(NameConventions);
            }
            return new byte[0];
        }
        private string AsuntoE = "Banco Azteca";

        private AdministradorDeEnvios EnvioElectronico = new AdministradorDeEnvios();
        private void SendURL(RequestCreateTicket request, string url)
        {
            string MensajeE = string.Format("Ha recibido un nuevo recibo Electronico de su operacion acceda a la siguiente Ruta {0}", url);
            EnvioElectronico.Tipo = request.EnviarEmail ? EnumTipoEnvio.Email : EnumTipoEnvio.None;
            if (request.EnviarSMS)
                EnvioElectronico.Tipo |= EnumTipoEnvio.SMS;
            if (request.EnviarWhatsApp)
                EnvioElectronico.Tipo |= EnumTipoEnvio.WhatsApp;
            if (EnvioElectronico.Tipo > 0)
                EnvioElectronico.Enviar(AsuntoE, MensajeE, request.Celular, request.Email);
        }
        public string FixURI(string absoluteUri)
        {
            if (!string.IsNullOrEmpty(absoluteUri) && !string.IsNullOrWhiteSpace(absoluteUri))
            {
                var urisplit = absoluteUri.Split('/').ToList();
                urisplit.RemoveAt(urisplit.Count - 1);
                return string.Join("/", urisplit.ToArray());
            }
            return string.Empty;
        }
    }
}
