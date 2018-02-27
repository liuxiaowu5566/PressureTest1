/*
 作者：徐剑
 */

using DemoService.Services.Interface.Ninety;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System.Threading.Tasks;

namespace DemoService.Controllers.Ninety
{
    [Route("Ninety/Test")]
    public class NinetyController : Controller
    {
        INinetyService zeroService = null;
        public NinetyController(INinetyService zeroService)
        {
            this.zeroService = zeroService;
        }

        /// <summary>
        /// 查询所有字段，用sql进行分页，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryPage/{index}/{pagesize}")]
        public async Task<ResponseModel<t1_house_nunety>> QueryPage(int index = 1, int pagesize = 15)
        {
            return await zeroService.QueryPage(index, pagesize);
        }
    }
}
