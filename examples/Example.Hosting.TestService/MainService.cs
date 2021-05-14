using Microsoft.Extensions.Hosting;
using Sean.Utility.Common;
using Sean.Utility.Contracts;
using Sean.Utility.Extensions;
using Sean.Utility.Impls.Log;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Example.Hosting.TestService
{
    public class MainService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public MainService(ISimpleLogger<MainService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            SimpleLocalLoggerBase.DateTimeFormat = time => time.ToLongDateTime();

            //ThreadPool.SetMinThreads(30, 30);

            ExceptionHelper.CatchGlobalUnhandledException(_logger);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                // Todo something...
                _logger.LogInfo($"{typeof(MainService).Namespace}服务启动");
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                // Todo something...
                _logger.LogInfo($"{typeof(MainService).Namespace}服务停止");
            }, cancellationToken);
        }
    }
}