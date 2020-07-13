using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model
{
    public class AbstractModel
    {
        protected DateTime DataRegistro { get; set; } = DateTime.Now;
    }
}
