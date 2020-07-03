using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Model.Enum;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SGCC_API.Services
{
    public class RecepcaoServices 
    {
        private readonly ApplicationDbContext _repository;

        public RecepcaoServices(ApplicationDbContext repository)
        {
            _repository = repository;
        }

        public void SalvarVisitante(FilterSalvaPessoaRecepcao filter)
        {
            //Visitante visitante = new Visitante();

            //visitante.Nome = filter.Nome;
            //visitante.Documento = filter.Documento;
            //visitante.Email = filter.Email;
            //visitante.Telefone = filter.Telefone;
            //visitante.TipoPessoa = filter.TipoDePessoa;
            //if (filter.TipoDePessoa == "Associado")
            //    visitante.TipoPessoa = ETipoRecepcao.Associado;
            //if (filter.TipoDePessoa == "Visitante Cliente")
            //    visitante.TipoPessoa = ETipoRecepcao.VisitanteCliente;
            //if (filter.TipoDePessoa == "Visitante Serviço")
            //    visitante.TipoPessoa = ETipoRecepcao.VisitanteServiço;
           
            //_repository.SaveChanges();
        }

    }
}
