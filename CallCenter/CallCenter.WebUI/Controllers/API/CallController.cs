using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.DTO.Calls;
using CallCenter.DTO.Settings;
using CallCenter.Entities;
using CallCenter.Services.Abstract;
using CallCenter.WebUI.Infrastructure.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace CallCenter.WebUI.Controllers.API
{
    /// <summary>
    /// Работа с заявками
    /// </summary>
    [Route("api/calls")]
    public class CallController : BaseApiController
    {
        private readonly ICallRepository _repository;
        private readonly IHubContext<CallHub, ITypedHubClient> _hub;
        
        /// <summary>
        /// Работа с настройками
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        public CallController(ILogger<CallController> logger, IMapper mapper, ICallRepository repository, IHubContext<CallHub, ITypedHubClient> hub) 
            : base(logger, mapper)
        {
            _repository = repository;
            _hub = hub;
        }
        
        
        /// <summary>
        /// Получить заявки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CallGetDto>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Call> data = await _repository.GetAsync();
                var dto = Mapper.Map<IEnumerable<CallGetDto>>(data);

                return Ok(dto);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Создать заявку
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(CallPostDto dto)
        {
            try
            {
                var data = Mapper.Map<Call>(dto);
                await _repository.InsertAsync(data);
                await _hub.Clients.All.SendMessageToClient();
                
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