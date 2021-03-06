using System;
using System.Collections.Generic;
using Sean.Core.Topshelf.Contracts;
using Topshelf;

namespace Sean.Core.Topshelf
{
    /// <summary>
    /// 服务配置
    /// </summary>
    public class ServiceOptions : IServiceOptions
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务显示名称
        /// </summary>
        public string ServiceDisplayName { get; set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription { get; set; }

        /// <summary>
        /// 服务启动方式
        /// </summary>
        public ServiceStartType StartType { get; set; }
        /// <summary>
        /// 服务运行身份
        /// </summary>
        public RunAsIdentity Identity { get; set; }

        /// <summary>
        /// 用户名（仅当 <see cref="Identity"/>=<see cref="RunAsIdentity.SpecialUser"/> 时生效）
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码（仅当 <see cref="Identity"/>=<see cref="RunAsIdentity.SpecialUser"/> 时生效）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否允许服务暂停\继续（默认禁用）
        /// </summary>
        public bool EnablePauseAndContinue { get; set; }
        public bool EnableShutdown { get; set; }

        /// <summary>
        /// 服务依赖
        /// </summary>
        public List<string> DependsOnServiceNames { get; set; }

        /// <summary>
        /// 自动恢复设置（服务重启）
        /// </summary>
        public Action<ServiceRecoveryConfigurator> ServiceRecovery { get; set; }
    }
}
