using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DynamicSunTestTask.Infrastructure.Common.Repository;

namespace DynamicSunTestTask.Infrastructure.Common.Extensions;

public static class IServiceCollectionExtensions
{
    #region static fiels

    private static string IRepositoryName => typeof(IRepository<>).Name;
    private static string RepositoryName => typeof(Repository<>).Name;

    #endregion

    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var repositories = assembly.GetTypes()
            .Where(type => type?.BaseType?.Name == RepositoryName)
            .ToList();

        foreach (var repository in repositories)
        {
            var contract = repository.GetInterfaces()
                .Where(x => x.GetInterface(IRepositoryName) is not null)
                .SingleOrDefault();

            if (contract is null) continue;

            services.AddScoped(contract, repository);
        }

        return services;
    }
}
