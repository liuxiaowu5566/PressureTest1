/*
 作者：张宁
 */

using DemoService.Services.Interface.Zero;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoService.Controllers.Zero
{
    [Route("ZeroVertical/Test")]
    public class ZeroController : Controller
    {
        IZeroService zeroService = null;
        public ZeroController(IZeroService zeroService)
        {
            this.zeroService = zeroService;
        }

        [HttpGet, Route("House/{pageIndex}/{pagesize}")]
        public async Task<IActionResult> House(int pageIndex, int pagesize)
        {
            var result = await zeroService.GetHouse(pageIndex, pagesize);
            return this.Ok(result);
        }
        [HttpGet, Route("HouseInfo/{id}")]
        public async Task<IActionResult> HouseInfo(string id)
        {
            var result = await zeroService.GetHouseInfo(id);
            return this.Ok(result);
        }
    }




}
