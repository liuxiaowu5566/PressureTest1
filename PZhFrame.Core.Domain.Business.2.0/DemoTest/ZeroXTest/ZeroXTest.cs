using DemoService.Services.Implements.ZeroX;
using DemoService.Services.Interface.ZeroX;
using System;
using System.Collections.Generic;
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
        public void QP1_9Time()
        {
            List<int> listTime = new List<int>();
            for (int i = 1; i < 500; i = i + 100)
            {
                int time = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                zeroXService.QP1_9(i, 15);
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
                zeroXService.QueryPage1_9(i,15);
                int t = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                listTime.Add(t - time);
            }
        }
    }
}
