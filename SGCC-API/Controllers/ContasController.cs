using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGCC_API.Controllers
{
    public class ContasController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public ContasController(ApplicationDbContext database)
        {
            this._database = database;
        }

    }
}
