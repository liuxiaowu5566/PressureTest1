using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model
{
    public partial class b_house_basic_attribute : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Insert()
        {
            int res=base.Insert();
            if(res==1)
            {

            }

            return res;
        }
    }
}
