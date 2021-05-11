using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sean.Core.Hosting
{
    public class HostBuilderHelper
    {
        public static IHostBuilder CreateDefaultBuilder() =>
            Host.CreateDefaultBuilder();

        public static IHostBuilder CreateDefaultBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);

        public static IHostBuilder CreateDefaultBuilder(string[] args, Action<IServiceCollection> configureServices) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                }).ConfigureServices(services =>
                {
                    configureServices?.Invoke(services);
                });

        public static IHostBuilder CreateDefaultBuilder<T>(string[] args, Action<IServiceCollection> configureServices) where T : class, IHostedService =>
            CreateDefaultBuilder(args, services =>
            {
                //services.AddSingleton(typeof(IHostedService), typeof(T));
                services.AddHostedService<T>();

                configureServices?.Invoke(services);
            });
    }
}
