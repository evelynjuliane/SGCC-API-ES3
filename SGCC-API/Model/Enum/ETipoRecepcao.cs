using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SGCC_API.Model.Enum
{
    public enum ETipoRecepcao
    {
        [Description("ASSOCIADO")]
        Associado = 0,
        [Description("VSERVICO")]
        VisitanteServico = 1,
        [Description("VCLIENTE")]
        VisitanteCliente = 2
    }
}















