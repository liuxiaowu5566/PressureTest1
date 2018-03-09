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

namespace DemoService.Controllers.ZeroX
{
    [Route("ZeroX3/Test")]
    public class ZeroX3Controller : Controller
    {
        IZeroX3Service zeroX3Service = null;
        public ZeroX3Controller(IZeroX3Service zeroX3Service)
        {
            this.zeroX3Service = zeroX3Service;
        }
        [HttpGet, Route("Qyery/{index}/{pagesize}")]
        public async Task<ResponseModel<t6_house>> Qyery(int index = 1, int pagesize = 15)
        {
            return await zeroX3Service.Qyery(index, pagesize);
        }

        [HttpGet, Route("QyeryMethod/{index}/{pagesize}")]
        public async Task<ResponseModel<t6_house>> QyeryMethod(int index = 1, int pagesize = 15)
        {
            return await zeroX3Service.QyeryMethod(index, pagesize);
        }
    }
}
