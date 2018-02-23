using DemoService.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Controllers
{
    [Route("Test")]
    public class TestController : Controller
    {
        ITestService service = null;
        public TestController(ITestService service)
        {
            this.service = service;
        }
        [HttpGet,Route("Add")]
        public string Add()
        {
            //for(int i=0; i<10;i++)
            //{
            //    service.Test();
            //}
            return service.Test();
        }

        [HttpGet, Route("AddJson")]
        public string AddJson()
        {
            service.TestJson();
            return "hello";
        }

        [HttpPost, Route("AddJson")]
        public string AddJson([FromBody] b_house_basic_attribute model)
        {
            service.TestJson(model);
            return "hello";
        }

        [HttpGet,Route("House/{index}/{pagesize}")]
        //public List<b_house_basic_attribute> House(int index=1, int pagesize = 15)
        //{
        //    return service.GetHouse(index, pagesize);
        //}

        public ResponseModel<b_house_basic_attribute> House(int index = 1, int pagesize = 15)
        {
            return service.GetHouse(index, pagesize);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("JsonHouse/{index}/{pagesize}")]
        public ResponseModel<b_house_basic_attribute> JsonHouse(int index = 1, int pagesize = 15)
        {
            return service.GetJsonHouse(index, pagesize);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("HouseSP/{index}/{pagesize}")]
        public ResponseModel<b_house_basic_attribute1> HouseSp(int index = 1, int pagesize = 15)
        {
            return service.GetHouseByExecuteStoredProcedure(index, pagesize);
        }
    }
}
