/*
 作者：徐剑
 */

using Models.Model;
using Models.Model.t6;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoService.Services.Interface.ZeroX
{
    public interface IZeroX3Service
    {
        Task<ResponseModel<t6_house>> Qyery(int index, int pagesize);

        Task<ResponseModel<t6_house>> QyeryMethod(int index = 1, int pagesize = 15);
    }
}
