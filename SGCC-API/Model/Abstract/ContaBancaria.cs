using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model.Abstract
{
    public class ContaBancaria : Pessoa
    {
        public string Agencia { get; set; }
        public string Conta { get; set; }
    }
}
