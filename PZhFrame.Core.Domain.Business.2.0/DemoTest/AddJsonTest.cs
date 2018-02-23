using DemoService.Services.Data;
using DemoService.Services.Json;
using Models.Model;
using PZhFrame.Core.Infrastructure.Lib;
using PZhFrame.Core.Infrastructure.Net;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace DemoTest
{
    public class AddJsonTest
    {
        [Fact]
        public void Add()
        {
            for (int i = 1; i <= 2; i++)
            {
                MainModel ss = new MainModel();
                foreach (var p in ss.GetType().GetProperties().ToList())
                {
                    p.SetValue(ss,i.ToString());
                }
                t2_house house = new t2_house();
                house.id = i;
                house.jsonstr = JsonHelper.Instance.Serialize(ss);
                PreparationService service = new PreparationService();
                service.Add(house);

            }

        }


    }
    public class MainModel
    {
        public string column1 { get; set; }
        public string column2 { get; set; }
        public string column3 { get; set; }
        public string column4 { get; set; }
        public string column5 { get; set; }
        public string column6 { get; set; }
        public string column7 { get; set; }
        public string column8 { get; set; }
        public string column9 { get; set; }
        public string column10 { get; set; }
        public string column11 { get; set; }
        public string column12 { get; set; }
        public string column13 { get; set; }
        public string column14 { get; set; }
        public string column15 { get; set; }
        public string column16 { get; set; }
    }
    public class TelModel
    {
        public string column1{ get; set; }
        public string column2{ get; set; }
        public string column3{ get; set; }
        public string column4 { get; set; }
    }
}
