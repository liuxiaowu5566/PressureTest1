using DemoService.Services.Implements.ZeroX;
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
    public class ZeroX3Test
    {
        IZeroX3Service zeroX3Service = new ZeroX3Service();
        public ZeroX3Test()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<t6_house>> result = new List<ResponseModel<t6_house>>();
            for (int i = 1; i < 6; i = i + 1)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await zeroX3Service.QyeryMethod(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
