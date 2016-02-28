using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatSQL.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public int ID { get; set; }

        /// <summary>
        /// 邮件地址，作为登录名使用
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户名，作为昵称，不用于登录
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 所属权限组
        /// </summary>
        public Group RuleGroup { get; set; }
    }
}