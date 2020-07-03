using SGCC_API.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.ViewModel
{
    public class FilterSalvaPessoaRecepcao : Pessoa
    {
        public string TipoDePessoa { get; set; }  ///Associado ou Visitante

    }
}
