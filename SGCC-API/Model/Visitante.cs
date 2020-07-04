﻿using SGCC_API.Model.Enum;
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

    }
}
