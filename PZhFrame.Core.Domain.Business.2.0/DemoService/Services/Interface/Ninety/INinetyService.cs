/*
 作者：徐剑
 */

using Models.Model;
using PZhFrame.ModelLayer.Models.Models;

namespace DemoService.Services.Interface.Ninety
{
    public interface INinetyService
    {
        ResponseModel<t1_house_nunety> QueryPage(int index, int pagesize);
    }
}
