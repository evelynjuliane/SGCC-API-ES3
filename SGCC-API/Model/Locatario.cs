using SGCC_API.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model
{
    public class Locatario : ContaBancaria
    {
        public int IdLocatario { get; set; }
        public List<Local> Local { get; set; }
    }
}
