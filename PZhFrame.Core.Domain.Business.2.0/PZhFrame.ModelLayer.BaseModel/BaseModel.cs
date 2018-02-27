using Dapper;
using Newtonsoft.Json;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.Core.Infrastructure.Net;
using PZhFrame.Data.DapperHelper;
using PZhFrame.Data.Repository.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public virtual List<T> Select<T>(string sql)
        {
            // 去缓存服务（层）中取数据
            return dbHelper.Select<T>(sql);
        }

        /// <summary>
        /// select 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async virtual Task<List<T>> SelectAsync<T>(string sql)
        {
            // 去缓存服务（层）中取数据
            return await dbHelper.SelectAsync<T>(sql);
        }

        /// <summary>
        /// select 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> Select<T>(object idValue=null,string filename=null)
        {
            // 去缓存服务（层）中取数据
            return dbHelper.Select<T>(selectSql(idValue, filename),idValue);
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
        /// select 同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> SelectPart<T>(string tableName, int index, int pageSize, string orderFiled = " modifytime desc ")
        {
            Type typeInfo = typeof(T);
            var pros = typeInfo.GetProperties().ToList();

            string columns = " ";
            foreach (PropertyInfo item in typeInfo.GetProperties())
            {
                columns = columns + item.Name + ", ";
            }
            columns = columns.Substring(0, columns.Length - 2);
            // 去缓存服务（层）中取数据
            return dbHelper.Select<T>(selectSql(columns, tableName, index, pageSize, orderFiled));
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
        /// 存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="dynamicParameters"></param>
        /// <returns></returns>
        public List<T> StoredProcedure<T>(string storedProcedureName, DynamicParameters dynamicParameters)
        {
            return dbHelper.ExecuteStoredProcedureWithParms<T>(storedProcedureName, dynamicParameters);
        }

        

        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// 适用于没有新建实体之间
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="d">返回的实体</param>
        /// <param name="s">数据源实体</param>
        /// <returns></returns>
        public static D Mapper<D, S>(D d, S s)
        {
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
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
        private string selectSql(object resourceIdValue = null,string filename= "resource_id")
        {
            if(resourceIdValue == null)
                return $"select * from {schema}.{tableName}";
            else
                return $"select * from {schema}.{tableName} where {filename}=@id";
        }

        private string selectSql(int index, int pageSize, string orderFiled = "modifytime desc")
        {
            return $"select * from {schema}.{tableName} order by {orderFiled} offset {pageSize * (index - 1)} row fetch next {pageSize} rows only";
        }

        private string selectSql(string columns, string tableN, int index, int pageSize, string orderFiled = "modifytime desc")
        {
            return $"select {columns} from {schema}.{tableN} order by {orderFiled} offset {pageSize * (index - 1)} row fetch next {pageSize} rows only";
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
