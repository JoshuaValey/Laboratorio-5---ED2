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
        int contadorSignos = 0;
        private List<char[,]> llenarArreglos(string cadena, int h, int b) 
        {
            int cantidadCaracteres = cadena.Length;
            int cantidadPorArreglo = h * b;
            Decimal cantidadArreglos = Decimal.Divide(cantidadCaracteres, cantidadPorArreglo);
            List<char[,]> listaArreglos = new List<char[,]>();
            Queue<char> listaCadena = recuperarBytes(cadena);
            char aux;

            if ((cantidadArreglos % 2) != 0)
            {
                cantidadArreglos = (cantidadCaracteres / cantidadPorArreglo) + 1;
            }
            else
            {
                cantidadArreglos = cantidadCaracteres / cantidadPorArreglo;
            }

            for(int i = 0; i < cantidadArreglos; i++)
            {
                char[,] nuevoArreglo = new char[b, h];
                for(int j = 0; j < b; j++)
                {
                    for(int k = 0; k < h; k++)
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
                listaArreglos.Add(nuevoArreglo);
            }
            return listaArreglos;
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

        private string leerListaArreglos(List<char[,]> lista, int b, int h)
        {
            string cifrado = "";
            int contador = 0;
            int fila = 0;

            while (contador <= b-1)
            {
                for(int i = 0; i < lista.Count; i++)
                {
                    char[,] nuevoArreglo = lista[i];
                    for(int j = 0; j < h; j++)
                    {
                        cifrado += nuevoArreglo[j, fila];
                    }
                }
                fila++;
                contador++;
            }
            
            return cifrado;
        }

        public string Cifrar(string cadena, int filas, int columnas)
        {
            List<char[,]> listaArreglos = llenarArreglos(cadena, filas, columnas);
            int cantidadNumeros = (Convert.ToString(contadorSignos)).Length;
            string cifrado = Convert.ToString(cantidadNumeros) + Convert.ToString(contadorSignos) + leerListaArreglos(listaArreglos, filas, columnas);
            return cifrado;
        }

        private List<char[,]> reconstruirArreglos(string cifrado, int filas, int columnas)
        {
            Queue<char> listaBytes = recuperarBytes(cifrado);
            List<char[,]> listaArreglos = new List<char[,]>();
            int cantidadCaracteres = cifrado.Length;
            int cantidadPorArreglo = columnas * filas;
            int contador = 0;
            int fila = 0;
            int cantidadArreglos = cantidadCaracteres/cantidadPorArreglo;

            for(int i = 0; i < cantidadArreglos; i++)
            {
                char[,] nuevoArreglo = new char[columnas, filas];
                listaArreglos.Add(nuevoArreglo);
            }

            while (contador <= filas - 1)
            {
                for (int i = 0; i < listaArreglos.Count; i++)
                {
                    char[,] nuevoArreglo = listaArreglos[i];
                    for (int j = 0; j < columnas; j++)
                    {
                        nuevoArreglo[j, fila] = listaBytes.Dequeue();
                    }
                }
                fila++;
                contador++;
            }

            return listaArreglos;
        }

        private string leerArregloReconstruido(List<char[,]> lista, int filas, int columnas)
        {
            string reconstruido = "";
            int contador = 0;

            while (contador < lista.Count)
            {
                char[,] nuevoArreglo = lista[contador];
                for(int i = 0; i < columnas; i++)
                {
                    for(int j = 0; j < filas; j++)
                    {
                        reconstruido += nuevoArreglo[i, j];
                    }
                }
                contador++;
            }
            return reconstruido;
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

            for(int i = 1; i <= cantNumeros; i++)
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

            //string cadenaOriginal = cadena.Substring(1, cadena.Length - 1);
            List<char[,]> listaReconstruida = reconstruirArreglos(cadenaOriginal, filas, columnas);
            string reconstruido = leerArregloReconstruido(listaReconstruida, filas, columnas);
            reconstruido = reconstruido.Substring(0, reconstruido.Length - cont);
            return reconstruido;
        }
    }
}
