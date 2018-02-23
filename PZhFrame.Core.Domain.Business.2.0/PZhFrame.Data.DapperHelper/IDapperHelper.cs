using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PZhFrame.Data.DapperHelper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDapperHelper
    {


        /// <summary>
        /// 
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 
        /// </summary>
        void RollbackTran();

        /// <summary>
        /// 
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        int Count(string sqlStr);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        Task<int> CountAsync(string sqlStr);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        List<T> Select<T>(string sqlStr,object idValue=null);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync<T>(string sqlStr);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert<T>(T model);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> InsertAsync<T>(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        int InsertAutoId<T>(T model);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> InsertAutoIdAsync<T>(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Delete<T>(T model);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(T model);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update<T>(T model);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="dynamicParameters"></param>
        /// <returns></returns>
        List<T> ExecuteStoredProcedureWithParms<T>(string storedProcedureName, DynamicParameters dynamicParameters);
    }
}
