using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.ZeroAndNinety
{
    public interface IZeroAndNinetyService
    {
        ResponseModel<string> QueryPage1_9(int index, int pagesize);
    }
}
