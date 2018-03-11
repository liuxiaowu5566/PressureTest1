/*
 作者：徐剑 
 */

using DemoService.Services.Interface.Ninety;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Model.t1;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoService.Services.Implements.Ninety
{
    public class NinetyService : INinetyService
    {
        IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public NinetyService()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }
        /// <summary>
        /// 在程序中进行分页，取最大，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<ResponseModel<t1_history_nunety>> QueryPageMethod(int index, int pagesize)
        {
            string sql = $@"select code.name,history.*
                            from (select hry.codeid,hry.houseid,hry.value,hry.createtime
                            	  from t1_history as hry
                            	  join (select top 15 id as houseid
                            				  from t1_house 
                            				  where id >=(select min(id) 
                            				  		      from (select  id 
                            				  			  	    from t1_house 
                            				  			  	    order by id desc offset {pagesize * (index - 1)} row fetch next {pagesize} rows only
                            				  			  	    ) as T
                            				  			 )
                            		   ) as  hid
                            	  on hry.houseid = hid.houseid
                            ) as history
                            left join t1_code as code
                            on history.codeid = code.id";
            List<t1_history_nunety> listModel = new List<t1_history_nunety>();
            List<nunety> nunetyModel = dataService.GetModelList<nunety>(sql);
            var houseList = nunetyModel.GroupBy(o => o.houseid);
            foreach (var house in houseList) //根据房ID分组然后遍历
            {
                t1_history_nunety model = new t1_history_nunety();
                model.houseid = house.Key;
                Type type = model.GetType();
                var codeList = house.GroupBy(o => o.name);
                foreach (var code in codeList)  //根据code分组然后遍历
                {
                    var value = code.Where(o => o.createtime == code.Max(j => j.createtime)).
                        Select(o => o.value).FirstOrDefault();//每组code取修改时间最大的一个
                    PropertyInfo Info = type.GetProperty(code.Key.ToLower());
                    if (Info != null)
                    {
                        Info.SetValue(model, value.ToString(), null);
                    }
                }
                listModel.Add(model);
            }
            return new ResponseModel<t1_history_nunety>(listModel);
        }

        /// <summary>
        /// 在程序中进行分页，取最大，表旋转(并发)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<ResponseModel<t1_history_nunety>> QueryPageMethodConcurrent(int index, int pagesize)
        {
            string sql = $@"select code.name,history.*
                            from (select hry.codeid,hry.houseid,hry.value,hry.createtime
                            	  from t1_history as hry
                            	  join (select top 15 id as houseid
                            				  from t1_house 
                            				  where id >=(select min(id) 
                            				  		      from (select  id 
                            				  			  	    from t1_house 
                            				  			  	    order by id desc offset {pagesize * (index - 1)} row fetch next {pagesize} rows only
                            				  			  	    ) as T
                            				  			 )
                            		   ) as  hid
                            	  on hry.houseid = hid.houseid
                            ) as history
                            left join t1_code as code
                            on history.codeid = code.id";
            List<t1_history_nunety> listModel = new List<t1_history_nunety>();
            List<nunety> nunetyModel = dataService.GetModelList<nunety>(sql);
            var houseList = nunetyModel.GroupBy(o => o.houseid);
            var objHouse = new Object();
            Parallel.ForEach(houseList, house => //根据房ID分组然后遍历
            {
                t1_history_nunety model = new t1_history_nunety();
                model.houseid = house.Key;
                Type type = model.GetType();
                var codeList = house.GroupBy(o => o.name);
                foreach(var code in codeList)  //根据code分组然后遍历
                {
                    var value = code.Where(o => o.createtime == code.Max(j => j.createtime)).
                        Select(o => o.value).FirstOrDefault();//每组code取修改时间最大的一个
                    PropertyInfo Info = type.GetProperty(code.Key.ToLower());
                    if (Info != null)
                    {
                        Info.SetValue(model, value.ToString(), null);
                    }
                }
                lock (objHouse)
                {
                    listModel.Add(model);
                }
            });
            return new ResponseModel<t1_history_nunety>(listModel);
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="queryBody"></param>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<ResponseModel<t1_history_nunety>> QueryPageLike(GenericQueryModel queryBody, int index, int pagesize)
        {
            string whereSql = WhereSql<t1_code,t1_history>(queryBody);
            string sql = $@"select code.name,history.value,history.houseid,history.createtime
                            from t1_history as history
                            join (select house.houseid
                            	  from ({whereSql}) as house
                            	  order by house.houseid desc offset {pagesize * (index - 1)} row fetch next {pagesize} rows only
                            	  ) as houseid
                            on history.houseid = houseid.houseid
                            left join t1_code as code
                            on history.codeid = code.id";
            List<t1_history_nunety> listModel = new List<t1_history_nunety>();
            List<nunety> nunetyModel = dataService.GetModelList<nunety>(sql);
            var houseList = nunetyModel.GroupBy(o => o.houseid);
            var objHouse = new Object();
            Parallel.ForEach(houseList, house => //根据房ID分组然后遍历
            {
                t1_history_nunety model = new t1_history_nunety();
                model.houseid = house.Key;
                Type type = model.GetType();
                var codeList = house.GroupBy(o => o.name);
                foreach (var code in codeList)  //根据code分组然后遍历
                {
                    var v = code.Where(o => o.createtime == code.Max(j => j.createtime)).
                        Select(o => o.value).FirstOrDefault();//每组name取修改时间最大的一个
                    PropertyInfo Info = type.GetProperty(code.Key.ToLower());
                    if (Info != null)
                    {
                        Info.SetValue(model, v.ToString(), null);
                    }
                }
                lock (objHouse)
                {
                    listModel.Add(model);
                }
            });
            return new ResponseModel<t1_history_nunety>(listModel);
        }


        /// <summary>
        /// 纯90度表条件拼接
        /// </summary>
        /// <typeparam name="CodeTable">字段表</typeparam>
        /// <typeparam name="T">值表</typeparam>
        /// <param name="queryBody">条件</param>
        /// <param name="resltName">最终结果的字段名</param>
        /// <param name="codeTableRelationName">字段表的关联字段名</param>
        /// <param name="codeTableNanmeName">字段表的条件字段名</param>
        /// <param name="tRelationName">值表的关联字段名</param>
        /// <param name="tValueName">值表值字段名</param>
        /// <returns></returns>
        private string WhereSql<CodeTable,T>(GenericQueryModel queryBody,
            string resltName = "houseid",
            string codeTableRelationName = "id" ,
            string codeTableNanmeName = "name",
            string tRelationName = "codeid",
            string tValueName = "value"
            ) where CodeTable : class
        {
            if (typeof(CodeTable).GetProperty(codeTableRelationName) == null || 
                typeof(CodeTable).GetProperty(codeTableNanmeName) == null ||
                typeof(T).GetProperty(tRelationName) == null ||
                typeof(T).GetProperty(resltName) == null
                )
            {
                return "";
            }
            List<CodeTable> list = dataService.GetModelList<CodeTable>($"select {codeTableRelationName},{codeTableNanmeName} from {typeof(CodeTable).Name}");
            string tTableName = typeof(T).Name;
            string whereSql = "del";
            string table = $" select {resltName} from {tTableName} where ";
            foreach (var item in queryBody)
            {
                switch (item.QueryType.Trim())
                {
                    case "like":
                        whereSql =
                        whereSql + " InterSect (" + table +
                        $@" {tRelationName} = '{list.Where(o => o.GetType().GetProperty("name").GetValue(o).ToString() == item.Name).
                        Select(o => o.GetType().GetProperty("id").GetValue(o)).FirstOrDefault()}' " +
                        " and " +
                        $" {tValueName} like '%{item.Value}%') ";
                        break;
                }
            }
            return whereSql.Replace("del InterSect", " ");
        }
    }
}
