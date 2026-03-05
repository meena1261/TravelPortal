using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Common;

namespace TravelPortal.web.Helpers
{
    public class Connection
    {
        internal static string ConnectionString = ConfigHelper.DatabaseConnection;

        internal static IEnumerable<T> Query<T>(string sql, object param = null)
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
                if (typeof(T) == typeof(JsonResponse))
                {
                    var errorResponse = new JsonResponse
                    {
                        status = 0,
                        message = ex.Message
                        // Set other default fields if needed
                    };
                    return new List<T> { (T)(object)errorResponse };
                }
                return Enumerable.Empty<T>();
            }
        }
        internal static IEnumerable<T> QueryTableType<T>(string sql, object param = null, CommandType commandType = default)
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

        internal static int Execute(string sql, object param = null)
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

        internal static T ExecuteScalar<T>(string sql, object param = null)
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

        internal static void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        //DapperORM.ExecuteReturnScalar<int>(_,_);
        internal static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            try
            {

                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
                }
            }
            catch
            {
                return default(T);
            }

        }
        //DapperORM.ReturnList<EmployeeModel> <=  IEnumerable<EmployeeModel>
        internal static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }
    }
}