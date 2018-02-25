using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Json
{
    public interface IJsonDataService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ResponseModel<t2_house_part_expand> GetJsonHousePart(int index, int pageSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        t2_house_expand_copy GetJsonHouse(string id);
    }
}
