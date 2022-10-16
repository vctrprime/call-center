using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CallCenter.WebUI.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

        public BaseApiController(ILogger logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }
    }
}