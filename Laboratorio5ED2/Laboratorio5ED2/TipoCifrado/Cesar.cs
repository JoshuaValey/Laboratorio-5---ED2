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

        public string Cifrar(string cadena) => AlgoritmoCesar(cadena, this.UpperAbc, this.EncriptUpperAbc, 
                                                                                this.LowerAbc, this.EncriptLowerAbc);
        

        public string DesCifrar(string cadena) => AlgoritmoCesar(cadena, this.EncriptUpperAbc, this.UpperAbc,
                                                                                this.EncriptLowerAbc, this.LowerAbc);
        

        Dictionary<char, char> MakeDiccionary(string originAbc, string changedAbc)
        {
            var retorno = new Dictionary<char, char>();

            for (int i = 0; i < originAbc.Length; i++)
                retorno.Add(originAbc[i], changedAbc[i]);

            return retorno;
        }

        private string AlgoritmoCesar(string cadena, string uClave, string uValor, 
            string lClave, string lValor)
        {
            string salida = "";

            var upperDic = MakeDiccionary(uClave, uValor);
            var lowerDic = MakeDiccionary(lClave, lValor);

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

    }
}
