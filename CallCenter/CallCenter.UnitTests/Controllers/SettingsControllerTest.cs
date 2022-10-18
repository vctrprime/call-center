using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.DTO.Settings;
using CallCenter.Entities;
using CallCenter.MappingProfiles;
using CallCenter.WebUI.Controllers.API;
using CallCenter.WebUI.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Swashbuckle.AspNetCore.Annotations;
using Xunit;

namespace CallCenter.UnitTests.Controllers
{
    public class SettingControllerTest : BaseTestApiController
    {
        private readonly Mock<ISettingRepository> _mockRepository;
        private readonly ILogger<SettingController> _logger;
        private readonly IMapper _mockMapper;
        private readonly Setting _setting;

        
        public SettingControllerTest()
        {
            _mockRepository = new Mock<ISettingRepository>();
            _logger = Mock.Of<ILogger<SettingController>>();
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new SettingProfile()); });
            _mockMapper = mockMapper.CreateMapper();
            _setting = new Setting();
        }
        
        [Fact]
        public void VerifyControllerHasApiControllerAttribute()
        {
            //Arrange
            var controller = new SettingController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyControllerHasAttribute<ApiControllerAttribute>(controller);
        }

        [Fact]
        public void VerifyControllerHasRouteAttribute()
        {
            //Arrange
            var controller = new SettingController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyControllerHasAttribute<RouteAttribute>(controller);
        }


        #region get

        [Fact]
        public void VerifyGetHasHttpGetAttribute()
        {
            //Arrange
            var controller = new SettingController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<HttpGetAttribute>(controller, "Get");
        }

        [Fact]
        public void VerifyGetHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new SettingController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Get");
        }

        [Fact]
        public async Task GetReturnsOkResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.GetAsync())
                .ReturnsAsync(_setting);

            var controller = new  SettingController(_logger, _mockMapper, _mockRepository.Object);

            // Act
            var result = await controller.Get();

            //Assert
            AssertOkDataResult<SettingGetDto>(result);
        }

        [Fact]
        public async Task GetReturnsBadResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.GetAsync())
                .Throws(new Exception("test"));
            var controller = new SettingController(_logger, _mockMapper, _mockRepository.Object);

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
            var controller = new  SettingController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<HttpPostAttribute>(controller, "Post");
        }

        [Fact]
        public void VerifyPostHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new SettingController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Post");
        }
        
        [Fact]
        public void VerifyPostHasSettingLimitAttribute()
        {
            //Arrange
            var controller = new  SettingController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<SettingLimitAttribute>(controller, "Post");
        }

        [Fact]
        public async Task PostReturnsOkResult()
        {
            // Arrange
            var dto = new SettingPostDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Setting>(dto))
                .Returns(_setting);
            _mockRepository.Setup(repository => repository.InsertAsync(_setting))
                .Verifiable();

            var controller = new SettingController(_logger, putMockMapper.Object, _mockRepository.Object);

            // Act
            var result = await controller.Post(dto);

            //Assert
            AssertOkResult(result);
        }

        [Fact]
        public async Task PostReturnsBadResult()
        {
            // Arrange
            var dto = new SettingPostDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Setting>(dto))
                .Returns(_setting);
            _mockRepository.Setup(repository => repository.InsertAsync(_setting))
                .Throws(new Exception("test"));

            var controller = new SettingController(_logger, putMockMapper.Object, _mockRepository.Object);

            // Act
            var result = await controller.Post(dto);

            //Assert
            AssertBadResult(result);
        }


        #endregion
        
    }
}