/*
 作者：徐剑
 */

using DemoService.Services.Interface.Zero;
using DemoService.Services.Interface.ZeroX;
using Models.Model;
using Models.Model.t6;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoService.Services.Implements.ZeroX
{
    public class ZeroX3Service : IZeroX3Service
    {
        IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public ZeroX3Service()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }
        public async Task<ResponseModel<t6_house>> QyeryMethod(int index, int pagesize)
        {
            string sql = $@"select f25.column1,f25.column2, f50.column26,f50.column27,
                            	   f75.column51,f75.column52, f100.column76,f100.column77,
                            	   f125.column101,f125.column102, f150.column126,f150.column127,
                            	   f175.column151, f200.column176
                            from Field25 as f25
                            join(select c2.column2
                            		   from (select column2
                            		   from Field25 
                            		   group by column2)as c2
                                 order by c2.column2 desc offset {pagesize * (index - 1)} row fetch next {pagesize} rows only
                                 ) as  column2
                            on f25.column2 = column2.column2
                            join Field50 as f50 on f50.column1 = f25.column1
                            join Field75 as f75 on f75.column1 = f25.column1
                            join Field100 as f100 on f100.column1 = f25.column1
                            join Field125 as f125 on f125.column1 = f25.column1
                            join Field150 as f150 on f150.column1 = f25.column1
                            join Field175 as f175 on f175.column1 = f25.column1
                            join Field200 as f200 on f200.column1 = f25.column1";
            List<t6_house> list = dataService.GetModelList<t6_house>(sql);

            /*-------------------------------------------------*/
            List<t6_house> result = new List<t6_house>();
            var group = list.GroupBy(o => o.column2);
            foreach (var item in group)
            {
                result.Add(item.Where(o => o.column1 == item.Max(j => j.column1)).FirstOrDefault());
            }
            return new ResponseModel<t6_house>(result);
            /*--------------------------------------------------*/
            /*---------------------另一种去重的方式-----------------------------
            list = list.Where(o => o.column1 == int.Parse(list.Where(j => j.column2 == o.column2).Max(k => k.column1).ToString())).ToList();
            return new ResponseModel<t6_house>(list);
            ----------------------------------------------------------*/
        }

        public async Task<ResponseModel<t6_house>> QyeryMethodLike(GenericQueryModel queryBody,int index, int pagesize)
        {

            return null;
        }
    }
}
