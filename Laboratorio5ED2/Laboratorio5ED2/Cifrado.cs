using System;
using System.Collections.Generic;
using System.Text;
using Laboratorio5ED2.TipoCifrado;


namespace Laboratorio5ED2
{
    public class Cifrado
    {
        #region CIFRADO CESAR
        public string CifradoCesar(string cadena)
        {
            Cesar cesar = new Cesar("androiD");
            return cesar.Cifrar(cadena);
        }
        public string DesCifradoCesar(string cadena)
        {
            Cesar cesar = new Cesar("Android");
            return cesar.DesCifrar(cadena);
        }
        #endregion

        #region CIFRADO ZIGZAG
        public string CifradoZigZag(string cadena)
        {
            ZigZag zigzag = new ZigZag(4);
            return zigzag.Cifrar(cadena);
        }
        public string DesCifradoZigZag(string cadena)
        {
            ZigZag zigzag = new ZigZag(4);
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
