using DemoService.Services.Interface.Mongo;
using Microsoft.AspNetCore.Mvc;
using Models.Model.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Controllers.Mongo
{
    [Route("Mongo/test")]
    public class MongoController: Controller
    {
        IMongoService mongoService = null;

        public MongoController(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        [HttpGet,Route("TestGetTable")]
        public List<table1> TestGetTable()
        {
            return mongoService.TestGetTable();
        }
        [HttpGet, Route("AddHouse")]
        public int AddHouse()
        {
            return mongoService.AddHouse(0);
        }
        [HttpGet, Route("AddBigHouse/{size}")]
        public string AddBigHouse(int size)
        {
            return mongoService.AddBigHouse(size);
        }

        [HttpGet, Route("AddBigHouseByPath/{path}")]
        public string AddBigHouseByPath(string path)
        {
            return mongoService.AddBigHouse(path);
        }
    }
}
