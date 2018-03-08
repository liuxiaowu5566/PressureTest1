using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using MongoDB.Driver.GridFS;
using System.Threading.Tasks;

namespace MongoDBHelper
{
    public class MongoDBHelper: IMongoDBHelper, IDisposable
    {
        private MongoClient client = null;
        private IMongoDatabase database = null;
        private static IMongoDBHelper mongoDBHelper=null;
        private static readonly object locker = new object();

        /// <summary>
        /// 构造
        /// </summary>
        private MongoDBHelper()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.json");
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(path);
            var configApp = builder.Build();
            string connString = configApp.GetSection("ConnectionStrings:MongoDBSysConnection:ConnectionString").Value;
            string dataBaseName = configApp.GetSection("ConnectionStrings:MongoDBSysConnection:DataBase").Value;
            client = new MongoClient(connString);
            database = client.GetDatabase(dataBaseName);
        }

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static IMongoDBHelper GetInstance()
        {
            if (mongoDBHelper == null)
            {
                lock (locker)
                {
                    if (mongoDBHelper == null)
                        mongoDBHelper = new MongoDBHelper();
                }
            }
            return mongoDBHelper;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert<T>(T model)
        {
            int res = 0;
            try
            {
                IMongoCollection<T> mongoCollection = getCollection<T>();
                mongoCollection.InsertOne(model);
                res = 1;
            }
            catch(Exception e)
            {
                res = 0;
            }
            return res;
        }

        /// <summary>
        /// 新增 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        async public Task<int> InsertAsync<T>(T model)
        {
            int res = 0;
            try
            {
                IMongoCollection<T> mongoCollection = getCollection<T>();
                await mongoCollection.InsertOneAsync(model);
                res = 1;
            }
            catch (Exception e)
            {
                res = 0;
            }
            return res;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int Insert<T>(List<T> modelList)
        {
            int res = 0;
            try
            {
                IMongoCollection<T> mongoCollection = getCollection<T>();
                mongoCollection.InsertMany(modelList);
                res = modelList.Count;
            }
            catch (Exception e)
            {
                res = 0;
            }
            return res;
        }

        /// <summary>
        /// 新增 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList"></param>
        /// <returns></returns>
        async public Task<int> InsertAsync<T>(List<T> modelList)
        {
            int res = 0;
            try
            {
                IMongoCollection<T> mongoCollection = getCollection<T>();
                await mongoCollection.InsertManyAsync(modelList);
                res = modelList.Count;
            }
            catch (Exception e)
            {
                res = 0;
            }
            return res;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Select<T>(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            return mongoCollection.Find(filter).ToList() ;
        }

        /// <summary>
        /// 查询 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        async public Task<List<T>> SelectAsync<T>(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            return (await mongoCollection.FindAsync(filter)).ToList();
        }

        /// <summary>
        /// 查询  分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Select<T>(Expression<Func<T, bool>> filter, int pageIndex=1, int pageSize=15, string oderbyFiled="_id")
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            //return mongoCollection.Find(filter).Skip((pageIndex-1)*pageSize).Limit(pageSize).ToList();
            return mongoCollection.Find(filter).Sort(Builders<T>.Sort.Descending(oderbyFiled)).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Update<T>(Expression<Func<T, bool>> filter,T model)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            BsonDocument bsonDocument = model.ToBsonDocument<T>();
            bsonDocument.Remove("_id");
            string bsonDocumentStr = "{$set:"+bsonDocument.ToString()+"}";
            bsonDocument = BsonDocument.Parse( bsonDocumentStr);
            var s=Builders<T>.Update.Combine(bsonDocument);
            //return mongoCollection.UpdateOne(Builders<T>.Filter.Where(filter),
            //                    Builders<T>.Update.Set("name", "12334111"),
            //                    new UpdateOptions { IsUpsert = false }).ModifiedCount;
            return mongoCollection.UpdateOne<T>(filter, bsonDocument, new UpdateOptions { IsUpsert=false}).ModifiedCount;
        }

        /// <summary>
        /// 更新 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        async public Task<long> UpdateAsync<T>(Expression<Func<T, bool>> filter, T model)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            BsonDocument bsonDocument = model.ToBsonDocument<T>();
            bsonDocument.Remove("_id");
            string bsonDocumentStr = "{$set:" + bsonDocument.ToString() + "}";
            bsonDocument = BsonDocument.Parse(bsonDocumentStr);
            var s = Builders<T>.Update.Combine(bsonDocument);
            //return mongoCollection.UpdateOne(Builders<T>.Filter.Where(filter),
            //                    Builders<T>.Update.Set("name", "12334111"),
            //                    new UpdateOptions { IsUpsert = false }).ModifiedCount;
            return ( await mongoCollection.UpdateOneAsync<T>(filter, bsonDocument, new UpdateOptions { IsUpsert = false })).ModifiedCount;
        }

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public long Update<T>(T model) where T : Entity
        //{
        //    Expression<Func<T, bool>> filter = o => o._id == model._id;
        //    IMongoCollection<T> mongoCollection = getCollection<T>();
        //    BsonDocument bsonDocument = model.ToBsonDocument<T>();
        //    string bsonDocumentStr = "{$set:" + bsonDocument.ToString() + "}";
        //    bsonDocument = BsonDocument.Parse(bsonDocumentStr);
        //    return mongoCollection.UpdateOne<T>(filter, bsonDocument, new UpdateOptions { IsUpsert = false }).ModifiedCount;
        //}

