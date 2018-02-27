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
            for (int i = 10; i < 60; i = i + 10)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await ninetyAndJsonService.QueryPage(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
