
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
        private readonly ApplicationDbContext _repository;
        public RecepcaoController(ApplicationDbContext repository)
        {
            _repository = repository;
        }

        [HttpGet("/Visitante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ConsultarCpf([FromQuery]string cpf)
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

        /*
         
      
         AVISO precisamos testar esses metodos depois  de fazer a controller QUE FAZ UM POST em LOCAL


        */
        //Post na tabela log autorizarVisita(cpf(se existir), id do local(...)) e se o cpf não estiver dentro do local 
        [HttpPost("/Visita")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarLog([FromQuery][Required]string cpf, [FromQuery][Required]int IdLocal, [Required]int IdRecepcao)
        {

            try
            {
                var validarCpf = _repository.Visitantes.First(c => c.Cpf == cpf);

                if (validarCpf == null) //Valida se o CPF tem uma pessoa cadastrada
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existe cpf cadastrado pra esse visitante ou não existe recepcâo com esse Id!");
                }

                var validar = _repository.Locais.First(c => c.IdLocal == IdLocal);

                if(validarCpf == null) //Valida se o CPF tem uma pessoa cadastrada
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existe cpf cadastrado pra esse visitante!");
                }
                var validarDataSaidaLog = _repository.Visitas.Last(c => c.Visitante.IdVisitante == validarCpf.IdVisitante);

                if(validarDataSaidaLog.dataSaida == null) // valida se ele está dentro do condominio
                {
                    Response.StatusCode = 204;
                    return new ObjectResult("O Visitante está dentro do Condominio!");
                }

                Visita log = new Visita(); //Registra um novo LOG

                log.Visitante = validarCpf;
                log.dataEntrada = DateTime.Now;

                _repository.Visitas.Add(log);
                _repository.SaveChanges();
                                
                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (Exception) { 
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }

        //Put para registrar saida do visitante
        [HttpPost("/Visita")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarSaidaLog([FromQuery][Required]string cpf, [Required] int IdRecepcao)
        {
            try
            {
                var validarCpf = _repository.Visitantes.First(c => c.Cpf == cpf);

                if (validarCpf == null) //Valida se o CPF tem uma pessoa cadastrada
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existe cpf cadastrado pra esse visitante ou não existe recepcâo com esse Id!");
                }

                var logrecepcao = _repository.Visitas.Last(c => c.Visitante.Cpf == cpf);


                if (logrecepcao.dataSaida == null)
                {
                    logrecepcao.dataSaida = DateTime.Now;
                    _repository.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult("Saída Salva com sucesso!");
                }
                Response.StatusCode = 204;
                return new ObjectResult("O visitante já saiu!!");
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
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
