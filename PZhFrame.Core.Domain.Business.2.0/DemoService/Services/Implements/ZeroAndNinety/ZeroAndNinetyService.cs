using DemoService.Services.Interface.ZeroAndNinety;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Implements.ZeroAndNinety
{
    public class ZeroAndNinetyService : IZeroAndNinetyService
    {
        IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public ZeroAndNinetyService()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }
        public ResponseModel<string> QueryPage1_9(int index, int pagesize)
        {

            return null;
        }
    }
}
