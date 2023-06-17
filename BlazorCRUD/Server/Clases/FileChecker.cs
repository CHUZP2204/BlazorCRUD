using System;
using System.IO;

namespace BlazorCRUD.Server.Clases
{
    public class FileChecker
    {
        public bool CheckIfFileExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine($"El archivo {filePath} existe.");
                return true;
            }
            else
            {
                Console.WriteLine($"El archivo {filePath} no existe.");
                return false;
            }
        }

       public string obtenerPathSErver()
        {

            string image = Path.Combine(Directory.GetCurrentDirectory(), "imagesSERVER", "LogoSinFondo.png");

            return image;
        }
    }
}
