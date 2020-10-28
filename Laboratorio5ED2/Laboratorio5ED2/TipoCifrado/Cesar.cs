using Laboratorio5ED2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio5ED2.TipoCifrado
{
    internal class Cesar : ICifrado
    {

        /***
         * Revisar por que se puede optimizar un poco más... 
         * Revisar por que se puede optimizar un poco más... 
         * 
         */

        private string UpperAbc;
        private string LowerAbc;
        private string EncriptUpperAbc;
        private string EncriptLowerAbc;

        public Cesar()
        {
            this.UpperAbc = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            this.LowerAbc = this.UpperAbc.ToLower();
            this.EncriptUpperAbc = "ANDROIBCEFGHJKLMÑPQSTUVWXYZ";
            this.EncriptLowerAbc = this.EncriptUpperAbc.ToLower();
        }

        public string Cifrar(string cadena)
        {
            string salida = "";

            var upperDic = MakeDiccionary(this.UpperAbc, this.EncriptUpperAbc);
            var lowerDic = MakeDiccionary(this.LowerAbc, this.EncriptLowerAbc);

            foreach (var item in cadena)
            {
                if (upperDic.ContainsKey(item))
                    salida += upperDic[item];
                else if (lowerDic.ContainsKey(item))
                    salida += lowerDic[item];
                else
                    salida += item;
            }
            return salida;
        }

        public string DesCifrar(string cadena)
        {
            string salida = "";

            var upperDic = MakeDiccionary(this.EncriptUpperAbc, this.UpperAbc);
            var lowerDic = MakeDiccionary(this.EncriptLowerAbc, this.LowerAbc);
            foreach (var item in cadena)
            {
                if (upperDic.ContainsKey(item))
                    salida += upperDic[item];
                else if (lowerDic.ContainsKey(item))
                    salida += lowerDic[item];
                else
                    salida += item;
            }
            return salida;
        }

        Dictionary<char, char> MakeDiccionary(string originAbc, string changedAbc)
        {
            var retorno = new Dictionary<char, char>();

            for (int i = 0; i < originAbc.Length; i++)
                retorno.Add(originAbc[i], changedAbc[i]);

            return retorno;
        }


    }
}
