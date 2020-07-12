using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.Linq;

namespace SGCC_API.Controllers
{
    public class AlmoxarifadoController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public AlmoxarifadoController(ApplicationDbContext database)
        {
            this._database = database;
        }

        [HttpPost("/Item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegistrarItem([FromQuery] FilterItem filter)
        {
            try
            {
                Item item = new Item
                {
                    Nome = filter.Nome.Trim(),
                    Valor = (Double)filter.Valor,
                    QtdDisponivel = filter.QtdDisponivel,
                    Predio = _database.Predios.First(p => p.IdPredio == filter.Predio)
                };

                if (item.Predio == null)
                    throw new ArgumentException("Predio indicado não encontrado!");

                if (_database.Items.First(I => I.Nome == filter.Nome) != null)
                    throw new ArgumentException("Item com esse nome já existe!");

                if (String.IsNullOrEmpty(item.Nome))
                    throw new ArgumentException("Nome não pode estar vazio");

                if (item.Valor < 0)
                    throw new ArgumentException("Valor não pode ser negativo!");

                if (item.QtdDisponivel < 0)
                    throw new ArgumentException("Quantidade não pode ser negativa!");

                _database.Items.Add(item);
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (ArgumentException ae)
            {
                Response.StatusCode = 400;
                return new ObjectResult(ae.Message);
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult("");
            }
        }
        [HttpGet("/Item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RecupararItem([FromQuery] int idItem)
        {
            try
            {
                Item item = _database.Items.First(I => I.IdItem == idItem);

                if (item == null)
                    throw new NullReferenceException("Item não encontrado!");

                Response.StatusCode = 200;
                return new ObjectResult(item);
            }
            catch (NullReferenceException ae)
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
        [HttpPut("/Item/Informacoes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AlterarInformacoes([FromQuery] int idItem, [FromQuery] FilterItem novoitem)
        {
            try
            {
                Item item = _database.Items.First(I => I.IdItem == idItem);

                if (item == null)
                    throw new NullReferenceException("Item não encontrado!");

                if (!String.IsNullOrEmpty(novoitem.Nome))
                    item.Nome = novoitem.Nome;

                if (novoitem.Valor != null)
                    item.Valor = (double)novoitem.Valor;

                _database.SaveChanges();

                Response.StatusCode = 200;
                return new ObjectResult(item);
            }
            catch (NullReferenceException ae)
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
        [HttpPut("/Item/Quantidade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AlterarEstoque([FromQuery] int idItem, [FromQuery] int deltaQtdDisponivel)
        {
            try
            {
                Item item = _database.Items.First(I => I.IdItem == idItem);

                if (item == null)
                    throw new NullReferenceException("Item não encontrado!");

                if (deltaQtdDisponivel == 0)
                    throw new ArgumentException("Quantidade a ser alterada não pode ser zero!");

                if ((item.QtdDisponivel + deltaQtdDisponivel) < 0)
                    throw new ArgumentException("Quantidade final não pode ser negativa!");

                item.QtdDisponivel += deltaQtdDisponivel;

                _database.SaveChanges();

                Response.StatusCode = 200;
                return new ObjectResult(item);
            }
            catch (ArgumentException ae)
            {
                Response.StatusCode = 400;
                return new ObjectResult(ae.Message);
            }
            catch (NullReferenceException ae)
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
