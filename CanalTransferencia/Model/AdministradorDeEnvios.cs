using CanalTransferencia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CanalTransferencia.Model
{
    public class AdministradorDeEnvios
    {
        const string accountSid = "AC9fc6266464ef6c7884c38564bb0565f7";
        const string authToken = "9146a5f63bd771c1c75bdf909b225d05";
        const string RemitenteSMS = "+13046525706";
        const string RemitenteWhatsApp = "+14155238886";
        const string RemitenteEmail = "danox40997@gmail.com";
        const string PrefijoMexico = "+52";
        const string PrefijoWhats = "1";
        const string emailPassword = "F46987DE89";

        public AdministradorDeEnvios() 
        {
            TwilioClient.Init(accountSid, authToken);
        }
        public EnumTipoEnvio Tipo { get; set; }

        public void Enviar(string Asunto, string Mensaje, string Telefono,string email)
        {
            if (Tipo.HasFlag(EnumTipoEnvio.SMS))
                SendSMS(Asunto, Mensaje, RemitenteSMS, Telefono);
            if (Tipo.HasFlag(EnumTipoEnvio.WhatsApp))
                SendWhats(Asunto, Mensaje, RemitenteWhatsApp, Telefono);
            if (Tipo.HasFlag(EnumTipoEnvio.Email))
                SendEmail(Asunto, Mensaje, RemitenteEmail, email);
        }
        private string  SendSMS(string Asunto, string Mensaje, string Remitente, string Destinatario) 
        {
            var message = MessageResource.Create(
                body: string.Format("{0}, {1}", Asunto,Mensaje),
                from: new PhoneNumber(Remitente),
                to: new PhoneNumber(string.Format("{0}{1}",PrefijoMexico, Destinatario))
            );

            return message.Sid;
        }
        private string SendWhats(string Asunto, string Mensaje, string Remitente, string Destinatario) 
        {
            var message = MessageResource.Create(
               from: new PhoneNumber(string.Format("whatsapp:{0}",Remitente)),
               to: new PhoneNumber(string.Format("whatsapp:{0}{1}{2}",PrefijoMexico,PrefijoWhats,Destinatario)),
               body: string.Format("{0}, {1}", Asunto, Mensaje)
           );
            return message.Sid;
        }
        private void SendEmail(string Asunto, string Mensaje, string Remitente, string Destinatario) 
        {
            var fromAddress = new MailAddress(Remitente, "Banco Azteca");
            var toAddress = new MailAddress(Destinatario, "Cliente");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, emailPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = Asunto,
                Body = Mensaje
            })
            {
                smtp.Send(message);
            }
        }

    }
}
