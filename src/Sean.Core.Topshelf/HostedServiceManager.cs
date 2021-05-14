using System;
using System.Configuration;
using System.Reflection;
using Sean.Core.Topshelf.Contracts;
using Sean.Core.Topshelf.Extensions;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Runtime;
using Topshelf.ServiceConfigurators;

#if NETSTANDARD
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
#endif

namespace Sean.Core.Topshelf
{
    /// <summary>
    /// Configure and run a service host using the HostFactory
    /// </summary>
    public class HostedServiceManager
    {
        public IServiceOptions Options => _options;

        private readonly ServiceOptions _options;

#if NETSTANDARD
        internal static IServiceProvider ServiceProvider { get; set; }
        //private readonly IConfiguration _configuration;
#endif

        public HostedServiceManager(Action<ServiceOptions> options = null)
        {
#if NETSTANDARD
            _options = ServiceProvider?.GetService<IOptionsMonitor<ServiceOptions>>()?.CurrentValue ?? new ServiceOptions();
#else
            _options = new ServiceOptions();
#endif

            options?.Invoke(_options);

            #region 读取默认配置
            if (string.IsNullOrWhiteSpace(_options.ServiceName))
            {
#if NETSTANDARD
                //if (_configuration != null)
                //{
                //    _options.ServiceName = _configuration.GetSection($"{nameof(ServiceOptions)}:{nameof(ServiceOptions.ServiceName)}")?.Value;
                //    _options.ServiceDisplayName = _configuration.GetSection($"{nameof(ServiceOptions)}:{nameof(ServiceOptions.ServiceDisplayName)}")?.Value;
                //    _options.ServiceDescription = _configuration.GetSection($"{nameof(ServiceOptions)}:{nameof(ServiceOptions.ServiceDescription)}")?.Value;
                //}
#else
                _options.ServiceName = ConfigurationManager.AppSettings[nameof(ServiceOptions.ServiceName)];
                _options.ServiceDisplayName = ConfigurationManager.AppSettings[nameof(ServiceOptions.ServiceDisplayName)];
                _options.ServiceDescription = ConfigurationManager.AppSettings[nameof(ServiceOptions.ServiceDescription)];
#endif
            }
            if (string.IsNullOrWhiteSpace(_options.ServiceName))
            {
                _options.ServiceName = Assembly.GetCallingAssembly().GetName().Name;
            }
            if (string.IsNullOrWhiteSpace(_options.ServiceDisplayName))
            {
                _options.ServiceDisplayName = _options.ServiceName;
            }
            #endregion
        }

        /// <summary>
        /// Configures and runs a new service host.
        /// </summary>
        /// <param name="configureCallback"></param>
        /// <returns></returns>
        public TopshelfExitCode Run(Action<HostConfigurator, ServiceOptions> configureCallback)
        {
            return HostFactory.Run(x =>
            {
                x.Configure(_options);

                configureCallback?.Invoke(x, _options);
            });
        }

        /// <summary>
        /// Configures and runs a new service host.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configureCallback"></param>
        /// <param name="serviceFactory"></param>
        /// <param name="serviceConfigurator"></param>
        /// <returns></returns>
        public TopshelfExitCode RunService<T>(Action<HostConfigurator, ServiceOptions> configureCallback = null, ServiceFactory<T> serviceFactory = null, Action<ServiceConfigurator<T>> serviceConfigurator = null) where T : class, IHostedService, new()
        {
            return Run((x, options) =>
            {
                x.Service<T>(sc =>
                {
                    sc.ConstructUsing(serviceFactory ?? (settings => new T { Settings = settings }));

                    // the start and stop methods for the service
                    sc.WhenStarted(s => s.Start());
                    sc.WhenStopped(s => s.Stop());

                    serviceConfigurator?.Invoke(sc);
                });

                configureCallback?.Invoke(x, options);
            });
        }

        /// <summary>
        /// Configures and runs a new service host.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configureCallback"></param>
        /// <param name="serviceFactory"></param>
        /// <param name="serviceConfigurator"></param>
        /// <returns></returns>
        public TopshelfExitCode RunCompleteService<T>(Action<HostConfigurator, ServiceOptions> configureCallback = null, ServiceFactory<T> serviceFactory = null, Action<ServiceConfigurator<T>> serviceConfigurator = null) where T : class, ICompleteHostedService, new()
        {
            return RunService(configureCallback, serviceFactory, sc =>
            {
                // optional pause/continue methods if used
                sc.WhenPaused(s => s.Pause());
                sc.WhenContinued(s => s.Continue());

                // optional, when shutdown is supported
                sc.WhenShutdown(s => s.Shutdown());

                serviceConfigurator?.Invoke(sc);
            });
        }
    }
}
