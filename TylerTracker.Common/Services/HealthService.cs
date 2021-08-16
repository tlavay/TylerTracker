using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TylerTracker.Common.Models;
using TylerTracker.Common.Repositories;
using TylerTracker.Common.Services.Interfaces;

namespace TylerTracker.Common.Services
{
    public sealed class HealthService : IHealthService
    {
        private readonly ITylerTrackerRepository tylerTrackerRepository;

        public HealthService(ITylerTrackerRepository tylerTrackerRepository)
        {
            this.tylerTrackerRepository = tylerTrackerRepository;
        }
        public async Task<IEnumerable<Health>> GetPreviousHealthData(DateTime date)
        {
            var queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.date > @date").WithParameter("@date", date);
            var healths = await this.tylerTrackerRepository.Query<Health>(queryDefinition).ConfigureAwait(false);
            return healths.OrderBy(h => h.Date);
        }

        public async Task SaveDistinctHealth(Health health)
        {
            //Update to distinct
            await this.tylerTrackerRepository.CreateItemAsync(health).ConfigureAwait(false);
        }
    }
}
