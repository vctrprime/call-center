using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.DTO.Calls;
using CallCenter.Entities;
using CallCenter.MappingProfiles;
using CallCenter.WebUI.Controllers.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Swashbuckle.AspNetCore.Annotations;
using Xunit;

namespace CallCenter.UnitTests.Controllers
{
    public class CallControllerTest : BaseTestApiController
    {
        private readonly Mock<ICallRepository> _mockRepository;
        private readonly ILogger<CallController> _logger;
        private readonly IMapper _mockMapper;
        private readonly IEnumerable<Call> _calls;

        
        public CallControllerTest()
        {
            _mockRepository = new Mock<ICallRepository>();
            _logger = Mock.Of<ILogger<CallController>>();
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new CallProfile()); });
            _mockMapper = mockMapper.CreateMapper();
            _calls = new List<Call>();
        }
        
        [Fact]
        public void VerifyControllerHasApiControllerAttribute()
        {
            //Arrange
            var controller = new CallController(_logger, _mockMapper, _mockRepository.Object, null);

            base.VerifyControllerHasAttribute<ApiControllerAttribute>(controller);
        }

        [Fact]
        public void VerifyControllerHasRouteAttribute()
        {
            //Arrange
            var controller = new CallController(_logger, _mockMapper, _mockRepository.Object, null);

            base.VerifyControllerHasAttribute<RouteAttribute>(controller);
        }


        #region get

        [Fact]
        public void VerifyGetHasHttpGetAttribute()
        {
            //Arrange
            var controller = new CallController(_logger, _mockMapper, _mockRepository.Object, null);

            base.VerifyActionHasAttribute<HttpGetAttribute>(controller, "Get");
        }

        [Fact]
        public void VerifyGetHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new CallController(_logger, _mockMapper, _mockRepository.Object, null);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Get");
        }

        [Fact]
        public async Task GetReturnsOkResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.GetAsync())
                .ReturnsAsync(_calls);

            var controller = new  CallController(_logger, _mockMapper, _mockRepository.Object, null);

            // Act
            var result = await controller.Get();

            //Assert
            AssertOkDataResult<IEnumerable<CallGetDto>>(result);
        }

        [Fact]
        public async Task GetReturnsBadResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.GetAsync())
                .Throws(new Exception("test"));
            var controller = new CallController(_logger, _mockMapper, _mockRepository.Object, null);

            // Act
            var result = await controller.Get();

            //Assert
            AssertBadResult(result);
        }


        #endregion

        #region post

        [Fact]
        public void VerifyPostHasHttpPostAttribute()
        {
            //Arrange
            var controller = new  CallController(_logger, _mockMapper, _mockRepository.Object, null);

            base.VerifyActionHasAttribute<HttpPostAttribute>(controller, "Post");
        }

        [Fact]
        public void VerifyPostHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new CallController(_logger, _mockMapper, _mockRepository.Object, null);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Post");
        }

        [Fact]
        public async Task PostReturnsOkResult()
        {
            // Arrange
            var data = new Call();
            var dto = new CallPostDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Call>(dto))
                .Returns(data);
            _mockRepository.Setup(repository => repository.InsertAsync(data))
                .Verifiable();

            var controller = new CallController(_logger, putMockMapper.Object, _mockRepository.Object, null);

            // Act
            var result = await controller.Post(dto);

            //Assert
            AssertOkResult(result);
        }

        [Fact]
        public async Task PostReturnsBadResult()
        {
            // Arrange
            var data = new Call();
            var dto = new CallPostDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Call>(dto))
                .Returns(data);
            _mockRepository.Setup(repository => repository.InsertAsync(data))
                .Throws(new Exception("test"));

            var controller = new CallController(_logger, putMockMapper.Object, _mockRepository.Object, null);

            // Act
            var result = await controller.Post(dto);

            //Assert
            AssertBadResult(result);
        }


        #endregion
       
    }
}