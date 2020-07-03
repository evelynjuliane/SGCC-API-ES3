using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Controllers
{
    public class ContasController : ControllerBase
    {
        //private readonly ApplicationDbContext database;
        //public ContasController(ApplicationDbContext database)
        //{
        //    this.database = database;
        //}

        //[HttpPost()]
        //public IActionResult SalvarLocal(FilterSalvarLocal filter)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Local local = new Local();
        //        local.Andar = filter.Andar;
        //        local.número = filter.número;
        //        local.Locatario = filter.Locatario;
        //        local.Locador = filter.Locador;


        //        database.Locais.Add(local);
        //        database.SaveChanges();

        //        return View();
        //    }
        //    return View();
        //}

        //[HttpPost()]
        //public IActionResult SalvarLocador(FilterSalvarLocador filter)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Locador locador = new Locador();
        //        locador.Nome = filter.Nome;
        //        locador.Documento = filter.Documento;
        //        locador.Telefone = filter.Telefone;
        //        locador.Email = filter.Email;
        //        locador.Agencia = filter.Agencia;
        //        locador.Conta = filter.Conta;
        //        locador.Local = filter.Local;

        //        database.Locadores.Add(locador);
        //        database.SaveChanges();

        //        return View();
        //    }
        //    return View();
        //}

        //[HttpPost()]
        //public IActionResult SalvarLocatario(FilterSalvarLocatario filter)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Locatario locatario = new Locatario();
        //        locatario.Nome = filter.Nome;
        //        locatario.Documento = filter.Documento;
        //        locatario.Telefone = filter.Telefone;
        //        locatario.Email = filter.Email;
        //        locatario.Agencia = filter.Agencia;
        //        locatario.Conta = filter.Conta;
        //        locatario.Local = filter.Local;

        //        database.Locatarios.Add(locatario);
        //        database.SaveChanges();

        //        return View();
        //    }
        //    return View();
        //}

    }
}
