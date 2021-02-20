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
    [SwaggerTag("Users API - Acciones del controlador para gestion de datos de la tabla Usuarios")]
    //[Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController<UserController>
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper Mapper;
        private readonly IUserRepo UserRepo;

        public UserController(IConfiguration configuration, IMapper mapper,
            IUserRepo userRepo, ILogger<UserController> logger) : base(logger)
        {
            Configuration = configuration;
            Mapper = mapper;
            UserRepo = userRepo;
        }

        /// <summary>
        /// Obtiene datos de registro de usuario por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>DTO del registro de usuario</returns>
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
                var response = UserRepo.Get(id);
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
        /// Crea o inserta registro de usuario
        /// </summary>
        /// <param name="user">DTO del registro de usuario</param>
        /// <returns>ID del registro</returns>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        public ActionResult Create(UserDTO user)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                User newUser = Mapper.Map<User>(user);
                newUser.IsValid = true;
                newUser.Id = 0;
                var response = UserRepo.Create(newUser);
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
        /// Actualiza registro de usuario por DTO
        /// </summary>
        /// <param name="user">DTO del registro de usuario a actualizar</param>
        /// <returns>ID del registro actualizado</returns>
        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public ActionResult Update(UserDTO user)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (user.Id > 0)
                {
                    User upUser = Mapper.Map<User>(user);
                    UserRepo.Update(upUser);
                    CreateLog(Enums.Success, GetMethodCode(method), LogLevel.Information);
                    return Ok(upUser.Id);
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
        /// Elimina registro de usuario por DTO
        /// </summary>
        /// <param name="user">DTO con registro de usuario a eliminar</param>
        /// <returns>Validacion exitosa de eliminacion</returns>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public ActionResult Delete(UserDTO user)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            try
            {
                if (user.Id > 0)
                {
                    User delUser = Mapper.Map<User>(user);
                    UserRepo.Delete(delUser);
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
        /// Elimina registro de usuario por ID
        /// </summary>
        /// <param name="id">ID del registro de usuario</param>
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
                    var delUser = UserRepo.Get(id);
                    if (delUser != null && delUser.Id > 0)
                    {
                        UserRepo.Delete(delUser);
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
