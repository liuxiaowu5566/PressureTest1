/*
 作者：徐剑
 */

using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System.Threading.Tasks;

namespace DemoService.Services.Interface.Ninety
{
    public interface INinetyService
    {
        /// <summary>
        /// 用sql语句进行分页，取最大，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        Task<ResponseModel<t1_history_nunety>> QueryPage(int index, int pagesize);

        /// <summary>
        /// 在程序中进行分页，取最大，表旋转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        Task<ResponseModel<t1_history_nunety>> QueryPageMethod(int index, int pagesize);

        /// <summary>
        /// 在程序中进行分页，取最大，表旋转（并发）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        Task<ResponseModel<t1_history_nunety>> QueryPageMethodParallel(int index, int pagesize);
    }
}
