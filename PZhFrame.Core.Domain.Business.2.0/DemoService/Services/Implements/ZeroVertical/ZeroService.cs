/*
 作者：张宁
 */

using DemoService.Services.Interface.Zero;
using Models.Model;
using Models.Model.t4;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.Zero
{
    public class ZeroService : IZeroService
    {
        IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public ZeroService()
        {

            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryBody"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetHouse(int pageIndex, int pageSize)
        {
            // 获取修改的列
            List<t4_code> fileds = new t4_code().Select<t4_code>().ToList();
            // 获取前9项实体属性
            Type typeInfo = typeof(Result);
            var properties = typeInfo.GetProperties().ToList();
            ApiResponse resp = new ApiResponse(null).OK();
            //获取房子的初始信息
            List<Result> List = new Result().SelectPart<Result>(typeof(t4_house).Name, pageIndex, pageSize, "column1");
            //pageSize数进行循环,依次进行判断是否有修改
            Parallel.ForEach(List, item =>
            {
                //根据houseid获取history的信息
                List<t5_history> listHistory = new t5_history(true).Select<t5_history>(item.column1, "houseid");
                //去处重复留下最新的记录
                listHistory = (listHistory.Where((x, i) => listHistory.FindLastIndex(z => z.codeid == x.codeid) == i)).ToList();
                Parallel.ForEach(listHistory, i =>
                {
                    t4_code columnName = fileds.Where(o => o.id == i.codeid).FirstOrDefault();//判断code                    
                    PropertyInfo proInfo = properties.Where(o => o.Name == columnName.name).FirstOrDefault();//判断code和house
                    if (proInfo != null)
                    {
                        proInfo.SetValue(item, i.value);//添加新的信息
                    }
                });
            });
            resp.Model = List;
            return resp;
        }

        /// <summary>
        /// 详细信息的查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetHouseInfo(string id)
        {
            List<t4_house> list = new List<t4_house>();
            List<t4_code> fileds = new t4_code().Select<t4_code>().ToList();
            Type typeInfo = typeof(t4_house);
            var properties = typeInfo.GetProperties().ToList();
            List<t5_history> listHistory = new List<t5_history>();
            ApiResponse resp = new ApiResponse(null).OK();
            string sqlStr = $@"select * from t4_house where column1 = {id}";
            list.AddRange(await dataService.GetAsync<t4_house>(sqlStr));
            foreach (var item in list)
            {
                string sqlH = $@"select * from t5_history where houseid = {item.column1}";
                listHistory = (await dataService.GetAsync<t5_history>(sqlH)).ToList();
                listHistory = (listHistory.Where((x, i) => listHistory.FindLastIndex(z => z.codeid == x.codeid) == i)).ToList();
                Parallel.ForEach(listHistory, i =>
                {
                    t4_code columnName = fileds.Where(o => o.id == i.codeid).FirstOrDefault();
                    PropertyInfo proInfo = properties.Where(o => o.Name == columnName.name).FirstOrDefault();
                    if (proInfo != null)
                    {
                        proInfo.SetValue(item, i.value);
                    }
                });
            }
            resp.Model = list;
            return resp;
        }
    }
}
