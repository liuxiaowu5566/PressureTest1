/*
 作者：徐剑
 */

using Microsoft.AspNetCore.Mvc;
using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System.Threading.Tasks;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoService.Services.Interface.Ninety
{
    public interface INinetyService
    {
        /// <summary>
        /// 在程序中进行分页，取最大，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        Task<ResponseModel<t1_history_nunety>> QueryPageMethod(int index, int pagesize);
        
        Task<ResponseModel<t1_history_nunety>> QueryPageLike(string value, int index, int pagesize);
    }
}
