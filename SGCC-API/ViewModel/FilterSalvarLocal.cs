using SGCC_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.ViewModel
{
    public class FilterSalvarLocal
    {

        public int IdLocal { get; set; }
        public int Andar { get; set; }
        public int Numero { get; set; }
        public float TamanhoM2 { get; set; }
        public double Valor { get; set; }
        public int Locatario { get; set; }
        public int Locador { get; set; }
    }

}
