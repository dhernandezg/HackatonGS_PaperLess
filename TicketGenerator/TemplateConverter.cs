using System;

namespace TicketGenerator
{
    using System.Collections.Generic;

    public static class TemplateConverter
    {
        public static string FillTemplate(string htmlTemplate, Dictionary<string, string> ticketData)
        {
            //TODO: improving performance using a different string replacing approach
            foreach (string key in ticketData.Keys)
            {
                htmlTemplate = htmlTemplate.Replace(key, ticketData[key]);
            }
            
            return htmlTemplate;
        }
    }
}
