using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Travel.API.Data
{
    public class Connection: ITravelPortalAPIDB
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString = "";
        public Connection(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    return conn.Query<T>(sql, param);
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<T>();
            }
        }
        public IEnumerable<T> QueryTableType<T>(string sql, object param = null, System.Data.CommandType commandType = default)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    return conn.Query<T>(sql, param);
                }
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }

        public int Execute(string sql, object param = null)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    return conn.Execute(sql, param);
                }
            }
            catch
            {
                return 0;
            }
        }

        public T ExecuteScalar<T>(string sql, object param = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    return conn.ExecuteScalar<T>(sql, param);
                }
            }
            catch
            {
                return default(T);
            }
        }

        public void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        //DapperORM.ExecuteReturnScalar<int>(_,_);
        public T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            try
            {

                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
                }
            }
            catch
            {
                return default(T);
            }

        }
        //DapperORM.ReturnList<EmployeeModel> <=  IEnumerable<EmployeeModel>
        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }
    }
}
