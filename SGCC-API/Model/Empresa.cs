
using System.ComponentModel.DataAnnotations;


namespace SGCC_API.Model
{
    public class Empresa
    {
        [Key]
        public int IdEmpresa { get; set; }
        public string NomeReal { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string AgenciaBancaria { get; set; }
        public string ContaBancaria { get; set; }
    }
}
