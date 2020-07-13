using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace SGCC_API.Model
{
    public class Conta : AbstractModel
    {
        [Key]
        public int IdConta { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string MesReferencia { get; set; }
        public bool Pago { get; set; }
        public string DataVencimento { get; set; }
        public Local LocalConta { get; set; }

    }
}