using DemoService.Services.Implements.Zero;
using DemoService.Services.Interface.Zero;
using Models.Model;
using PZhFrame.Data.DataService;
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
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ApiResponse> result = new List<ApiResponse>();
            for (int i = 1; i < 500; i = i + 100)
            { 
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await zeroService.GetHouse(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }

    }
}
