using System;
using Laboratorio5ED2;


namespace PruebasCifrado
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Cifrado cifrado = new Cifrado();
            byte dato = Convert.ToByte('á');
            Console.WriteLine(Convert.ToChar(dato));
            
        }
    }
}
