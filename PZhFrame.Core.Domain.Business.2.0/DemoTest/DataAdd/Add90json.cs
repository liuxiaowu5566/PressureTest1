using DemoService.Services.Data;
using Models.Model;
using Models.Model.t2;
using Models.Model.t3;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.Data.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DemoTest
{
    public class Add90json
    {
        [Fact]
        public void AddJson()
        {
            for (int i = 285295; i <= 400000; i++)
            {
                for (int j = 1; j <= 200; j++)
                {
                    if (j == 4 || j == 5 || j == 6 || j == 7 || j == 8)
                    {
                        List<T2_ModifyLogModel> lst = new List<T2_ModifyLogModel>();
                        for (int k = 1; k <= 5; k++)
                        {
                            T2_ModifyLogModel mmodel = new T2_ModifyLogModel();
                            mmodel.Column205 = "Column205" + "-" + j.ToString() + k.ToString();
                            mmodel.Column206 = "Column206" + "-" + j.ToString() + k.ToString();
                            mmodel.Column207 = "Column207" + "-" + j.ToString() + k.ToString();
                            lst.Add(mmodel);
                        }
                        CreatModel(i, j, lst);
                    }
                    else if (j == 10)
                    {
                        List<T2_ModifyJsonModel> lst = new List<T2_ModifyJsonModel>();
                        for (int l = 1; l <= 4; l++)
                        {
                            T2_ModifyJsonModel tel = new T2_ModifyJsonModel();
                            foreach (var t in tel.GetType().GetProperties().ToList())
                            {
                                t.SetValue(tel, t.Name.ToString() + "-" + l.ToString());
                            }
                            lst.Add(tel);
                        }
                        CreatModel(i, j, lst);
                    }
                    else
                    {
                        T2_ModifyLogModel mmodel = new T2_ModifyLogModel();
                        mmodel.Column205 = "Column205" + "-" + j.ToString();
                        mmodel.Column206 = "Column206" + "-" + j.ToString();
                        mmodel.Column207 = "Column207" + "-" + j.ToString();
                        CreatModel(i, j, mmodel);
                    }
                }
            }
        }

        int id = 57058862;
        private void CreatModel(int i, int j, object lst)
        {
            id++;
            t3_json json = new t3_json();
            json.id = id;
            json.houseid = i;
            json.codeid = j;
            json.createtime = DateTime.Now;
            json.operatorid = Guid.NewGuid();
            json.jsonstr = JsonHelper.Instance.Serialize(lst);
            json.Insert();
        }
    }
}
