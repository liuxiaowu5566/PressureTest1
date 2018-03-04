using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MongoDBHelper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMongoDBHelper
    {
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
        /// <param name="modelList"></param>
        /// <returns></returns>
        int Insert<T>(List<T> modelList);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Select<T>(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> Select<T>(Expression<Func<T, bool>> filter, int pageIndex=1, int pageSize= 15, string oderbyFiled = "_id");

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        long Delete<T>(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        long Update<T>(Expression<Func<T, bool>> filter, T model);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="jsonJoinStr"></param>
        /// <returns></returns>
        List<TOutput> Join<TInput, TOutput>(string jsonJoinStr);
    }
}
