using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICifrados.Models
{
    public class Key
    {
        public string word { get; set; }
        public int level { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
    }
}
