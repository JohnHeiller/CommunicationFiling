using AutoMapper;
using CommunicationFiling.Controllers;
using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using CommunicationFiling.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Tynamix.ObjectFiller;

namespace CommunicationFiling.Test
{
    public class TestRoleController 
    {
        private RoleController _roleController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<IRoleRepo> _roleRepo;
        private Mock<ILogger<RoleController>> _logger;

        private Role role;
        private RoleDTO roleDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _roleRepo = new Mock<IRoleRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<RoleController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<Role> pFiller = new Filler<Role>();
            Filler<RoleDTO> pFillerDTO = new Filler<RoleDTO>();
            role = pFiller.Create();
            roleDTO = pFillerDTO.Create();

            // Service under test
            _roleController = new RoleController(_configuration.Object, _mapper, _roleRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getRole = _roleController.Get(role.Id);
            Assert.NotNull(getRole);
            Assert.IsInstanceOf<Role>(getRole);
        }

        [Test]
        public void CreateTests()
        {
            var getRole = _roleController.Create(roleDTO);
            Assert.NotNull(getRole);
            Assert.IsInstanceOf<long>(getRole);
        }

        [Test]
        public void UpdateTests()
        {
            var getRole = _roleController.Update(roleDTO);
            Assert.NotNull(getRole);
            Assert.IsInstanceOf<long>(getRole);
        }

        [Test]
        public void DeleteTests()
        {
            var getRole = _roleController.Delete(roleDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getRole);
        }
    }
}