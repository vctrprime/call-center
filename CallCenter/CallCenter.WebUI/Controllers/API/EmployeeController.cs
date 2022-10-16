using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.DTO.Calls;
using CallCenter.DTO.Employees;
using CallCenter.DTO.Settings;
using CallCenter.Entities;
using CallCenter.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace CallCenter.WebUI.Controllers.API
{
    /// <summary>
    /// Работа с сотрудниками
    /// </summary>
    [Route("api/employees")]
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeRepository _repository;
        
        /// <summary>
        /// Работа с сотрудниками
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        public EmployeeController(ILogger<EmployeeController> logger, IMapper mapper, IEmployeeRepository repository) 
            : base(logger, mapper)
        {
            _repository = repository;
        }
        
        
        /// <summary>
        /// Получить сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK,
            Type = typeof(IEnumerable<EmployeeGetDto>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Employee> data = await _repository.GetAsync();
                var dto = Mapper.Map<IEnumerable<EmployeeGetDto>>(data);

                return Ok(dto);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(EmployeePostDto dto)
        {
            try
            {
                var data = Mapper.Map<Employee>(dto);
                await _repository.InsertAsync(data);
                
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(EmployeePutDto dto)
        {
            try
            {
                var data = Mapper.Map<Employee>(dto);
                await _repository.UpdateAsync(data);
                
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int employeeId)
        {
            try
            {
                await _repository.DeleteAsync(employeeId);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        
        
    }
}