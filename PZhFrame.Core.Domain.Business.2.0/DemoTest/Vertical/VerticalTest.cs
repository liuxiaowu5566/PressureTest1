/*
 作者:孙靖武
 */
using DemoService.Services.Implements.Vertical;
using DemoService.Services.Interface.Vertical;
using Models.Model;
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
            List<ResponseModel<ColumnModel>> result = new List<ResponseModel<ColumnModel>>();
            List<double> listTime = new List<double>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 1; i < 500; i = i + 100)
            {
                result.Add(verticalService.GetHouse(i, 15));
            }
            sw.Stop();
            double totalMs = sw.Elapsed.TotalMilliseconds;
        }
    }
}
