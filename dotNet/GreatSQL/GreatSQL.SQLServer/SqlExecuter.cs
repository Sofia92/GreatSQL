using System.Data.SqlClient;
using GreatSQL.Models;
using GreatSQL.Models.Interfaces;

namespace GreatSQL.SQLServer
{
    public class SqlExecuter : ISqlExecuter
    {
        public int Execute(SqlItem sql)
        {
            using (var conn = new SqlConnection(""))
            {
                conn.Open();
                using (var command = new SqlCommand(sql.Body, conn))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
