using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio5ED2.Interfaces
{
    interface ICifrado
    {
        public string Cifrar(string cadena);
        public string DesCifrar(string cadena);
    }
}
