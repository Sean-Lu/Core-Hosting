using System;
using System.Collections.Generic;
using Topshelf;

namespace Sean.Core.Topshelf.Contracts
{
    public interface IServiceOptions
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// 服务显示名称
        /// </summary>
        string ServiceDisplayName { get; }

        /// <summary>
        /// 服务描述
        /// </summary>
        string ServiceDescription { get; }

        /// <summary>
        /// 服务启动方式
        /// </summary>
        ServiceStartType StartType { get; }

        /// <summary>
        /// 服务运行身份
        /// </summary>
        RunAsIdentity Identity { get; }

        /// <summary>
        /// 用户名（仅当 <see cref="Identity"/>=<see cref="RunAsIdentity.SpecialUser"/> 时生效）
        /// </summary>
        string Username { get; }

        /// <summary>
        /// 密码（仅当 <see cref="Identity"/>=<see cref="RunAsIdentity.SpecialUser"/> 时生效）
        /// </summary>
        string Password { get; }

        /// <summary>
        /// 是否允许服务暂停\继续（默认禁用）
        /// </summary>
        bool EnablePauseAndContinue { get; }

        bool EnableShutdown { get; }

        /// <summary>
        /// 服务依赖
        /// </summary>
        List<string> DependsOnServiceNames { get; }

        /// <summary>
        /// 自动恢复设置（服务重启）
        /// </summary>
        Action<ServiceRecoveryConfigurator> ServiceRecovery { get; }
    }
}