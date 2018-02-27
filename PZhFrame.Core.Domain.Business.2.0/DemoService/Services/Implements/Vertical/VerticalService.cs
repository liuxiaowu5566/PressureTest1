/*
 作者:孙靖武
 */
using DemoService.Services.Interface.Vertical;
using Models.Model;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Implements.Vertical
{
    public class VerticalService : IVerticalService
    {
        public void AddFiledCode()
        {
            for(int i=1;i<20100;i++)
            {
                t1_resource model = new t1_resource { createtime=DateTime.Now};
                model.InsertAutoId();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseModel<ColumnModel> GetHouse(int index, int pageSize)
        {
            List<ColumnModel> hList = new b_house_basic_attribute().SelectExVertical<ColumnModel>(index, pageSize);
            ResponseModel<ColumnModel> resModel = new ResponseModel<ColumnModel>(hList);
            return resModel;
        }

    }
    
}
