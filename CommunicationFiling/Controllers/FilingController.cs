using System;
using System.Reflection;
using AutoMapper;
using CommunicationFiling.Controllers.Base;
using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using CommunicationFiling.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace CommunicationFiling.Controllers
{
    [SwaggerTag("Filings API - Acciones del controlador para gestion de datos de la tabla Radicados")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FilingController : BaseController<FilingController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly IFilingRepo FilingRepo;

        public FilingController(IConfiguration configuration, IMapper mapper,
            IFilingRepo filingRepo, ILogger<FilingController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            FilingRepo = filingRepo;
        }

        /// <summary>
        /// Consulta registro de radicado por ID
        /// </summary>
        /// <param name="id">ID del registro de radicado</param>
        /// <returns>DTO con datos de registro de radicado</returns>
        [Authorize(Roles = "Administrador, Gestor, Destinatario")]
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
                var response = FilingRepo.Get(id);
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
        /// Consulta registro de radicado por consecutivo
        /// </summary>
        /// <param name="consecutive">Consecutivo del radicado</param>
        /// <returns>DTO del registro de radicado</returns>
        [Authorize(Roles = "Administrador, Gestor, Destinatario")]
        [HttpGet]
        [Route("GetByConsecutive/{consecutive}")]
        public ActionResult GetByConsecutive(string consecutive)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                var response = FilingRepo.Get(x => x.Consecutive == consecutive);
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
        /// Consultar registro de radicado por ID de destinatario
        /// </summary>
        /// <param name="userId">ID de usuario del destinatario</param>
        /// <returns>DTO del registro de radicado</returns>
        [Authorize(Roles = "Administrador, Gestor, Destinatario")]
        [HttpGet]
        [Route("GetByAddresseeUser/{userId}")]
        public ActionResult GetByAddresseeUser(long userId)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                var response = FilingRepo.Get(x => x.AddresseeUserId == userId);
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
        /// Consulta registro de radicado por ID de remitente
        /// </summary>
        /// <param name="userId">ID de usuario remitente</param>
        /// <returns>DTO con registro de radicado</returns>
        [Authorize(Roles = "Administrador, Gestor")]
        [HttpGet]
        [Route("GetBySenderUser/{userId}")]
        public ActionResult GetBySenderUser(long userId)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                var response = FilingRepo.Get(x => x.SenderUserId == userId);
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
        /// Crea o inserta registro de radicado por DTO
        /// </summary>
        /// <param name="filing">DTO de registro de radicado</param>
        /// <returns>ID del registro de radicado creado</returns>
        [Authorize(Roles = "Administrador, Gestor")]
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(FilingDTO filing)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                Filing newAction = Mapper.Map<Filing>(filing);
                newAction.IsValid = true;
                newAction.Id = 0;
                var response = FilingRepo.Create(newAction);
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
        /// Actualiza registro de radicado por DTO
        /// </summary>
        /// <param name="filing">DTO con datos de radicado</param>
        /// <returns>ID del registro actualizado</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(Filing filing)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (filing.Id > 0)
                {
                    Filing upAction = Mapper.Map<Filing>(filing);
                    FilingRepo.Update(upAction);
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
        /// Elimina registro de radicado
        /// </summary>
        /// <param name="filing">DTO con datos de radicado a eliminar</param>
        /// <returns>Validacion exitosa del proceso</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(FilingDTO filing)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (filing.Id > 0)
                {
                    Filing delAction = Mapper.Map<Filing>(filing);
                    FilingRepo.Delete(delAction);
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
        /// Elimina registro de radicado por ID
        /// </summary>
        /// <param name="id">ID del registro de radicado a eliminar</param>
        /// <returns>Validacion exitosa del proceso</returns>
        [Authorize(Roles = "Administrador, Gestor, Destinatario")]
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
                    var delAction = FilingRepo.Get(id);
                    if (delAction != null && delAction.Id > 0)
                    {
                        FilingRepo.Delete(delAction);
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
