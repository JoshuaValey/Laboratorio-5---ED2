using System;
using System.Collections.Generic;
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
        public string CifradoRuta(string cadena, int fila, int columna)
        {
            Ruta ruta = new Ruta();
            return ruta.Cifrar(cadena, fila, columna);
        }
        public string DesCifradoRuta(string cadena, int filas, int columnas)
        {
            Ruta ruta = new Ruta();
            return ruta.DesCifrar(cadena, filas, columnas);
        }
        #endregion
    }
}
