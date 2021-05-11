namespace Sean.Core.Topshelf
{
    /// <summary>
    /// 服务启动类型
    /// </summary>
    public enum ServiceStartType
    {
        /// <summary>
        /// 自动
        /// </summary>
        Automatically,
        /// <summary>
        /// 自动（延时启动）
        /// </summary>
        AutomaticallyDelayed,
        /// <summary>
        /// 手动
        /// </summary>
        Manually,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled
    }

    /// <summary>
    /// 服务运行身份
    /// </summary>
    public enum RunAsIdentity
    {
        /// <summary>
        /// 本地系统
        /// </summary>
        LocalSystem,
        /// <summary>
        /// 本地服务
        /// </summary>
        LocalService,
        /// <summary>
        /// 网络服务
        /// </summary>
        NetworkService,
        /// <summary>
        /// 虚拟网络账号
        /// </summary>
        VirtualServiceAccount,
        /// <summary>
        /// 在安装服务时，会要求输入用户名和密码
        /// </summary>
        Prompt,
        /// <summary>
        /// 指定用户名和密码
        /// </summary>
        SpecialUser
    }
}
