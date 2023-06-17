using BlazorCRUD.Shared;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCRUD.Server.Clases;
using OfficeOpenXml.Export.ToDataTable;
using BlazorCRUD.Server.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD.Server.Controllers
{
    [Route("api/ReportePdf")]
    [ApiController]
    public class ReportePdfController : ControllerBase
    {
        readonly PruebablazorContext dbContext = new();
        // GET: ReportePdfController
        [HttpGet("ObtenerPdf")]
        public IActionResult ObtenerPdf()
        {

            FileChecker oFile = new FileChecker();

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


            //PageSize.Letter.Rotate() esto coloca la pagina en horizontal
            //Podemos acceder a la instancia como los siguientes ejemplos (ver ejemplos #1 y # 2

            //ItextSharp no se trabaja por pixeles ni milimetros,la unidad de medida utilizada es puntos
            //Un punto a centimetros, 1 CM a POINT es 28.3464, se utiliza en formato float 28.3464f
            //Tener esto en cuenta para trabajar en el PDF

            //Ejemplo #1
            Document pdf = new Document(
            PageSize.Letter,
            margeLeft.ToDpi(),
            margeRight.ToDpi(),
            margeTop.ToDpi(),
            margeBottom.ToDpi()
            );

            //Con el siguiente comando y usando la clase Rectangle de ItextSharp podemos crear nuestro propio
            //Tamanio de hoja para el pdf que vamos a crear
            //pdf.SetPageSize(new Rectangle(612.28f,935.43f));

            //Ejemplo #2

            //pdf.SetPageSize(PageSize.Letter);

            pdf.AddTitle("Reporte De Repuestos PDF");
            pdf.AddAuthor("PRUEBA CHUZ");
            pdf.AddCreationDate();
            pdf.AddKeywords("Blazor");
            pdf.AddSubject("Pdf Generado con Libreria iText Sharp");

            PdfWriter writter = PdfWriter.GetInstance(pdf, memoryStream);

            //Create Header

            var fontStyle = FontFactory.GetFont("Arial", 16, BaseColor.Blue);
            var labelHeader = new Chunk("Blazor PDF HEADER", fontStyle);

            HeaderFooter header = new HeaderFooter(new Phrase(labelHeader), false)
            {
                BackgroundColor = new BaseColor(255, 255, 255), //50, 20, 120
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

            //Texto

            //Agregar Texto, una frase o parrafo
            // la clase Phrase = es para escribir en una sola linea
            // la clase Paragrah = para mostrar texto de una forma ajustada al tamanio del documento
            //En paragrah podemos alinear el texto por medio Element.ALIGN_CENTER, se puede agregar dentro
            //de la instancia o a la variable que creamos, acceder a sus atributos

            //Salto De linea = \n
            //Tabulaciones = \t\t


            //Titulo 
            //Agregar alineacion de texto directamente al instanciar Paragrahp
            //var title = new Paragraph("Blazor PDF Report", new Font(Font.HELVETICA, 20, Font.BOLD)) { Alignment = Element.ALIGN_JUSTIFIED };

            var title = new Paragraph("Blazor PDF Report", new Font(Font.HELVETICA, 20f, Font.BOLD));
            title.SpacingAfter = 18;

            //Agregar alineacion despues de crear la instancia
            //title.Alignment = Element.ALIGN_CENTER;

            pdf.Add(title);


            Font _fontStyle = FontFactory.GetFont("Tahoma", 12f, Font.NORMAL);
            var _myText = "Bienvenido a mi PDF por: CHUZ P";
            var phrase = new Phrase(_myText, _fontStyle);
            pdf.Add(phrase);

            pdf.Add(new Paragraph(" ")); //Espacio en blanco


            //Agregar un parafo de ejemplo con texto generado automaticamente ---> Lorem Ipsum

            var parrafoPrueba = new Paragraph("\t\tMorbi bibendum id elit ut fermentum. \"\" Nullam quam nibh, accumsan non pharetra viverra, tristique laoreet tortor.\n Quisque iaculis neque id laoreet rhoncus. Nullam vitae lacinia mauris. Proin vestibulum dui et tellus tempus, eu facilisis ex interdum. Nunc eleifend pellentesque nibh. Nullam mattis nulla non ligula ultricies semper. Donec lobortis, metus sed venenatis rutrum, turpis libero lobortis magna, id ornare quam eros quis velit. Etiam volutpat enim eu elit pretium blandit at sed eros. Cras ut porttitor arcu. Nulla auctor iaculis orci non tincidunt. Cras vitae magna eget leo feugiat semper. Ut eget erat dolor. Donec tristique pharetra erat sed vulputate. Suspendisse pellentesque ipsum convallis vulputate lacinia.") { Alignment = Element.ALIGN_JUSTIFIED };
            pdf.Add(parrafoPrueba);


            pdf.Add(new Chunk("\n"));
            pdf.Add(Chunk.Newline); //Agrega espacio en blanco


            //Set Image agregar imagen al PDF
            // string image2 = @"\ImagesJPS\userExample.png";//System.IO.Directory.GetCurrentDirectory();  //Environment.CurrentDirectory; Directory.GetCurrentDirectory();
            string image = oFile.obtenerPathSErver(); /*AppDomain.CurrentDomain.BaseDirectory;*/ //$"{_webHostEnvironment.ContentRootPath}/ImagesJPS/userExample.png";
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

            Image img = Image.GetInstance(image);

            //Maneras de determinar tamanio de la imagen


            /*img.SetAbsolutePosition(
                (PageSize.A4.Width - img.ScaledWidth) / 4,
                (PageSize.A4.Width - img.ScaledHeight) / 4
                );*/

            img.ScaleAbsolute(99.21f, 56.69f); // 56.69 puntos = 2 centimetros

            //colocar la imagen en punto en especifico
            //Tener en cuenta el eje X y el eje Y para colocar en el PDF

            img.SetAbsolutePosition(0, 728f);
            pdf.Add(img);


            //Agregar Lineas

            Chunk linea = new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(
                5f,
                100f,
                BaseColor.Red,
                Element.ALIGN_CENTER,
                50f));

            pdf.Add(linea);

            //Agregar linea personalizada

            //PdfContentByte cb = writter.DirectContent;
            //cb.MoveTo(0, 0); //Punto inicial Ubicamos en eje X y eje Y
            //cb.LineTo(200, 200); //Aca nos ubicamos con eje X y eje Y
            //cb.ClosePathStroke(); // Creara una linea desde MoveTo hasta LineTo

            //Agregar una Tabla en el PDF

            //Recibe un array, dentro de las llaves recibe el porcentaje que ocupara nuestra columnas
            // WidthPercentage = ocupar el 100% del ancho de la pagina
            var tbl = new PdfPTable(new float[] { 25f, 25f, 25f, 25f }) { WidthPercentage = 100f };

            //Agregar Celdas

            tbl.AddCell(new Phrase("Nombre"));
            tbl.AddCell(new Phrase("Apellido"));
            tbl.AddCell(new Phrase("Telefono"));
            tbl.AddCell(new Phrase("Correo"));
            pdf.Add(tbl);

            //

            pdf.Close();

            byte[] bytesStream = memoryStream.ToArray();

            //memoryStream = new MemoryStream();
            //memoryStream.Write(bytesStream, 0, bytesStream.Length);
            //memoryStream.Position = 0;



            //return new FileStreamResult(memoryStream, "applicaction/pdf");
            // application/pdf es para que el navegador entienda que archivo se envia
            return new FileContentResult(bytesStream, "applicaction/pdf");

            // return memoryStream.ToArray();

        }

        [HttpGet("ObtenerPdf2")]
        public async Task<IActionResult> ObtenerPdf2Async()
        {
            var memoryStream = new MemoryStream();
            /*float margeLeft = 1.5f;
            float margeRight = 1.5f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;*/

            float margeLeft = 1.5f;
            float margeRight = 1.5f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;


            //PageSize.Letter.Rotate() esto coloca la pagina en horizontal
            //Podemos acceder a la instancia como los siguientes ejemplos (ver ejemplos #1 y # 2

            //ItextSharp no se trabaja por pixeles ni milimetros,la unidad de medida utilizada es puntos
            //Un punto a centimetros, 1 CM a POINT es 28.3464, se utiliza en formato float 28.3464f
            //Tener esto en cuenta para trabajar en el PDF

            //Ejemplo #1
            Document pdf = new Document(
            PageSize.Letter,
            margeLeft.ToDpi(),
            margeRight.ToDpi(),
            margeTop.ToDpi(),
            margeBottom.ToDpi()
            );

            //Con el siguiente comando y usando la clase Rectangle de ItextSharp podemos crear nuestro propio
            //Tamanio de hoja para el pdf que vamos a crear
            //pdf.SetPageSize(new Rectangle(612.28f,935.43f));

            //Ejemplo #2

            //pdf.SetPageSize(PageSize.Letter);

            pdf.AddTitle("Reporte De Repuestos PDF");
            pdf.AddAuthor("PRUEBA CHUZ");
            pdf.AddCreationDate();
            pdf.AddKeywords("Blazor");
            pdf.AddSubject("Pdf Generado con Libreria iText Sharp");

            PdfWriter writter = PdfWriter.GetInstance(pdf, memoryStream);

            //Create Header

            var fontStyle = FontFactory.GetFont("Arial", 16, BaseColor.Blue);
            var labelHeader = new Chunk("Blazor PDF HEADER", fontStyle);

            HeaderFooter header = new HeaderFooter(new Phrase(labelHeader), false)
            {
                BackgroundColor = new BaseColor(255, 255, 255), //50, 20, 120
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

            FileChecker oFile = new FileChecker();

            //Set Image agregar imagen al PDF
            // string image2 = @"\ImagesJPS\userExample.png";//System.IO.Directory.GetCurrentDirectory();  //Environment.CurrentDirectory; Directory.GetCurrentDirectory();
            string image = oFile.obtenerPathSErver(); /*AppDomain.CurrentDomain.BaseDirectory;*/ //$"{_webHostEnvironment.ContentRootPath}/ImagesJPS/userExample.png";



            //bool existe = Directory.Exists(image);

            /*if (existe)
            {
                Console.WriteLine("Existe");
            }
            else
            {
                Console.WriteLine("no existe");
            }
            //Console.WriteLine(file);*/

            Image img = Image.GetInstance(image);

            //Maneras de determinar tamanio de la imagen


            /*img.SetAbsolutePosition(
                (PageSize.A4.Width - img.ScaledWidth) / 4,
                (PageSize.A4.Width - img.ScaledHeight) / 4
                );*/

            img.ScaleAbsolute(99.21f, 56.69f); // 56.69 puntos = 2 centimetros

            //colocar la imagen en punto en especifico
            //Tener en cuenta el eje X y el eje Y para colocar en el PDF

            img.SetAbsolutePosition(0, 728f);
            pdf.Add(img);



            BaseFont _titulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font titulo = new iTextSharp.text.Font(_titulo, 20f, Font.BOLD, new BaseColor(0, 0, 0));

            BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            iTextSharp.text.Font subtitulo = new iTextSharp.text.Font(_subtitulo, 12f, Font.BOLD, new BaseColor(0, 0, 0));

            BaseFont _parrafo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            iTextSharp.text.Font parrafo = new iTextSharp.text.Font(_parrafo, 12f, Font.NORMAL, new BaseColor(0, 0, 0));

            BaseFont _negrita = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            iTextSharp.text.Font negrita = new iTextSharp.text.Font(_parrafo, 10f, Font.BOLD, new BaseColor(0, 0, 0));


            BaseFont _toinvoice = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font toinvoice = new iTextSharp.text.Font(_toinvoice, 20f, Font.BOLD, new BaseColor(255, 255, 255));

            BaseFont _textoBlanco = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            iTextSharp.text.Font textoBlanco = new iTextSharp.text.Font(_toinvoice, 10f, Font.BOLD, new BaseColor(255, 255, 255));



            pdf.Add(Chunk.Newline);

            Chunk barra = new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(5f, 30f, new BaseColor(0, 69, 161), Element.ALIGN_RIGHT, -1));
            pdf.Add(barra);


            pdf.Add(new Phrase(" "));
            var tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100 };

            ///Encabezado

            tbl.AddCell(new PdfPCell(new Phrase("MOTO REPUESTOS MAKO", titulo)) { Border = 0, Rowspan = 3, VerticalAlignment = Element.ALIGN_MIDDLE });  //El rowspan es para unir las filas
            tbl.AddCell(new PdfPCell(new Phrase("500 International SpeedWay, San Jose, Costa Rica.", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            tbl.AddCell(new PdfPCell(new Phrase("+(506) 2525-2626 jesussaenz@04gmail.com", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            tbl.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

            pdf.Add(tbl);

            pdf.Add(new Phrase(" "));
            pdf.Add(new Phrase(" "));

            ///Detalle

            tbl = new PdfPTable(new float[] { 20f, 50f, 30f }) { WidthPercentage = 100 };
            tbl.AddCell(new PdfPCell(new Phrase("TO:", toinvoice)) { BorderColor = new BaseColor(0, 69, 161), BackgroundColor = new BaseColor(0, 69, 161), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Rowspan = 3 });

            tbl.AddCell(new PdfPCell(new Phrase("Costa Rican Gift Network:", parrafo)) { BorderColor = new BaseColor(0, 69, 161), BorderWidthBottom = 0 });
            tbl.AddCell(new PdfPCell(new Phrase("INVOICE:", toinvoice)) { BorderColor = new BaseColor(0, 69, 161), BackgroundColor = new BaseColor(0, 69, 161), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Rowspan = 3 });

            tbl.AddCell(new PdfPCell(new Phrase("Quitirrisi Mora:", parrafo)) { BorderColor = new BaseColor(0, 69, 161), BorderWidthBottom = 0, BorderWidthTop = 0 });
            tbl.AddCell(new PdfPCell(new Phrase("Barrio Canias cas 52:", parrafo)) { BorderColor = new BaseColor(0, 69, 161), BorderWidthTop = 0 });

            pdf.Add(tbl);

            pdf.Add(new Phrase(" "));
            pdf.Add(new Phrase(" "));

            ///Tabla con el detalle

            tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100 };

            var col1 = new PdfPCell(new Phrase("Att. Tony Calagahn", negrita)) { Border = 0, Padding = 5 };
            var col2 = new PdfPCell(new Phrase("INVOICE #4817824-4", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT };

            tbl.AddCell(col1);
            tbl.AddCell(col2);

            col1.Phrase = new Phrase("Reporte De Ventas: 1601", parrafo);
            col2.Phrase = new Phrase(" # De Cuenta", parrafo);

            tbl.AddCell(col1);
            tbl.AddCell(col2);

            col1.Phrase = new Phrase("Lista de Entrega: 30 Dias ", parrafo);
            col2.Phrase = new Phrase("Fecha: 20 Marzo de 2023", parrafo);

            tbl.AddCell(col1);
            tbl.AddCell(col2);

            pdf.Add(tbl);


            pdf.Add(new Phrase(" "));


            ///Generar tabla de datos

            tbl = new PdfPTable(new float[] { 15f, 40f, 15f, 15f, 15f }) { WidthPercentage = 100f };
            var c1 = new PdfPCell(new Phrase("CODIGO", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 69, 161) };
            var c2 = new PdfPCell(new Phrase("NOMBRE DEL PRODUCTO", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 69, 161), Padding = 2f };
            var c3 = new PdfPCell(new Phrase("PRECIO UNITARIO", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 69, 161), Padding = 2f };
            var c4 = new PdfPCell(new Phrase("CANTIDAD", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 69, 161), Padding = 2f };
            var c5 = new PdfPCell(new Phrase("PRECIO TOTAL", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 69, 161), Padding = 2f };

            tbl.AddCell(c1);
            tbl.AddCell(c2);
            tbl.AddCell(c3);
            tbl.AddCell(c4);
            tbl.AddCell(c5);



            c1.Border = 0;
            c2.Border = 0;
            c3.Border = 0;
            c4.Border = 0;
            c5.Border = 0;

            var ventas = await dbContext.Products.Take(10).ToListAsync();

            double totalPago = 0;

            //Agregar color a las filas por pares e impares

            int contador = 0;

            foreach (var e in ventas)
            {
                if(contador %2== 0)
                {
                    c1.BackgroundColor = new BaseColor(204, 204, 204);
                    c2.BackgroundColor = new BaseColor(204, 204, 204);
                    c3.BackgroundColor = new BaseColor(204, 204, 204);
                    c4.BackgroundColor = new BaseColor(204, 204, 204);
                    c5.BackgroundColor = new BaseColor(204, 204, 204);
                }
                else
                {
                    c1.BackgroundColor = BaseColor.White;
                    c2.BackgroundColor = BaseColor.White;
                    c3.BackgroundColor = BaseColor.White;
                    c4.BackgroundColor = BaseColor.White;
                    c5.BackgroundColor = BaseColor.White;
                }

                c1.Phrase = new Phrase(e.IdProducto.ToString());
                c2.Phrase = new Phrase(e.NombreProd);
                c3.Phrase = new Phrase(e.PrecioProd.ToString());
                c4.Phrase = new Phrase(e.CantidadProd.ToString());
                c5.Phrase = new Phrase(e.PrecioXprod.ToString());

                totalPago = totalPago + e.PrecioXprod;

                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);

                contador++;
            }


            c1.Colspan = 5;
            c1.Phrase = new Phrase(totalPago.ToString());
            c1.HorizontalAlignment = Element.ALIGN_RIGHT;

            tbl.AddCell(c1);

            pdf.Add(tbl);

            //Tablas anidadas

            tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 90f };
            var tbl1 = new PdfPTable(new float[] { 33f, 33f, 34f }) { WidthPercentage = 100f };
            var tbl2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

            tbl.DefaultCell.Border = 0;
            tbl.DefaultCell.Padding = 10f;

            c1.Colspan = 3;
            c1.HorizontalAlignment = Element.ALIGN_CENTER;
            c1.BackgroundColor = new BaseColor(0, 69, 161);
            c1.Phrase = new Phrase("Historial De pago", textoBlanco);

            tbl1.AddCell(c1);

            c2.Phrase = new Phrase("Fecha", negrita);
            c3.Phrase = new Phrase("# De Cheque", negrita);
            c4.Phrase = new Phrase("Monto", negrita);

            tbl1.AddCell(c2);
            tbl1.AddCell(c3);
            tbl1.AddCell(c4);

            c2.Phrase = new Phrase(DateTime.Now.ToString("dd:MM:yyyy"), parrafo); ;
            c3.Phrase = new Phrase("# 7814-7", parrafo);
            c4.Phrase = new Phrase("25000", parrafo);

            tbl1.AddCell(c2);
            tbl1.AddCell(c3);
            tbl1.AddCell(c4);


            tbl.AddCell(tbl1);

            var c_firma = new PdfPCell() { BackgroundColor = new BaseColor(255,198,7)};
            c_firma.Phrase = new Phrase("Enviar a Jesus Perez,\n Lugar Entrega, \n Quitirrisi De Mora \n Telefono:84332461");

            tbl2.AddCell(c_firma);


            tbl.AddCell(tbl2);

            pdf.Add(tbl);


            //Texto

            //Agregar Texto, una frase o parrafo
            // la clase Phrase = es para escribir en una sola linea
            // la clase Paragrah = para mostrar texto de una forma ajustada al tamanio del documento
            //En paragrah podemos alinear el texto por medio Element.ALIGN_CENTER, se puede agregar dentro
            //de la instancia o a la variable que creamos, acceder a sus atributos

            //Salto De linea = \n
            //Tabulaciones = \t\t


            //Titulo 
            //Agregar alineacion de texto directamente al instanciar Paragrahp
            //var title = new Paragraph("Blazor PDF Report", new Font(Font.HELVETICA, 20, Font.BOLD)) { Alignment = Element.ALIGN_JUSTIFIED };

            //var title = new Paragraph("Blazor PDF Report", new Font(Font.HELVETICA, 20f, Font.BOLD));
            //title.SpacingAfter = 18;

            //Agregar alineacion despues de crear la instancia
            //title.Alignment = Element.ALIGN_CENTER;

            //pdf.Add(title);
            pdf.Add(Chunk.Newline);
            pdf.Add(Chunk.Newline);

            Font _fontStyle = FontFactory.GetFont("Tahoma", 12f, Font.NORMAL);
            var _myText = "Bienvenido a mi PDF por: CHUZ P";

            pdf.Add(new Paragraph(" "));
            pdf.Add(new Paragraph(" "));
            pdf.Add(new Paragraph(" "));

            var phrase = new Phrase(_myText, _fontStyle);
            pdf.Add(phrase);

            pdf.Add(new Paragraph(" ")); //Espacio en blanco


            //Agregar un parafo de ejemplo con texto generado automaticamente ---> Lorem Ipsum

            var parrafoPrueba = new Paragraph("\t\tMorbi bibendum id elit ut fermentum. \"\" Nullam quam nibh, accumsan non pharetra viverra, tristique laoreet tortor.\n Quisque iaculis neque id laoreet rhoncus. Nullam vitae lacinia mauris. Proin vestibulum dui et tellus tempus, eu facilisis ex interdum. Nunc eleifend pellentesque nibh. Nullam mattis nulla non ligula ultricies semper. Donec lobortis, metus sed venenatis rutrum, turpis libero lobortis magna, id ornare quam eros quis velit. Etiam volutpat enim eu elit pretium blandit at sed eros. Cras ut porttitor arcu. Nulla auctor iaculis orci non tincidunt. Cras vitae magna eget leo feugiat semper. Ut eget erat dolor. Donec tristique pharetra erat sed vulputate. Suspendisse pellentesque ipsum convallis vulputate lacinia.") { Alignment = Element.ALIGN_JUSTIFIED };
            pdf.Add(parrafoPrueba);


            pdf.Add(new Chunk("\n"));
            pdf.Add(Chunk.Newline); //Agrega espacio en blanco


            //string imageURL = _env.BaseAddress ;




            //Agregar Lineas

            Chunk linea2 = new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(
                5f,
                100f,
                BaseColor.Red,
                Element.ALIGN_CENTER,
                50f));

            pdf.Add(linea2);

            //Agregar linea personalizada

            //PdfContentByte cb = writter.DirectContent;
            //cb.MoveTo(0, 0); //Punto inicial Ubicamos en eje X y eje Y
            //cb.LineTo(200, 200); //Aca nos ubicamos con eje X y eje Y
            //cb.ClosePathStroke(); // Creara una linea desde MoveTo hasta LineTo

            //Agregar una Tabla en el PDF

            //Recibe un array, dentro de las llaves recibe el porcentaje que ocupara nuestra columnas
            // WidthPercentage = ocupar el 100% del ancho de la pagina
            //var tbl = new PdfPTable(new float[] { 25f, 25f, 25f, 25f }) { WidthPercentage = 100f };

            //Agregar Celdas

            //tbl.AddCell(new Phrase("Nombre"));
            //tbl.AddCell(new Phrase("Apellido"));
            //tbl.AddCell(new Phrase("Telefono"));
            //tbl.AddCell(new Phrase("Correo"));
            //pdf.Add(tbl);

            //

            pdf.Close();

            byte[] bytesStream = memoryStream.ToArray();

            //memoryStream = new MemoryStream();
            //memoryStream.Write(bytesStream, 0, bytesStream.Length);
            //memoryStream.Position = 0;



            //return new FileStreamResult(memoryStream, "applicaction/pdf");
            // application/pdf es para que el navegador entienda que archivo se envia
            return new FileContentResult(bytesStream, "applicaction/pdf");

        }

    }
}
