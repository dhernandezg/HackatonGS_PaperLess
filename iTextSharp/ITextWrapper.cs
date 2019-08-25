using System;

namespace ITextSharp
{
    using System.IO;
    using iText.Kernel.Pdf;
    using iText.Html2pdf;

    public static class ITextWrapper
    {
        public static bool CreatePDF(string htmlTemplate, string fileName)
        {
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    using (PdfWriter writer = new PdfWriter(stream))
                    {
                        HtmlConverter.ConvertToPdf(htmlTemplate, writer);
                    }
                }
            }
            catch(Exception ex)
            {
                //by now this do nothing, it might log the error
                return false;
            }

            return true;
        }
    }
}
