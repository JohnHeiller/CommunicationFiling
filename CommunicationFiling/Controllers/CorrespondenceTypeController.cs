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
    [SwaggerTag("CorrespondenceTypes API - Acciones del controlador para gestion de datos de la tabla Tipos de correspondencia")]
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("[controller]")]
    public class CorrespondenceTypeController : BaseController<CorrespondenceTypeController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly ICorrespondenceTypeRepo CorrespondenceTypeRepo;

        public CorrespondenceTypeController(IConfiguration configuration, IMapper mapper,
            ICorrespondenceTypeRepo auditRepo, ILogger<CorrespondenceTypeController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            CorrespondenceTypeRepo = auditRepo;
        }

        /// <summary>
        /// Obtiene datos de registro de tipo de correspondencia por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>DTO del registro de tipo de correspondencia</returns>
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
                var response = CorrespondenceTypeRepo.Get(id);
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
        /// Crea o inserta registro de tipo de correspondencia
        /// </summary>
        /// <param name="audit">DTO del registro de tipo de correspondencia</param>
        /// <returns>ID del registro</returns>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(CorrespondenceTypeDTO audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                CorrespondenceType newCorrespondenceType = Mapper.Map<CorrespondenceType>(audit);
                newCorrespondenceType.IsValid = true;
                newCorrespondenceType.Id = 0;
                var response = CorrespondenceTypeRepo.Create(newCorrespondenceType);
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
        /// Actualiza registro de tipo de correspondencia por DTO
        /// </summary>
        /// <param name="audit">DTO del registro de tipo de correspondencia a actualizar</param>
        /// <returns>ID del registro actualizado</returns>
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(CorrespondenceType audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (audit.Id > 0)
                {
                    CorrespondenceType upCorrespondenceType = Mapper.Map<CorrespondenceType>(audit);
                    CorrespondenceTypeRepo.Update(upCorrespondenceType);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(upCorrespondenceType.Id);
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
        /// Elimina registro de tipo de correspondencia por DTO
        /// </summary>
        /// <param name="audit">DTO con registro de tipo de correspondencia a eliminar</param>
        /// <returns>Validacion exitosa de eliminacion</returns>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(CorrespondenceTypeDTO audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (audit.Id > 0)
                {
                    CorrespondenceType delCorrespondenceType = Mapper.Map<CorrespondenceType>(audit);
                    CorrespondenceTypeRepo.Delete(delCorrespondenceType);
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
        /// Elimina registro de tipo de correspondencia por ID
        /// </summary>
        /// <param name="id">ID del registro de tipo de correspondencia</param>
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
                    var delCorrespondenceType = CorrespondenceTypeRepo.Get(id);
                    if (delCorrespondenceType != null && delCorrespondenceType.Id > 0)
                    {
                        CorrespondenceTypeRepo.Delete(delCorrespondenceType);
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
