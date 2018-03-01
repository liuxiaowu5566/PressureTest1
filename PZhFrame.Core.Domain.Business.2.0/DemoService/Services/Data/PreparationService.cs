
using Models.Model;
using Models.Model.t2;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Services.Data
{
    public class PreparationService
    {
        IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public PreparationService()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }

        public async Task<ResponseModel<t2_house>> SelectMatch()
        {
            return null;
            return new ResponseModel<t2_house>(null);
        }
        public int Add(t2_house model)
        {
            return model.Insert();
        }
    }
}
