using System;

namespace MainTester
{
    using System.Collections.Generic;
    using AccesoDatos;
    using ITextSharp;
    using TicketGenerator;

    class Program
    {
        const string testPdfFile = @"C:\Windows\Temp\sodi.pdf";

        static void Main(string[] args)
        {
            using (var DataAccess = new SQLAccess())
            {
                //Transferencias de Dinero
                var Areas = DataAccess.UpdateArea(new Area() {Id=1,Nombre= "Transferencias de Dinero" });
            }
            if (ITextWrapper.CreatePDF(GetTemplate(), testPdfFile))
            {
                Console.WriteLine(string.Format("File {0} was generated successfully.", testPdfFile));
            }
            else
            {
                Console.WriteLine(string.Format("Error creating PDF file. Try again."));
            }

            Console.Read();

            TestConverter();
        }

        private static void TestConverter()
        {
            string htmlTicket = TemplateConverter.FillTemplate(GetTemplate(), GetTicketValues());
            ITextWrapper.CreatePDF(htmlTicket, testPdfFile);
        }

        /// <summary>
        /// Gets template base for printing (should receive it from database)
        /// </summary>
        private static string GetTemplate()
        {
            return "<html><body><p><b>Referencia: Banco Azteca, S.A.</b></p><p>@@Referencia@@</p><br/><p><b>ENVIOS DE DINERO</b></p><p>@@Negocio@@</p><p><b>--PAGO--</b></p><br/><br/><p><b>CLAVE DE ENVIO:</b></p><p>@@ClaveCobro@@</p><p><b>BENEFICIARIO:</b></p><p>@@Beneficiario@@</p><p><b>REMITENTE:</b></p><p>@@Remitente@@</p><p><b>ORIGEN:</b></p><p>@@Origen@@</p><p><b>DESTINO:</b></p><p>@@Destino@@</p><p><b>MONTO:</b></p><p>$@@MontoPago@@ M.N.</p></body></html>";
        }

        /// <summary>
        /// Gets ticket "tags" (identifier strings in template to be replaced) and values (final values in template)
        /// </summary>
        private static Dictionary<string, string> GetTicketValues()
        {
            Dictionary<string, string> ticketValues = new Dictionary<string, string>();
            ticketValues.Add("@@Referencia@@", "REFSUC1234_WSCAJA01");
            ticketValues.Add("@@Negocio@@", "Dinero Express");
            ticketValues.Add("@@ClaveCobro@@", "D9871236450");
            ticketValues.Add("@@Beneficiario@@", "JUAN PEREZ GARCIA");
            ticketValues.Add("@@Remitente@@", "MARIA DEL ROSARIO PEREZ");
            ticketValues.Add("@@Origen@@", "LOS ANGELES, CA, USA");
            ticketValues.Add("@@Destino@@", "CDMX, MEXICO");
            ticketValues.Add("@@MontoPago@@", "500.99");

            return ticketValues;
        }
    }
}
