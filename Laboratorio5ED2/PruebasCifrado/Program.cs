using System;
using Laboratorio5ED2;
using System.IO;


namespace PruebasCifrado
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Cifrado cifrado = new Cifrado();

            //FileStream file = new FileStream(@"C:\Users\marce\Desktop\2020\Semestre II 2020\Estructura de datos II\Laboratorio\Laboratorio-5---ED2\Laboratorio5ED2\PruebasCifrado\cuento.txt", FileMode.Open, FileAccess.Read);
            //StreamReader reader = new StreamReader(file);
            //string mensajeCifrado = cifrado.CifradoCesar(reader.ReadToEnd());

            //string mensajeDesCifrado = cifrado.DesCifradoCesar(mensajeCifrado);
            //string mensajeCifrado = cifrado.CifradoRuta(reader.ReadToEnd(), 3, 4);
            //cifrado.DesCifradoRuta(mensajeCifrado, 3, 4);
            //int flag = 1;

            cifrado.CifradoZigZag("Hola");

        }
    }
}
