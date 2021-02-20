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
    public class TestRoleActionController 
    {
        private RoleActionController _roleActionController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<IRoleActionRepo> _roleActionRepo;
        private Mock<ILogger<RoleActionController>> _logger;

        private RoleAction roleAction;
        private RoleActionDTO roleActionDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _roleActionRepo = new Mock<IRoleActionRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<RoleActionController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<RoleAction> pFiller = new Filler<RoleAction>();
            Filler<RoleActionDTO> pFillerDTO = new Filler<RoleActionDTO>();
            roleAction = pFiller.Create();
            roleActionDTO = pFillerDTO.Create();

            // Service under test
            _roleActionController = new RoleActionController(_configuration.Object, _mapper, _roleActionRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getRoleAction = _roleActionController.Get(roleAction.Id);
            Assert.NotNull(getRoleAction);
            Assert.IsInstanceOf<RoleAction>(getRoleAction);
        }

        [Test]
        public void CreateTests()
        {
            var getRoleAction = _roleActionController.Create(roleActionDTO);
            Assert.NotNull(getRoleAction);
            Assert.IsInstanceOf<long>(getRoleAction);
        }

        [Test]
        public void UpdateTests()
        {
            var getRoleAction = _roleActionController.Update(roleActionDTO);
            Assert.NotNull(getRoleAction);
            Assert.IsInstanceOf<long>(getRoleAction);
        }

        [Test]
        public void DeleteTests()
        {
            var getRoleAction = _roleActionController.Delete(roleActionDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getRoleAction);
        }
    }
}