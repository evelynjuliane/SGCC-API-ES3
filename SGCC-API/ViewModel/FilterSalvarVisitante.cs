

using SGCC_API.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace SGCC_API.ViewModel
{
    public class FilterSalvarVisitante 
    {
        public ETipoRecepcao TipoPessoa { get; set; }
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }
        public string DataNasc { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }



    }
}
