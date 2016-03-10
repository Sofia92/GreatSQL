namespace GreatSQL.Models.Interfaces
{
    public interface ISqlExecuter
    {
        int Execute(SqlItem sql);
    }
}
