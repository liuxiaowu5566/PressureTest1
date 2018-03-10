using DemoService.Services.Implements.ZeroX;
using DemoService.Services.Interface.ZeroX;
using Models.Model;
using Models.Model.t6;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoTest.ZeroXTest
{
    public class ZeroXTest
    {
        IZeroXService zeroXService = new ZeroXService();
        public ZeroXTest()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t6_house>> result = new List<ResponseModel<t6_house>>();
            for (int i = 100; i < 301; i = i + 100)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await zeroXService.QueryPage(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }

        [Fact]
        public async void QueryPageLikeTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t6_house>> result = new List<ResponseModel<t6_house>>();
            
            for (int i = 100; i < 400; i = i + 100)
            {
                Stopwatch sw = new Stopwatch();
                GenericQueryModel queryBody = new GenericQueryModel()
                {
                    new GenericQueryItem() {Name = "column3", Value = i.ToString(),QueryType = "like" }
                };
                sw.Start();
                result.Add(await zeroXService.QueryPageLike(queryBody, i, 150));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
