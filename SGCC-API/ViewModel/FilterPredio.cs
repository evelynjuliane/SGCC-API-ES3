using System;
using System.Collections.Generic;

namespace SGCC_API.ViewModel
{
    public class FilterPredio
    {
        public int IdPredio { get; set; }
        public char Bloco { get; set; }
        public int NumeroPredio { get; set; }
        public int NumAndares { get; set; }
        public IList<FilterLocal> locais { get; set; }

    }
}
