using DemoService.Services.Implements.Ninety;
using DemoService.Services.Interface.Ninety;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace DemoTest.Ninety
{
    public class NinetyTest
    {
        INinetyService ninetyService = new NinetyService();
        public NinetyTest()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t1_history_nunety>> result = new List<ResponseModel<t1_history_nunety>>();
            for (int i = 100; i < 400; i = i + 100)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await ninetyService.QueryPage(i, 150));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
