using Models.Model.t6;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTest.DataDelete
{
    public class Delete0Test
    {
        [Fact]
        public void DeleteTest()
        {
            for (int i = 30; i <= 600000; i = i + 15)
            {
                t6_house model = new t6_house();
                model.column1 = i;
                model.Delete();
                for (int j = i * 4; j > i * 4 - 4; j--)
                {
                    t6_tel tel = new t6_tel();
                    tel.column201 = j;
                    tel.Delete();
                }
            }
        }
    }
}
