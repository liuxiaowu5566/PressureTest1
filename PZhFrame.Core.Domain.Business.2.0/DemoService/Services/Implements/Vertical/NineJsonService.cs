using DemoService.Services.Interface.Vertical;
using Models.Model;
using Newtonsoft.Json;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoService.Services.Implements.Vertical
{
    public class NineJsonService : INineJsonService
    {
        private IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        public NineJsonService()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }
        /// <summary>
        /// 90°+json分页查询
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseModel<T3_Part> GetHousePart(int index, int pageSize)
        {
            List<T3_Part> hList = new t3_json().SelectNineJson<T3_Part>(index, pageSize);
            ResponseModel<T3_Part> resModel = new ResponseModel<T3_Part>(hList);
            return resModel;
        }
        /// <summary>
        /// 90°+json详细查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel<T3> GetHouse(string id)
        {
            List<T3> hList = new t3_json().NineJsonModel<T3>(id);
            ResponseModel<T3> resModel = new ResponseModel<T3>(hList);
            return resModel;
        }
    }
    
}
