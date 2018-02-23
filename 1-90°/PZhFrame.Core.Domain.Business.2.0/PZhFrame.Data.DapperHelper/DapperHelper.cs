using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PZhFrame.Data.Repository.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZhFrame.Data.DapperHelper
{
    /// <summary>
    /// Dapper封装
    /// </summary>
    public class DapperHelper : IDapperHelper, IDisposable
    {

        private string schema = "dbo";

        public void SetSchema(string schema) { this.schema = schema; }

        /// <summary>
        /// 
        /// </summary>
        private IDbConnection dbConnection = null;
        private IDbTransaction dbTransaction = null;
        private static IDapperHelper dapperHelperInstance = null;
        private static readonly object locker = new object();

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static IDapperHelper GetInstance()
        {
            if (dapperHelperInstance == null)
            {
                lock (locker)
                {
                    if (dapperHelperInstance == null)
                        dapperHelperInstance = new DapperHelper();
                }
            }
            return dapperHelperInstance;
        }

        /// <summary>
        /// 获取新实例
        /// </summary>
        /// <returns></returns>
        public static IDapperHelper GetNewInstance()
        {
            IDapperHelper dapperHelper = new DapperHelper();
            return dapperHelper;
        }

        /// <summary>
        /// 构造
        /// </summary>
        private DapperHelper()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.json");
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(path);
            var configApp = builder.Build();
            string sqlType = configApp.GetSection("ConnectionStrings:SysConnection:ProviderName").Value;
            string connString = configApp.GetSection("ConnectionStrings:SysConnection:ConnectionString").Value;
            switch (sqlType.ToUpper())
            {
                case "SQLSERVER": dbConnection = new SqlConnection(connString); break;
                case "MYSQLSERVER": dbConnection = new MySqlConnection(connString); break;
            }
        }

        /// <summary>
        /// 释放数据库连接
        /// </summary>
        public void Dispose()
        {
            if (dbConnection != null && dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Close();
                dbConnection.Dispose();
                dbConnection = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckConnection()
        {
            if(dbConnection.State!=ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BeginTran()
        {
            CheckConnection();
            dbTransaction = dbConnection.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RollbackTran()
        {
            dbTransaction?.Rollback();
            dbTransaction = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CommitTran()
        {
            dbTransaction?.Commit();
            dbTransaction = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public int Count(string sqlStr)
        {
            return (int)dbConnection.ExecuteScalar(sqlStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        async public Task<int> CountAsync(string sqlStr)
        {
            return (int)await dbConnection.ExecuteScalarAsync(sqlStr);
        }

        /// <summary>
        /// 查询 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public List<T> Select<T>(string sqlStr,object idValue=null)
        {
            return dbConnection.Query<T>(sqlStr,new { id=idValue},dbTransaction)?.ToList();
        }

        /// <summary>
        /// 查询 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        async public Task<List<T>> SelectAsync<T>(string sqlStr)
        {
            return (await dbConnection.QueryAsync<T>(sqlStr, null,dbTransaction))?.ToList();
        }

        /// <summary>
        /// 新增 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert<T>(T model)
        {
            string cmd = MakeInsertCommand(model);
            int res = dbConnection.Execute(cmd, model, dbTransaction);
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
            string cmd = MakeInsertCommand(model);
            int res = await dbConnection.ExecuteAsync(cmd, model, dbTransaction);
            return res;
        }

        /// <summary>
        /// 新增 同步 自增id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertAutoId<T>(T model)
        {
            string cmd = MakeInsertCommandAutoId(model);
            int res = dbConnection.Execute(cmd, model, dbTransaction);
            return res;
        }

        /// <summary>
        /// 新增 异步   自增id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        async public Task<int> InsertAutoIdAsync<T>(T model)
        {
            string cmd = MakeInsertCommandAutoId(model);
            int res = await dbConnection.ExecuteAsync(cmd, model, dbTransaction);
            return res;
        }

        public List<T> ExecuteStoredProcedureWithParms<T>(string storedProcedureName, DynamicParameters dynamicParameters)
        {
            return dbConnection.Query<T>(storedProcedureName, dynamicParameters, dbTransaction,true, null, CommandType.StoredProcedure).ToList();
        }

        /// <summary>
        /// 组织insert sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private string MakeInsertCommand<T>(T model)
        {
            Type typeInfo = model.GetType();

            string tableName = typeInfo.Name;
            var properties = typeInfo.GetProperties();
            StringBuilder itemName = new StringBuilder();
            StringBuilder itemValue = new StringBuilder();
            foreach (var p in properties)
            {
                string item = p.Name + ",";
                itemName.Append(item);
                itemValue.Append("@" + item);
            }
            itemName.Remove(itemName.Length - 1, 1);
            itemValue.Remove(itemValue.Length - 1, 1);
            string cmd = $"insert into {schema}.{tableName} ({itemName}) values ({itemValue})";
            return cmd;
        }

        /// <summary>
        /// 组织insert sql语句  使用数据库自增id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private string MakeInsertCommandAutoId<T>(T model)
        {
            Type typeInfo = model.GetType();

            string tableName = typeInfo.Name;
            var properties = typeInfo.GetProperties().ToList();
            var keyProps = properties.Where(o => o.GetCustomAttributes(true).Any(a => a is ExplicitKeyAttribute)).ToList();
            properties = properties.Except(keyProps).ToList();
            StringBuilder itemName = new StringBuilder();
            StringBuilder itemValue = new StringBuilder();
            foreach (var p in properties)
            {
                string item = p.Name + ",";
                itemName.Append(item);
                itemValue.Append("@" + item);
            }
            itemName.Remove(itemName.Length - 1, 1);
            itemValue.Remove(itemValue.Length - 1, 1);
            string cmd = $"insert into {schema}.{tableName} ({itemName}) values ({itemValue})";
            return cmd;
        }

        /// <summary>
        /// 删除 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete<T>(T model)
        {
            string cmd = MakeDeleteCommand(model);
            return dbConnection.Execute(cmd, model, dbTransaction) > 0;
        }

        /// <summary>
        /// 删除 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        async public Task<bool> DeleteAsync<T>(T model)
        {
            string cmd = MakeDeleteCommand(model);
            return await dbConnection.ExecuteAsync(cmd, model, dbTransaction) > 0;
        }

        /// <summary>
        /// 组织delete sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private string MakeDeleteCommand<T>(T model)
        {
            Type typeInfo = model.GetType();
            string tableName = typeInfo.Name;
            var properties = typeInfo.GetProperties().ToList();
            properties = properties.Where(o => o.GetCustomAttributes(true).Any(a => a is ExplicitKeyAttribute)).ToList();
            StringBuilder items = new StringBuilder();
            foreach (var p in properties)
            {
                items.AppendFormat("{0}=@{0} and", p.Name);
            }
            items.Remove(items.Length - 3, 3);
            string cmd = $"delete from {schema}.{tableName} where {items}";
            return cmd;
        }

        /// <summary>
        /// 修改 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update<T>(T model)
        {
            string cmd = MakeUpdateCommand(model);
            return dbConnection.Execute(cmd, model, dbTransaction) > 0;
        }

        /// <summary>
        /// 修改 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        async public Task<bool> UpdateAsync<T>(T model)
        {
            string cmd = MakeUpdateCommand(model);
            return await dbConnection.ExecuteAsync(cmd, model, dbTransaction) > 0;
        }

        /// <summary>
        /// 组织update sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private string MakeUpdateCommand<T>(T model)
        {
            Type typeInfo = model.GetType();
            string tableName = typeInfo.Name;
            var properties = typeInfo.GetProperties().ToList();
            var keyProps = properties.Where(o => o.GetCustomAttributes(true).Any(a => a is ExplicitKeyAttribute)).ToList();
            var itemProps = properties.Except(keyProps).ToList();
            StringBuilder items = new StringBuilder();
            StringBuilder keys = new StringBuilder();
            foreach (var p in itemProps)
            {
                items.AppendFormat("{0}=@{0},", p.Name);
            }
            items.Remove(items.Length - 1, 1);
            foreach (var p in keyProps)
            {
                keys.AppendFormat("{0}=@{0} and", p.Name);
            }
            keys.Remove(keys.Length - 3, 3);
            string cmd = $"update {schema}.{tableName} set {items} where {keys}";
            return cmd;
        }

    }
}
