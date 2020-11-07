using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Laboratorio5ED2.TipoCifrado;


namespace Laboratorio5ED2
{
    public class Cifrado
    {
        #region CIFRADO CESAR
        public FileStream CifradoCesar(string cadena, string llave, StreamReader file, string nombre)
        {
            Cesar cesar = new Cesar(llave);
            FileStream filestream = new FileStream(nombre + ".csr", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter documento = new StreamWriter(filestream);
            //documento.WriteLine(textoCifrado);
            string fila = "";
            while (file.Peek() > -1)
            {
                string linea = file.ReadLine();
                string lineaCifrada = cesar.Cifrar(linea);
                if (!String.IsNullOrEmpty(linea))
                {
                    documento.WriteLine(lineaCifrada);
                }
            }
            documento.Close();
            file.Close();
            fila = "";
            return filestream;
            //return cesar.Cifrar(cadena);
        }
        public FileStream DesCifradoCesar(string cadena, string llave, StreamReader file, string nombre)
        {
            Cesar cesar = new Cesar(llave);
            FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter documento = new StreamWriter(filestream);
            //documento.WriteLine(textoCifrado);
            string fila = "";
            while (file.Peek() > -1)
            {
                string linea = file.ReadLine();
                string lineaCifrada = cesar.DesCifrar(linea);
                if (!String.IsNullOrEmpty(linea))
                {
                    documento.WriteLine(lineaCifrada);
                }
            }
            //documento.Close();
            //file.Close();
            return filestream;
        }
        #endregion

        #region CIFRADO ZIGZAG
        public string CifradoZigZag(string cadena, int llave)
        {
            ZigZag zigzag = new ZigZag(llave);
            return zigzag.Cifrar(cadena);
        }
        public string DesCifradoZigZag(string cadena, int llave)
        {
            ZigZag zigzag = new ZigZag(llave);
            return zigzag.DesCifrar(cadena);
        }
        #endregion

        #region CIFRADO RUTA
        public string CifradoRuta(string cadena, int fila, int columna, FileStream aCifrar)
        {
            Ruta ruta = new Ruta();
            string cifrado2 = "";
            aCifrar.Position = 0;
            int cantidadNumeros = 0;
            int cont = 0;

            using var reader = new BinaryReader(aCifrar);
            var buffer = new byte[(fila*columna)*20];
            while (aCifrar.Position < aCifrar.Length)
            {
                string lineas = "";
                buffer = reader.ReadBytes((fila * columna) * 20);
                foreach(var item in buffer)
                {
                    lineas += Convert.ToChar(item);
                }
                cifrado2 += ruta.llenarLeer(lineas, fila, columna);
                cont = ruta.contadorSignos;
            }
            reader.Close();
            aCifrar.Write(buffer);
            aCifrar.Close();

            cantidadNumeros = (Convert.ToString(ruta.contadorSignos)).Length;
            cifrado2 = Convert.ToString(cantidadNumeros) + Convert.ToString(ruta.contadorSignos) + cifrado2;
            return cifrado2;
        }
        public string DesCifradoRuta(string cadena, int fila, int columna, FileStream aDescifrar)
        {
            Ruta ruta = new Ruta();
            return ruta.DesCifrar(cadena, fila, columna);
        }
        #endregion
    }
}
