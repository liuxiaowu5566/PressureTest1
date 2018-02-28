using Models.Model.t1;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTest.DataAdd
{
    public class T1_table
    {
        [Fact]
        public void AddT1_House()
        {
            for (int i = 1; i <= 400000; i++)
            {
                t1_house table = new t1_house()
                {
                    id = i,
                    createtime = DateTime.Now,
                    opreatorid = Guid.NewGuid()
                };
                table.Insert();
            }
        }
    }
}
