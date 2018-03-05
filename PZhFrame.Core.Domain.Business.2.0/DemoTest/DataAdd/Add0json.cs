using Models;
using Models.Model;
using Models.Model.t2;
using Models.Model.t4;
using PZhFrame.Core.Infrastructure.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DemoTest
{
    public class Add0json
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
        private static readonly object locker = new object();
        [Fact]
        public void Add()
        {
            int code = 0;
            Parallel.For(1, 400001, i =>
            {
                Parallel.For(1, 6, j =>
                {
                    t4_json jsonmodel = new t4_json();
                    List<T2_ModifyLogModel> lst = new List<T2_ModifyLogModel>();
                    Parallel.For(1, 6, k =>
                    {
                        T2_ModifyLogModel mmodel = new T2_ModifyLogModel();
                        mmodel.Column205 = "Column205" + "-" + k.ToString() + j.ToString();
                        mmodel.Column206 = "Column206" + "-" + k.ToString() + j.ToString();
                        mmodel.Column207 = "Column207" + "-" + k.ToString() + j.ToString();
                        lst.Add(mmodel);
                    });
                    int id;
                    lock (locker)
                    {
                        id = ++code;
                    };
                    jsonmodel.id = id;
                    jsonmodel.houseid = i;
                    jsonmodel.codeid = j;
                    jsonmodel.jsonstr = JsonHelper.Instance.Serialize(lst); ;
                    jsonmodel.Insert();
                });
            });

            //for (int i = 1; i <= 400000; i++)
            //{
            //    for (int j = 1; j <= 5; j++)
            //    {
            //        t4_json jsonmodel = new t4_json();
            //        List<T2_ModifyLogModel> lst = new List<T2_ModifyLogModel>();
            //        for (int k = 1; i <= 5; i++)
            //        {
            //            T2_ModifyLogModel mmodel = new T2_ModifyLogModel();
            //            mmodel.Column205 = "Column205" + "-" + k.ToString() + j.ToString();
            //            mmodel.Column206 = "Column206" + "-" + k.ToString() + j.ToString();
            //            mmodel.Column207 = "Column207" + "-" + k.ToString() + j.ToString();
            //            lst.Add(mmodel);
            //        }
            //        jsonmodel.id = ++code;
            //        jsonmodel.houseid = i;
            //        jsonmodel.codeid = j;
            //        jsonmodel.jsonstr = JsonHelper.Instance.Serialize(lst); ;
            //        jsonmodel.Insert();
            //    }

            //}
        }
    }
}
