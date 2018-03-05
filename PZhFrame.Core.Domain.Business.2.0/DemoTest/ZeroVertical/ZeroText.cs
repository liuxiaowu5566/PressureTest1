using DemoService.Services.Implements.Zero;
using DemoService.Services.Interface.Zero;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace DemoTest.Zero
{
    public class ZeroText
    {

        IZeroService zeroService = new ZeroService();
        public ZeroText()
        {

        }

        [Fact]
        public void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<TimeSpan> listResult = new List<TimeSpan>();
            List<ResponseModel<Result>> result = new List<ResponseModel<Result>>();
            for (int i = 100; i <301; i = i+100)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add( zeroService.GetHouse(i, 150));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
            listResult.AddRange(listTime);
        }
    }
}
