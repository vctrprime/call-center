using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.DTO.Employees;
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
    public class EmployeeControllerTest : BaseTestApiController
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mockMapper;
        private readonly IEnumerable<Employee> _employees;

        
        public EmployeeControllerTest()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _logger = Mock.Of<ILogger<EmployeeController>>();
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new EmployeeProfile()); });
            _mockMapper = mockMapper.CreateMapper();
            _employees = new List<Employee>();
        }
        
        [Fact]
        public void VerifyControllerHasApiControllerAttribute()
        {
            //Arrange
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyControllerHasAttribute<ApiControllerAttribute>(controller);
        }

        [Fact]
        public void VerifyControllerHasRouteAttribute()
        {
            //Arrange
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyControllerHasAttribute<RouteAttribute>(controller);
        }


        #region get

        [Fact]
        public void VerifyGetHasHttpGetAttribute()
        {
            //Arrange
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<HttpGetAttribute>(controller, "Get");
        }

        [Fact]
        public void VerifyGetHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Get");
        }

        [Fact]
        public async Task GetReturnsOkResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.GetAsync())
                .ReturnsAsync(_employees);

            var controller = new  EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            // Act
            var result = await controller.Get();

            //Assert
            AssertOkDataResult<IEnumerable<EmployeeGetDto>>(result);
        }

        [Fact]
        public async Task GetReturnsBadResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.GetAsync())
                .Throws(new Exception("test"));
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

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
            var controller = new  EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<HttpPostAttribute>(controller, "Post");
        }

        [Fact]
        public void VerifyPostHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Post");
        }

        [Fact]
        public async Task PostReturnsOkResult()
        {
            // Arrange
            var data = new Employee();
            var dto = new EmployeePostDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Employee>(dto))
                .Returns(data);
            _mockRepository.Setup(repository => repository.InsertAsync(data))
                .Verifiable();

            var controller = new EmployeeController(_logger, putMockMapper.Object, _mockRepository.Object);

            // Act
            var result = await controller.Post(dto);

            //Assert
            AssertOkResult(result);
        }

        [Fact]
        public async Task PostReturnsBadResult()
        {
            // Arrange
            var data = new Employee();
            var dto = new EmployeePostDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Employee>(dto))
                .Returns(data);
            _mockRepository.Setup(repository => repository.InsertAsync(data))
                .Throws(new Exception("test"));

            var controller = new EmployeeController(_logger, putMockMapper.Object, _mockRepository.Object);

            // Act
            var result = await controller.Post(dto);

            //Assert
            AssertBadResult(result);
        }


        #endregion
        
        #region put

        [Fact]
        public void VerifyPutHasHttpPutAttribute()
        {
            //Arrange
            var controller = new  EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<HttpPutAttribute>(controller, "Put");
        }

        [Fact]
        public void VerifyPutHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Put");
        }

        [Fact]
        public async Task PutReturnsOkResult()
        {
            // Arrange
            var data = new Employee();
            var dto = new EmployeePutDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Employee>(dto))
                .Returns(data);
            _mockRepository.Setup(repository => repository.UpdateAsync(data))
                .Verifiable();

            var controller = new EmployeeController(_logger, putMockMapper.Object, _mockRepository.Object);

            // Act
            var result = await controller.Put(dto);

            //Assert
            AssertOkResult(result);
        }

        [Fact]
        public async Task PutReturnsBadResult()
        {
            // Arrange
            var data = new Employee();
            var dto = new EmployeePutDto();
            var putMockMapper = new Mock<IMapper>();
            putMockMapper.Setup(x => x.Map<Employee>(dto))
                .Returns(data);
            _mockRepository.Setup(repository => repository.UpdateAsync(data))
                .Throws(new Exception("test"));

            var controller = new EmployeeController(_logger, putMockMapper.Object, _mockRepository.Object);

            // Act
            var result = await controller.Put(dto);

            //Assert
            AssertBadResult(result);
        }


        #endregion
        
        #region delete

        [Fact]
        public void VerifyDeleteHasHttpDeleteAttribute()
        {
            //Arrange
            var controller = new  EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<HttpDeleteAttribute>(controller, "Delete");
        }

        [Fact]
        public void VerifyDeleteHasSwaggerResponseAttribute()
        {
            //Arrange
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            base.VerifyActionHasAttribute<SwaggerResponseAttribute>(controller, "Delete");
        }

        [Fact]
        public async Task DeleteReturnsOkResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.DeleteAsync(1))
                .Verifiable();
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            // Act
            var result = await controller.Delete(1);

            //Assert
            AssertOkResult(result);
        }

        [Fact]
        public async Task DeleteReturnsBadResult()
        {
            // Arrange
            _mockRepository.Setup(repository => repository.DeleteAsync(1))
                .Throws(new Exception("test"));
            var controller = new EmployeeController(_logger, _mockMapper, _mockRepository.Object);

            // Act
            var result = await controller.Delete(1);

            //Assert
            AssertBadResult(result);
        }


        #endregion
    }
}