using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommunicationFiling.WebAppClient.Models;
using CommunicationFiling.WebAppClient.DTO;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace CommunicationFiling.WebAppClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        public IConfiguration _configuration { get; }
        private string _conectionString { get; }


        public ClientController(ILogger<ClientController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _conectionString = _configuration.GetConnectionString("ServicesConnection");
        }

        public async Task<IActionResult> GetAllCommTypes()
        {
            using var correspondencesTypesService = new ClientBase<List<CorrespondenceTypeDTO>>(_conectionString);
            var dataResult = await correspondencesTypesService.GetTAsync("/CorrespondenceType/GetAll/");
            
            return Json(dataResult);
        }

        public async Task<IActionResult> GetAllUsers()
        {
            using var correspondencesTypesService = new ClientBase<List<UserDTO>>(_conectionString);
            var dataResult = await correspondencesTypesService.GetTAsync("/User/GetAll/");

            return Json(dataResult);
        }

        public async Task<IActionResult> GetUserById(long id)
        {
            using var userService = new ClientBase<UserDTO>(_conectionString);
            var dataResult = await userService.GetTAsync("/User/Get/" + id);

            return Json(dataResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFiling(FilingDTO data)
        {
            using var filingService = new ClientBase<FilingDTO>(_conectionString);
            var dataResult = await filingService.CreateData("/Filing/Create/", data);

            return Json(dataResult);
        }

        public async Task<IActionResult> GetFilingById(long id)
        {
            using var filingService = new ClientBase<FilingDTO>(_conectionString);
            var dataResult = await filingService.GetTAsync("/Filing/Get/" + id);

            return Json(dataResult);
        }

        public async Task<IActionResult> GetFilingByConsecutive(string consecutive)
        {
            using var filingService = new ClientBase<List<FilingDTO>>(_conectionString);
            var dataResult = await filingService.GetTAsync("/Filing/GetByConsecutive/" + consecutive);
            if (dataResult.Count() > 0)
            {
                return Json(dataResult.FirstOrDefault());
            }
            else
            {
                return Json(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFilingById(long id)
        {
            using var filingService = new ClientBase<FilingDTO>(_conectionString);
            var dataResult = await filingService.DeleteTAsync("/Filing/Delete/", id);
            if (dataResult == HttpStatusCode.OK)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}
