using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net7PostgressCompose.Data;
using Net7PostgressCompose.Models;

namespace Net7PostgressCompose.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {

        private readonly ILogger<DriversController> _logger;
        private readonly ApiDbContext _context;

        public DriversController(ApiDbContext context, ILogger<DriversController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetDrivers")]
        public async Task<IActionResult> Get()
        {
            var driver1 = new Driver()
            {
                DriverNumber = 1,
                Name = "Bob"
            };
            try
            {
                _context.Drivers.Add(driver1);
                var result = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            

            var drivers = await _context.Drivers.ToListAsync();
            return Ok(drivers);
        }

        [HttpPost]
        public Task Post(Driver driver)
        {
            _context.Drivers.Add(driver);
            return Task.CompletedTask;
        }
    }
}
