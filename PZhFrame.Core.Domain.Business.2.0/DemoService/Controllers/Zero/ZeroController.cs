using DemoService.Services.Interface.Zero;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
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
        
    }
}
