using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.Linq;

namespace SGCC_API.Controllers
{
    [Route("/Locais")]
    [ApiController]
    public class LocaisController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public LocaisController(ApplicationDbContext database)
        {
            this._database = database;
        }

        [HttpPost("/Predio")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarPredio([FromQuery]FilterPredio filter)
        {
           try{
                Predio predio = new Predio
                {
                    NumeroPredio = filter.NumeroPredio,
                    Bloco = filter.Bloco,
                    NumAndares = filter.NumAndares
                };
                _database.Predios.Add(predio);
                foreach (FilterLocal filterLocal in filter.locais) {
                    Local local = new Local
                    {
                        Andar = filterLocal.Andar,
                        Numero = filterLocal.Numero,
                        TamanhoM2 = filterLocal.TamanhoM2,
                        Valor = filterLocal.Valor,
                        Locatario = _database.Empresas.First(c => c.IdEmpresa == filterLocal.Locatario),
                        Locador = _database.Empresas.First(c => c.IdEmpresa == filterLocal.Locador)
                    };
                    if (local.Locatario == null)
                        throw new ArgumentException("Empresa locatária não existente!");
                    _database.Locais.Add(local);
                }
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (ArgumentException ae)
            {
                Response.StatusCode = 404;
                return new ObjectResult(ae.Message);
            }
            catch (Exception) { 
                Response.StatusCode = 400;
                return new ObjectResult("");
            }
        }

        [HttpPost("/Empresa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarEmpresa(FilterEmpresa filter)
        {
            try
            {
                Empresa empresa = new Empresa
                {
                    NomeFantasia = filter.NomeFantasia,
                    NomeReal = filter.NomeReal,
                    Telefone = filter.Telefone,
                    Email = filter.Email,
                    AgenciaBancaria = filter.AgenciaBancaria,
                    ContaBancaria = filter.ContaBancaria
                };

                if (validarCnpj(filter.Cnpj))
                    empresa.Cnpj = filter.Cnpj;
                else
                    throw new ArgumentException("Cnpj inválido");

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

        [HttpPut("/Empresa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AlterarEmpresa(FilterEmpresa filter)
        {
            try
            {
                Empresa empresa = _database.Empresas.First(c => c.IdEmpresa == filter.IdEmpresa);

                empresa.NomeFantasia = filter.NomeFantasia;
                empresa.NomeReal = filter.NomeReal;
                empresa.Telefone = filter.Telefone;
                empresa.Email = filter.Email;
                empresa.AgenciaBancaria = filter.AgenciaBancaria;
                empresa.ContaBancaria = filter.ContaBancaria;


                _database.Empresas.Add(empresa);
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
                Response.StatusCode = 500;
                return new ObjectResult("Erro inesperado.");
            }
        }

        [HttpGet("/Empresa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ConsultarEmpresa([FromQuery]int idEmpresa)
        {
            try
            {
                Empresa empresa = _database.Empresas.First(c => c.IdEmpresa == idEmpresa);

                Response.StatusCode = 200;
                return new ObjectResult(empresa);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Empresa inexistente!");
            }
        }

        [HttpPut("/Locador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult VincularLocador([FromQuery]int idLocal, [FromQuery]int idEmpresa)
        {
            return VincularEmpresa(idLocal, idEmpresa, true);
        }
        [HttpPut("/Locatario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult VincularLocatario([FromQuery]int idLocal, [FromQuery]int idEmpresa)
        {
            return VincularEmpresa(idLocal, idEmpresa, false);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private IActionResult VincularEmpresa(int idLocal, int idEmpresa, bool locador)
        {
            try
            {
                Local local = _database.Locais.First(l => l.IdLocal == idLocal);

                Empresa empresa = _database.Empresas.First(e => e.IdEmpresa == idEmpresa);

                if (empresa != null)
                    if (locador)
                        local.Locador = empresa;
                    else
                        local.Locatario = empresa;
                else
                    throw new Exception();

                _database.SaveChanges();
                Response.StatusCode = 200;
                return new ObjectResult("");
            }
            catch (NullReferenceException nre)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Local inexistente!");
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Empresa inexistente!");
            }
        }

        private bool validarCnpj(String cnpj)
        {
            cnpj = cnpj.Replace("-", "").Replace("/", "").Replace(".", "");
            if (cnpj.Length != 14)
                return false;
            foreach (char c in cnpj)
                if (!Char.IsDigit(c))//apenas digitos numéricos são permitidos
                    return false;
            //chaves padrão na checagem
            char[] chaves = new char[] { '6', '5', '4', '3', '2', '9', '8', '7', '6', '5', '4', '3', '2' };
            int soma = 0;
            //multiplicar digito com chave e acumular
            for (int i = 0; i < chaves.Length - 1; i++)
                soma += (chaves[i + 1] - '0') * (cnpj[i] - '0');
            int resto = soma % 11;
            //se resto < 2, considerar 1° digito com valor 0, senão, considerar 1° digito com valor 11 - resto
            if ((cnpj[12] - '0') != (resto < 2 ? 0 : 11 - resto))
                return false;
            soma = 0;
            for (int i = 0; i < chaves.Length; i++)
                soma += (chaves[i] - '0') * (cnpj[i] - '0');
            resto = soma % 11;
            if ((cnpj[13] - '0') != (resto < 2 ? 0 : 11 - resto))
                return false;
            return true;
        }
    }
}
