﻿using System;
using Laboratorio5ED2;
using System.IO;


namespace PruebasCifrado
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Cifrado cifrado = new Cifrado();

            FileStream file = new FileStream(@"C:\Users\Usuario DELL\Desktop\4to semestre 2020\Estructura de datos II\Laboratorio-5---ED2\Laboratorio5ED2\PruebasCifrado\cuento.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string mensajeCifrado = cifrado.CifradoCesar(reader.ReadToEnd());

            string mensajeDesCifrado = cifrado.DesCifradoCesar(mensajeCifrado);

            int flag = 1;

        }
    }
}
