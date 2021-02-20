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
    public class TestUserController 
    {
        private UserController _userController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<IUserRepo> _userRepo;
        private Mock<ILogger<UserController>> _logger;

        private User user;
        private UserDTO userDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _userRepo = new Mock<IUserRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<UserController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<User> pFiller = new Filler<User>();
            Filler<UserDTO> pFillerDTO = new Filler<UserDTO>();
            user = pFiller.Create();
            userDTO = pFillerDTO.Create();

            // Service under test
            _userController = new UserController(_configuration.Object, _mapper, _userRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getUser = _userController.Get(user.Id);
            Assert.NotNull(getUser);
            Assert.IsInstanceOf<User>(getUser);
        }

        [Test]
        public void CreateTests()
        {
            var getUser = _userController.Create(userDTO);
            Assert.NotNull(getUser);
            Assert.IsInstanceOf<long>(getUser);
        }

        [Test]
        public void UpdateTests()
        {
            var getUser = _userController.Update(userDTO);
            Assert.NotNull(getUser);
            Assert.IsInstanceOf<long>(getUser);
        }

        [Test]
        public void DeleteTests()
        {
            var getUser = _userController.Delete(userDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getUser);
        }
    }
}