using Laboratorio5ED2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratorio5ED2.TipoCifrado
{
    internal class ZigZag : ICifrado
    {

        private int numNivel;
        private int valoresEnOla;
        private byte padCharacter = 42;
        
        public ZigZag(int cantidadNiveles)
        {
            this.numNivel = cantidadNiveles;
            this.valoresEnOla = numNivel * 2 - 2;
        }

        public string Cifrar(string cadena)
        {
            
            throw new NotImplementedException();
        }
        /// <summary>
        /// Cifrado diferente a la interfaz al usar bytes y no caracteres.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public List<byte> Cifrar2(byte[] cadena)
        {

            //int cantidadOlas;
            Queue<byte> mensaje = new Queue<byte>();
            foreach (var item in cadena)
            {
                mensaje.Enqueue(item);
            }


            //Crear Niveles
            var listaNiveles = new List<Queue<byte>>();
            for (int i = 0; i < numNivel; i++)
            {
                listaNiveles.Add(new Queue<byte>());
            }


            int numPadChars = 0;
            while (mensaje.Count != 0)
            {
                Queue<byte> valoresOla = ValoresParaUnaOla(mensaje);
                LlenarUnaOla(valoresOla, listaNiveles, ref numPadChars);
            }

            List<byte> resultado = new List<byte>();

            foreach (var item in listaNiveles)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    resultado.Add(item.Dequeue());
                }
            }


            return resultado;

        }

        private void LlenarUnaOla(Queue<byte> valoresParaOla, List<Queue<byte>> niveles, ref int numPadChars)
        {
            int posNivel = this.numNivel - 1;
            numPadChars = this.valoresEnOla - valoresParaOla.Count();
           
            for (int i = 0; i <= posNivel; i++)
            {
                
                if (i != posNivel)
                {
                    niveles[i].Enqueue(valoresParaOla.Dequeue());
                }
                if (i == posNivel)
                {
                    for (int j = posNivel ; j > 0; j--)
                    {
                        if (valoresParaOla.Count != 0)
                        {
                            niveles[j].Enqueue(valoresParaOla.Dequeue());
                        }
                        else
                        {
                            niveles[j].Enqueue(padCharacter);
                        }
                    }
                }
            }

        }
        private Queue<byte> ValoresParaUnaOla(Queue<byte> mensajeOriginal)
        {
            var valoresParaOla = new Queue<byte>();
            for (int i = 0; i < this.valoresEnOla; i++)
            {
                if (mensajeOriginal.Count != 0)
                {
                    valoresParaOla.Enqueue(mensajeOriginal.Dequeue());
                }
            }
            return valoresParaOla;
        }

        public string DesCifrar(string cadena)
        {
            throw new NotImplementedException();
        }
    }
}
