namespace GreatSQL.Models
{
    /// <summary>
    /// 用于表示权限分组
    /// </summary>
    public class Group
    {
        public int ID { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限值，需要与 <see cref="Enums.Rule"/> 枚举比较
        /// </summary>
        public int Rule { get; set; } 
    }
}