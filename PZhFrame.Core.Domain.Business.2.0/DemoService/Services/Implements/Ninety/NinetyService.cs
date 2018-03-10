/*
 作者：徐剑 
 */

using DemoService.Services.Interface.Ninety;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                var nameList = house.GroupBy(o => o.name);
                foreach (var name in nameList)  //根据code分组然后遍历
                {
                    var value = name.Where(o => o.createtime == name.Max(j => j.createtime)).
                        Select(o => o.value).FirstOrDefault();//每组name取修改时间最大的一个
                    PropertyInfo Info = type.GetProperty(name.Key.ToLower());
                    if (Info != null)
                    {
                        Info.SetValue(model, value.ToString(), null);
                    }
                }
                listModel.Add(model);
            }
            return new ResponseModel<t1_history_nunety>(listModel);
        }

        public async Task<ResponseModel<t1_history_nunety>> QueryPageLike(string value, int index, int pagesize)
        {
            string sql = $@"select code.name,history.value,history.houseid,history.createtime
                            from t1_history as history
                            join (select houseid
                            	  from t1_history
                            	  where codeid = 3 and value like '%{value}%'
                            	  order by houseid desc offset {pagesize * (index - 1)} row fetch next {pagesize} rows only
                            	  ) as houseid
                            on history.houseid = houseid.houseid
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
                var nameList = house.GroupBy(o => o.name);
                foreach (var name in nameList)  //根据code分组然后遍历
                {
                    var v = name.Where(o => o.createtime == name.Max(j => j.createtime)).
                        Select(o => o.value).FirstOrDefault();//每组name取修改时间最大的一个
                    PropertyInfo Info = type.GetProperty(name.Key.ToLower());
                    if (Info != null)
                    {
                        Info.SetValue(model, v.ToString(), null);
                    }
                }
                listModel.Add(model);
            }
            return new ResponseModel<t1_history_nunety>(listModel);
        }
    }
}
