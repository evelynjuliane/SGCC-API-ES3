
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

        [HttpPost("/salvarrecepcao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarRecepcao([FromQuery][Required] string NomeDeEntrada)
        {
            try
            {
                Recepcao recepcao = new Recepcao();

                recepcao.NomeEntrada = NomeDeEntrada;
                _repository.Recepcoes.Add(recepcao);
                _repository.SaveChanges();
                
                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
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

        /*
         
      
         AVISO precisamos testar esses metodos depois  de fazer a controller QUE FAZ UM POST em LOCAL


        */
        //Post na tabela log autorizarVisita(cpf(se existir), id do local(...)) e se o cpf não estiver dentro do local 
        [HttpPost("/salvarentradalogrecepcao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarLog([FromQuery][Required]string cpf, [FromQuery][Required]int IdLocal, [Required]int IdRecepcao)
        {

            try
            {
                var validarCpf = _repository.Visitantes.First(c => c.Cpf == cpf);
                var validarRecepcao = _repository.Recepcoes.First(c => c.IdRecepcao == IdRecepcao);

                if (validarCpf == null && validarRecepcao == null) //Valida se o CPF tem uma pessoa cadastrada
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
                var validarDataSaidaLog = _repository.Logs.Last(c => c.Visitante.IdVisitante == validarCpf.IdVisitante);

                if(validarDataSaidaLog.dataSaida == null) // valida se ele está dentro do condominio
                {
                    Response.StatusCode = 204;
                    return new ObjectResult("O Visitante está dentro do Condominio!");
                }

                LogRecepcao log = new LogRecepcao(); //Registra um novo LOG

                log.RecepcaoEntrada = _repository.Recepcoes.First(c => c.IdRecepcao == IdRecepcao); ;
                log.Visitante = validarCpf;
                log.dataEntrada = DateTime.Now;

                _repository.Logs.Add(log);
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
        [HttpPost("/salvarsaidalogrecepcao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarSaidaLog([FromQuery][Required]string cpf, [Required] int IdRecepcao)
        {
            try
            {
                var validarCpf = _repository.Visitantes.First(c => c.Cpf == cpf);
                var validarRecepcao = _repository.Recepcoes.First(c => c.IdRecepcao == IdRecepcao);

                if (validarCpf == null && validarRecepcao == null ) //Valida se o CPF tem uma pessoa cadastrada
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existe cpf cadastrado pra esse visitante ou não existe recepcâo com esse Id!");
                }

                var logrecepcao = _repository.Logs.Last(c => c.Visitante.Cpf == cpf);


                if (logrecepcao.RecepcaoSaida == null)
                {
                    logrecepcao.RecepcaoSaida= _repository.Recepcoes.First(c => c.IdRecepcao == IdRecepcao);
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
