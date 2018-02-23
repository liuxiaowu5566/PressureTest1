using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services
{
    public interface ITestService
    {
        string Test();
        void TestJson();
        ResponseModel<b_house_basic_attribute> GetHouse(int index, int pageSize);
        ResponseModel<b_house_basic_attribute> GetJsonHouse(int index, int pageSize);
        ResponseModel<b_house_basic_attribute1> GetHouseByExecuteStoredProcedure(int pageIndex, int pageSize);
    }
}
