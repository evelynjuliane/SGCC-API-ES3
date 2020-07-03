
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Model.Enum;
using SGCC_API.Repository;
using SGCC_API.Services;
using SGCC_API.ViewModel;
using System.ComponentModel.DataAnnotations;

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
        //private readonly RecepcaoServices _service;
        //public RecepcaoController(RecepcaoServices service)
        //{
        //    _service = service;
        //}

        [HttpPost("/salvarvisitante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SalvarVisitante([FromQuery]FilterSalvaPessoaRecepcao filter)
        {
            if (ModelState.IsValid)
            {
                //_service.SalvarVisitante(filter);
                Visitante visitante = new Visitante();
                

                visitante.Nome = filter.Nome;
                visitante.Documento = filter.Documento;
                visitante.Email = filter.Email;
                visitante.Telefone = filter.Telefone;
                visitante.TipoPessoa = filter.TipoDePessoa;



                _repository.Visitantes.Add(visitante);
                _repository.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            Response.StatusCode = 400;
            return new ObjectResult("");
        }
    }
}
