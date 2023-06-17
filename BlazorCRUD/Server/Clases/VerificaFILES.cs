using BlazorCRUD.Shared;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.JSInterop;

namespace BlazorCRUD.Server.Clases
{
    public class VerificaFILES
    {


        public void DescargaPdf(IJSRuntime js, string filename = "reporte.pdf")
        {
            js.InvokeVoidAsync("DownloadPdf",
                filename,
                Convert.ToBase64String(PDFReporte())
                );


        }

        private byte[] PDFReporte()
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
            string image = Path.Combine("/", "Clases"); /*AppDomain.CurrentDomain.BaseDirectory;*/ //$"{_webHostEnvironment.ContentRootPath}/ImagesJPS/userExample.png";
            //string imageURL = _env.BaseAddress ;



            bool existe = File.Exists(image);//Directory.Exists(image);

            if (existe)
            {
                Console.WriteLine("Existe");
            }
            else
            {
                Console.WriteLine("no existe");
            }
            //Console.WriteLine(file);

            /*Image img = Image.GetInstance(image);

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
