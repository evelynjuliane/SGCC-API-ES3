using SGCC_API.Model;
using System;
using System.Collections.Generic;

namespace SGCC_API.ViewModel
{
    public class FilterItem
    {
        public String Nome { get; set; }
        public double? Valor { get; set; }
        public int QtdDisponivel { get; set; }
        public int Predio { get; set; }
    }
}
