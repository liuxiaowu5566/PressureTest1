using Models.Model;
using Models.Model.t1;
using System;
using Xunit;

namespace DemoTest
{

    public class AddTable90Test1
    {
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

        [Fact]
        public void AddHouseData()
        {
            for (int i = 1; i <= 400000; i++)
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
            for (int i = 137910; i <= 400000; i++)
            {
                for (int j = 1; j <= 200; j++)
                {
                    if (j == 4 || j == 5 || j == 6 || j == 7 || j == 8)
                    {
                        for (int k = 1; k <= 5; k++)
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
        public int id = 30340099;
        private void AddModel(int i, int j)
        {
            id++;
            t1_history history = new t1_history()
            {
                id = id,
                codeid = j,
                houseid = i,
                createtime = DateTime.Now,
                operatorid = Guid.NewGuid(),
                value = i.ToString() + "-" + "column" + j.ToString()

            };

            history.Insert();
        }
    }
}
