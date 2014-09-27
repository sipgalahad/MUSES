using System.Data;

namespace CodeX.Data.Core.Dal
{
    public interface IDbConfig
    {
        IDbConnection GetConnection();
        IDataAdapter GetDataAdapter(IDbCommand command);
        IDataParameter GetDataParameter();
        string GetParameterName(string name);
    }
}