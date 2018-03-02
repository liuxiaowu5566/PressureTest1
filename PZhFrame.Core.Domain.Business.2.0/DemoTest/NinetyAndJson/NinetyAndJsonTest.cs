using DemoService.Services.Implements.NinetyAndJson;
using DemoService.Services.Interface.NinetyAndJson;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace DemoTest.NinetyAndJson
{
    public class NinetyAndJsonTest
    {
        INinetyAndJsonService ninetyAndJsonService = new NinetyAndJsonService();
        public NinetyAndJsonTest()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t3_house_nunety>> result = new List<ResponseModel<t3_house_nunety>>();
            for (int i = 1; i < 10; i = i + 1)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await ninetyAndJsonService.QueryPage(i, 150));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
