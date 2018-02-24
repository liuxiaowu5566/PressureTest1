using DemoService.Services.Interface.Zero;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
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

        [HttpGet, Route("QueryPage")]
        public ResponseModel<t6_house> QueryPage(int index = 1, int pagesize = 15)
        {
            return zeroService.QueryPage(index, pagesize);
        }
    }
}
