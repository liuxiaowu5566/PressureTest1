/*
 作者：徐剑
 */

using DemoService.Services.Interface.Zero;
using DemoService.Services.Interface.ZeroX;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.ZeroX
{
    public class ZeroXService : IZeroXService
    {
        IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public ZeroXService()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }
        
        /// <summary>
        /// 每页15条查询1-9字段
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<ResponseModel<t6_house1_9>> QueryPage1_9(int index, int pagesize)
        {
            string sql = $@"select distinct column2 from t6_house order by column2 offset {pagesize * (index - 1)} row fetch next {pagesize} rows only";
            List<t6_house1_9> list = dataService.GetModelList<t6_house1_9>(sql);
            List<t6_house1_9> modelList = new List<t6_house1_9>();
            foreach (var item in list)
            {
                sql = $@"select top 1  house.*
                         from (select column1,column2,column3,column4,column5,column6,column7,column8,column9 
                         	  from t6_house where column2 ='{item.column2}') as house
                         order by house.column9 desc";
                t6_house1_9 model = dataService.GetSingle<t6_house1_9>(sql);
                modelList.Add(model);
            }
            return new ResponseModel<t6_house1_9>(modelList);
        }
        public async Task<ResponseModel<t6_house1_9>> QP1_9(int index, int pagesize)
        {
            string sql = $@"select house.column1,house.column2,house.column3,house.column4,house.column5,house.column6,house.column7,house.column8,house.column9  
                                    from t6_house as house
                                    join (select max(column1) column1
                                    	  from t6_house
                                    	  group by column2) as id
                                    on house.column1 = id.column1
                                    order by house.column9 desc offset {pagesize * (index - 1)} row fetch next {pagesize} rows only";
            List<t6_house1_9> list = dataService.GetModelList<t6_house1_9>(sql);
            return new ResponseModel<t6_house1_9>(list);
        }

        /// <summary>
        /// 查询某个字段的历史记录
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<string>> que(string houseId, string name)
        {
            string sql = $@"select {name} from t6_house where column2 = '{houseId}'";
            List<string> listV = dataService.GetLstStr(sql);
            return listV;
        }
    }
}
