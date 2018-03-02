using DemoService.Services.Implements.NinetyAndJson;
using DemoService.Services.Implements.Vertical;
using DemoService.Services.Interface.NinetyAndJson;
using DemoService.Services.Interface.Vertical;
using Models.Model;
using Models.Model.t3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace DemoTest.NinetyAndJson
{
    public class NineJsonTest
    {
        INineJsonService nineJsonService = new NineJsonService();
        public NineJsonTest()
        {

        }

        [Fact]
        public void QueryPageTime()
        {
            List<Double> listTime = new List<Double>();
            List<ResponseModel<T3_Part>> result = new List<ResponseModel<T3_Part>>();            
            for (int i = 100; i < 600; i = i + 100)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(nineJsonService.GetHousePart(i, 150));                
                sw.Stop();
                listTime.Add(sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}
