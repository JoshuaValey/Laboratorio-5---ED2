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
            //MY SPIDER SENSES ARE TINGLING
            byte[] mensaje = {77,89,32,83,80,73,68,69,82,32,83,69,78,83,69,83,32,65,82,69,32,84,73,78,71,76,73,78,71};
            ZigZag zigzag = new ZigZag(4);
           var listado = zigzag.Cifrar2(mensaje);
            //Ver este valor
            string cadenaEncript = "";
            foreach (var item in listado)
            {
                cadenaEncript += Convert.ToChar(item);
            }

            int flag = 1;
        }

        public void DesCifradoZigZag(string cadena)
        {
            ZigZag zigzag = new ZigZag(4);
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
