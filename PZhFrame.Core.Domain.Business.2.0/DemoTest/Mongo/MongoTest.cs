using DemoService.Services.Implements.Mongo;
using DemoService.Services.Interface.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTest.Mongo
{
    public class MongoTest
    {
        public IMongoService mongoService = null;
        public MongoTest()
        {
            mongoService = new MongoService();
        }
        [Fact]
        public void Add()
        {
            
            mongoService.AddHouse(0);
        }
    }
}
