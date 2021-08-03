using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TylerTracker.Common.IntegrationTests.Helpers;
using TylerTracker.Common.Services.Interfaces;
using Xunit;

namespace TylerTracker.Common.IntegrationTests.Services
{
    public class HealthServiceTests
    {

        private readonly IHealthService healthService;
        public HealthServiceTests()
        {
            this.healthService = TestHelper.GetService<IHealthService>();
        }

        [Fact]
        public async Task GetPrevious6Months_WithCurrentDate_ShouldReturnHealthDatas()
        {
            //Act
            var healths = await this.healthService.GetPrevious6Months(DateTime.Now).ConfigureAwait(false);

            //Assert
            healths.Any();
        }
    }
}
