/*
 作者：徐剑
 */

using DemoService.Services.Interface.Ninety;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
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

        public async Task<ResponseModel<t1_history_nunety>> QueryPage(int index, int pagesize)
        {
            string sqlName = $@"declare @sql varchar(max)
                                select @sql = ISNULL(@sql+',','')+name
                                from t1_code
                                select @sql";
            string name = dataService.GetString(sqlName);
            string sql = $@"select * 
                            from (select code.name,history.houseid,history.value from( 
                            			select id,
                            				   codeid,
                            				   houseid,
                            				   value,
                            				   createtime, 
                            				   ROW_NUMBER() over(partition by codeid,houseid order by houseid,codeid,createtime desc) as rowNum
                            			from t1_history 
                            			where houseid in (select distinct houseid as hid from t1_history order by houseid  offset {pagesize * (index - 1)} row fetch next {pagesize} rows only)
                            			)as history
                            left join t1_code as code
                            on history.codeid = code.id
                            where history.rowNum <= 1 
                            ) as code
                            pivot (max(code.value) for name in ({name})) t";
            List<t1_history_nunety> listModel = dataService.GetModelList<t1_history_nunety>(sql);

            return new ResponseModel<t1_history_nunety>(listModel);
        }
    }
}
