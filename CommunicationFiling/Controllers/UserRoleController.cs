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
    [SwaggerTag("UsersRoles API - Acciones del controlador para gestion de datos de la tabla relacional Usuarios-Roles")]
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : BaseController<UserRoleController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly IUserRoleRepo UserRoleRepo;

        public UserRoleController(IConfiguration configuration, IMapper mapper,
            IUserRoleRepo auditRepo, ILogger<UserRoleController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            UserRoleRepo = auditRepo;
        }

        /// <summary>
        /// Obtiene datos de registro de relacion Usuario y Rol por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>DTO del registro de relacion Usuario y Rol</returns>
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
                var response = UserRoleRepo.Get(id);
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
        /// Crea o inserta registro de relacion Usuario y Rol
        /// </summary>
        /// <param name="audit">DTO del registro de relacion Usuario y Rol</param>
        /// <returns>ID del registro</returns>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(UserRoleDTO audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                UserRole newUserRole = Mapper.Map<UserRole>(audit);
                newUserRole.IsValid = true;
                newUserRole.Id = 0;
                var response = UserRoleRepo.Create(newUserRole);
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
        /// Actualiza registro de relacion Usuario y Rol por DTO
        /// </summary>
        /// <param name="audit">DTO del registro de relacion Usuario y Rol a actualizar</param>
        /// <returns>ID del registro actualizado</returns>
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(UserRole audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (audit.Id > 0)
                {
                    UserRole upUserRole = Mapper.Map<UserRole>(audit);
                    UserRoleRepo.Update(upUserRole);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(upUserRole.Id);
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
        /// Elimina registro de relacion Usuario y Rol por DTO
        /// </summary>
        /// <param name="audit">DTO con registro de relacion Usuario y Rol a eliminar</param>
        /// <returns>Validacion exitosa de eliminacion</returns>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(UserRoleDTO audit)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (audit.Id > 0)
                {
                    UserRole delUserRole = Mapper.Map<UserRole>(audit);
                    UserRoleRepo.Delete(delUserRole);
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
        /// Elimina registro de relacion Usuario y Rol por ID
        /// </summary>
        /// <param name="id">ID del registro de relacion Usuario y Rol</param>
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
                    var delUserRole = UserRoleRepo.Get(id);
                    if (delUserRole != null && delUserRole.Id > 0)
                    {
                        UserRoleRepo.Delete(delUserRole);
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
