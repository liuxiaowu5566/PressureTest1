using DemoService.Services.Interface.ZeroAndNinety;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Controllers.ZeroAndNinety
{
    [Route("ZeroAndNinety/Test")]
    public class ZeroAndNinetyController : Controller
    {
        IZeroAndNinetyService zeroAndNinetyService = null;
        public ZeroAndNinetyController(IZeroAndNinetyService zeroAndNinetyService)
        {
            this.zeroAndNinetyService = zeroAndNinetyService;
        }

        [HttpGet, Route("QueryPage1_9/{index}/{pagesize}")]
        public ResponseModel<string> QueryPage1_9(int index = 1, int pagesize = 15)
        {
            return zeroAndNinetyService.QueryPage1_9(index, pagesize);
        }
    }
}
