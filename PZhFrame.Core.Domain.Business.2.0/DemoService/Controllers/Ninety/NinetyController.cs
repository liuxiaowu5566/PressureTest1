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
        /// 用sql语句进行分页，取最大，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryPage/{index}/{pagesize}")]
        public async Task<ResponseModel<t1_history_nunety>> QueryPage(int index = 1, int pagesize = 15)
        {
            return await zeroService.QueryPage(index, pagesize);
        }

        /// <summary>
        /// 在程序中进行分页，取最大，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet,Route("QueryPageMethod/{index}/{pagesize}")]
        public async Task<ResponseModel<t1_history_nunety>> QueryPageMethod(int index, int pagesize)
        {
            return await zeroService.QueryPageMethod(index,pagesize);
        }

        /// <summary>
        /// 在程序中进行分页，取最大，表旋转（并发）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryPageMethodParallel/{index}/{pagesize}")]
        public async Task<ResponseModel<t1_history_nunety>> QueryPageMethodParallel(int index, int pagesize)
        {
            return await zeroService.QueryPageMethodParallel(index, pagesize);
        }
    }
}
