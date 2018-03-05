using Models.Model.t6;
using System.Linq;
using Xunit;

namespace DemoTest
{
    public class AddTable0Test6
    {
        [Fact]
        public void AddHouse()
        {
            int id = 0;
            int telid = 0;
            //房源
            for (int i = 1; i <= 400000; i++)
            {

                t6_house model = new t6_house();
                //每套房源修改5次
                for (int j =1; j <= 5; j++)
                {
                    foreach (var p in model.GetType().GetProperties().ToList())
                    {
                        if (j == 5)
                        {
                            if (p.Name == "Column200")
                            {
                                p.SetValue(model, "0");
                            }
                        }
                        else
                        {
                            if (p.Name == "Column200")
                            {
                                p.SetValue(model, "1");
                            }
                        }
                        if (p.Name == "Column1")
                        {
                            id++;
                            p.SetValue(model, id);
                        }
                        else if (p.Name == "Column2")
                        {
                            p.SetValue(model, i);
                        }
                        else if (p.Name == "Column10")
                        {
                            //每套房源4个联系电话
                            p.SetValue(model, i);
                            for (int k = 1; k <= 4; k++)
                            {
                                t6_tel telmodel = new t6_tel();

                                foreach (var t in telmodel.GetType().GetProperties().ToList())
                                {
                                    if (t.Name == "Column201")
                                    {
                                        telid++;
                                        t.SetValue(telmodel, telid);
                                    }
                                    else if (t.Name == "Column202")
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
                            p.SetValue(model, p.Name.ToString() + "-" + i.ToString());
                        }

                    }
                    model.Insert();
                }
            }
        }
    }
}
