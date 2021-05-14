using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sean.Core.Hosting;
using Sean.Utility.Contracts;
using Sean.Utility.Impls.Log;

namespace Example.Hosting.TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostBuilderHelper.CreateDefaultBuilder<MainService>(args, services =>
            {
                services.AddTransient(typeof(ISimpleLogger<>), typeof(SimpleLocalLogger<>));
            }).UseConsoleLifetime().Build().Run();
        }
    }
}
