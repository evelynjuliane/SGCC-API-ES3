
using System;
using System.ComponentModel.DataAnnotations;


namespace SGCC_API.Model
{
    public class Empresa : AbstractModel
    {
        [Key]
        public int IdEmpresa { get; set; }
        public string NomeReal { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int AgenciaBancaria { get; set; }
        public int ContaBancaria { get; set; }

        public static String ValidarCnpj(String cnpj)
        {
            cnpj = cnpj.Replace("-", "").Replace("/", "").Replace("\\", "").Replace(".", "");
            if (cnpj.Length != 14)
                return null;
            foreach (char c in cnpj)
                if (!Char.IsDigit(c))//apenas digitos numéricos são permitidos
                    return null;
            //chaves padrão na checagem
            char[] chaves = new char[] { '6', '5', '4', '3', '2', '9', '8', '7', '6', '5', '4', '3', '2' };
            int soma = 0;
            //multiplicar digito com chave e acumular
            for (int i = 0; i < chaves.Length - 1; i++)
                soma += (chaves[i + 1] - '0') * (cnpj[i] - '0');
            int resto = soma % 11;
            //se resto < 2, considerar 1° digito com valor 0, senão, considerar 1° digito com valor 11 - resto
            if ((cnpj[12] - '0') != (resto < 2 ? 0 : 11 - resto))
                return null;
            soma = 0;
            for (int i = 0; i < chaves.Length; i++)
                soma += (chaves[i] - '0') * (cnpj[i] - '0');
            resto = soma % 11;
            if ((cnpj[13] - '0') != (resto < 2 ? 0 : 11 - resto))
                return null;
            return cnpj;
        }
    }
}
