/*
 作者:徐章壹
 */
using DemoService.Services.Interface.Vertical;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Model.t3;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Controllers.Vertical
{
    [Route("Vertical/Test")]
    public class NineJsonController: Controller
    {
        public INineJsonService ninejsonService = null;
        public NineJsonController(INineJsonService ninejsonService)
        {
            this.ninejsonService = ninejsonService;
        }
        /// <summary>
        /// 90°+json分页查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("JsonHouse/{index}/{pagesize}")]
        public ResponseModel<T3_Part> JsonHouse(int index = 1, int pagesize = 15)
        {
            return ninejsonService.GetHousePart(index, pagesize);
        }
        /// <summary>
        /// 90°+json详细查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("House/{id}")]
        public ResponseModel<T3> House(string id)
        {
            return ninejsonService.GetHouse(id);
        }
    }
}
