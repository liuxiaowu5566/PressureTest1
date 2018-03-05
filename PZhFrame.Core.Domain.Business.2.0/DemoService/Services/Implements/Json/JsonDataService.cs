/*
 作者:徐章壹,杨川
 */
using DemoService.Services.Interface.Json;
using Models.Model;
using Models.Model.t2;
using Models.Model.t4;
using Newtonsoft.Json;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public async Task<ResponseModel<t2_house_part_expand>> GetJsonHousePart(int index, int pageSize)
        {
            //string sql;
            //if (index > 1)
            //{
            //    sql = $"select top {pageSize} * from t2_house where id > (select max(id) from (select top(({index} - 1) * {pageSize}) id from t2_house order by id) as T) order by id";
            //}
            //else
            //{
            //    sql = $"select top {pageSize} * from t2_house";
            //}
            string sql = $"select jsonstr from t2_house order by id offset {pageSize * (index - 1)} row fetch next {pageSize} rows only";
            //string sql = $"select top {pageSize} jsonstr from t2_house where id >(select max(id) from (select id from t2_house order by id offset ({pageSize} * ({index} - 1)) row fetch next {pageSize} rows only) as T) order by id";
            ConcurrentBag<t2_house_part_expand> t2modelList = new ConcurrentBag<t2_house_part_expand>();
            var result = await dataService.GetListAsync<t2_house>(sql);
            ParallelOptions opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };
            //string data = "Data Source=192.168.0.89 ; Initial Catalog = PressureTest; Persist Security Info=True; User ID = sa; Password=qj12345678@";
            //ConcurrentBag<t2_house> cb = new ConcurrentBag<t2_house>();
            //using (SqlConnection conn = new SqlConnection(data))
            //{
            //    conn.Open();
            //    string sql = $"select top {pageSize} jsonstr from t2_house where id >(select max(id) from (select id from t2_house order by id offset ({pageSize} * ({index} - 2)) row fetch next {pageSize} rows only) as T) order by id";
            //    SqlCommand command = new SqlCommand(sql, conn);
            //    DataTable dt = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter(command);
            //    da.Fill(dt);
            //    Parallel.For(0, dt.Rows.Count, i =>
            //    {
            //        Parallel.Invoke(() =>
            //        {
            //            cb.Add(new t2_house { jsonstr = dt.Rows[i][0].ToString() });
            //        });
            //    });
            //}
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
                    BaseModel.Mapper(modeltemp, model);
                    modeltemp.column4 = column4.Column205;
                    modeltemp.column5 = column5.Column205;
                    modeltemp.column6 = column6.Column205;
                    modeltemp.column7 = column7.Column205;
                    modeltemp.column8 = column8.Column205;
                    t2modelList.Add(modeltemp);
                });
            });
            ResponseModel<t2_house_part_expand> resModel = new ResponseModel<t2_house_part_expand>(t2modelList.ToList().OrderByDescending(o=>o.column1).ToList());
            return resModel;
        }
        /// <summary>
        /// 单条200字段查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<t2_house_expand_copy> GetJsonHouse(string id)
        {
            string str = $"select jsonstr from t2_house where JSON_VALUE(jsonstr,'$.Column1')='{id}'";
            var result = await dataService.GetSingleAsync<t2_house>(str);
            t2_house_expand t2Json = JsonConvert.DeserializeObject<t2_house_expand>(result.jsonstr);

            t2_house_expand_copy exp = new t2_house_expand_copy();
            BaseModel.Mapper(exp, t2Json);

            T2_ModifyLogModel column4 = t2Json.column4.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column5 = t2Json.column5.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column6 = t2Json.column6.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column7 = t2Json.column7.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyLogModel column8 = t2Json.column8.OrderByDescending(a => a.Column207).FirstOrDefault();
            T2_ModifyJsonModel column10= t2Json.column10.OrderByDescending(a => a.Column204).FirstOrDefault();
            exp.column4 = column4.Column205;
            exp.column5 = column5.Column205;
            exp.column6 = column6.Column205;
            exp.column7 = column7.Column205;
            exp.column8 = column8.Column205;
            exp.column10 = column10;
            return exp;
        }
    }
}
