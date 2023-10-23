using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using DynamicSunTestTask.Infrastructure.Common.Repository;

namespace DynamicSunTestTask.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IWeatherConditionsRepository : IRepository<WeatherConditions>
{

}
