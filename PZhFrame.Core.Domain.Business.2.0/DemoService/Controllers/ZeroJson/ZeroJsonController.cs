/*
 作者：杨川 
 */
using DemoService.Services.Interface.Zero;
using DemoService.Services.Interface.ZeroJson;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Model.t4;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Controllers.ZeroJson
{
    [Route("ZeroJson/Test")]
    public class ZeroJsonController:Controller
    {
        IZeroJsonService zeroJsonService = null;
        public ZeroJsonController(IZeroJsonService zjService)
        {
            this.zeroJsonService = zjService;
        }

        /// <summary>
        /// 获取前9项分页查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryPage/{index}/{pagesize}")]
        public ResponseModel<T4_House_Part> QueryPage(int index = 1, int pagesize = 15)
        {
            return  zeroJsonService.QueryPage(index, pagesize);
        }

        /// <summary>
        /// 获取详情信息
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryInformation/{houseId}")]
        public T4_House_Show QueryInformation(string houseId)
        {
            return zeroJsonService.QueryInformation(houseId);
        }
    }
}
