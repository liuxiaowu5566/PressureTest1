/*
 作者:徐章壹,杨川
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DemoService.Services.Interface.Json;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Model.t2;

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
        /// Json格式200字段查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("JsonHouse/{id}")]
        public async Task<t2_house_expand_copy> GetJsonHouse(string id)
        {
            return await service.GetJsonHouse(id);
        }

        /// <summary>
        /// Json格式前九字段查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("JsonHousePart/{index}/{pagesize}")]
        public async Task<ResponseModel<t2_house_part_expand>> JsonHousePart(int index = 1, int pagesize = 15)
        {
            return await service.GetJsonHousePart(index, pagesize);
        }
    }
}
