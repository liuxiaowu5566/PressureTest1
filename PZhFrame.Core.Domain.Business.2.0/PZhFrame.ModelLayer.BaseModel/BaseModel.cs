using Dapper;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.Core.Infrastructure.Net;
using PZhFrame.Data.DapperHelper;
using PZhFrame.Data.Repository.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PZhFrame.ModelLayer.BaseModels
{
    /// <summary>
    /// 实体类 抽象基类
    /// </summary>
    public abstract class BaseModel : IDisposable
    {
        /// <summary>
        /// 数据访问
        /// </summary>
        private IDapperHelper dbHelper = null;
        private string schema = "dbo";

        /// <summary>
        /// 当前表名称
        /// </summary>
        private string tableName => getTableName();

        /// <summary>
        /// 构造
        /// </summary>
        public BaseModel(bool newInstance = false)
        {
            dbHelper = newInstance ? DapperHelper.GetNewInstance() : DapperHelper.GetInstance();
        }


        /// <summary>
        /// 数据数量 同步
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return dbHelper.Count(countSql());
        }

        /// <summary>
        /// 数据数量 异步
        /// </summary>
        /// <returns></returns>
        async public virtual Task<int> CountAsync()
        {
            return await dbHelper.CountAsync(countSql());
        }

        /// <summary>
        /// select 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> Select<T>(object idValue=null)
        {
            // 去缓存服务（层）中取数据
            return dbHelper.Select<T>(selectSql(idValue),idValue);
        }

        /// <summary>
        /// select 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> Select<T>(int index, int pageSize, string orderFiled = " modifytime desc ")
        {
            // 去缓存服务（层）中取数据
            return dbHelper.Select<T>(selectSql(index, pageSize, orderFiled));
        }


        /// <summary>
        /// select 同步 Expression 暂未实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual List<T> Select<T>(Expression<Func<T, bool>> expression)
        {
            Expression e = expression.Body;
            return dbHelper.Select<T>(selectSql());
        }

        /// <summary>
        /// select 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        async public virtual Task<List<T>> SelectAsync<T>()
        {
            return await dbHelper.SelectAsync<T>(selectSql());
        }

        /// <summary>
        /// 新增 同步
        /// </summary>
        /// <returns></returns>
        public virtual int Insert()
        {
            return dbHelper.Insert(this);
        }

        /// <summary>
        /// 新增 异步
        /// </summary>
        /// <returns></returns>
        async public virtual Task<int> InsertAsync()
        {
            return await dbHelper.InsertAsync(this);
        }

        /// <summary>
        /// 新增 同步 自增id
        /// </summary>
        /// <returns></returns>
        public virtual int InsertAutoId()
        {
            return dbHelper.InsertAutoId(this);
        }

        /// <summary>
        /// 新增 异步 自增id
        /// </summary>
        /// <returns></returns>
        async public virtual Task<int> InsertAutoIdAsync()
        {
            return await dbHelper.InsertAutoIdAsync(this);
        }

        /// <summary>
        /// 删除 同步
        /// </summary>
        /// <returns></returns>
        public virtual bool Delete()
        {
            return dbHelper.Delete(this);
        }

        /// <summary>
        /// 删除 异步
        /// </summary>
        /// <returns></returns>
        async public virtual Task<bool> DeleteAsync()
        {
            return await dbHelper.DeleteAsync(this);
        }

        /// <summary>
        /// 更新 同步
        /// </summary>
        /// <returns></returns>
        public virtual bool Update()
        {
            return dbHelper.Update(this);
        }

        /// <summary>
        /// 更新 异步
        /// </summary>
        /// <returns></returns>
        async public virtual Task<bool> UpdateAsync()
        {
            return await dbHelper.UpdateAsync(this);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            dbHelper.BeginTran();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            dbHelper.RollbackTran();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            dbHelper.CommitTran();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            dbHelper = null;
        }

        /// <summary>
        /// 新增 同步
        /// </summary>
        /// <returns></returns>
        /*public virtual int InsertEx()
        {

            List<new_fileds_code> fileds = new new_fileds_code().Select<new_fileds_code>().
                Where(o => o.table_name == tableName && o.isdelete == false).ToList();
            Guid tableid = fileds.First().table_id;
            int res = 0;// dbHelper.Insert(this);
            new_tables_code table = new new_tables_code().Select<new_tables_code>().Where(o => o.name ==tableName).FirstOrDefault();

            Type typeInfo = GetType();
            //var s = typeInfo.GetProperty
            Guid resId = Guid.Parse(typeInfo.GetProperty("id").GetValue(this).ToString());
            Guid accountId=Guid.Parse(typeInfo.GetProperty("accountid").GetValue(this).ToString());
            DateTime createTime = DateTime.Parse(typeInfo.GetProperty("createtime").GetValue(this).ToString());
            DateTime modifyTime = DateTime.Parse(typeInfo.GetProperty("modifytime").GetValue(this).ToString());
            BeginTran();
            try
            {
                new_resource_info resourceInfo = new new_resource_info
                {
                    id = resId,
                    table_id = tableid,
                    isdelete = false,
                    accountid = accountId,
                    createtime = createTime,
                    modifytime = modifyTime
                };
                res = dbHelper.Insert(resourceInfo);
                if (res != 1) throw new Exception("ERROR：new_resource_info");
                var properties = typeInfo.GetProperties().ToList();
                //properties = properties.Where(o => !(o.GetCustomAttributes(true).Any(a => a is ExplicitKeyAttribute))).ToList();
                foreach (var p in properties)
                {
                    object filedValue = p.GetValue(this);
                    Guid filedId = fileds.Where(o => o.name == p.Name).FirstOrDefault().id;
                    new_filed_modify_log logModel = new new_filed_modify_log
                    {
                        id = Guid.NewGuid(),
                        resource_id = resId,
                        filed_id = filedId,
                        old_id = new Guid(),
                        value = filedValue?.ToString(),
                        accountid = accountId,
                        is_delete = false,
                        createtime = createTime,
                        modifytime = modifyTime
                    };
                    res=dbHelper.Insert(logModel);
                    if (res != 1) throw new Exception($"ERROR：{tableName}\t{p.Name}");
                }
                CommitTran();
            }
            catch (Exception e)
            {
                RollbackTran();
                res = -1;
            }
            return res;
        }*/

        /// <summary>
        /// 新增 同步
        /// </summary>
        /// <returns></returns>
        public virtual int InsertEx()
        {
            int res = 0;
            List<new_fileds_code> fileds = new new_fileds_code().Select<new_fileds_code>().
                Where(o => o.table_name == tableName && o.isdelete == false).ToList();
            Type typeInfo = GetType();
            var properties = typeInfo.GetProperties().ToList();
            var keyFiled = properties.Where(o => o.GetCustomAttributes(true).Any(a => a is ExplicitKeyAttribute)).FirstOrDefault();
            var accountFiled = properties.Where(o => o.GetCustomAttributes(true).Any(a => a is AccountIdAttribute)).FirstOrDefault();
            Guid resId = Guid.Parse(typeInfo.GetProperty(keyFiled.Name).GetValue(this).ToString());
            Guid accountId = Guid.Parse(typeInfo.GetProperty(accountFiled.Name).GetValue(this).ToString());
            foreach (var p in properties)
            {
                object filedValue = p.GetValue(this);
                Guid filedId = fileds.Where(o => o.name == p.Name).FirstOrDefault().id;
                new_filed_modify_log logModel = new new_filed_modify_log
                {
                    id = Guid.NewGuid(),
                    resource_id = resId,
                    filed_id = filedId,
                    old_id = new Guid(),
                    value = filedValue?.ToString(),
                    accountid = accountId,
                    is_delete = false,
                    createtime = DateTime.Now,
                    modifytime = DateTime.Now
                };
                res = dbHelper.Insert(logModel);
            }
            return res;
        }


        public virtual List<T> SelectEx<T>(int index, int pageSize) where T : class, new()
        {
            DateTime t = DateTime.Now;
            List<new_fileds_code> fileds = new new_fileds_code().Select<new_fileds_code>().
                Where(o => o.table_name == tableName && o.isdelete == false).ToList();
            //Guid tableid = fileds.First().table_id;
            List<new_resource_info> infos = new new_resource_info().Select<new_resource_info>(index, pageSize);
            ConcurrentBag<T> res = new ConcurrentBag<T>();
            Type typeInfo = typeof(T);
            var properties = typeInfo.GetProperties().ToList();

            //计算机 内核数量 最大并发数
            ParallelOptions opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };


            //List<FiledAuth> filedAuths = GetFiledAuth(tableName, "0c945d67-c95d-4ecc-8e7a-3e63e040ec7a");
            Parallel.ForEach(infos, opt, info =>
              {
                  T model = new T();
                  //List<new_filed_modify_log> filedValues = total.Where(o => o.resource_id == info.id).ToList();
                  List<new_filed_modify_log> filedValues = new new_filed_modify_log(true).Select<new_filed_modify_log>(info.id).ToList();
                  Parallel.ForEach(properties, opt, p =>
                  {
                      new_fileds_code filed = fileds.Where(o => o.name == p.Name).FirstOrDefault();
                      
                      //if (filedAuths.Where(o => o.id == filed.id).FirstOrDefault().auth)
                      //{
                          var value = filedValues.Where(o => o.filed_id == filed.id).OrderByDescending(o => o.modifytime).FirstOrDefault().value;
                          Task.Run(() => { p.SetValue(model, convertValue(filed.utype, value)); }).ConfigureAwait(false);
                      //}
                  });
                  res.Add(model);
              });

            return res.ToList();
        }


        public virtual List<T> GetJson<T>(int index, int pageSize) where T:class,new()
        {

            //List<new_resource_json> jsonList = new new_resource_json().Select<new_resource_json>(index,pageSize,"json_value(jsonvalue,'$.modifytime')");
            List<new_resource_json_info> jsonList = new new_resource_json_info().Select<new_resource_json_info>(index, pageSize, "json_value(jsonvalue,'$.modifytime')");
            ParallelOptions opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };

            ConcurrentBag<T> res = new ConcurrentBag<T>();
            Parallel.ForEach(jsonList, opt, o => {
                res.Add(JsonHelper.Instance.Deserialize<T>(o.jsonvalue));
            });
            //List<T> res = (from o in jsonList
            //               select JsonHelper.Instance.Deserialize<T>(o.jsonvalue)).ToList();
            return res.ToList();
        }

        public List<T> StoredProcedure<T>(string storedProcedureName, DynamicParameters dynamicParameters)
        {
            return dbHelper.ExecuteStoredProcedureWithParms<T>(storedProcedureName, dynamicParameters);
        }

        #region private methods

        private dynamic convertValue(string uType, string value)
        {
            dynamic resValue = null;
            switch (uType.ToLower())
            {
                case "uniqueidentifier": resValue = Guid.Parse(value ?? new Guid().ToString()); break;
                case "int": resValue = int.Parse(value ?? "0"); break;
                case "bigint": resValue = Int64.Parse(value ?? "0"); break;
                case "numeric":
                case "money":
                case "decimal": resValue = decimal.Parse(value ?? "0"); break;
                case "bit": resValue = bool.Parse(value ?? "false"); break;
                case "date":
                case "datetime": resValue = DateTime.Parse(value ?? null); break;
                case "varbinary":
                case "image": resValue = (value ?? "").ToCharArray(); break;
                //默认字符串
                default: resValue = value ?? null; break;
            }
            return resValue;
        }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        /// <returns></returns>
        private string getTableName()
        {
            Type typeInfo = GetType();
            return typeInfo.Name;
        }

        private string countSql()
        {
            return $"select count(0) from {schema}.{tableName}";
        }
        private string selectSql(object resourceIdValue = null)
        {
            if(resourceIdValue == null)
                return $"select * from {schema}.{tableName}";
            else
                return $"select * from {schema}.{tableName} where resource_id=@id";
        }

        private string selectSql(int index, int pageSize, string orderFiled = "modifytime desc")
        {
            return $"select * from {schema}.{tableName} order by {orderFiled} offset {pageSize * (index - 1)} row fetch next {pageSize} rows only";
        }

        private List<FiledAuth> GetFiledAuth(string filedId, string accountId)
        {
            //ApiClient client = new ApiClient("http://192.168.0.89:8008/sys/auth/permission");
            //http://localhost:50012
            ApiClient client = new ApiClient("http://localhost:50012/sys/auth/permission");
            client.AddToken(accountId);
            var res = client.Get($"GetFiledAuth/{filedId}");
            Dictionary<string, object> dics = JsonHelper.Instance.Deserialize<Dictionary<string, object>>(res.Content);
            object authSql = dics["Model"];
            //return bool.Parse(authSql ?? "false");
            List<FiledAuth> resList = JsonHelper.Instance.Deserialize<List<FiledAuth>>(authSql.ToString());
            return resList;
        }


        #endregion
    }

    public class FiledAuth
    {
        public Guid id { get; set; }
        public bool auth { get; set; }
    }
}
