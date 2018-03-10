/*
 作者：徐剑
 */

using DemoService.Services.Interface.Zero;
using DemoService.Services.Interface.ZeroX;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Model.t6;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

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

        [HttpGet, Route("QueryPage/{index}/{pagesize}")]
        public async Task<ResponseModel<t6_house>> QueryPage(int index = 1, int pagesize = 15)
        {
            return await zeroXService.QueryPage(index, pagesize);
        }

        [HttpPost, Route("QueryPageLike/{index}/{pagesize}")]
        public async Task<ResponseModel<t6_house>> QueryPageLike([FromBody]GenericQueryModel queryBody, int index = 1, int pagesize = 15)
        {
            return await zeroXService.QueryPageLike(queryBody,index, pagesize);
        }
    }
}
