using System;
using Topshelf;
using Topshelf.HostConfigurators;

namespace Sean.Core.Topshelf.Extensions
{
    public static class HostConfiguratorExtensions
    {
        public static void Configure(this HostConfigurator configurator, ServiceOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.ServiceName))
                configurator.SetServiceName(options.ServiceName);
            if (!string.IsNullOrWhiteSpace(options.ServiceDisplayName))
                configurator.SetDisplayName(options.ServiceDisplayName);
            if (!string.IsNullOrWhiteSpace(options.ServiceDescription))
                configurator.SetDescription(options.ServiceDescription);

            // 服务启动类型
            configurator.Configure(options.StartType);

            // 服务运行身份
            configurator.Configure(options.Identity, options.Username, options.Password);

            if (options.EnablePauseAndContinue)
            {
                configurator.EnablePauseAndContinue();
            }
        }
        public static void Configure(this HostConfigurator configurator, ServiceStartType type)
        {
            // 服务启动类型
            switch (type)
            {
                case ServiceStartType.Automatically:
                    configurator.StartAutomatically();
                    break;
                case ServiceStartType.AutomaticallyDelayed:
                    configurator.StartAutomaticallyDelayed();
                    break;
                case ServiceStartType.Manually:
                    configurator.StartManually();
                    break;
                case ServiceStartType.Disabled:
                    configurator.Disabled();
                    break;
                default:
                    throw new NotSupportedException($"Unsupported type: {type}");
            }
        }

        public static void Configure(this HostConfigurator configurator, RunAsIdentity type, string username = null, string password = null)
        {
            // 服务运行身份
            switch (type)
            {
                case RunAsIdentity.LocalSystem:
                    configurator.RunAsLocalSystem();
                    break;
                case RunAsIdentity.LocalService:
                    configurator.RunAsLocalService();
                    break;
                case RunAsIdentity.NetworkService:
                    configurator.RunAsNetworkService();
                    break;
                case RunAsIdentity.VirtualServiceAccount:
                    configurator.RunAsVirtualServiceAccount();
                    break;
                case RunAsIdentity.Prompt:
                    configurator.RunAsPrompt();
                    break;
                case RunAsIdentity.SpecialUser:
                    configurator.RunAs(username, password);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported type: {type}");
            }
        }
    }
}
