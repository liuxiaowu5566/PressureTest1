using Models.Model;
using Models.Model.t2;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Json
{
    public interface IJsonDataService
    {
        /// <summary>
        /// json格式前九字段查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ResponseModel<t2_house_part_expand> GetJsonHousePart(int index, int pageSize);
        /// <summary>
        /// json格式200字段查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        t2_house_expand_copy GetJsonHouse(string id);
    }
}
