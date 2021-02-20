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
    public class TestCorrespondenceTypeController 
    {
        private CorrespondenceTypeController _correspondenceTypeController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<ICorrespondenceTypeRepo> _correspondenceTypeRepo;
        private Mock<ILogger<CorrespondenceTypeController>> _logger;

        private CorrespondenceType correspondenceType;
        private CorrespondenceTypeDTO correspondenceTypeDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _correspondenceTypeRepo = new Mock<ICorrespondenceTypeRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<CorrespondenceTypeController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<CorrespondenceType> pFiller = new Filler<CorrespondenceType>();
            Filler<CorrespondenceTypeDTO> pFillerDTO = new Filler<CorrespondenceTypeDTO>();
            correspondenceType = pFiller.Create();
            correspondenceTypeDTO = pFillerDTO.Create();

            // Service under test
            _correspondenceTypeController = new CorrespondenceTypeController(_configuration.Object, _mapper, _correspondenceTypeRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getCorrespondenceType = _correspondenceTypeController.Get(correspondenceType.Id);
            Assert.NotNull(getCorrespondenceType);
            Assert.IsInstanceOf<CorrespondenceType>(getCorrespondenceType);
        }

        [Test]
        public void CreateTests()
        {
            var getCorrespondenceType = _correspondenceTypeController.Create(correspondenceTypeDTO);
            Assert.NotNull(getCorrespondenceType);
            Assert.IsInstanceOf<long>(getCorrespondenceType);
        }

        [Test]
        public void UpdateTests()
        {
            var getCorrespondenceType = _correspondenceTypeController.Update(correspondenceTypeDTO);
            Assert.NotNull(getCorrespondenceType);
            Assert.IsInstanceOf<long>(getCorrespondenceType);
        }

        [Test]
        public void DeleteTests()
        {
            var getCorrespondenceType = _correspondenceTypeController.Delete(correspondenceTypeDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getCorrespondenceType);
        }
    }
}