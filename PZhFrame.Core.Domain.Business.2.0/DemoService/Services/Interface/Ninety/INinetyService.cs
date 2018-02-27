using Models.Model;
using Models.WebModel;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Ninety
{
    public interface INinetyService
    {
        ResponseModel<t1_house_nunety> QueryPage(int index, int pagesize);
    }
}
