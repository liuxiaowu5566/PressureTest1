/*
 作者：张宁
 */

using Models.Model;
using PZhFrame.Data.DataService;
using System.Threading.Tasks;

namespace DemoService.Services.Interface.Zero
{
    public interface IZeroService
    {
        ResponseModel<Result> GetHouse(int pageIndex, int pageSize);


        Task<ApiResponse> GetHouseInfo(string id);
    }
}
