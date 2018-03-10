/*
 作者：杨川 
 */
using Models.Model;
using Models.Model.t4;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static PZhFrame.Core.Infrastructure.Lib.GenericQueryAnalizer;

namespace DemoService.Services.Interface.ZeroJson
{
    public interface IZeroJsonService
    {
        Task<ResponseModel<T4_House_Part>> QueryPage(GenericQueryModel queryModel, int index, int pagesize);
        Task<T4_House_Show> QueryInformation(string houseId);
    }
}
