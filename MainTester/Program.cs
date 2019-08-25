﻿using System;

namespace MainTester
{
    using System.Collections.Generic;
    using ITextSharp;
    using TicketGenerator;

    class Program
    {
        static void Main(string[] args)
        {
            string originHtml = "<html><body><p><b>Referencia: Banco Azteca, S.A.</b></p><p>@@Referencia@@</p><br/><p><b>ENVIOS DE DINERO</b></p><p>@@Negocio@@</p><p><b>--PAGO--</b></p><br/><br/><p><b>CLAVE DE ENVIO:</b></p><p>@@ClaveCobro@@</p><p><b>BENEFICIARIO:</b></p><p>@@Beneficiario@@</p><p><b>REMITENTE:</b></p><p>@@Remitente@@</p><p><b>ORIGEN:</b></p><p>@@Origen@@</p><p><b>DESTINO:</b></p><p>@@Destino@@</p><p><b>MONTO:</b></p><p>$@@MontoPago@@ M.N.</p></body></html>";
            string testPdfFile = @"C:\Windows\Temp\sodi.pdf";

            if (ITextWrapper.CreatePDF(originHtml, testPdfFile))
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

        private static string TestConverter()
        {
            return TemplateConverter.FillTemplate(GetTemplate(), GetTicketValues());
        }

        private static string GetTemplate()
        {
            return "<html><body><p><b>Referencia: Banco Azteca, S.A.</b></p><p>@@Referencia@@</p><br/><p><b>ENVIOS DE DINERO</b></p><p>@@Negocio@@</p><p><b>--PAGO--</b></p><br/><br/><p><b>CLAVE DE ENVIO:</b></p><p>@@ClaveCobro@@</p><p><b>BENEFICIARIO:</b></p><p>@@Beneficiario@@</p><p><b>REMITENTE:</b></p><p>@@Remitente@@</p><p><b>ORIGEN:</b></p><p>@@Origen@@</p><p><b>DESTINO:</b></p><p>@@Destino@@</p><p><b>MONTO:</b></p><p>$@@MontoPago@@ M.N.</p></body></html>";
        }

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
