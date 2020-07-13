using System;

namespace SGCC_API.ViewModel
{
    public class FilterConta
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string MesReferencia { get; set; }
        public bool Pago { get; set; }
        public string DataVencimento { get; set; }
        public int LocalConta { get; set; }
       
    }
}
