using JHLA.Entity.Contexts;
using JHLA.Entity.Entities.Sys;
using Microsoft.AspNetCore.Mvc;

namespace JHLA.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        protected readonly JHLADbContext _dbContext;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, JHLADbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public IActionResult Post()
        {
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