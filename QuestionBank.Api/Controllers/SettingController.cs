using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuestionBank.Api.CustomAttribute;
using QuestionBank.Common;
using QuestionBank.Service.Interface;


namespace QuestionBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISettingService _settingService;
        private readonly ILogger<SettingController> _logger;

        private readonly AppSettings _appSettings;

        // GET
        public SettingController(IMapper mapper, ISettingService settingService, ILogger<SettingController> logger, IOptions<AppSettings> options)
        {
            _mapper = mapper;
            _settingService = settingService;
            _logger = logger;
            _appSettings = options.Value;
        }

        [HttpGet("Roles")]
        [Authorize]
        public async Task<IActionResult> GetRoles()
        {
            _logger.LogDebug("Executing setting controller api model executed");
            var result = await _settingService.GetRoles();
            return Ok(result);
        }
    }
}
