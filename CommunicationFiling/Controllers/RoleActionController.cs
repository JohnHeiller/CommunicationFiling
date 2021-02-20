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
    [SwaggerTag("RolesActions API - Acciones del controlador para gestion de datos de la tabla relacional Roles-Acciones")]
    //[Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("[controller]")]
    public class RoleActionController : BaseController<RoleActionController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly IRoleActionRepo RoleActionRepo;

        public RoleActionController(IConfiguration configuration, IMapper mapper,
            IRoleActionRepo roleActionRepo, ILogger<RoleActionController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            RoleActionRepo = roleActionRepo;
        }

        /// <summary>
        /// Obtiene datos de registro de relacion Rol y Accion por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>DTO del registro de relacion Rol y Accion</returns>
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
                var response = RoleActionRepo.Get(id);
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
        /// Crea o inserta registro de relacion Rol y Accion
        /// </summary>
        /// <param name="roleAction">DTO del registro de relacion Rol y Accion</param>
        /// <returns>ID del registro</returns>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(RoleActionDTO roleAction)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                RoleAction newRoleAction = Mapper.Map<RoleAction>(roleAction);
                newRoleAction.IsValid = true;
                newRoleAction.Id = 0;
                var response = RoleActionRepo.Create(newRoleAction);
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
        /// Actualiza registro de relacion Rol y Accion por DTO
        /// </summary>
        /// <param name="roleAction">DTO del registro de relacion Rol y Accion a actualizar</param>
        /// <returns>ID del registro actualizado</returns>
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(RoleActionDTO roleAction)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (roleAction.Id > 0)
                {
                    RoleAction upRoleAction = Mapper.Map<RoleAction>(roleAction);
                    RoleActionRepo.Update(upRoleAction);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(upRoleAction.Id);
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
        /// Elimina registro de relacion Rol y Accion por DTO
        /// </summary>
        /// <param name="roleAction">DTO con registro de relacion Rol y Accion a eliminar</param>
        /// <returns>Validacion exitosa de eliminacion</returns>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(RoleActionDTO roleAction)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (roleAction.Id > 0)
                {
                    RoleAction delRoleAction = Mapper.Map<RoleAction>(roleAction);
                    RoleActionRepo.Delete(delRoleAction);
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
        /// Elimina registro de relacion Rol y Accion por ID
        /// </summary>
        /// <param name="id">ID del registro de relacion Rol y Accion</param>
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
                    var delRoleAction = RoleActionRepo.Get(id);
                    if (delRoleAction != null && delRoleAction.Id > 0)
                    {
                        RoleActionRepo.Delete(delRoleAction);
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
