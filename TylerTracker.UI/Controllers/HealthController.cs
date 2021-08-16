using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<HealthController> logger;

        private readonly IHealthService healthService;

        public HealthController(
            ILogger<HealthController> logger,
            IHealthService healthService)
        {
            this.logger = logger;
            this.healthService = healthService;
        }

        [HttpPost, Route("create-health")]
        public async Task<IActionResult> CreateHealth(Health health)
        {
            string message;
            try
            {
                await this.healthService.SaveDistinctHealth(health).ConfigureAwait(false);
                message = "Successfully saved health data.";
            }
            catch (Exception ex)
            {
                message = $"An internal error occurred for health data, id: {health.Id}, date: {health.Date}";
                this.logger.LogError(ex, message);
            }

            return Ok(message);
        }

        [HttpGet, Route("get-last-6-months-of-health-data")]
        public async Task<IActionResult> GetLast6Months()
        {
            var results = await this.healthService.GetPreviousHealthData(DateTime.Now.AddDays(-30)).ConfigureAwait(false);
            return Ok(results);
        }
    }
}
