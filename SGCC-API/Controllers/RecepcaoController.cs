using BenchmarkDotNet.Reports;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Services;
using SGCC_API.ViewModel;

namespace SGCC_API.Controllers
{
    [Route("recepcao")]
    public class RecepcaoController : ControllerBase
    {
        private readonly RecepcaoServices _service;
        public RecepcaoController(RecepcaoServices service)
        {
            _service = service;
        }
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        [HttpPost("/salvarvisitante")]
        public IActionResult SalvarPessoa(FilterSalvaPessoaRecepcao filter)
        {
            if (ModelState.IsValid)
            {
                _service.SalvarVisitante(filter);

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            Response.StatusCode = 400;
            return new ObjectResult("");
        }
    }
}
