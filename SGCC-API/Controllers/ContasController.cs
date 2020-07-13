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
    public class ContasController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public ContasController(ApplicationDbContext database)
        {
            this._database = database;
        }

        [HttpPost("/Conta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarConta([FromBody] FilterConta filter)
        {
            try
            {

                if (String.IsNullOrEmpty(filter.Nome))
                    throw new ArgumentException("Conta deve conter um nome.");

                if (filter.Valor < 0)
                    throw new ArgumentException("Conta não pode ter um valor negativo.");

                Conta conta = new Conta
                {
                    Nome = filter.Nome,
                    Valor = filter.Valor,
                    MesReferencia = filter.MesReferencia,
                    Pago = filter.Pago,
                    DataVencimento = filter.DataVencimento,
                    LocalConta = _database.Locais.First(lc => lc.IdLocal == filter.LocalConta)
                };

                if (conta.LocalConta == null)
                    throw new ArgumentException("Local não encontrado.");

                _database.Contas.Add(conta);
                _database.SaveChanges();

                return Ok();
            }
            catch (ArgumentException ae)
            {
                Response.StatusCode = 404;
                return new ObjectResult(ae.Message);

            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult("");

            }
        }

        [HttpGet("/Conta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RecuperarConta([FromQuery] int idConta)
        {
            try
            {
                Conta conta = _database.Contas.First(c => c.IdConta == idConta);

                if (conta == null)
                    throw new Exception();

                Response.StatusCode = 200;
                return Ok(conta);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Conta Não Existe!");
            }
        }

        [HttpPut("/Status")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AlterarStatus([FromQuery] int idConta, [FromBody]FilterConta filter)
        {
            try
            {
                Conta conta = _database.Contas.First(c => c.IdConta == idConta);

                if (conta == null)
                    throw new ArgumentException();

                conta.Pago = filter.Pago;


                _database.Contas.Add(conta);
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (ArgumentException ae)
            {
                Response.StatusCode = 404;
                return new ObjectResult(ae.Message);
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult("");
            }

        }
    }
}
