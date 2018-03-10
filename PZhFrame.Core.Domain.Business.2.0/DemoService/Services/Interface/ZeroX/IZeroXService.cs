/*
 作者：徐剑
 */

using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Model.t6;
using PZhFrame.ModelLayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoService.Services.Interface.ZeroX
{
    public interface IZeroXService
    {
        Task<ResponseModel<t6_house>> QueryPage(int index, int pagesize);

        Task<ResponseModel<t6_house>> QueryPageLike(GenericQueryModel queryBody, int index, int pagesize);
    }
}
