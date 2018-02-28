using Models.Model;
using Models.Model.t2;
using Models.Model.t4;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DemoTest
{
    public class Add0jsonTest
    {
        //添加表结构
        //[Fact]
        //public void Addt4_house()
        //{
        //    string sql = " create table t4_house (";
        //    for (int i = 1; i <= 200; i++)
        //    {
        //        sql += $" column{i} nvarchar(200) null, ";
        //    }
        //    sql += ")";

        //    //var ss = sercive.Execute(sql);
        //}

        [Fact]
        public void Addt4house()
        {

            for (int i = 1; i <= 400000; i++)
            {
                t4_house model = new t4_house();
                foreach (var p in model.GetType().GetProperties().ToList())
                {
                    if (p.Name == "Column1" || p.Name == "Column10")
                    {
                        p.SetValue(model, i);
                    }
                    else
                    {
                        p.SetValue(model, p.Name.ToString() + "-" + i.ToString());
                    }
                }
                model.Insert();
            }
        }
        [Fact]
        public void Addt4Tel()
        {
            int id = 0;

            for (int i = 1; i <= 400000; i++)
            {
                for (int j = 1; i <= 4; i++)
                {
                    id++;
                    t4_tel model = new t4_tel();
                    foreach (var p in model.GetType().GetProperties().ToList())
                    {
                        if (p.Name == "Column201")
                        {
                            p.SetValue(model, id);
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
        [Fact]
        public void Add()
        {
            int id = 0;
            for (int i = 1; i <= 400000; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    id++;
                    t4_json jsonmodel = new t4_json();
                    List<T2_ModifyLogModel> lst = new List<T2_ModifyLogModel>();
                    for (int k = 1; k <= 5; k++)
                    {
                        T2_ModifyLogModel mmodel = new T2_ModifyLogModel();
                        mmodel.Column205 = "Column205" + "-" + k.ToString() + j.ToString();
                        mmodel.Column206 = "Column206" + "-" + k.ToString() + j.ToString();
                        mmodel.Column207 = "Column207" + "-" + k.ToString() + j.ToString();
                        lst.Add(mmodel);
                    }
                    jsonmodel.id = id;
                    jsonmodel.houseid = i;
                    jsonmodel.codeid = j;
                    jsonmodel.jsonstr = JsonHelper.Instance.Serialize(lst); ;
                    jsonmodel.Insert();
                }

            }
        }
    }
}
