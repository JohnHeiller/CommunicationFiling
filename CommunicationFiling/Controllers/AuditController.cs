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
    [SwaggerTag("Audits API - Acciones del controlador para gestion de datos de la tabla Auditorias")]
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("[controller]")]
    public class AuditController : BaseController<AuditController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly IAuditRepo AuditRepo;

        public AuditController(IConfiguration configuration, IMapper mapper,
            IAuditRepo auditRepo, ILogger<AuditController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            AuditRepo = auditRepo;
        }

        /// <summary>
        /// Obtiene datos de registro de auditoria por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>DTO del registro de auditoria</returns>
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
                var response = AuditRepo.Get(id);
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
        /// Crea o inserta registro de auditoria
        /// </summary>
        /// <param name="audit">DTO del registro de auditoria</param>
        /// <returns>ID del registro</returns>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(AuditDTO audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                Audit newAudit = Mapper.Map<Audit>(audit);
                newAudit.IsValid = true;
                newAudit.Id = 0;
                var response = AuditRepo.Create(newAudit);
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
        /// Actualiza registro de auditoria por DTO
        /// </summary>
        /// <param name="audit">DTO del registro de auditoria a actualizar</param>
        /// <returns>ID del registro actualizado</returns>
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(AuditDTO audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (audit.Id > 0)
                {
                    Audit upAudit = Mapper.Map<Audit>(audit);
                    AuditRepo.Update(upAudit);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(upAudit.Id);
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
        /// Elimina registro de auditoria por DTO
        /// </summary>
        /// <param name="audit">DTO con registro de auditoria a eliminar</param>
        /// <returns>Validacion exitosa de eliminacion</returns>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(AuditDTO audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (audit.Id > 0)
                {
                    Audit delAudit = Mapper.Map<Audit>(audit);
                    AuditRepo.Delete(delAudit);
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
        /// Elimina registro de auditoria por ID
        /// </summary>
        /// <param name="id">ID del registro de auditoria</param>
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
                    var delAudit = AuditRepo.Get(id);
                    if (delAudit != null && delAudit.Id > 0)
                    {
                        AuditRepo.Delete(delAudit);
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
