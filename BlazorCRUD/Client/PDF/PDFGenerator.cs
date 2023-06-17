using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.JSInterop;
using BlazorCRUD.Shared;
using BlazorCRUD.Shared.Clases;
using Microsoft.AspNetCore.Hosting;
using System.util;

namespace BlazorCRUD.Shared.Clases
{
    public class PDFGenerator
    {
        
        //Acceder a directorio CORE
        //private IWebAssemblyHostEnvironment  _webHostEnvironment;
        //private IWebAssemblyHostEnvironment _webAssemblyHostEnvironment;

        /* public PDFGenerator (IWebAssemblyHostEnvironment webAssemblyHostEnvironment)
         {
             _webAssemblyHostEnvironment = webAssemblyHostEnvironment;
         }*/

        // public PDFGenerator(IHostingEnvironment env)
        //{
        //    _webHostEnvironment = env;
        //}


        public void DownloadPdf(IJSRuntime js, string filename = "reporte.pdf")
        {
            js.InvokeVoidAsync("DownloadPdf",
                filename,
                Convert.ToBase64String(PDFReport())
                );


        }
        public void ViewPdf(IJSRuntime js, string idIFrame)
        {
            js.InvokeVoidAsync("ViewPdf",
               idIFrame,
               Convert.ToBase64String(PDFReport())
               );
            //string webRoot = (string)_webHostEnvironment.WebRootPath;
            //string webroot = (string)_webAssemblyHostEnvironment.BaseAddress;
            // string url = _webHostEnvironment.BaseAddress;
            //Console.WriteLine( url );
        }
        public void ViewPdfNewTab(IJSRuntime js, string filename = "reporte.pdf")
        {
            js.InvokeVoidAsync("OpenPdfNewTab",
               filename,
               Convert.ToBase64String(PDFReport())
               );
        }

        //ItextSharp Report
        private byte[] PDFReport()
        {
            /*string webRoot = _webHostEnvironment.WebRootPath;
            Console.WriteLine(webRoot);
            string file = System.IO.Path.Combine(webRoot, "ImagesJPS/userExample.png");*/

            var memoryStream = new MemoryStream();
            /*float margeLeft = 1.5f;
            float margeRight = 1.5f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;*/

            float margeLeft = 1.5f;
            float margeRight = 1.5f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;

            Document pdf = new Document(
            PageSize.A4,
            margeLeft.ToDpi(),
            margeRight.ToDpi(),
            margeTop.ToDpi(),
            margeBottom.ToDpi()
            );

            pdf.AddTitle("BlazorCRUD PDF");
            pdf.AddAuthor("PRUEBA CHUZ");
            pdf.AddCreationDate();
            pdf.AddKeywords("Blazor");
            pdf.AddSubject("Pdf Generate with iText Sharp");

            PdfWriter writter = PdfWriter.GetInstance(pdf, memoryStream);

            //Create Header

            var fontStyle = FontFactory.GetFont("Arial", 16, BaseColor.White);
            var labelHeader = new Chunk("Blazor PDF HEADER", fontStyle);

            HeaderFooter header = new HeaderFooter(new Phrase(labelHeader), false)
            {
                BackgroundColor = new BaseColor(50, 20, 120),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };

            pdf.Header = header;

            //create footer

            var labelFooter = new Chunk("Page", fontStyle);

            HeaderFooter footer = new HeaderFooter(new Phrase(labelHeader), true)
            {
                BackgroundColor = new BaseColor(120, 3, 120),
                Alignment = Element.ALIGN_RIGHT,
                Border = Rectangle.NO_BORDER
            };

            pdf.Footer = footer;


            //Body 

            pdf.Open();

            var title = new Paragraph("Blazor PDF Report", new Font(Font.HELVETICA, 20, Font.BOLD));
            title.SpacingAfter = 18;

            pdf.Add(title);


            Font _fontStyle = FontFactory.GetFont("Tahoma", 12f, Font.NORMAL);

            var _myText = "Welcome to Page CHUZ P";
            var phrase = new Phrase(_myText, _fontStyle);

            pdf.Add(phrase);



            //Set Image
            // string image2 = @"\ImagesJPS\userExample.png";//System.IO.Directory.GetCurrentDirectory();  //Environment.CurrentDirectory; Directory.GetCurrentDirectory();
            string image = Path.Combine("~/images/user.png"); /*AppDomain.CurrentDomain.BaseDirectory;*/ //$"{_webHostEnvironment.ContentRootPath}/ImagesJPS/userExample.png";
            //string imageURL = _env.BaseAddress ;



            bool existe = Directory.Exists(image);

            if (existe)
            {
                Console.WriteLine("Existe");
            }
            else
            {
                Console.WriteLine("no existe");
            }
            //Console.WriteLine(file);
            /*
            Image img = Image.GetInstance(image);

            img.SetAbsolutePosition(
                (PageSize.A4.Width - img.ScaledWidth) / 2,
                (PageSize.A4.Width - img.ScaledHeight) / 2
                );

            pdf.Add(img);*/

            pdf.Close();

            return memoryStream.ToArray();



        }

    }
}
