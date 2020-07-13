using System;

namespace SGCC_API.ViewModel
{
    public class FilterEmpresa
    {
        public string NomeReal { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int AgenciaBancaria { get; set; }
        public int ContaBancaria { get; set; }
    }
}
