using Models.Model;
using Models.Model.t1;
using System;
using Xunit;

namespace DemoTest
{

    public class AddTable90Test1
    {
        /*90度的字段*/
        [Fact]
        public void AddTableData()
        {
            for (int i = 1; i <= 200; i++)
            {
                string name = "column";
                t1_code code = new t1_code();
                code.id = i;
                code.name = name + i.ToString();
                code.Insert();
            }
        }
/*-----------------------------------------------------------------------------------------*/
        [Fact]
        public void AddHouseData()
        {
            for (int i = 1; i <= 40000; i++)
            {
                t1_house house = new t1_house();
                house.id = i;
                house.createtime = DateTime.Now;
                house.opreatorid = Guid.NewGuid();
                house.Insert();
            }
        }
        [Fact]
        public void AddHistoryData()
        {
            for (int i = 37085; i <= 40000; i++)
            {
                for (int j = 1; j <= 200; j++)
                {
                    if (j == 2 || j == 3 || j == 4 || j == 5 || j == 6|| j == 7 || j == 8 || j == 9 || j == 10 || j == 11|| j == 12 || j == 13 || j == 14 || j == 15)
                    {
                        for (int k = 1; k <= 14; k++)
                        {
                            AddModel(i, j);
                        }
                    }
                    else
                    {
                        AddModel(i, j);
                    }

                }
            }
        }
        public int id = 14166088;
        private void AddModel(int i, int j)
        {
            id++;
            t1_history history = new t1_history()
            {
                id = id,
                codeid = j,
                houseid = i,
                createtime = DateTime.Now,
                opreatorid = Guid.NewGuid(),
                value = i.ToString() + "-" + "column" + j.ToString()

            };

            history.Insert();
        }
    }
}
