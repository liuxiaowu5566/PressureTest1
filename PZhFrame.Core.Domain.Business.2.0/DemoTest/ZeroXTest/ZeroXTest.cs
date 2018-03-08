﻿using DemoService.Services.Implements.ZeroX;
using DemoService.Services.Interface.ZeroX;
using Models.Model;
using Models.Model.t6;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

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

        //[Fact]
        //public async void QueryPage1_9Time()
        //{
        //    List<TimeSpan> listTime = new List<TimeSpan>();
        //    List<ResponseModel<t6_house1_9>> result = new List<ResponseModel<t6_house1_9>>();
        //    for (int i = 1; i < 3; i = i + 1)
        //    {
        //        Stopwatch sw = new Stopwatch();
        //        sw.Start();
        //        result.Add(await zeroXService.QueryPage1_9(i,150));
        //        sw.Stop();
        //        listTime.Add(sw.Elapsed);
        //    }
        //}
    }
}
