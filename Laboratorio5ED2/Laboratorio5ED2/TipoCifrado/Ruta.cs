using Laboratorio5ED2.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Laboratorio5ED2.TipoCifrado
{
    internal class Ruta
    {
        public int contadorSignos = 0;

        public string llenarLeer(string cadena, int h, int b)
        {
            string cifrado = "";
            Queue<char> listaCadena = recuperarBytes(cadena);
            char aux;

            while (listaCadena.Count != 0)
            {
                char[,] nuevoArreglo = new char[b, h];
                for (int j = 0; j < b; j++)
                {
                    for (int k = 0; k < h; k++)
                    {
                        if (listaCadena.Count != 0)
                        {
                            aux = listaCadena.Dequeue();
                            nuevoArreglo[j, k] = aux;
                        }
                        else
                        {
                            nuevoArreglo[j, k] = Convert.ToChar("$");
                            contadorSignos++;
                        }
                    }
                }

                for (int j = 0; j < h; j++)
                {
                    for (int k = 0; k < b; k++)
                    {
                        cifrado += nuevoArreglo[k, j];
                    }
                }
            }
            return cifrado;
        }

        private Queue<char> recuperarBytes(string cadena)
        {
            Queue<char> listaBytes = new Queue<char>();
            foreach(var item in cadena)
            {
                listaBytes.Enqueue(item);
            }
            return listaBytes;
        }

        public string Cifrar(string cadena, int filas, int columnas)
        {
            string cifrado = llenarLeer(cadena, filas, columnas);
            int cantidadNumeros = (Convert.ToString(contadorSignos)).Length;
            cifrado = Convert.ToString(cantidadNumeros) + Convert.ToString(contadorSignos) + cifrado;
            return cifrado;
        }

        public string reconstruirLeer(string cifrado, int filas, int columnas)
        {
            string recuperado = "";
            Queue<char> listaBytes = recuperarBytes(cifrado);

            while (listaBytes.Count != 0)
            {
                char[,] nuevoArreglo = new char[filas, columnas];
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        if (listaBytes.Count != 0)
                        {
                            nuevoArreglo[i, j] = listaBytes.Dequeue();
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                }

                for(int i = 0; i < columnas; i++)
                {
                    for(int j = 0; j < filas; j++)
                    {
                        if (Convert.ToChar(nuevoArreglo[j, i]) != 0) 
                        {
                            recuperado += nuevoArreglo[j, i];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return recuperado;
        }

        public string DesCifrar(string cadena, int filas, int columnas)
        {
            byte[] numeros = System.Text.ASCIIEncoding.ASCII.GetBytes(cadena);
            string cantidadSignos = "";
            string cantidadNumeros = "";
            int cont = 0;
            int c = 0;
            string cadenaOriginal = "";

            cantidadNumeros += Convert.ToChar(numeros[0]);
            int cantNumeros = Convert.ToInt32(cantidadNumeros);

            for (int i = 1; i <= cantNumeros; i++)
            {
                cantidadSignos += Convert.ToChar(numeros[i]);
            }

            cont = Convert.ToInt32(cantidadSignos);

            foreach(var item in numeros)
            {
                if (c >= cantNumeros+1)
                {
                    cadenaOriginal += Convert.ToChar(item);
                }
                else
                {
                    c++; 
                }
            }
            cont = cont + 2;
            string reconstruido = reconstruirLeer(cadenaOriginal, filas, columnas);
            reconstruido = reconstruido.Substring(0, reconstruido.Length - cont);
            return reconstruido;
        }
    }
}
