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
    [SwaggerTag("Roles API - Acciones del controlador para gestion de datos de la tabla Roles")]
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : BaseController<RoleController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly IRoleRepo RoleRepo;

        public RoleController(IConfiguration configuration, IMapper mapper,
            IRoleRepo roleRepo, ILogger<RoleController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            RoleRepo = roleRepo;
        }

        /// <summary>
        /// Obtiene datos de registro del rol por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>DTO del registro de roleoria</returns>
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
                var response = RoleRepo.Get(id);
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
        /// Crea o inserta registro del rol
        /// </summary>
        /// <param name="role">DTO del registro de rol</param>
        /// <returns>ID del registro</returns>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(RoleDTO role)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                Role newRole = Mapper.Map<Role>(role);
                newRole.IsValid = true;
                newRole.Id = 0;
                var response = RoleRepo.Create(newRole);
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
        /// Actualiza registro de rol por DTO
        /// </summary>
        /// <param name="role">DTO del registro de rol a actualizar</param>
        /// <returns>ID del registro actualizado</returns>
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(RoleDTO role)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (role.Id > 0)
                {
                    Role upRole = Mapper.Map<Role>(role);
                    RoleRepo.Update(upRole);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(upRole.Id);
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
        /// Elimina registro de rol por DTO
        /// </summary>
        /// <param name="role">DTO con registro de rol a eliminar</param>
        /// <returns>Validacion exitosa de eliminacion</returns>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(RoleDTO role)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (role.Id > 0)
                {
                    Role delRole = Mapper.Map<Role>(role);
                    RoleRepo.Delete(delRole);
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
        /// Elimina registro de rol por ID
        /// </summary>
        /// <param name="id">ID del registro de rol</param>
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
                    var delRole = RoleRepo.Get(id);
                    if (delRole != null && delRole.Id > 0)
                    {
                        RoleRepo.Delete(delRole);
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
