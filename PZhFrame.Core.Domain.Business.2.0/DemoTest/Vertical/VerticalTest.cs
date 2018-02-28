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
            

            for (int i = 1; i < 101; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(verticalService.GetHouse(i, 15));
                sw.Stop();
                listTime.Add(sw.Elapsed.TotalMilliseconds);
            }
            
        }
        [Fact]
        public void QueryPageVerticalTimeEx()
        {
            List<ResponseModel<ColumnModel>> result = new List<ResponseModel<ColumnModel>>();
            List<double> listTime = new List<double>();
            List<int> list = new List<int> { 1, 2, 100, 200, 300 };
            foreach (var i in list)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(verticalService.GetHouse(i, 150));
                sw.Stop();
                listTime.Add(sw.Elapsed.TotalMilliseconds);
            }

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //result.Add(verticalService.GetHouse(300, 15));
            //sw.Stop();

            //double i = sw.Elapsed.TotalMilliseconds;

        }


        

    }
}
