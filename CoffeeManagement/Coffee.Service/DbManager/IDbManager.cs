using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Coffee.Core.DbManager
{
    public interface IDbManager
    {
        IDbConnection GetConnection { get; }

        /// <summary>
        /// Thực thi một câu query mà không cần nhận về kết quả sau khi thực thi.
        /// </summary>
        /// <param name="sql">Câu lệnh thực thi</param>
        /// <param name="param">Các biến cần truyền vào bên trong câu sql</param>
        /// <param name="transaction">Khi cần sử dụng transaction
        /// , open connection có được tự <see cref="GetConnection"/>
        /// , sau đó mở connectiion, tạo mới một transaction </param>
        /// <param name="commandType">Mặt định sẽ thực thi store procedure</param>
        /// <returns>Số dòng tác động khi thực thi câu sql này</returns>
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure);

        int Execute(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Gán cômmand với schema
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        string MapCommandWithDbSchema(string commandName);

        /// <summary>
        /// Thực thi query và nhận về kết quả dưới dạng <see cref="List{T}"/>
        /// </summary>
        /// <typeparam name="T">Type của object trả về</typeparam>
        /// <param name="sql">Câu lệnh thực thi</param>
        /// <param name="param">Các biến cần truyền vào bên trong câu sql</param>
        /// <param name="transaction">Khi cần sử dụng transaction
        /// , open connection có được tự <see cref="GetConnection"/>
        /// , sau đó mở connectiion, tạo mới một transaction </param>
        /// <param name="commandType">Mặt định sẽ thực thi store procedure</param>
        /// <returns>Trả về danh sách những hàng lấy được khi thực thi query/></returns>
        Task<List<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Thực thi và trả về một đối tượng <see cref="T"/>, nếu không có sẽ trả về null;
        /// </summary>
        /// <typeparam name="T">Type của object trả về</typeparam>
        /// <param name="sql">Câu lệnh thực thi</param>
        /// <param name="param">Các biến cần truyền vào bên trong câu sql</param>
        /// <param name="transaction">Khi cần sử dụng transaction
        /// , open connection có được tự <see cref="GetConnection"/>
        /// , sau đó mở connectiion, tạo mới một transaction </param>
        /// <param name="commandType">Mặt định sẽ thực thi store procedure</param>
        /// <returns>Trả về một đối tượng được truyền vào, nếu query không có hàng nào, sẽ trả về null</returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Thực thi và trả về một đối tượng <see cref="T"/>, nếu không có sẽ báo lỗi;
        /// </summary>
        /// <typeparam name="T">Type của object trả về</typeparam>
        /// <param name="sql">Câu lệnh thực thi</param>
        /// <param name="param">Các biến cần truyền vào bên trong câu sql</param>
        /// <param name="transaction">Khi cần sử dụng transaction
        /// , open connection có được tự <see cref="GetConnection"/>
        /// , sau đó mở connectiion, tạo mới một transaction </param>
        /// <param name="commandType">Mặt định sẽ thực thi store procedure</param>
        /// <returns>Trả về một đối tượng được truyền vào, nếu query không có hàng nào, sẽ trả về null</returns>
        Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure);

        List<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Thực thi và trả về nhiều table. Dùng hàm ReadFirstAsync,Read để lấy dữ liệu từng table.
        /// Example:  _dbManager.QueryMultipleAsync =>  await result.ReadAsync<TemplateActiveOutput>()
        /// </summary>
        /// <param name="sql">Câu lệnh thực thi</param>
        /// <param name="param">Các biến cần truyền vào bên trong câu sql</param>
        /// <param name="transaction">Khi cần sử dụng transaction
        /// , open connection có được tự <see cref="GetConnection"/>
        /// , sau đó mở connectiion, tạo mới một transaction</param>
        /// <param name="commandType">Mặt định sẽ thực thi store procedure</param>
        /// <returns></returns>
        Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = CommandType.StoredProcedure);
    }
}
