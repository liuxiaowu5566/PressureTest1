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
        IJsonDataService ninetyAndJsonService = new JsonDataService();
        public JsonDataTest()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t2_house_part_expand>> result = new List<ResponseModel<t2_house_part_expand>>();
            for (int i = 10; i < 60; i = i + 10)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await ninetyAndJsonService.GetJsonHousePart(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
