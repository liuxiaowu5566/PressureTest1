
using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Data
{
    public class PreparationService
    {
        public int Add(t2_house model)
        {
            return model.Insert();
        }
    }
}
