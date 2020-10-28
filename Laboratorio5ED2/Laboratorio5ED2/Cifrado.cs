using System;
using System.Collections.Generic;
using System.Text;
using Laboratorio5ED2.TipoCifrado;


namespace Laboratorio5ED2
{
    public class Cifrado
    {
        public void CifradoCesar(string cadena)
        {
            Cesar cesar = new Cesar();
            cesar.Cifrar(cadena);
        }
        public void DesCifradoCesar(string cadena)
        {
            Cesar cesar = new Cesar();
            cesar.DesCifrar(cadena);
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
