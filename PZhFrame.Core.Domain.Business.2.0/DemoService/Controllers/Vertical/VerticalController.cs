/*
 作者:孙靖武
 */
using DemoService.Services.Interface.Vertical;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Controllers.Vertical
{
    [Route("Vertical/Test")]
    public class VerticalController: Controller
    {
        public IVerticalService verticalService = null;
        public VerticalController(IVerticalService verticalService)
        {
            this.verticalService = verticalService;
        }

        [HttpGet,Route("Add")]
        public void Add()
        {
            verticalService.AddFiledCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet, Route("House/{index}/{pagesize}")]
        public ResponseModel<ColumnModel> House(int index = 1, int pagesize = 15)
        {
            return verticalService.GetHouse(index, pagesize);
        }
    }
}
