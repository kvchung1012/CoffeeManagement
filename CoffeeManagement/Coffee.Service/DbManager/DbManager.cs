using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Coffee.Core.DbManager
{
    public class DbManager : IDbManager, IDisposable
    {
        public readonly IDbConnection Connection;
        private readonly IConfiguration _configuration;

        public DbManager(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("Default"));
            _configuration = configuration;
        }

        public IDbConnection GetConnection => this.Connection;

        // master/slave db
        //  rename query command
        public string MapCommandWithDbSchema(string commandName)
        {
            var schema = _configuration.GetSection("Setting:DbSchema").Value;
            return $"{schema}.{commandName}";
        }

        // excute create/update no return result async
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            return await Connection.ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            return Connection.Execute(sql: sql, param: param, transaction: transaction, commandType: commandType);
        }

        // query list data
        public async Task<List<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            return (await Connection.QueryAsync<T>(sql, param, transaction, commandType: commandType)).AsList();
        }

        public async Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            return (await Connection.QueryMultipleAsync(sql, param, transaction, commandType: commandType));
        }

        public List<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            return Connection.Query<T>(sql, param, transaction, commandType: commandType).AsList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            return await Connection.QuerySingleAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Connection.Dispose();
        }
    }
}
