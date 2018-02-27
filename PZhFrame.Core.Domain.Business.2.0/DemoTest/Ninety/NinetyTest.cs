using DemoService.Services.Implements.Ninety;
using DemoService.Services.Interface.Ninety;
using System;
using System.Collections.Generic;
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
        public void QueryPageTime()
        {
            List<int> listTime = new List<int>();
            for (int i = 1; i < 500; i = i + 100)
            {
                int time = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                ninetyService.QueryPage(i, 15);
                int t = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                listTime.Add(t - time);
            }
        }
    }
}
