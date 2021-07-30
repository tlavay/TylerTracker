using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TylerTracker.Common.Exceptions;

namespace TylerTracker.Common.Repositories
{
    public sealed class TylerTrackerRepository : ITylerTrackerRepository
    {
        private readonly Container container;
        public TylerTrackerRepository(Container container)
        {
            this.container = container;
        }

        public async Task CreateDistinctItemAsync<T>(T item, QueryDefinition queryDefinition)
        {
            if (await IsDistinct<T>(queryDefinition).ConfigureAwait(false))
            {
                await CreateItemAsync<T>(item).ConfigureAwait(false);
            }
        }

        public async Task CreateItemAsync<T>(T item)
        {
            await this.container.CreateItemAsync(item).ConfigureAwait(false);
        }

        public async Task<List<T>> Query<T>(QueryDefinition queryDefinition)
        {
            return await QueryInternal<T>(queryDefinition).ConfigureAwait(false);
        }

        public async Task<T> QuerySingle<T>(QueryDefinition queryDefinition)
        {
            var items = await QueryInternal<T>(queryDefinition).ConfigureAwait(false);
            if (!items.Any())
            {
                throw new TylerTrackerCosmosException($"The query result did not return any items. Query: {queryDefinition.QueryText}");
            }
            else if (items.Count > 1)
            {
                throw new TylerTrackerCosmosException($"The query returned more than 1 item. Query: {queryDefinition.QueryText}");
            }

            return items[0];
        }

        private async Task<bool> IsDistinct<T>(QueryDefinition queryDefinition)
        {
            var items = await QueryInternal<T>(queryDefinition);
            return items.Any();
        }

        private async Task<List<T>> QueryInternal<T>(QueryDefinition queryDefinition)
        {
            var items = new List<T>();
            var feedIterator = this.container.GetItemQueryIterator<T>(queryDefinition, null);

            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync().ConfigureAwait(false))
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
