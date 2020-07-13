using System;
using System.ComponentModel.DataAnnotations;

namespace SGCC_API.Model
{
    public class Predio : AbstractModel
    {
        [Key]
        public int IdPredio { get; set; }
        public char Bloco { get; set; }
        public int NumeroPredio { get; set; }
        public int NumAndares { get; set; }
    }
}
