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
    public class TestActionController 
    {
        private ActionController _actionController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<IActionRepo> _actionRepo;
        private Mock<ILogger<ActionController>> _logger;

        private Action action;
        private ActionDTO actionDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _actionRepo = new Mock<IActionRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<ActionController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<Action> pFiller = new Filler<Action>();
            Filler<ActionDTO> pFillerDTO = new Filler<ActionDTO>();
            action = pFiller.Create();
            actionDTO = pFillerDTO.Create();

            // Service under test
            _actionController = new ActionController(_configuration.Object, _mapper, _actionRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getAction = _actionController.Get(action.Id);
            Assert.NotNull(getAction);
            Assert.IsInstanceOf<Action>(getAction);
        }

        [Test]
        public void CreateTests()
        {
            var getAction = _actionController.Create(actionDTO);
            Assert.NotNull(getAction);
            Assert.IsInstanceOf<long>(getAction);
        }

        [Test]
        public void UpdateTests()
        {
            var getAction = _actionController.Update(actionDTO);
            Assert.NotNull(getAction);
            Assert.IsInstanceOf<long>(getAction);
        }

        [Test]
        public void DeleteTests()
        {
            var getAction = _actionController.Delete(actionDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getAction);
        }
    }
}