using DemoService.Services.Implements.ZeroJson;
using DemoService.Services.Interface.ZeroJson;
using Models.Model;
using Models.Model.t4;
using Models.WebModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace DemoTest.ZeroJson
{
    public class ZeroJsonTest
    {
        IZeroJsonService zeroJsonService = new ZeroJsonService();
        public ZeroJsonTest()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            
            for (int i = 1; i < 500; i = i + 100)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                ResponseModel<T4_House_Part> result= await zeroJsonService.QueryPage(i, 15);
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
