using Topshelf.Runtime;

namespace Sean.Core.Topshelf.Contracts
{
    public interface IHostedService
    {
        HostSettings Settings { get; set; }

        void Start();
        void Stop();
    }

    public interface ICompleteHostedService : IHostedService
    {
        void Pause();
        void Continue();

        void Shutdown();
    }
}
