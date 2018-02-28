using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTest
{
    public class Add090Test
    {
        [Fact]
        public void AddHistoryData()
        {
            int id = 0;
            for (int i = 1; i <= 400000; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    for (int k = 1; k <= 5; k++)
                    {
                        id++;
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
                    }
                    
                }
            }
        }
    }
}
