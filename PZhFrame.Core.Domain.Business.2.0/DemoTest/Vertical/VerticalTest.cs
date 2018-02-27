using DemoService.Services.Implements.Vertical;
using DemoService.Services.Interface.Vertical;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
namespace DemoTest.Vertical
{
    public class VerticalTest
    {
        public IVerticalService verticalService = null;
        public VerticalTest()
        {
            verticalService = new VerticalService();
        }
        [Fact]
        public void QueryPageVerticalTime()
        {
            List<double> listTime = new List<double>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 1; i < 500; i = i + 100)
            {
                verticalService.GetHouse(i, 15);
            }
            sw.Stop();
            double totalMs = sw.Elapsed.TotalMilliseconds;
        }

        [Fact]
        public void QueryPageVerticalTimeEx()
        {
            List<double> listTime = new List<double>();
            
            for (int i = 1; i < 500; i = i + 100)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                verticalService.GetHouse(i, 15);
                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                listTime.Add(ts2.TotalMilliseconds);
            }
            
        }
    }
}
