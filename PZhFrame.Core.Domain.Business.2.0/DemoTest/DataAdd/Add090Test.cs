using DemoTest.DataAdd;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DemoTest
{
    public class Add090Test
    {
        private static readonly object locker = new object();
        [Fact]
        public void AddHistoryData()
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            List<t8_history> lst = new List<t8_history>();
            Stopwatch st = new Stopwatch();
            st.Start();
            int code = 0;
            Parallel.For(1, 10001, i =>
                {
                    Parallel.For(1, 6, j =>
                    {
                        Parallel.For(1, 6, k =>
                        {
                            Parallel.Invoke(() =>
                            {
                                int id;
                                lock (locker)
                                {
                                    id = ++code;
                                };
                                t8_history model = new t8_history(true)
                                {
                                    id = id,
                                    houseid = i,
                                    codeid = j,
                                    value = "column" + i.ToString() + "-" + j.ToString() + "-" + k.ToString(),
                                    createtime = DateTime.Now,
                                    opreatorid = Guid.NewGuid()
                                };
                                lst.Add(model);
                                model.Insert();
                            });
                        });
                    });
                });
            st.Stop();
            listTime.Add(st.Elapsed);
        }
    }
}
