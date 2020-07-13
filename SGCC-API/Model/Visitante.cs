using System;
using SGCC_API.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace SGCC_API.Model
{

    public class Visitante 
    {
        [Key]
        public int IdVisitante { get; set; }
        public ETipoRecepcao TipoPessoa { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DataNasc { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public static String ValidarCpf(string cpf)
        {
            cpf = cpf.Replace("-", "").Replace("/", "").Replace(".", "");
            if (cpf.Length != 11)
                return null;
            foreach (char c in cpf)
                if (!Char.IsDigit(c))//apenas digitos numéricos são permitidos
                    return null;
            //chaves padrão na checagem
            int[] chaves = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            //multiplicar digito com chave e acumular
            for (int i = 0; i < chaves.Length - 2; i++)
                soma += chaves[i + 1] * (cpf[i] - '0');
            int resto = soma % 11;
            //se resto < 2, considerar 1° digito com valor 0, senão, considerar 1° digito com valor 11 - resto
            if ((cpf[9] - '0') != (resto < 2 ? 0 : 11 - resto))
                return null;
            soma = 0;
            for (int i = 0; i < chaves.Length - 1; i++)
                soma += chaves[i] * (cpf[i] - '0');
            resto = soma % 11;
            if ((cpf[110] - '0') != (resto < 2 ? 0 : 11 - resto))
                return null;
            return cpf;
        }

    }
}
