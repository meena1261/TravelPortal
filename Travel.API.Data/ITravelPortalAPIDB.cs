using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.API.Data
{
    public interface ITravelPortalAPIDB
    {
        IEnumerable<T> Query<T>(string sql, object param = null);
        IEnumerable<T> QueryTableType<T>(string sql, object param = null, System.Data.CommandType commandType = default);
        int Execute(string sql, object param = null);
        T ExecuteScalar<T>(string sql, object param = null);
        void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null);
        T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null);
        IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null);
    }
}
