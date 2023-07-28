using JDA.Core.Runtime;
using JDA.Core.Users.Abstractions;
using JDA.Entity.Contexts;
using JDA.Entity.Entities.Sys;
using Microsoft.AspNetCore.Mvc;

namespace JDA.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        protected readonly JDADbContext _dbContext;
        protected readonly ICurrentRunningContext _currentRunningContext;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, JDADbContext dbContext, ICurrentRunningContext currentRunningContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _currentRunningContext = currentRunningContext;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public IActionResult Post()
        {
            var systemRunEvnInfo = EnvironmentInfo.GetSystemRunEvnInfo();
            var systemPlatformInfo = EnvironmentInfo.GetSystemPlatformInfo();
            var applicationRunInfo = EnvironmentInfo.GetApplicationRunInfo();
            var environmentVariables = EnvironmentInfo.GetEnvironmentVariables();
            _dbContext.Set<SysUser>().Add(new SysUser()
            {
                Id = 2,
                CreateSource = string.Empty,
                CreateDate = DateTime.Now,
                CreateId = 0,
                UpdateSource = string.Empty,
                UpdateDate = DateTime.Now,
                UpdateId = 0,
                PasswordSalt = string.Empty,
                Account = "ss,",
                Name = "ss",
                IsDeleted = 0,
                Enabled = 0,
                Email = "jid",
                Gender = 0,
                Mobile = "sdfdsgf",
                Password = "sfjiewfjoiewiewi"
            });
            _dbContext.SaveChanges();
            return new JsonResult(new { });
        }
    }
}