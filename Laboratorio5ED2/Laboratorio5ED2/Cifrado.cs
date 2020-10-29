using System;
using System.Collections.Generic;
using System.Text;
using Laboratorio5ED2.TipoCifrado;


namespace Laboratorio5ED2
{
    public class Cifrado
    {
        public string CifradoCesar(string cadena)
        {
            Cesar cesar = new Cesar();
            return cesar.Cifrar(cadena);
        }
        public string DesCifradoCesar(string cadena)
        {
            Cesar cesar = new Cesar();
            return cesar.DesCifrar(cadena);
        }


        public void CifradoZigZag(string cadena)
        {
            ZigZag zigzag = new ZigZag();
            zigzag.Cifrar(cadena);
        }
        public void DesCifradoZigZag(string cadena)
        {
            ZigZag zigzag = new ZigZag();
            zigzag.DesCifrar(cadena);
        }


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

    }
}
