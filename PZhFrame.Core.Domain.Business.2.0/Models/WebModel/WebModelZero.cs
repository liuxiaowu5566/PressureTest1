using Models.Model.t6;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.WebModel
{
    public class WebModelZero
    {
        public t6_house house { get; set; }

        public List<t6_tel> listTel { get; set; }
    }
}
