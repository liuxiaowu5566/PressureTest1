using Models.Model;
using Models.Model.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Mongo
{
    public interface IMongoService
    {
        List<table1> TestGetTable();
        int AddHouse(int startId);
        string AddBigHouse(int size);
        string AddBigHouse(int id, string filePath);
        ResponseModel<houseinfo> GetHouse(int pageIndex = 1, int pagesize = 15, string oderbyFiled = "_id");
    }
}
