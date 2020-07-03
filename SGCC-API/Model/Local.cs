using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model
{
    public class Local
    {
        public int IdLocal { get; set; }
        public int Andar { get; set; }
        public int número { get; set; }
        public Locatario Locatario { get; set; }
        public Locador Locador { get; set; }
    }
}
