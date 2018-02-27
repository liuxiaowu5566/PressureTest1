using Models.Model;
using Models.WebModel;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Zero
{
    public interface IZeroService
    {
        /// <summary>
        /// 每页15条查询1-9字段
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        ResponseModel<t6_house1_9> QueryPage1_9(int index, int pagesize);
        ResponseModel<t6_house1_9> QP1_9(int index, int pagesize);

        /// <summary>
        /// 查询某个字段的历史记录
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        List<string> que(string houseId, string name);
    }
}
