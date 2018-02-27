using DemoService.Services.Interface.Zero;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.WebModel;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Controllers.Zero
{
    [Route("Zero/Test")]
    public class ZeroController : Controller
    {
        IZeroService zeroService = null;
        public ZeroController(IZeroService zeroService)
        {
            this.zeroService = zeroService;
        }

        /// <summary>
        /// 每页15条查询1-9字段
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryPage1_9/{index}/{pagesize}")]
        public ResponseModel<t6_house1_9> QueryPage1_9(int index = 1, int pagesize = 15)
        {
            return zeroService.QueryPage1_9(index, pagesize);
        }
        [HttpGet, Route("QP1_9/{index}/{pagesize}")]
        public ResponseModel<t6_house1_9> QP1_9(int index = 1, int pagesize = 15)
        {
            return zeroService.QP1_9(index, pagesize);
        }

        /// <summary>
        /// 查询某个字段的历史记录
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet,Route("QueryRecord/{houseId}/{name}")]
        public List<string> QueryRecord(string houseId,string name)
        {
            return zeroService.que(houseId,name);
        }
    }
}
