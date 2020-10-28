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


        public void CifradoRuta(string cadena)
        {
            Ruta ruta = new Ruta();
            ruta.Cifrar(cadena);
        }
        public void DesCifradoRuta(string cadena)
        {
            Ruta ruta = new Ruta();
            ruta.DesCifrar(cadena);
        }

    }
}
