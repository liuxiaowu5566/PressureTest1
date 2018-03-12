using DemoService.Services.Implements.Ninety;
using DemoService.Services.Interface.Ninety;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoTest.Ninety
{
    public class NinetyTest
    {
        INinetyService ninetyService = null;
        public NinetyTest()
        {
            ninetyService= new NinetyService();
        }
        /*
        [Fact]
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t1_history_nunety>> result = new List<ResponseModel<t1_history_nunety>>();
            for (int i = 1; i < 5; i = i + 1)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await ninetyService.QueryPageMethod(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
        */
        /*
        [Fact]
        public async void QueryPageMethodConcurrentTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t1_history_nunety>> result = new List<ResponseModel<t1_history_nunety>>();
            for (int i = 1; i < 5; i = i + 1)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await ninetyService.QueryPageMethodConcurrent(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
        */
        
        [Fact]
        public async void QueryPageLikeTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t1_history_nunety>> result = new List<ResponseModel<t1_history_nunety>>();
            for (int i = 1; i < 5; i = i + 1)
            {
                Stopwatch sw = new Stopwatch();
                GenericQueryModel queryBody = new GenericQueryModel()
                {
                    new GenericQueryItem() {Name = "column3", Value = "1",QueryType = "like" }
                };
                sw.Start();
                result.Add(await ninetyService.QueryPageLike(queryBody, i, 150));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
        
    }
}
