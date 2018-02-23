using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Json
{
    public interface ITestService
    {
        string Test();
        void TestJson();
        void TestJson(b_house_basic_attribute model);
        ResponseModel<b_house_basic_attribute1> GetHouse(int index, int pageSize);
        ResponseModel<b_house_basic_attribute> GetJsonHouse(int index, int pageSize);
        ResponseModel<b_house_basic_attribute1> GetHouseByExecuteStoredProcedure(int pageIndex, int pageSize);
    }
}
