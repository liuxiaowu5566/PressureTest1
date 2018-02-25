using System;
using System.Collections.Generic;
using System.Text;
using DemoService.Services.Interface.Json;
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace DemoService.Controllers.Json
{
    /// <summary>
    /// 
    /// </summary>
    [Route("Json/JsonData")]
    public class JsonDataController:Controller
    {
        IJsonDataService service = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public JsonDataController(IJsonDataService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 200字段查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("JsonHouse/{id}")]
        public t2_house_expand_copy GetJsonHouse(string id)
        {
            return service.GetJsonHouse(id);
        }

        /// <summary>
        /// 前九字段查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("JsonHousePart/{index}/{pagesize}")]
        public ResponseModel<t2_house_part_expand> JsonHousePart(int index = 1, int pagesize = 15)
        {
            return service.GetJsonHousePart(index, pagesize);
        }
    }
}
