
using SGCC_API.Model.Abstract;
using SGCC_API.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.ViewModel
{
    public class FilterSalvaPessoaRecepcao : Pessoa
    {
        public ETipoRecepcao TipoDePessoa { get; set; }  ///Associado ou Visitante



    }
}
