using DemoService.Services.Implements.Json;
using DemoService.Services.Implements.NinetyAndJson;
using DemoService.Services.Interface.Json;
using DemoService.Services.Interface.NinetyAndJson;
using Models.Model;
using Models.Model.t2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace DemoTest.NinetyAndJson
{
    public class JsonDataTest
    {
        IJsonDataService jsonDataService = new JsonDataService();
        public JsonDataTest()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            List<double> listTime = new List<double>();
            List<ResponseModel<t2_house_part_expand>> result = new List<ResponseModel<t2_house_part_expand>>();
            for (int i = 1; i < 5; i = i + 1)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await jsonDataService.GetJsonHousePart(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}
