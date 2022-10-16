using System;
using System.Threading.Tasks;
using AutoMapper;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.DTO.Settings;
using CallCenter.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace CallCenter.WebUI.Controllers.API
{
    /// <summary>
    /// Работа с настройками
    /// </summary>
    [Route("api/settings")]
    public class SettingController : BaseApiController
    {
        private readonly ISettingRepository _repository;
        
        /// <summary>
        /// Работа с настройками
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        public SettingController(ILogger<SettingController> logger, IMapper mapper, ISettingRepository repository) 
            : base(logger, mapper)
        {
            _repository = repository;
        }
        
        
        /// <summary>
        /// Получить текущие настройки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK,
            Type = typeof(SettingGetDto))]
        public async Task<IActionResult> Get()
        {
            try
            {
                Setting data = await _repository.GetAsync();
                var dto = Mapper.Map<SettingGetDto>(data);

                return Ok(dto);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Записать настройки
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(SettingPostDto dto)
        {
            try
            {
                var data = Mapper.Map<Setting>(dto);
                await _repository.InsertAsync(data);
                
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