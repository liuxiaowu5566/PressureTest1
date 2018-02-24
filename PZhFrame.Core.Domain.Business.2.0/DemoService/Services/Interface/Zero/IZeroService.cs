using Models.Model;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Zero
{
    public interface IZeroService
    {
        ResponseModel<t6_house> QueryPage(int index, int pagesize);
    }
}
