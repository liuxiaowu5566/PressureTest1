using DemoService.Services.Implements.ZeroJson;
using DemoService.Services.Interface.ZeroJson;
using Models.Model;
using Models.Model.t4;
using Models.WebModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoTest.ZeroJson
{
    public class ZeroJsonTest
    {
        IZeroJsonService zeroJsonService = new ZeroJsonService();
        public ZeroJsonTest()
        {

        }

        [Fact]
        public async void QueryPageTime()
        {
            GenericQueryModel query = new GenericQueryModel();
            query.TryAddQuery("column3", "1", "like");

            List<TimeSpan> listTime = new List<TimeSpan>();
            List<ResponseModel<T4_House_Part>> result = new List<ResponseModel<T4_House_Part>>();
            for (int i = 100; i < 400; i = i + 100)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result.Add(await zeroJsonService.QueryPage(query,i, 150));
                sw.Stop();
                listTime.Add(sw.Elapsed);
            }
        }
    }
}
