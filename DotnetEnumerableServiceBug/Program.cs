using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetEnumerableServiceBug
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<ICustomService, CustomService1>();
            services.AddSingleton<ICustomService, CustomService2>();
            services.AddSingleton<ICustomService, CustomService3>();
            services.AddSingleton<ICustomService, CustomService4>();
            services.AddSingleton<ICustomService, CustomService5>();
            var serviceProvider = services.BuildServiceProvider(
                new ServiceProviderOptions
                {
                    ValidateOnBuild = true
                }
            );
            var customServices = serviceProvider
                .GetService<IEnumerable<ICustomService>>()
                .ToArray();
            if (customServices[1] is CustomService5 && customServices[4] is CustomService5)
            {
                throw new Exception(
                    "This is not right"
                );
            }
        }
    }
}