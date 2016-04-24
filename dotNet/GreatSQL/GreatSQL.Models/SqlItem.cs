using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreatSQL.Models
{
    /// <summary>
    /// 表示一次 SQL 记录
    /// </summary>
    public class SqlItem
    {
        public int ID { get; set; }

        /// <summary>
        /// SQL 语句的主体
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 受影响的行数
        /// </summary>
        public int? Record { get; set; }

        /// <summary>
        /// 失败或其他信息
        /// </summary>
        public string Message { get; set; }

        public int Creater_ID { get; set; }
        
        public int? Runner_ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public DateTime? RunTime { get; set; }

        /// <summary>
        /// 运行消耗时间 (记录的结束时间)
        /// </summary>
        public TimeSpan? ElapsedTime { get; set; }

        /// <summary>
        /// 运行人
        /// </summary>
        [ForeignKey("Runner_ID")]
        public User Runner { get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        [ForeignKey("Creater_ID")]
        public User Creater { get; set; }
    }
}