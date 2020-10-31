using Laboratorio5ED2.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Laboratorio5ED2.TipoCifrado
{
    internal class ZigZag : ICifrado
    {

        private byte numNivel;
        private int valoresEnOla;
        private byte padCharacter = 220; // ▄
        
        public ZigZag(byte cantidadNiveles)
        {
            this.numNivel = cantidadNiveles;
            this.valoresEnOla = numNivel * 2 - 2;
        }

        public string Cifrar(string cadena)
        {
            
            throw new NotImplementedException();
        }


        // public List<byte> DesCifrar2(byte[] cadena)
        public List<byte> DesCifrar2(List<byte> cadena)
        {
            //int coutOfEncriptValues = cadena.Length;
            int coutOfEncriptValues = cadena.Count;
            int niveles = this.numNivel;

            Queue<byte> mensajeEncriptadoAuxiliar = new Queue<byte>();
            foreach (var item in cadena)
            {
                mensajeEncriptadoAuxiliar.Enqueue(item);
            }


            //Crear Auxiliares
            var nivelesAuxiliar = new List<Queue<byte>>();
            for (int i = 0; i < numNivel; i++)
            {
                nivelesAuxiliar.Add(new Queue<byte>());
            }



            //Crear zigzag
            int numPadChars = 0;
            while (mensajeEncriptadoAuxiliar.Count != 0)
            {
                Queue<byte> valoresOla = ValoresParaUnaOla(mensajeEncriptadoAuxiliar);
                LlenarUnaOla(valoresOla, nivelesAuxiliar, ref numPadChars);
            }

            Queue<byte> mensajeEncriptado = new Queue<byte>();
            foreach (var item in cadena)
            {
                mensajeEncriptado.Enqueue(item);
            }

            int valoresPicos = nivelesAuxiliar[0].Count;
            int valoresMedios = valoresPicos * 2;

            //Crear Niveles
            var listaNiveles = new List<Queue<byte>>();
            for (int i = 0; i < numNivel; i++)
            {
                listaNiveles.Add(new Queue<byte>());
            }
            /////////////////////////////////////////////////////
            

            int pos = 0;
            while (mensajeEncriptado.Count != 0)
            {
                if (pos == 0)
                {   //Llenar el primer nivel
                    for (int i = 0; i < valoresPicos; i++)
                    {
                        listaNiveles[pos].Enqueue(mensajeEncriptado.Dequeue());
                    }
                    pos++;
                }//llenar el ultimo nivel
                else if (mensajeEncriptado.Count < valoresMedios)
                {
                    while (mensajeEncriptado.Count != 0)
                    {
                        listaNiveles[pos].Enqueue(mensajeEncriptado.Dequeue());
                    }
                }//llenar niveles medios
                else
                {
                    for (int i = 0; i < valoresMedios; i++)
                    {
                        listaNiveles[pos].Enqueue(mensajeEncriptado.Dequeue());
                    }
                    pos++;
                }
            }

            //Leer el zigzag


            List<byte> respuesta = new List<byte>();
            int iterationsCountOFValues = coutOfEncriptValues;

            int posZigZag = 0;
            bool reves = false;
            while (listaNiveles[1].Count != 0)
            {

                respuesta.Add(listaNiveles[posZigZag].Dequeue());

                if (posZigZag == 0) reves = false;
                else if (posZigZag == 3) reves = true;
                if (reves) posZigZag--; else posZigZag++;
            }

            //Codigo para prubar con entrada MY SPIDER...
             string depuracion = "";

             foreach (var item in respuesta)
             {
                 depuracion += Convert.ToChar(item);

             }

            return respuesta;
        }

        /// <summary>
        /// Cifrado diferente a la interfaz al usar bytes y no caracteres.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns>El primer byte de la lista devuelta es la canti</returns>
        public List<byte> Cifrar2(byte[] cadena)
        {

            
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

            //Crear zigzag
            int numPadChars = 0;
            while (mensaje.Count != 0)
            {
                Queue<byte> valoresOla = ValoresParaUnaOla(mensaje);
                LlenarUnaOla(valoresOla, listaNiveles, ref numPadChars);
            }

            //Crar mensaje encriptado en lista de bytes.
            //Agregar como primer bit el número de niveles... 
            List<byte> resultado = new List<byte>();

            for (int i = 0; i < listaNiveles.Count; i++)
            {
                //if (i == 0) resultado.Add(this.numNivel);

                while (listaNiveles[i].Count != 0)
                {
                    resultado.Add(listaNiveles[i].Dequeue());
                }
            }


            //Codigo para prubar con entrada MY SPIDER...
            string depuracion = "";

            foreach (var item in resultado)
            {
                depuracion += Convert.ToChar(item);

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
