﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public IActionResult CriarPredio([FromBody]FilterPredio filter)
        {
           try{
                if (filter.NumeroPredio <= 0)
                    throw new ArgumentException("Número de prédio deve ser um número natural.");

                filter.Bloco = Char.ToUpper(filter.Bloco);
                if (filter.Bloco < 'A' || filter.Bloco > 'Z')
                    throw new ArgumentException("Letra de bloco inválida.");

                if(filter.NumeroPredio <= 0)
                    throw new ArgumentException("Número de andares deve ser um número natural.");

                Predio predio = new Predio
                {
                    NumeroPredio = filter.NumeroPredio,
                    Bloco = filter.Bloco,
                    NumAndares = filter.NumAndares
                };
                _database.Predios.Add(predio);
                int i = 0;
                foreach (FilterLocal filterLocal in filter.locais) {

                    if (filterLocal.Andar < 0 || filterLocal.Andar >= filter.NumAndares)
                        throw new ArgumentException("Local com andar inválido");

                    if (filterLocal.Numero <= 0)
                        throw new ArgumentException("Local com andar inválido");

                    int j = 0;
                    foreach (FilterLocal ls in filter.locais)
                        if(i != j++)
                            if (ls.Andar == filterLocal.Andar && ls.Numero == filterLocal.Numero) 
                                throw new ArgumentException("Locais com número e andar repetidos.");

                    if (filterLocal.TamanhoM2 < 0)
                        throw new ArgumentException("Local com tamanho negativo.");

                    if(filterLocal.Valor < 0)
                        throw new ArgumentException("Local com valor negativo.");

                    Local local = new Local
                    {
                        Andar = filterLocal.Andar,
                        Numero = filterLocal.Numero,
                        TamanhoM2 = filterLocal.TamanhoM2,
                        Valor = filterLocal.Valor,
                        Locatario = _database.Empresas.First(e => e.IdEmpresa == filterLocal.Locatario),
                        Locador = _database.Empresas.First(e => e.IdEmpresa == filterLocal.Locador)
                    };
                    if (local.Locador == null)
                        throw new ArgumentException("Empresa locadora não existente!");

                    if (local.Locador == null && filterLocal.Locatario != null)
                        throw new ArgumentException("Empresa locatária não existente!");

                    _database.Locais.Add(local);
                    i++;
                }

                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (ArgumentException ae)
            {
                Response.StatusCode = 400;
                return new ObjectResult(ae.Message);
            }
            catch (Exception) { 
                Response.StatusCode = 404;
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
                filter.Cnpj = Empresa.ValidarCnpj(filter.Cnpj);
                if (filter.Cnpj == null)
                    throw new ArgumentException("Cnpj inválido");

                if (String.IsNullOrEmpty(filter.NomeReal) || filter.NomeReal.Length < 3)
                    throw new ArgumentException("Tamanho do nome insuficiente.");

                if (filter.AgenciaBancaria <= 0 || filter.ContaBancaria <= 0)
                    throw new ArgumentException("Dados bancários inválidos");


                //////////////////////////////////////////
                if (String.IsNullOrEmpty(filter.NomeFantasia))
                    filter.NomeFantasia = filter.NomeReal;

                Empresa empresa = new Empresa
                {
                    NomeFantasia = filter.NomeFantasia,
                    NomeReal = filter.NomeReal,
                    Telefone = filter.Telefone,
                    Email = filter.Email,
                    AgenciaBancaria = filter.AgenciaBancaria,
                    ContaBancaria = filter.ContaBancaria
                };

                _database.Empresas.Add(empresa);
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch (Exception e) { 
                Response.StatusCode = 400;
                return new ObjectResult("");
            }
        }

        [HttpPut("/Empresa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AlterarEmpresa([FromQuery] int IdEmpresa, FilterEmpresa filter)
        {
            try
            {
                Empresa empresa = _database.Empresas.First(c => c.IdEmpresa == IdEmpresa);

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
                    throw new NullReferenceException();

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
    }
}
