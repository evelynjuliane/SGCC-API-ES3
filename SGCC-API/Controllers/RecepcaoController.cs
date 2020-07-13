
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;

namespace SGCC_API.Controllers
{
    [Route("/Recepcao")]
    [ApiController]
    public class RecepcaoController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public RecepcaoController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpGet("/Visitante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ConsultarCpf([FromQuery]string cpf)
        {
            try { 
                var visitante = _database.Visitantes.First(c => c.Cpf == cpf);
                return Ok(visitante);
            }catch (Exception)
            {
                return BadRequest();
            }
        }

        /*
         
      
         AVISO precisamos testar esses metodos depois  de fazer a controller QUE FAZ UM POST em LOCAL


        */
        //Post na tabela log autorizarVisita(cpf(se existir), id do local(...)) e se o cpf não estiver dentro do local 
        [HttpPost("/Visita")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegistrarVisita([FromQuery][Required]string cpf)
        {
            try
            {
                Visitante visitante = _database.Visitantes.First(v => v.Cpf == cpf);

                if (visitante == null) //Valida se o CPF tem uma pessoa cadastrada
                    throw new ArgumentException("Não existe cpf cadastrado pra esse visitante!");
                Visita visita = null;
                try
                {
                    visita = _database.Visitas.First(v => v.Visitante.IdVisitante == visitante.IdVisitante);
                }
                catch (Exception){ }

                String mensagem;

                if (visita == null)
                {
                    visita = new Visita()
                    {
                        Visitante = visitante,
                        dataEntrada = DateTime.Now
                    };
                    _database.Visitas.Add(visita);
                    mensagem = "Entrando";
                }
                else
                {
                    visita.dataSaida = DateTime.Now;
                    mensagem = "Saindo";
                }
                _database.SaveChanges();

                return Ok(mensagem);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("/Visitante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarVisitante([FromQuery]FilterVisitante filter)
        {
            try
            {
                Visitante visitante = new Visitante();

                if (String.IsNullOrEmpty(filter.Nome))
                    throw new ArgumentException("Nome vazio ou não declarado.");

                visitante.Nome = filter.Nome;
                visitante.Cpf = filter.Cpf;
                visitante.Email = filter.Email;
                visitante.Telefone = filter.Telefone;
                visitante.TipoPessoa = filter.TipoPessoa;

                _database.Visitantes.Add(visitante);
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult("");
            }
        }
       
    }
}
