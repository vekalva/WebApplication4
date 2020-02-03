using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Persistence.Config;
using Weather.Persistence.Models;
using Weather.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSubstitute;
using Serilog;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Weather.Test.Persistence
{
    public class RepositoryTest 
    {
        private DbContextOptions<WeatherDbContext> _contextOptions;
        private City _testData;    
        private WeatherDbContext _appContext;
        private IOptions<DbContextSettings> _settings;
        private IDbContextFactory _dbContextFactory;  
        private Repository<City> _subject;
        private City _result;

        public RepositoryTest()
        {
            _testData = new City
            {
                Id = "26216",
                Name = "Melbourne",
                CountryId = "AU",
                AccessedDate = new DateTime(2018, 12, 29, 10, 1, 2)
            };

        }
    }
    [Fact]
    public void CreateCityShouldThrowException()
    {
        this.Given(x => GivenADatabase("TestDb"))
            .Given(x => GivenTheDatabaseHasACity(_testData))
            .When(x => WhenCreateSameIdIsCalledWithTheCityAsync(_testData))
            .Then(x => ThenItShouldBeSuccessful())
            .BDDfy();
    }
    private void GivenTheDatabaseHasACity(City city)
    {
        _appContext.Cities.Add(city);
        _appContext.SaveChanges();
    }
    private async Task WhenCreateSameIdIsCalledWithTheCityAsync(City city)
    {
        await Assert.ThrowsAsync<ArgumentException>(async () => await _subject.AddEntity(city));
    }
    private void ThenItShouldBeSuccessful()
    { }
}