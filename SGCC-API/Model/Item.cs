using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SGCC_API.Model
{
    public class Item : AbstractModel
    {
        [Key]
        public int IdItem { get; set; }
        public String Nome { get; set; }
        public Double Valor { get; set; }
        public int QtdDisponivel { get; set; }
        public Predio Predio { get; set; }
    }
}
