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
        public int CrearTicket(RequestCreateTicket request)
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
        private string AsuntoE = "***Banco Azteca***\n";
        private string MensajeE = "";
        private AdministradorDeEnvios EnvioElectronico = new AdministradorDeEnvios();
        private void SendURL(RequestCreateTicket request)
        {
            EnvioElectronico.Tipo = request.EnviarEmail ? EnumTipoEnvio.Email : EnumTipoEnvio.None;
            if (request.EnviarSMS)
                EnvioElectronico.Tipo |= EnumTipoEnvio.SMS;
            if (request.EnviarWhatsApp)
                EnvioElectronico.Tipo |= EnumTipoEnvio.WhatsApp;
            if(EnvioElectronico.Tipo>0)
                EnvioElectronico.Enviar(AsuntoE, MensajeE, request.Celular, request.Email);
        }
    }
}
