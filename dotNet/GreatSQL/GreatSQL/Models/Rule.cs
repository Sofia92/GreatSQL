using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatSQL.Models
{
    /// <summary>
    /// 权限码
    /// </summary>
    [Flags]
    public enum Rule
    {
        /// <summary>
        /// 基础的创建 SQL 权限
        /// </summary>
        CreateSql = 1,
        /// <summary>
        /// 读记录权限，可读所有用户的执行 SQL 和结果
        /// </summary>
        ReadLog = 2,
        /// <summary>
        /// SQL 的执行权限
        /// </summary>
        RunSql = 4,
        /// <summary>
        /// 用户管理权限，可配置用户和用户的权限
        /// </summary>
        User = 8
    }
}