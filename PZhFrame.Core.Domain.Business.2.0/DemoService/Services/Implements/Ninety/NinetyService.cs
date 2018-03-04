/*
 作者：徐剑 
 */

using DemoService.Services.Interface.Ninety;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
        /// 用sql语句进行分页，取最大，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<ResponseModel<t1_history_nunety>> QueryPage(int index, int pagesize)
        {
            string sqlName = $@"declare @sql varchar(max)
                                select @sql = ISNULL(@sql+',','')+name
                                from t1_code
                                select @sql";
            string name = dataService.GetString(sqlName);
            string sql = $@"select *
                            from (select history.houseid,
                            			 code.name,
                            			 history.value
                            	  from (select codeid,
                            	  	  		   houseid,
                            	  	  		   value,
                            	  	  		   ROW_NUMBER() over(partition by codeid,houseid order by houseid,codeid desc) as rowNum
                            	  	    from t1_history
                            	  	    where houseid in (select id as houseid
                            							  from t1_house 
                            							  order by id desc offset {(pagesize * (index - 1))} row fetch next {pagesize} rows only
                            							  )
                            	  	  )as history
                            	  left join t1_code as code
                            	  on history.codeid = code.id
                            	  where history.rowNum = 1)as house
                            pivot (max(house.value) for name in ({name})) t
                            order by houseid desc ";
            List<t1_history_nunety> listModel = dataService.GetModelList<t1_history_nunety>(sql);

            return new ResponseModel<t1_history_nunety>(listModel);
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

        /// <summary>
        /// 在程序中进行分页，取最大，表旋转（并发）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<ResponseModel<t1_history_nunety>> QueryPageMethodParallel(int index, int pagesize)
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
            ParallelOptions opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };
            Parallel.ForEach(houseList, opt, house =>
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
                /*
                Parallel.ForEach(nameList, opt, name =>
                {
                    var value = name.Where(o => o.createtime == name.Max(j => j.createtime)).
                        Select(o => o.value).FirstOrDefault();//每组name取修改时间最大的一个
                    PropertyInfo Info = type.GetProperty(name.Key.ToLower());
                    if (Info != null)
                    {
                        Info.SetValue(model, value.ToString(), null);
                    }
                });
                listModel.Add(model);
                */
            });
            return new ResponseModel<t1_history_nunety>(listModel);
        }
    }
}
