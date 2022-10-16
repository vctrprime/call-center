using System;
using CallCenter.Entities;
using CallCenter.Entities.Enums;
using CallCenter.Services.Concrete;
using Xunit;

namespace CallCenter.UnitTests.Services
{
    public class CallConstraintServiceTest
    {
        [Theory]
        [InlineData(EmployeePosition.Operator, 10, 20, -5, true)]
        [InlineData(EmployeePosition.Manager, 10, 20, -5, false)]
        [InlineData(EmployeePosition.Director, 10, 20, -5, false)]
        [InlineData(EmployeePosition.Manager, 10, 20, -15, true)]
        [InlineData(EmployeePosition.Director, 10, 20, -15, false)]
        [InlineData(EmployeePosition.Director, 10, 20, -25, true)]
        [InlineData(EmployeePosition.Operator, 10, 20, -25, true)]
        [InlineData(EmployeePosition.Manager, 10, 20, -25, true)]
        public void EmployeeCanTakeCallReturnRightResult(EmployeePosition position,
            int timeManager, int timeDirector, int timeBackCreated, bool expected)
        {
            var employee = new Employee
            {
                Position = position
            };
            var call = new Call
            {
                CreatedDate = DateTime.Now.AddSeconds(timeBackCreated)
            };
            var setting = new Setting
            {
                TimeManager = timeManager,
                TimeDirector = timeDirector
            };
            
            var service = new CallConstraintService();
            var result = service.EmployeeCanTakeCall(employee, call, setting);

            Assert.Equal(expected, result);
        }
    }
}