﻿using Models.Model.t4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DemoTest
{
   public class t4_Test
    {
        /*T4的电话*/
        [Fact]
        public void Addt4Tel()
        {
            int id = 0;

            for (int i = 1; i <= 40000; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    id++;
                    t4_tel model = new t4_tel();
                    foreach (var p in model.GetType().GetProperties().ToList())
                    {
                        if (p.Name == "column201")
                        {
                            p.SetValue(model, id);
                        }
                        else if (p.Name == "column202")
                        {
                            p.SetValue(model, i);
                        }
                        else
                        {
                            p.SetValue(model, p.Name.ToString() + "-" + i.ToString() + "-" + j.ToString());
                        }
                    }
                    model.Insert();
                }

            }
        }
    }
}
