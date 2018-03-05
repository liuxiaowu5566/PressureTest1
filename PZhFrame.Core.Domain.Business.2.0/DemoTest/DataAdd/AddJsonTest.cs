using Models.Model;
using Models.Model.t2;
using PZhFrame.Core.Infrastructure.Lib;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DemoTest
{
    public class AddJsonTest
    {
        [Fact]
        public void Add()
        {
            Parallel(int i = 1; i <= 400000; i++)
            {
                ColumnModel MianModel = new ColumnModel();
                foreach (var p in MianModel.GetType().GetProperties().ToList())
                {
                    //µç»°
                    if (p.Name == "Column30")
                    {
                        List<T2_ModifyJsonModel> lst = new List<T2_ModifyJsonModel>();
                        for (int j = 1; j <= 4; j++)
                        {
                            T2_ModifyJsonModel tel = new T2_ModifyJsonModel();
                            foreach (var t in tel.GetType().GetProperties().ToList())
                            {
                                t.SetValue(tel, t.Name.ToString() + "-" + j.ToString());
                            }
                            lst.Add(tel);
                        }

                        p.SetValue(MianModel, lst);
                    }
                    else if (p.Name == "Column4"
                        || p.Name == "Column5"
                        || p.Name == "Column6"
                        || p.Name == "Column7"
                        || p.Name == "Column8"
                        )
                    {
                        List<T2_ModifyLogModel> lst = new List<T2_ModifyLogModel>();
                        for (int j = 1; j <= 15; j++)
                        {
                            T2_ModifyLogModel mod = new T2_ModifyLogModel();
                            foreach (var t in mod.GetType().GetProperties().ToList())
                            {
                                t.SetValue(mod, t.Name.ToString() + "-" + j.ToString());
                            }
                            lst.Add(mod);
                        }

                        p.SetValue(MianModel, lst);
                    }
                    else
                    {
                        p.SetValue(MianModel, p.Name.ToString() + "-" + i.ToString());
                    }

                }
                t2_house house = new t2_house();
                house.id = i;
                house.jsonstr = JsonHelper.Instance.Serialize(MianModel);
                house.Insert();

            }

        }
    }


   
}
