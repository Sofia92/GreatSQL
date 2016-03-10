using System;

namespace GreatSQL.Models.Enums
{
    /// <summary>
    /// 权限码
    /// </summary>
    [Flags]
    public enum Rule
    {
        /// <summary>
        /// 基础的创建 SQL 权限，同时该权限拥有更改自己 SQL 的权限
        /// </summary>
        CreateSql = 1,
        /// <summary>
        /// 读记录权限，可读当前用户的执行 SQL 和结果
        /// </summary>
        ReadLog = 2,
        /// <summary>
        /// SQL 的执行权限
        /// </summary>
        RunSql = 4,
        /// <summary>
        /// 用户管理权限，可配置用户和用户的权限
        /// </summary>
        User = 8,
        /// <summary>
        /// 读取所有记录
        /// </summary>
        ReadAllLog = 16,
        /// <summary>
        /// 更改 SQL 的权限，该权限可更改所有
        /// </summary>
        UpdateSql = 32
    }
}