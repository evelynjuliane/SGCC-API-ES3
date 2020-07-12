using Microsoft.AspNetCore.Mvc;
using SGCC_API.Model;
using SGCC_API.Model.Enum;
using SGCC_API.Repository;
using SGCC_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SGCC_API.Services
{
    public class RecepcaoServices 
    {
        private readonly ApplicationDbContext _repository;

        public RecepcaoServices(ApplicationDbContext repository)
        {
            _repository = repository;
        }

    }
}
