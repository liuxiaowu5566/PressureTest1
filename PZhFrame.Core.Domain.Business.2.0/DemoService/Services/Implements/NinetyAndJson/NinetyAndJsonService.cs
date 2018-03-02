/*
 作者：徐剑
 */

using DemoService.Services.Interface.NinetyAndJson;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.NinetyAndJson
{
    public class NinetyAndJsonService : INinetyAndJsonService
    {
        IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public NinetyAndJsonService()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }
        public async Task<ResponseModel<t3_house_nunety>> QueryPage(int index, int pagesize)
        {
            string sqlName = $@"declare @sql varchar(max)
                                select @sql = ISNULL(@sql+',','')+name
                                from t1_code
                                select @sql";
            string name = dataService.GetString(sqlName);
            int number = Regex.Split(name, ",", RegexOptions.IgnoreCase).Length;
            string sql = $@"select *
                            from (select code.name,
                            	   		 houseid,
                            			 dbo.json_value_max(jsonstr,'Column205','Column206') as value
                            	   		 from t3_json as json
                            			 left join t1_code as code
                            			 on json.codeid = code.id
                            	   		 order by houseid  offset {(pagesize * number) * (index - 1)} row fetch next {pagesize * number} rows only
                                 ) as res
                            pivot (max(res.value) for name in ({name})) t
                            ";
            List<t3_house_nunety> listModel = dataService.GetModelList<t3_house_nunety>(sql);
            return new ResponseModel<t3_house_nunety>(listModel);
        }
    }
}
