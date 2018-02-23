using DemoService.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("Test")]
    public class TestController : Controller
    {
        ITestService service = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public TestController(ITestService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("Add")]
        public string Add()
        {
            return service.Test();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("AddJson")]
        public string AddJson()
        {
            service.TestJson();
            return "hello";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("AddJson")]
        public string AddJson([FromBody] b_house_basic_attribute model)
        {
            service.TestJson(model);
            return "hello";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet,Route("House/{index}/{pagesize}")]
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
