
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
    [Route("/recepcao")]
    [ApiController]
    public class RecepcaoController : ControllerBase
    {
        private readonly ApplicationDbContext _repository;

        public RecepcaoController(ApplicationDbContext repository)
        {
            _repository = repository;
        }


        [HttpGet("/pesquisarvisitante/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PesquisarVisitante([FromQuery]string cpf)
        {
            try { 
                var visitante = _repository.Visitantes.First(c => c.Cpf == cpf);
                return Ok(visitante);
            }catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }


        //Post na tabela log autorizarVisita(cpf(se existir), id do local(...)) e se o cpf não estiver dentro do local 
        [HttpPost("/salvarlogrecepcao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarLog([FromQuery][Required]string cpf, [FromQuery][Required]int IdLocal)
        {

            try
            {
                var validarCpf = _repository.Visitantes.First(c => c.Cpf == cpf);
                var validar = _repository.Locais.First(c => c.IdLocal == IdLocal);

                //if(validarCpf.)
                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (Exception) { 
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }    

            //Put para registrar saida do visitante


        [HttpPost("/salvarvisitante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarVisitante([FromQuery]FilterSalvarVisitante filter)
        {
            try
            {
                Visitante visitante = new Visitante();


                visitante.Nome = filter.Nome;
                visitante.Cpf = filter.Cpf;
                visitante.Email = filter.Email;
                visitante.Telefone = filter.Telefone;
                visitante.TipoPessoa = filter.TipoPessoa;



                _repository.Visitantes.Add(visitante);
                _repository.SaveChanges();

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
