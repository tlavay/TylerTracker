using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TylerTracker.Common.Repositories
{
    public interface ITylerTrackerRepository
    {
        Task CreateItemAsync<T>(T item);
        /// <summary>
        /// Only creates new item if query returns false.
        /// </summary>
        /// <typeparam name="T">Any valid object</typeparam>
        /// <param name="item">Input</param>
        /// <param name="queryDefinition">The query definition.</param>
        /// <returns>Task</returns>
        Task CreateDistinctItemAsync<T>(T item, QueryDefinition queryDefinition);
        Task<List<T>> Query<T>(QueryDefinition queryDefinition);
        Task<T> QuerySingle<T>(QueryDefinition queryDefinition);
    }
}
