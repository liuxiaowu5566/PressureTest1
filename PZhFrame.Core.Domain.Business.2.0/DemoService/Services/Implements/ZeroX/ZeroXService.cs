/*
 作者：徐剑
 */

using DemoService.Services.Interface.Zero;
using DemoService.Services.Interface.ZeroX;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Model.t6;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

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
        public async Task<ResponseModel<t6_house>> QueryPage(int index, int pagesize)
        {
            string sql = $@"select house.column1,house.column2,house.column3,house.column4,house.column5,house.column6,house.column7,house.column8,house.column9,house.column10,house.column11,house.column12,house.column13,house.column14
                            from t6_house as house
                            join (select max(house.column1) as houseid
                            	  from t6_house house
                            	  join (select distinct column2 from t6_house order by column2 desc offset {pagesize * (index - 1)} ROW FETCH NEXT {pagesize} rows only) as c2
                            	  on house.column2 = c2.column2
                            	  group by house.column2) as id
                            on house.column1 = id.houseid";
            List<t6_house> list = dataService.GetModelList<t6_house>(sql);
            return new ResponseModel<t6_house>(list);
        }

        public async Task<ResponseModel<t6_house>> QueryPageLike(GenericQueryModel queryBody, int index, int pagesize)
        {
            string wheresql = GenericQueryAnalizer.Build(queryBody);
            string sql = $@"select house.column1,house.column2,house.column3,house.column4,house.column5,house.column6,house.column7,house.column8,house.column9,house.column10,house.column11,house.column12,house.column13,house.column14
                            from t6_house as house
                            join (select max(column1) as column1,column2
                            	  from t6_house
                            	  where 1=1 {wheresql}
                            	  group by column2
                            	  order by column1 desc offset {pagesize * (index - 1)} ROW FETCH NEXT {pagesize} rows only
                            	  ) as id
                            on house.column1 = id.column1";
            List<t6_house> list = dataService.GetModelList<t6_house>(sql);
            return new ResponseModel<t6_house>(list);
        }
    }
}
