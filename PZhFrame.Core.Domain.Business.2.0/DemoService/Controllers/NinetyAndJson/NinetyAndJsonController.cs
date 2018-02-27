/*
 作者：徐剑
 */

using DemoService.Services.Interface.NinetyAndJson;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;

namespace DemoService.Controllers.NinetyAndJson
{
    [Route("NinetyAndJson/Test")]
    public class NinetyAndJsonController : Controller
    {
        INinetyAndJsonService ninetyAndJsonService = null;
        public NinetyAndJsonController(INinetyAndJsonService ninetyAndJsonService)
        {
            this.ninetyAndJsonService = ninetyAndJsonService;
        }

        /// <summary>
        /// 查询所有字段，用sql进行分页，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryPage/{index}/{pagesize}")]
        public ResponseModel<t3_house_nunety> QueryPage(int index = 1, int pagesize = 15)
        {
            return ninetyAndJsonService.QueryPage(index, pagesize);
        }
    }
}
