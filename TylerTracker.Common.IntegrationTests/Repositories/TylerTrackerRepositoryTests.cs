using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using TylerTracker.Common.IntegrationTests.Helpers;
using TylerTracker.Common.Models;
using TylerTracker.Common.Repositories;
using Xunit;

namespace TylerTracker.Common.IntegrationTests.Repositories
{
    public class TylerTrackerRepositoryTests
    {
        private readonly ITylerTrackerRepository tylerTrackerRepo;
        public TylerTrackerRepositoryTests()
        {
            var serviceProvider = TestHelper.GetServiceProvider();
            this.tylerTrackerRepo = serviceProvider.GetService<ITylerTrackerRepository>();
        }

        [Fact]
        public async Task CreateItemAsync_WithValidHealthObject_ShouldCreate()
        {
            //Arrange
            var expectedHealth = new Health(250, 135, 80, new Measurements(17.5, 38.7, 12, 42));
            expectedHealth.Date = DateTime.Now;

            //Act
            await this.tylerTrackerRepo.CreateItemAsync(expectedHealth);

            var queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", expectedHealth.Id);
            var actualHealth = await this.tylerTrackerRepo.QuerySingle<Health>(queryDefinition);

            //Assert
            expectedHealth.Should().BeEquivalentTo(actualHealth);
        }

        //[Fact]
        //public async Task Temp()
        //{
        //    var startDate = DateTime.Now.AddMonths(-6);
        //    var endDate = DateTime.Now;
        //    var currentDate = startDate;
        //    var random = new Random();
        //    while (endDate > currentDate)
        //    {
        //        var weight = double.Parse($"2{random.Next(2, 6)}{random.Next(0, 10)}.{random.Next(0, 10)}");
        //        var health = new Health(currentDate, weight);
        //        await this.tylerTrackerRepo.CreateItemAsync(health);
        //        currentDate = currentDate.AddDays(1);
        //    }
        //}
    }
}
