using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns></returns>
        Task<List<T>> SelectAsync<T>(Expression<Func<T, bool>> filter);

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
        /// <param name="model"></param>
        /// <returns></returns>
        long Update<T>(Expression<Func<T, bool>> filter, T model);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> UpdateAsync<T>(Expression<Func<T, bool>> filter, T model);

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
        Task<long> DeleteAsync<T>(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="jsonJoinStr"></param>
        /// <returns></returns>
        List<TOutput> Join<TInput, TOutput>(string jsonJoinStr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        ObjectId UpLoad(string fileName, byte[] fileData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        Task<ObjectId> UpLoadAsync(string fileName, byte[] fileData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>s
        /// <returns></returns>
        ObjectId UpLoad(string fileName, Stream fileStream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        Task<ObjectId> UpLoadAsync(string fileName, Stream fileStream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        byte[] DownLoad(string fileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<byte[]> DownLoadAsync(string fileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        byte[] DownLoad(ObjectId fileId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<byte[]> DownLoadAsync(ObjectId fileId);
    }
}
