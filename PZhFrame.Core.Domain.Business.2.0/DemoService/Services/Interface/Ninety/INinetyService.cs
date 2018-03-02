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
        Task<ResponseModel<t1_history_nunety>> QueryPage(int index, int pagesize);

        Task<ResponseModel<t1_history_nunety>> QueryPageMethod(int index, int pagesize);
    }
}
