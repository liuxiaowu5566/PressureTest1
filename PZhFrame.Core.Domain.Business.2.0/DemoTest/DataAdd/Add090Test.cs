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
        [Fact]
        public void AddHistoryData()
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            int id = 0;
            Parallel.For(1, 40, i => {
                Parallel.For(1, 5, j => {
                    Parallel.For(1, 5, k => {
                        id++;
                        Parallel.Invoke(()=> {
                            t5_history model = new t5_history()
                            {
                                id = id,
                                houseid = i,
                                codeid = j,
                                value = "column" + i.ToString() + "-" + j.ToString() + "-" + k.ToString(),
                                createtime = DateTime.Now,
                                opreatorid = Guid.NewGuid()
                            };
                            model.Insert();

                        });
                        
                    });
                });
            });
            st.Stop();


            //for (int i = 1; i <= 400000; i++)
            //{
            //    for (int j = 1; j <= 5; j++)
            //    {
            //        for (int k = 1; k <= 5; k++)
            //        {
            //            id++;
            //            t5_history model = new t5_history()
            //            {
            //                id = id,
            //                houseid = i,
            //                codeid = j,
            //                value = "column" + i.ToString() + "-" + j.ToString() + "-" + k.ToString(),
            //                createtime = DateTime.Now,
            //                opreatorid = Guid.NewGuid()
            //            };
            //            model.Insert();
            //        }
                    
            //    }
            //}
        }
    }
}
