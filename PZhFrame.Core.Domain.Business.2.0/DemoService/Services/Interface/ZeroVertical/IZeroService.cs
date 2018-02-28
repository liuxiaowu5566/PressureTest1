/*
 作者：张宁
 */

using PZhFrame.Data.DataService;
using System.Threading.Tasks;

namespace DemoService.Services.Interface.Zero
{
    public interface IZeroService
    {
        Task<ApiResponse> GetHouse(int pageIndex, int pageSize);


        Task<ApiResponse> GetHouseInfo(string id);
    }
}
