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
    public class TestAuditController 
    {
        private AuditController _auditController;

        // Service under test
        public Mock<IConfiguration> _configuration { get; set; }
        private Mock<IAuditRepo> _auditRepo;
        private Mock<ILogger<AuditController>> _logger;

        private Audit audit;
        private AuditDTO auditDTO;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Mock
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _auditRepo = new Mock<IAuditRepo>(MockBehavior.Loose);
            _logger = new Mock<ILogger<AuditController>>(MockBehavior.Loose);

            // Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = new Mapper(config);

            // Config
            Filler<Audit> pFiller = new Filler<Audit>();
            Filler<AuditDTO> pFillerDTO = new Filler<AuditDTO>();
            audit = pFiller.Create();
            auditDTO = pFillerDTO.Create();

            // Service under test
            _auditController = new AuditController(_configuration.Object, _mapper, _auditRepo.Object, _logger.Object);
        }

        [Test]
        public void GetByIdTests()
        {
            var getAudit = _auditController.Get(audit.Id);
            Assert.NotNull(getAudit);
            Assert.IsInstanceOf<Audit>(getAudit);
        }

        [Test]
        public void CreateTests()
        {
            var getAudit = _auditController.Create(auditDTO);
            Assert.NotNull(getAudit);
            Assert.IsInstanceOf<long>(getAudit);
        }

        [Test]
        public void UpdateTests()
        {
            var getAudit = _auditController.Update(auditDTO);
            Assert.NotNull(getAudit);
            Assert.IsInstanceOf<long>(getAudit);
        }

        [Test]
        public void DeleteTests()
        {
            var getAudit = _auditController.Delete(auditDTO);
            Assert.ReferenceEquals(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, getAudit);
        }
    }
}