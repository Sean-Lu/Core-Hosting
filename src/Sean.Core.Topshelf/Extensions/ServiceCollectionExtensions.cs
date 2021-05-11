#if NETSTANDARD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sean.Core.Topshelf.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServiceOptions(this IServiceCollection services, Action<ServiceOptions> configureOptions = null, IConfiguration configuration = null)
        {
            services.Configure<ServiceOptions>((configuration ?? services.BuildServiceProvider().GetService<IConfiguration>()).GetSection(nameof(ServiceOptions)));
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }

            HostingServiceManager.ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
#endif