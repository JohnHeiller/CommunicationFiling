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
    public class TestUserRoleController 
    {
        private UserRoleController _userRoleController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<IUserRoleRepo> _userRoleRepo;
        private Mock<ILogger<UserRoleController>> _logger;

        private UserRole userRole;
        private UserRoleDTO userRoleDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _userRoleRepo = new Mock<IUserRoleRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<UserRoleController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<UserRole> pFiller = new Filler<UserRole>();
            Filler<UserRoleDTO> pFillerDTO = new Filler<UserRoleDTO>();
            userRole = pFiller.Create();
            userRoleDTO = pFillerDTO.Create();

            // Service under test
            _userRoleController = new UserRoleController(_configuration.Object, _mapper, _userRoleRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getUserRole = _userRoleController.Get(userRole.Id);
            Assert.NotNull(getUserRole);
            Assert.IsInstanceOf<UserRole>(getUserRole);
        }

        [Test]
        public void CreateTests()
        {
            var getUserRole = _userRoleController.Create(userRoleDTO);
            Assert.NotNull(getUserRole);
            Assert.IsInstanceOf<long>(getUserRole);
        }

        [Test]
        public void UpdateTests()
        {
            var getUserRole = _userRoleController.Update(userRoleDTO);
            Assert.NotNull(getUserRole);
            Assert.IsInstanceOf<long>(getUserRole);
        }

        [Test]
        public void DeleteTests()
        {
            var getUserRole = _userRoleController.Delete(userRoleDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getUserRole);
        }
    }
}