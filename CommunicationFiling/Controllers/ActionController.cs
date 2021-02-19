using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunicationFiling.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Action = CommunicationFiling.DAL.Entities.Action;

namespace CommunicationFiling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        private readonly CommFilingContext _context;

        public ActionController(CommFilingContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
    }
}
