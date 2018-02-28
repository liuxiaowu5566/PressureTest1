using Models.Model;
using Models.Model.t3;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Vertical
{
    public interface INineJsonService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ResponseModel<T3_Part> GetHousePart(int index, int pageSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseModel<T3> GetHouse(string id);
    }
}
