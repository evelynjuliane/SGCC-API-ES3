using System;
using System.ComponentModel.DataAnnotations;

namespace SGCC_API.Model
{
    public class Local
    {
        [Key]
        public int IdLocal { get; set; }
        public int Andar { get; set; }
        public int Numero { get; set; }
        public float TamanhoM2 { get; set; }
        public double Valor { get; set; }
        [Required]
        public Empresa Locatario { get; set; }
        public Empresa Locador { get; set; }
    }
}
