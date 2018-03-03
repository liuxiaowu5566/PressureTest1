using DemoService.Services.Interface.Mongo;
using Models.Model.Mongo;
using MongoDBHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.Mongo
{
    public class MongoService: IMongoService
    {
        IMongoDBHelper mongoDBHelper = null;
        private static readonly object locker = new object();
        public MongoService()
        {
            mongoDBHelper = MongoDBHelper.MongoDBHelper.GetInstance();
        }

        public List<table1> TestGetTable()
        {
            return mongoDBHelper.Select<table1>(o=>1==1);
        }

        public int AddHouse(int startId)
        {
            ConcurrentBag<houseinfo> houses = new ConcurrentBag<houseinfo>();
            Type typeInfo = typeof(houseinfo);
            var properties = typeInfo.GetProperties().ToList();
            Parallel.For(0, 10000, i => 
            {
                int id = 0;
                lock(locker)
                {
                    id = startId++;
                }
                houseinfo house = new houseinfo { _id=id};
                Parallel.ForEach(properties, p =>
                {
                    if(p.Name!="_id")
                        Task.Run(() => { p.SetValue(house, p.Name+"--"+id.ToString()); }).ConfigureAwait(false);
                });
                houses.Add(house);
            });
            int res = mongoDBHelper.Insert(houses.ToList());
            return res;
        }

        public string AddBigHouse(int size)
        {
            string reslut = null;
            try
            {
                byte[] ss = new byte[size];
                bighouseinfo house = new bighouseinfo { _id = 1, remark = ss.ToList() };
                int res = mongoDBHelper.Insert(house);
                if(res==1)
                {
                    mongoDBHelper.Delete<bighouseinfo>(o => o._id == 1);
                    reslut = "成功";
                }
                else
                {
                    reslut = "失败："+size;
                }

            }
            catch (Exception e)
            {
                reslut = e.Message;
            }
            return reslut;
        }

        public string AddBigHouse(string filePath)
        {
            string reslut = null;
            try
            {
                filePath = "D:\\" + filePath;
                List<byte> fileData = File.ReadAllBytes(filePath).ToList();
                bighouseinfo house = new bighouseinfo { _id = 1, remark = fileData };
                int res = mongoDBHelper.Insert(house);
                if (res == 1)
                {
                    mongoDBHelper.Delete<bighouseinfo>(o => o._id == 1);
                    reslut = "成功";
                }
                else
                {
                    reslut = "失败：" + filePath;
                }

            }
            catch (Exception e)
            {
                reslut = e.Message;
            }
            return reslut;
        }
    }

    
}
