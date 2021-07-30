using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TylerTracker.Common.Models;
using TylerTracker.Common.Services.Interfaces;

namespace TylerTracker.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService healthService;

        public HealthController(IHealthService healthService)
        {
            this.healthService = healthService;
        }

        [HttpPost, Route("create-health")]
        public async Task<IActionResult> CreateHealth(Health health)
        {
            //update to create distinct
            await this.healthService.SaveDistinctHealth(health);
            return Ok();
        }

        [HttpGet, Route("get-last-6-months")]
        public async Task<IActionResult> GetLast6Months()
        {
            return Ok(await this.healthService.GetPrevious6Months(DateTime.Now).ConfigureAwait(false));
        }
    }
}
