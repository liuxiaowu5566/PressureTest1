using DemoService.Services.Implements.Zero;
using DemoService.Services.Interface.Zero;
using Models.Model;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.Models.Models;
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
            List<double> listResult = new List<double>();
            List<double> listTime = new List<double>();
            //List<ResponseModel<t4_house>> result = new List<ResponseModel<t4_house>>();
            //for (int i = 1; i < 11; i = i + 10)
            //{
            //    Stopwatch sw = new Stopwatch();
            //    sw.Start();
            //    result.Add(await zeroService.GetHouse(i, 15));
            //    sw.Stop();
            //    listTime.Add(sw.Elapsed);
            //}
            List<ResponseModel<Result>> result = new List<ResponseModel<Result>>();
            for (int i = 1; i < 101; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(zeroService.GetHouse(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed.TotalMilliseconds);
            }
            listResult.AddRange(listTime);
        }
    }
}
