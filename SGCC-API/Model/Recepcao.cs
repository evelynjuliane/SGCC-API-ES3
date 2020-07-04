using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model
{
    public class Recepcao
    {
        [Key]
        public int IdRecepcao { get; set; }
        public string NomeEntrada { get; set; }
    }
}
