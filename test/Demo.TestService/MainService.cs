using Sean.Core.Ioc;
using Sean.Core.Topshelf;
using Sean.Core.Topshelf.Contracts;
using Sean.Utility.Common;
using Sean.Utility.Contracts;

namespace Demo.TestService
{
    public class MainService : IHostedService
    {
        public ServiceOptions Options { get; set; }

        private readonly ILogger _logger;

        public MainService()
        {
            _logger = ServiceManager.GetService<ISimpleLogger<MainService>>();

            //ThreadPool.SetMinThreads(30, 30);

            ExceptionHelper.CatchGlobalUnhandledException(_logger);
        }

        public void Start()
        {
            // Todo something...
            _logger.LogInfo($"{Options.ServiceName}服务启动");
        }

        public void Stop()
        {
            // Todo something...
            _logger.LogInfo($"{Options.ServiceName}服务停止");
        }
    }
}