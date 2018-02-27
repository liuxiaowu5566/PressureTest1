using DemoService.Services.Implements.NinetyAndJson;
using DemoService.Services.Interface.NinetyAndJson;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTest.NinetyAndJson
{
    public class NinetyAndJsonTest
    {
        INinetyAndJsonService ninetyAndJsonService = new NinetyAndJsonService();
        public NinetyAndJsonTest()
        {

        }

        [Fact]
        public void QueryPageTime()
        {
            List<int> listTime = new List<int>();
            for (int i = 1; i < 500; i = i + 100)
            {
                int time = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                ninetyAndJsonService.QueryPage(i, 15);
                int t = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                listTime.Add(t - time);
            }
        }
    }
}
