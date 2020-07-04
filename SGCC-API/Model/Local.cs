﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Model
{
    public class Local
    {
        [Key]
        public int IdLocal { get; set; }
        public int Andar { get; set; }
        public int Numero { get; set; }
        public float TamanhoM2 { get; set; }
        public double Valor { get; set; }
        public Empresa Locatario { get; set; }
        public Empresa Locador { get; set; }
    }
}
