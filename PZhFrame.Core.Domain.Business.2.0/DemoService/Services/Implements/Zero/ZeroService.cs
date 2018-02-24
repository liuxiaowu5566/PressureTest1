using DemoService.Services.Interface.Zero;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        public ResponseModel<t6_house> QueryPage(int index, int pagesize)
        {
            dataService.Get<t6_tel>("select * from t6_tel where column201 = 1");
            return null;
        }
    }
}
