using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.NinetyAndJson
{
    public interface INinetyAndJsonService
    {
        ResponseModel<t3_house_nunety> QueryPage(int index, int pagesize);
    }
}
