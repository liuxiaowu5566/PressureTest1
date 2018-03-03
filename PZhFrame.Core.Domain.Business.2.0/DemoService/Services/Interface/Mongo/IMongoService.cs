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
        string AddBigHouse(string filePath);
    }
}
