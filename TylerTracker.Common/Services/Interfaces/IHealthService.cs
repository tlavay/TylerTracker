using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TylerTracker.Common.Models;

namespace TylerTracker.Common.Services.Interfaces
{
    public interface IHealthService
    {
        /// <summary>
        /// Gets all health data from the 6 months from the input date.
        /// </summary>
        /// <param name="date">Start date</param>
        /// <returns>Health Data</returns>
        Task<IEnumerable<Health>> GetPreviousHealthData(DateTime date);
        /// <summary>
        /// Saves distinct health data. This way we dont have to worry about duplicates.
        /// Determines duplicate based on date.
        /// </summary>
        /// <param name="health">Health Data</param>
        /// <returns>Completed Task</returns>
        Task SaveDistinctHealth(Health health);
    }
}
