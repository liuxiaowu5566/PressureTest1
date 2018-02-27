/*
 作者：徐剑
 */

using DemoService.Services.Interface.Zero;
using DemoService.Services.Interface.ZeroX;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoService.Controllers.ZeroX
{
    [Route("ZeroX/Test")]
    public class ZeroXController : Controller
    {
        IZeroXService zeroXService = null;
        public ZeroXController(IZeroXService zeroXService)
        {
            this.zeroXService = zeroXService;
        }

        /// <summary>
        /// 每页15条查询1-9字段
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryPage1_9/{index}/{pagesize}")]
        public async Task<ResponseModel<t6_house1_9>> QueryPage1_9(int index = 1, int pagesize = 15)
        {
            return await zeroXService.QueryPage1_9(index, pagesize);
        }
        [HttpGet, Route("QP1_9/{index}/{pagesize}")]
        public async Task<ResponseModel<t6_house1_9>> QP1_9(int index = 1, int pagesize = 15)
        {
            return await zeroXService.QP1_9(index, pagesize);
        }

        /// <summary>
        /// 查询某个字段的历史记录
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet,Route("QueryRecord/{houseId}/{name}")]
        public async Task<List<string>> QueryRecord(string houseId,string name)
        {
            return await zeroXService.que(houseId,name);
        }
    }
}
