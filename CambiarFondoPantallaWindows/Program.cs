using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace CambiarFondoPantallaWindows
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction,
        int uParam, string lpvParam, int fuWinIni);

        private static readonly int MAX_PATH = 260;
        private static readonly int SPI_GETDESKWALLPAPER = 0x73;
        private static readonly int SPI_SETDESKWALLPAPER = 0x14;
        private static readonly int SPIF_UPDATEINIFILE = 0x01;
        private static readonly int SPIF_SENDWININICHANGE = 0x02;

        static string obtenerFondoPantallaActual()
        {
            string wallpaper = new string('\0', MAX_PATH);
            SystemParametersInfo(SPI_GETDESKWALLPAPER, (int)wallpaper.Length, wallpaper, 0);
            return wallpaper.Substring(0, wallpaper.IndexOf('\0'));
        }

        static void cambiarFondoPantalla(string filename)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }


        static void Main(string[] args)
        {
            System.Console.WriteLine("El fondo de escritorio actual: " + obtenerFondoPantallaActual());
            try
            {
                cambiarFondoPantalla(args[0]);
            }
            catch (IndexOutOfRangeException ex)
            {
                System.Console.WriteLine("Debe indicar la ruta y nombre de la imagen de fondo de escritorio.");
            }
        }

    }
}

