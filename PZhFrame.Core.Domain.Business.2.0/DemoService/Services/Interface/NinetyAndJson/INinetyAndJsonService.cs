/*
 作者：徐剑
 */

using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System.Threading.Tasks;

namespace DemoService.Services.Interface.NinetyAndJson
{
    public interface INinetyAndJsonService
    {
        Task<ResponseModel<t3_house_nunety>> QueryPage(int index, int pagesize);
    }
}
