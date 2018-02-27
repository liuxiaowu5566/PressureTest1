using DemoService.Services.Implements.Json;
using DemoService.Services.Implements.Zero;
using DemoService.Services.Interface.Json;
using DemoService.Services.Interface.Zero;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTest.ZeroTest
{
    public class ZeroTest
    {
        IZeroService zeroService = new ZeroService();
        public ZeroTest()
        {

        }

        [Fact]
        public void QP1_9Time()
        {
            List<int> listTime = new List<int>();
            for (int i = 1; i < 500; i = i + 100)
            {
                int time = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                zeroService.QP1_9(i, 15);
                int t = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                listTime.Add(t - time);
            }
        }

        [Fact]
        public void QueryPage1_9Time()
        {
            List<int> listTime = new List<int>();
            for (int i = 1; i < 500; i = i + 100)
            {
                int time = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                zeroService.QueryPage1_9(i,15);
                int t = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                listTime.Add(t - time);
            }
        }
    }
}
