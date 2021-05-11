using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sean.Core.Ioc;
using Sean.Core.Topshelf;
using Sean.Core.Topshelf.Extensions;
using Sean.Utility.Contracts;
using Sean.Utility.Extensions;
using Sean.Utility.Impls.Log;
using Topshelf;

namespace Demo.TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceManager.ConfigureServices(services =>
            {
                services.AddTransient(typeof(ISimpleLogger<>), typeof(SimpleLocalLogger<>));
                services.ConfigureServiceOptions();// 使用默认配置：appsettings.json
            });

            SimpleLocalLoggerBase.DateTimeFormat = time => time.ToLongDateTime();

            var logger = ServiceManager.GetService<ISimpleLogger<MainService>>();
            var configuration = ServiceManager.GetService<IConfiguration>();

            var serviceManager = new HostedServiceManager(options => { });
            serviceManager.RunService<MainService>((x, options) =>
            {
                x.BeforeInstall(settings => { logger.LogInfo("Install service => Start"); });
                x.AfterInstall(settings => { logger.LogInfo("Install service => End"); });
                x.BeforeUninstall(() => { logger.LogInfo("Uninstall service => Start"); });
                x.AfterUninstall(() => { logger.LogInfo("Uninstall service => End"); });
            });
        }
    }
}
