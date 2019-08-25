using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO.PaperLessAPI
{
    public class RequestCreateTicket
    {
        public bool EnviarEmail { get; set; }
        public bool EnviarWhatsApp { get; set; }
        public bool EnviarSMS { get; set; }
        public Dictionary<string, string> Valores { get; set; }
        public string Description { get; set; }
        public int IdTemplate { get; set; }
        public int IdArea { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }
}