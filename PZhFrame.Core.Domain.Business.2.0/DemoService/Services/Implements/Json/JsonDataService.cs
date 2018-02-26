using DemoService.Services.Interface.Json;
using Models.Model;
using Newtonsoft.Json;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.Json
{
    public class JsonDataService : IJsonDataService
    {
        private IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();

        public JsonDataService()
        {
            dataService = new DataService(connection.ConnString(),connection.SqlType());
        }
        /// <summary>
        /// 前九字段查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseModel<t2_house_part_expand> GetJsonHousePart(int index, int pageSize)
        {
            string sql = $"select jsonstr from t2_house order by id offset {pageSize * (index - 1)} row fetch next {pageSize} rows only";
            ConcurrentBag<t2_house_part_expand> t2modelList = new ConcurrentBag<t2_house_part_expand>();
            var result = dataService.GetList<t2_house>(sql);
            ParallelOptions opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };
            Parallel.ForEach(result, opt, item =>
            {
                var model = JsonConvert.DeserializeObject<t2_house_expand>(item.jsonstr);
                t2_house_part_expand modeltemp = new t2_house_part_expand();
                Parallel.Invoke(() =>
                {
                    T2_ModifyLogModel column4 = model.column4.OrderByDescending(a => a.Column207).FirstOrDefault();
                    T2_ModifyLogModel column5 = model.column5.OrderByDescending(a => a.Column207).FirstOrDefault();
                    T2_ModifyLogModel column6 = model.column6.OrderByDescending(a => a.Column207).FirstOrDefault();
                    T2_ModifyLogModel column7 = model.column7.OrderByDescending(a => a.Column207).FirstOrDefault();
                    T2_ModifyLogModel column8 = model.column8.OrderByDescending(a => a.Column207).FirstOrDefault();
                    Mapper(modeltemp, model);
                    modeltemp.column4 = column4.Column205;
                    modeltemp.column5 = column5.Column205;
                    modeltemp.column6 = column6.Column205;
                    modeltemp.column7 = column7.Column205;
                    modeltemp.column8 = column8.Column205; t2modelList.Add(modeltemp);
                });
            });
           
            ResponseModel<t2_house_part_expand> resModel = new ResponseModel<t2_house_part_expand>(t2modelList.ToList());
            return resModel;
        }
        /// <summary>
        /// 单条200字段查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public t2_house_expand_copy GetJsonHouse(string id)
        {
            string str = $"select jsonstr from t2_house where JSON_VALUE(jsonstr,'$.Column1')='{id}'";
            var result = dataService.GetSingle<t2_house>(str);
            t2_house_expand ss = JsonConvert.DeserializeObject<t2_house_expand>(result.jsonstr);
            t2_house_expand_copy exp = new t2_house_expand_copy();
            Mapper(exp,ss);
            T2_ModifyLogModel column4 = ss.column4.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column5 = ss.column5.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column6 = ss.column6.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column7 = ss.column7.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column8 = ss.column8.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyJsonModel column10= ss.column10.OrderByDescending(a => a.Column204).FirstOrDefault();
            exp.column4 = column4.Column205;
            exp.column5 = column5.Column205;
            exp.column6 = column6.Column205;
            exp.column7 = column7.Column205;
            exp.column8 = column8.Column205;
            exp.column10 = column10;
            return exp;
        }
        /// <summary>
        /// copyentity 
        /// </summary>
        /// <param name="s"></param>
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
    }
}
