using System;
using System.Reflection;
using AutoMapper;
using CommunicationFiling.Controllers.Base;
using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Action = CommunicationFiling.DAL.Entities.Action;

namespace CommunicationFiling.Controllers
{
    [SwaggerTag("Actions API - Acciones del controlador para gestion de datos de la tabla Acciones")]
    //[Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("[controller]")]
    public class ActionController : BaseController<ActionController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly IActionRepo ActionRepo;

        public ActionController(IConfiguration configuration, IMapper mapper, 
            IActionRepo actionRepo, ILogger<ActionController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            ActionRepo = actionRepo;
        }

        /// <summary>
        /// Obtiene datos de registro parametrico de la accion por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>DTO con registro de accion</returns>
        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult Get(long id)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (id < 1)
                {
                    CreateLog(Enums.BadRequest, GetMethodCode(method), LogLevel.Information);
                    return BadRequest();
                }

                var response = ActionRepo.Get(id);
                if (response != null)
                {
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(response);
                }
                else
                {
                    CreateLog(Enums.NotFound, GetMethodCode(method), LogLevel.Warning);
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message, GetMethodCode(method));
            }
        }

        /// <summary>
        /// Obtiene datos de registro parametrico por codigo
        /// </summary>
        /// <param name="code">Codigo</param>
        /// <returns>DTO con datos del registro</returns>
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ActionResult GetByCode(string code)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                var response = ActionRepo.Get(x => x.Code == code);
                if (response != null)
                {
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(response);
                }
                else
                {
                    CreateLog(Enums.NotFound, GetMethodCode(method), LogLevel.Warning);
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message, GetMethodCode(method));
            }
        }

        /// <summary>
        /// Crea o inserta registro parametrico para una accion
        /// </summary>
        /// <param name="action">DTO con datos de la accion</param>
        /// <returns>ID resultante del registro</returns>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(ActionDTO action)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                Action newAction = Mapper.Map<Action>(action);
                newAction.IsValid = true;
                newAction.Id = 0;
                var response = ActionRepo.Create(newAction);
                if (response > 0)
                {
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(response);
                }
                else
                {
                    CreateLog(Enums.BadRequest, GetMethodCode(method), LogLevel.Warning);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message, GetMethodCode(method));
            }
        }

        /// <summary>
        /// Actualiza registro parametrico de una accion
        /// </summary>
        /// <param name="action">DTO de registro de accion</param>
        /// <returns>ID del registro actualizada de accion</returns>
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(ActionDTO action)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (action.Id > 0)
                {
                    Action upAction = Mapper.Map<Action>(action);
                    ActionRepo.Update(upAction);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(upAction.Id);
                }
                else
                {
                    CreateLog(Enums.BadRequest, GetMethodCode(method), LogLevel.Information);
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message, GetMethodCode(method));
            }
        }

        /// <summary>
        /// Elimina registro parametrico de accion
        /// </summary>
        /// <param name="action">DTO de registro de accion</param>
        /// <returns>Validacion exitosa del proceso</returns>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(ActionDTO action)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (action.Id > 0)
                {
                    Action delAction = Mapper.Map<Action>(action);
                    ActionRepo.Delete(delAction);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(true);
                }
                else
                {
                    CreateLog(Enums.BadRequest, GetMethodCode(method), LogLevel.Information);
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message, GetMethodCode(method));
            }
        }

        /// <summary>
        /// Elimina registro parametrico de accion por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>Validacion exitosa de eliminacion del registro</returns>
        [HttpPost]
        [Route("Delete/{id}")]
        [Produces("application/json")]
        public ActionResult Delete(long id)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (id > 0)
                {
                    var delAction = ActionRepo.Get(id);
                    if (delAction != null && delAction.Id > 0)
                    {
                        ActionRepo.Delete(delAction);
                        CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                        return Ok(true);
                    }
                    else
                    {
                        CreateLog(Enums.NotFound, GetMethodCode(method), LogLevel.Information);
                        return NotFound();
                    }
                }
                else
                {
                    CreateLog(Enums.BadRequest, GetMethodCode(method), LogLevel.Warning);
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex.Message, GetMethodCode(method));
            }
        }
    }
}
