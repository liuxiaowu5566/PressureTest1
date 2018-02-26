using Models.Model;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Services.Interface.ZeroJson
{
    public interface IZeroJsonService
    {
        ResponseModel<T4_House_Part> QueryPage(int index, int pagesize);
        T4_House_Show QueryInformation(string houseId);
    }
}
