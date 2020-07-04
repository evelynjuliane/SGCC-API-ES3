using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Controllers
{
    public class LocaisController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public LocaisController(ApplicationDbContext database)
        {
            this._database = database;
        }
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarLocal([FromQuery]FilterSalvarLocal filter)
        {
           try{
                Local local = new Local();
                local.Andar = filter.Andar;
                local.Numero = filter.Numero;
                local.TamanhoM2 = filter.TamanhoM2;
                local.Valor = filter.Valor;
                local.Locatario = _database.Empresas.First(c => c.IdEmpresa == filter.Locatario);
                local.Locador = _database.Empresas.First(c => c.IdEmpresa == filter.Locador);


                _database.Locais.Add(local);
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch(Exception) { 
                Response.StatusCode = 400;
                return new ObjectResult("");
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarEmpresa(FilterSalvarEmpresa filter)
        {
            try
            {
                Empresa empresa = new Empresa();
                empresa.NomeFantasia = filter.NomeFantasia;
                empresa.NomeReal = filter.NomeReal;
                empresa.Cnpj = filter.Cnpj;
                empresa.Telefone = filter.Telefone;
                empresa.Email = filter.Email;
                empresa.AgenciaBancaria = filter.AgenciaBancaria;
                empresa.ContaBancaria = filter.ContaBancaria;


                _database.Empresas.Add(empresa);
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (Exception) { 
                Response.StatusCode = 400;
                return new ObjectResult("");
            }
        }
    }
}
