using CommunicationFiling.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CommunicationFiling.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase
    {
        private readonly ILogger Logger;

        public BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }

        protected string GetMethodCode(MethodBase method)
        {
            return string.Concat(method.ReflectedType.Name, ".", method.Name);
        }

        protected void CreateLog(string message, string methodBase, LogLevel level)
        {
            Logger.Log(level, string.Concat(message, " in ", methodBase));
        }

        protected ActionResult HandleError(string error, string code = "")
        {
            CreateLog(error, code, LogLevel.Error);
            return StatusCode(StatusCodes.Status400BadRequest, new ErrorDTO()
            {
                State = StatusCodes.Status400BadRequest,
                Code = code,
                Description = error
            });
        }

    }
}
