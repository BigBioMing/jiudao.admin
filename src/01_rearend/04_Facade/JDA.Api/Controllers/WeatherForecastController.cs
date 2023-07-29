using JDA.Core.Persistence.Repositories.Abstractions;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Runtime;
using JDA.Core.Users.Abstractions;
using JDA.Entity.Entities.Sys;
using Microsoft.AspNetCore.Mvc;

namespace JDA.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        protected readonly IRepository<SysUser> _sysUserRepository;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<SysUser> sysUserRepository)
        {
            _sysUserRepository = sysUserRepository;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public IActionResult Post()
        {
            _sysUserRepository.Insert(new SysUser()
            {
                Id = 4,
                PasswordSalt = string.Empty,
                Account = "ss,",
                Name = "ss",
                Enabled = 0,
                Email = "jid",
                Gender = 0,
                Mobile = "sdfdsgf",
                Password = "sfjiewfjoiewiewi"
            });
            return new JsonResult(new { });
        }
    }
}