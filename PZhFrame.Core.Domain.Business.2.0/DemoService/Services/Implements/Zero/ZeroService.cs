/*
 作者：张宁
 */

using DemoService.Services.Interface.Zero;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ApiResponse> GetHouse(int index, int pageSize)
        {
            List<Result> list = new List<Result>();
            List<t5_history> listHistory = new List<t5_history>();
            List<t5_history> listResult = new List<t5_history>();
            ApiResponse resp = new ApiResponse(null).OK();
            string sqlStr = $@"select * from t6_house order by column1 OFFSET {(pageSize) * ((index) - 1)} ROW FETCH NEXT {pageSize} rows only";
            list.AddRange(await dataService.GetAsync<Result>(sqlStr));
            foreach (var item in list)
            {
                string sqlH = $@"select * from t5_history where houseid = {item.column2}";
                listHistory.AddRange(await dataService.GetAsync<t5_history>(sqlH));
                listResult.AddRange(listHistory.Where((x, i) => listHistory.FindLastIndex(z => z.codeid == x.codeid) == i));
                foreach (var i in listResult)
                {
                    switch (i.codeid)
                    {
                        case 1:
                            item.column4 = i.value;
                            break;
                        case 2:
                            item.column5 = i.value;
                            break;
                        case 3:
                            item.column6 = i.value;
                            break;
                        case 4:
                            item.column7 = i.value;
                            break;
                        case 5:
                            item.column8 = i.value;
                            break;
                    }
                }
            }           
            resp.Model = list;
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
            List<t5_history> listHistory = new List<t5_history>();
            List<t5_history> listResult = new List<t5_history>();
            ApiResponse resp = new ApiResponse(null).OK();
            string sqlStr = $@"select * from t4_house where column2 = {id}";
            list.AddRange(await dataService.GetAsync<t4_house>(sqlStr));
            foreach (var item in list)
            {
                string sqlH = $@"select * from t5_history where houseid = {item.column2}";
                listHistory.AddRange(await dataService.GetAsync<t5_history>(sqlH));
                listResult.AddRange(listHistory.Where((x, i) => listHistory.FindLastIndex(z => z.codeid == x.codeid) == i));
                foreach (var i in listResult)
                {
                    switch (i.codeid)
                    {
                        case 1:
                            item.column4 = i.value;
                            break;
                        case 2:
                            item.column5 = i.value;
                            break;
                        case 3:
                            item.column6 = i.value;
                            break;
                        case 4:
                            item.column7 = i.value;
                            break;
                        case 5:
                            item.column8 = i.value;
                            break;
                    }
                }
            }
            resp.Model = list;
            return resp;
        }
    }
}
//Parallel.ForEach(list,item =>
//{
//    string sqlH = $@"select * from t5_history where houseid = {item.column2}";
//    listHistory.AddRange(dataService.GetModelList<t5_history>(sqlH));
//    listResult.AddRange(listHistory.Where((x, i) => listHistory.FindLastIndex(z => z.codeid == x.codeid) == i));
//    Parallel.ForEach(listResult, i =>
//    {
//        switch (i.codeid)
//        {
//            case 1:
//                item.column4 = i.value;
//                break;
//            case 2:
//                item.column5 = i.value;
//                break;
//            case 3:
//                item.column6 = i.value;
//                break;
//            case 4:
//                item.column7 = i.value;
//                break;
//            case 5:
//                item.column8 = i.value;
//                break;
//        }
//    });
//});