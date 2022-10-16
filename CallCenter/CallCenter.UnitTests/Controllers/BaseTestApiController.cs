using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CallCenter.UnitTests.Controllers
{
    public class BaseTestApiController
    {
        protected void VerifyControllerHasAttribute<T>(ControllerBase controller) where T : Attribute
        {

            //Act
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(T), true);

            //Arrange
            Assert.True(attributes.Any());
        }
        
        protected void VerifyActionHasAttribute<T>(ControllerBase controller, string action) where T : Attribute
        {
            //Act
            var type = controller.GetType();
            var methodInfo = type.GetMethod(action);
            var attributes = methodInfo?.GetCustomAttributes(typeof(T), true);

            //Arrange
            Assert.True(attributes?.Any());
        }
        
        protected void AssertOkResult(IActionResult result)
        {
            Assert.IsType<OkResult>(result);
        }
        
        protected void AssertOkDataResult<T>(IActionResult result)
        {
            var objectResult = (result as OkObjectResult)?.Value;
            
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(objectResult);
            Assert.IsAssignableFrom<T>(objectResult);
        }
        
        protected void AssertBadResult(IActionResult result)
        {
            var objectResult = (result as BadRequestObjectResult)?.Value;
            
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(objectResult);
            Assert.IsAssignableFrom<string>(objectResult);
        }
    }
}