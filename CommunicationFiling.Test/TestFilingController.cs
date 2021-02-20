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
    public class TestFilingController 
    {
        private FilingController _filingController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<IFilingRepo> _filingRepo;
        private Mock<ILogger<FilingController>> _logger;

        private Filing filing;
        private FilingDTO filingDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _filingRepo = new Mock<IFilingRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<FilingController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<Filing> pFiller = new Filler<Filing>();
            Filler<FilingDTO> pFillerDTO = new Filler<FilingDTO>();
            filing = pFiller.Create();
            filingDTO = pFillerDTO.Create();

            // Service under test
            _filingController = new FilingController(_configuration.Object, _mapper, _filingRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getFiling = _filingController.Get(filing.Id);
            Assert.NotNull(getFiling);
            Assert.IsInstanceOf<Filing>(getFiling);
        }

        [Test]
        public void CreateTests()
        {
            var getFiling = _filingController.Create(filingDTO);
            Assert.NotNull(getFiling);
            Assert.IsInstanceOf<long>(getFiling);
        }

        [Test]
        public void UpdateTests()
        {
            var getFiling = _filingController.Update(filingDTO);
            Assert.NotNull(getFiling);
            Assert.IsInstanceOf<long>(getFiling);
        }

        [Test]
        public void DeleteTests()
        {
            var getFiling = _filingController.Delete(filingDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getFiling);
        }
    }
}