        /// <summary>
        /// 左关联
        /// </summary>
        /// <typeparam name="TInput">主表</typeparam>
        /// <typeparam name="TOutput">返回类型</typeparam>
        /// <param name="jsonJoinStr"></param>
        /// <returns></returns>
        public List<TOutput> Join<TInput,TOutput>(string jsonJoinStr)
        {
            IMongoCollection<TInput> mongoCollection = getCollection<TInput>();
            var stages = new List<IPipelineStageDefinition>();
            stages.Add(new JsonPipelineStageDefinition<TInput, TOutput>(jsonJoinStr));
            return mongoCollection.Aggregate<TOutput>(stages).ToList();
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long Delete<T>(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            return mongoCollection.DeleteMany(filter).DeletedCount;
        }

        /// <summary>
        /// 物理删除 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        async public Task<long> DeleteAsync<T>(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            return (await mongoCollection.DeleteManyAsync(filter)).DeletedCount;
        }

        /// <summary>
        /// GridFS文件操作——上传
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public ObjectId UpLoad(string fileName, byte[] fileData)
        {
            var gridfs = new GridFSBucket(database);
            ObjectId oId = gridfs.UploadFromBytes(fileName, fileData);
            return oId;
        }

        /// <summary>
        /// GridFS文件操作——上传异步
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        async public Task<ObjectId> UpLoadAsync(string fileName, byte[] fileData)
        {
            var gridfs = new GridFSBucket(database);
            ObjectId oId = await gridfs.UploadFromBytesAsync(fileName, fileData);
            return oId;
        }

        /// <summary>
        /// GridFS文件操作——上传
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public ObjectId UpLoad(string fileName, Stream fileStream)
        {
            var gridfs = new GridFSBucket(database, new GridFSBucketOptions
            {
            });
            ObjectId oId = gridfs.UploadFromStream(fileName, fileStream);
            return oId;
        }

        /// <summary>
        /// GridFS文件操作——上传 异步
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        async public Task<ObjectId> UpLoadAsync(string fileName, Stream fileStream)
        {
            var gridfs = new GridFSBucket(database, new GridFSBucketOptions
            {
            });
            ObjectId oId = await gridfs.UploadFromStreamAsync(fileName, fileStream);
            return oId;
        }

        /// <summary>
        /// GridFS文件操作——下载
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] DownLoad(string fileName)
        {
            var gridfs = new GridFSBucket(database);
            byte[] fileData = gridfs.DownloadAsBytesByName(fileName);
            return fileData;
        }

        /// <summary>
        /// GridFS文件操作——下载 异步
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        async public Task<byte[]> DownLoadAsync(string fileName)
        {
            var gridfs = new GridFSBucket(database);
            byte[] fileData = await gridfs.DownloadAsBytesByNameAsync(fileName);
            return fileData;
        }

        /// <summary>
        /// GridFS文件操作——下载
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public byte[] DownLoad(ObjectId fileId)
        {
            var gridfs = new GridFSBucket(database);
            byte[] fileData = gridfs.DownloadAsBytes(fileId);
            return fileData;
        }

        /// <summary>
        /// GridFS文件操作——下载
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        async public Task<byte[]> DownLoadAsync(ObjectId fileId)
        {
            var gridfs = new GridFSBucket(database);
            byte[] fileData = await gridfs.DownloadAsBytesAsync(fileId);
            return fileData;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IMongoCollection<T> getCollection<T>()
        {
            Type type = typeof(T);
            string collection = type.Name;
            return database.GetCollection<T>(collection);
        }
    }
}
