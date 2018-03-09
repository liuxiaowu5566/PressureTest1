using Models.Model.t6;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DemoTest
{
    public class AddTable0Test
    {
        /*0度房源*/
        [Fact]
        public void AddHouse()
        {
            int id = 474390;
            int telid = 1897560;
            //房源
            for (int i = 31627; i <= 40000; i++)
            {
                t6_house model = new t6_house();
                //每套房源修改5次
                for(int j = 1;j<=15;j++)
                  {
                      foreach (var p in model.GetType().GetProperties().ToList())
                      {
                          if (j == 5)
                          {
                              if (p.Name == "column200")
                              {
                                  p.SetValue(model, "0");
                              }
                          }
                          else
                          {
                              if (p.Name == "column200")
                              {
                                  p.SetValue(model, "1");
                              }
                          }
                          if (p.Name == "column1")
                          {
                              id++;
                              p.SetValue(model, id);
                          }
                          else if (p.Name == "column2")
                          {
                              p.SetValue(model, i);
                          }

                        //电话
                        else if (p.Name == "column30")
                          {
                            //每套房源4个联系电话
                            p.SetValue(model, i);
                              for (int k = 1; k <= 4; k++)
                              {
                                  t6_tel telmodel = new t6_tel();

                                  foreach (var t in telmodel.GetType().GetProperties().ToList())
                                  {
                                      if (t.Name == "column201")
                                      {
                                          telid++;
                                          t.SetValue(telmodel, telid);
                                      }
                                      else if (t.Name == "column202")
                                      {
                                          t.SetValue(telmodel, id);
                                      }
                                      else
                                      {
                                          t.SetValue(telmodel, t.Name + "-" + j.ToString());
                                      }
                                  }
                                  telmodel.Insert();
                              }

                          }
                          else
                          {
                              if (p.Name == "column3" ||
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
                                  p.Name == "column14" ||
                                  p.Name == "column15" ||
                                  p.Name == "column16"
                                  )
                              {
                                  p.SetValue(model, p.Name.ToString() + "-" + i.ToString() + "-" + j.ToString());
                              }
                              else
                              {
                                  p.SetValue(model, p.Name.ToString() + "-" + i.ToString());
                              }
                          }
                      }
                      model.Insert();
                  }
            }
        }
    }
}
