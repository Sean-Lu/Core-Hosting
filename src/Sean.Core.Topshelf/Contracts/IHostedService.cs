namespace Sean.Core.Topshelf.Contracts
{
    public interface IHostedService
    {
        ServiceOptions Options { get; set; }

        void Start();

        void Stop();

        //void Shutdown();
    }
}
