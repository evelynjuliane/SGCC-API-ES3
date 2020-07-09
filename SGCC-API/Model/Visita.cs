using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model
{
    public class Visita
    {
        [Key]
        public int IdLog { get; set; }
        public Visitante Visitante { get; set; }
        public DateTime dataEntrada { get; set; }
        public DateTime dataSaida { get; set; }
    }
}
