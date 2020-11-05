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
        private char padCharacter = '▄'; // ▄

        public ZigZag(byte cantidadNiveles)
        {
            this.numNivel = cantidadNiveles;
            this.valoresEnOla = numNivel * 2 - 2;
        }
        public string Cifrar(string cadena)
        {
            string resultado = "";

            if (this.numNivel < 2)
            {
                resultado = cadena;
            }
            else
            {
                Queue<char> mensaje = new Queue<char>(cadena);

                //Crear Niveles
                var listaNiveles = new List<Queue<char>>();
                for (int i = 0; i < numNivel; i++)
                {
                    listaNiveles.Add(new Queue<char>());
                }

                //Crear zigzag
                int numPadChars = 0;
                while (mensaje.Count != 0)
                {
                    Queue<char> valoresOla = ValoresParaUnaOla(mensaje);
                    LlenarUnaOla(valoresOla, listaNiveles, ref numPadChars);
                }

                bool picos = true;
                int numPicos = listaNiveles[0].Count;

                for (int i = 0; i < listaNiveles.Count; i++)
                {


                    while (listaNiveles[i].Count != 0)
                    {
                        //Asignar cantidad de inicio de olas al string resultante
                        if (picos)
                        {
                            resultado += Convert.ToChar(numPicos);
                            picos = false;
                        }
                        resultado += listaNiveles[i].Dequeue();
                    }
                }
            }

            return resultado;
        }
        public string DesCifrar(string cadena)
        {
            string respuesta = "";
            if (this.numNivel < 2)
            {
                respuesta = cadena;
            }
            else
            {

                char primerChar = Convert.ToChar(cadena.Substring(0, 1));
                int valoresPicos = Convert.ToInt32(primerChar);
                int valoresMedios = valoresPicos * 2;
                cadena = cadena[1..];

                string mensajeEncriptado = cadena;

                //Crear Niveles
                var listaNiveles = new List<Queue<char>>();
                for (int i = 1; i <= this.numNivel; i++)
                {
                    if (i == 1)
                    {
                        listaNiveles.Add(new Queue<char>(mensajeEncriptado.Substring(0, valoresPicos)));
                        mensajeEncriptado = mensajeEncriptado.Substring(valoresPicos, mensajeEncriptado.Length - valoresPicos);
                    }
                    else if (i == this.numNivel)
                    {
                        listaNiveles.Add(new Queue<char>(mensajeEncriptado));
                    }
                    else
                    {
                        if (mensajeEncriptado.Length < valoresMedios)
                        {
                            string cadenaRelleno = "";
                            Random rnd = new Random();
                            for (int k = 1; k <= valoresMedios; k++)
                            {
                                cadenaRelleno += Convert.ToChar(rnd.Next(96, 122));
                            }
                            listaNiveles.Add(new Queue<char>(cadenaRelleno));


                        }
                        else
                        {
                            listaNiveles.Add(new Queue<char>(mensajeEncriptado.Substring(0, valoresMedios)));
                            mensajeEncriptado = mensajeEncriptado.Substring(valoresMedios, mensajeEncriptado.Length - valoresMedios);
                        }
                    }
                }

                //leer zigzag

                int posZigZag = 0;
                bool reves = false;
                while (listaNiveles[1].Count != 0)
                {

                    respuesta += listaNiveles[posZigZag].Dequeue();

                    if (posZigZag == 0) reves = false;
                    else if (posZigZag == this.numNivel - 1) reves = true;
                    if (reves) posZigZag--; else posZigZag++;
                }
                respuesta = respuesta.Replace(this.padCharacter.ToString(), "");
            }

            return respuesta;
        }
        private void LlenarUnaOla(Queue<char> valoresParaOla, List<Queue<char>> niveles, ref int numPadChars)
        {
            int posNivel = this.numNivel - 1;
            numPadChars = this.valoresEnOla - valoresParaOla.Count();

            for (int i = 0; i <= posNivel; i++)
            {

                if (i != posNivel)
                {

                    if (valoresParaOla.Count != 0)
                    {
                        niveles[i].Enqueue(valoresParaOla.Dequeue());
                    }
                    else
                    {
                        niveles[i].Enqueue(padCharacter);
                    }
                }
                if (i == posNivel)
                {
                    for (int j = posNivel; j > 0; j--)
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
        private Queue<char> ValoresParaUnaOla(Queue<char> mensajeOriginal)
        {
            var valoresParaOla = new Queue<char>();
            for (int i = 0; i < this.valoresEnOla; i++)
            {
                if (mensajeOriginal.Count != 0)
                {
                    valoresParaOla.Enqueue(mensajeOriginal.Dequeue());
                }
            }
            return valoresParaOla;
        }  
    }
}
