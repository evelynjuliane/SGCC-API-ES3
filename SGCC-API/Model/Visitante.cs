using SGCC_API.Model.Abstract;
using SGCC_API.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model
{

    public class Visitante : Pessoa
    {
        [Key]
        public int IdVisitante { get; set; }
        public ETipoRecepcao TipoPessoa { get; set; }

    }
}
