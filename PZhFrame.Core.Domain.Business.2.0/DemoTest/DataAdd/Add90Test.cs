using Models.Model.t1;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace DemoTest.DataAdd
{
    public class Add90Test
    {
        [Fact]
        public void AddTest()
        {
            int id = 0;
            for (int i = 1; i <= 40000; i++)
            {
                t1_code code = new t1_code();
                PropertyInfo[] properties = code.GetType().GetProperties();
                int z = 1;
                foreach (var p in properties)
                {
                    t1_history model = new t1_history();
                    if (p.Name == "column2" ||
                        p.Name == "column3" ||
                        p.Name == "column4" ||
                        p.Name == "column5" ||
                        p.Name == "column6" ||
                        p.Name == "column7" ||
                        p.Name == "column8" ||
                        p.Name == "column9" ||
                        p.Name == "column10" ||
                        p.Name == "column11" ||
                        p.Name == "column12" ||
                        p.Name == "column13" ||
                        p.Name == "column14")
                    {
                        for (int j = 1; j <= 14; j++)
                        {
                            id = id + 1;
                            model.id = id;
                            model.codeid = z;
                            model.houseid = i;
                            model.value = $"{i}-{p.Name}{z}-{j}";
                            model.createtime = DateTime.Now;
                            model.opreatorid = Guid.NewGuid();
                        }
                    }
                    else
                    {
                        id = id + 1;
                        model.id = id;
                        model.codeid = z;
                        model.houseid = i;
                        model.value = $"{i}-column{z}";
                        model.createtime = DateTime.Now;
                        model.opreatorid = Guid.NewGuid();
                    }
                    model.Insert();
                    z = z + 1;
                }
            }
        }
    }
}
