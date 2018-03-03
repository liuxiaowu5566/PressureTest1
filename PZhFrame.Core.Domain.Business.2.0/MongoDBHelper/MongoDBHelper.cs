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
        /// 查询  分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Select<T>(Expression<Func<T, bool>> filter, int pageIndex=1, int pageSize=15)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            return mongoCollection.Find(filter).Skip((pageIndex-1)*pageSize).Limit(pageSize).ToList();
        }

        public long Update<T>(Expression<Func<T, bool>> filter,T model)
        {
            IMongoCollection<T> mongoCollection = getCollection<T>();
            BsonDocument bsonDocument = model.ToBsonDocument<T>();
            bsonDocument.Remove("_id");
            string ss = "{$set:"+bsonDocument.ToString()+"}";
            var ssss=BsonDocument.Parse( ss);
            var s=Builders<T>.Update.Combine(bsonDocument);
            //return mongoCollection.UpdateOne(Builders<T>.Filter.Where(filter),
            //                    Builders<T>.Update.Set("name", "12334111"),
            //                    new UpdateOptions { IsUpsert = false }).ModifiedCount;
            return mongoCollection.UpdateOne<T>(filter, ssss, new UpdateOptions { IsUpsert=false}).ModifiedCount;
        }

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
