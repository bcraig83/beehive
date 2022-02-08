using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Beehive.Api2.Test.Utilities;

public static class SwapDependencyExtensions
{
    public static void Swap<TService>(this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory)
    {
        if (services.Any(x => x.ServiceType == typeof(TService)))
        {
            var serviceDescriptors = services
                .Where(x => x.ServiceType == typeof(TService)).ToList();

            foreach (var serviceDescriptor in serviceDescriptors)
            {
                services.Remove(serviceDescriptor);
            }
        }

        services.AddSingleton(typeof(TService), sp => implementationFactory(sp));
    }
}