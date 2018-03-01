/*
 作者:孙靖武
 */
using Dapper;
using DemoService.Services.Interface.Vertical;
using Models.Model;
using Models.Model.t1;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.Vertical
{
    public class VerticalService : IVerticalService
    {
        public void AddFiledCode()
        {
            for(int i=1;i<20100;i++)
            {
                t1_resource model = new t1_resource { createtime=DateTime.Now};
                model.InsertAutoId();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseModel<ColumnModel> GetHouse(int index, int pageSize)
        {
            List<ColumnModel> hList = selectExVertical<ColumnModel>(index, pageSize).OrderByDescending(o=>o.Column1).ToList();
            ResponseModel<ColumnModel> resModel = new ResponseModel<ColumnModel>(hList);
            return resModel;
        }
        public ResponseModel<ColumnModel> GetHouseSP(int pageIndex, int pageSize)
        {
            string storedProcedureName = "ROW2COL";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@resourceTableName", "t1_house", System.Data.DbType.String);
            parameters.Add("@pageIndex", pageIndex, System.Data.DbType.Int32);
            parameters.Add("@pageSize", pageSize, System.Data.DbType.Int32);
            List<ColumnModel> hList = new t1_house().StoredProcedure< ColumnModel>(storedProcedureName, parameters);
            ResponseModel<ColumnModel> resModel = new ResponseModel<ColumnModel>(hList);
            return resModel;
        }
        private List<T> selectExVertical<T>(int index, int pageSize) where T : class, new()
        {
            List<t1_code> fileds = new t1_code().Select<t1_code>().ToList();
            //Guid tableid = fileds.First().table_id;
            List<t1_house> infos = new t1_house().Select<t1_house>(index, pageSize, "createtime desc");
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
                List<t1_history> filedValues = new t1_history(true).Select<t1_history>(info.id, "houseid").ToList();
                Parallel.ForEach(properties, opt, p =>
                {
                    t1_code filed = fileds.Where(o => o.name == p.Name).FirstOrDefault();

                    //if (filedAuths.Where(o => o.id == filed.id).FirstOrDefault().auth)
                    //{
                    var value = filedValues.Where(o => o.codeid == filed.id).FirstOrDefault().value;
                    Task.Run(() => { p.SetValue(model, value); }).ConfigureAwait(false);
                    //}
                });
                res.Add(model);
            });

            return res.ToList();
        }

    }
    
}
