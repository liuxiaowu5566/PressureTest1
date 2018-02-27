/*
 作者:孙靖武
 */
using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Interface.Vertical
{
    public interface IVerticalService
    {
        void AddFiledCode();
        ResponseModel<ColumnModel> GetHouse(int index, int pageSize);
    }
}
