/*
 作者：杨川 
 */
using DemoService.Services.Implements.Json;
using DemoService.Services.Interface.Json;
using DemoService.Services.Interface.ZeroJson;
using Models.Model;
using Newtonsoft.Json;
using PZhFrame.Data.DataService;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.ZeroJson
{
    public class ZeroJsonService : IZeroJsonService
    {
        private IDataService dataService = null;
        static ConnectionStringsHelper connection = new ConnectionStringsHelper();
        /// <summary>
        /// 构造
        /// </summary>
        public ZeroJsonService()
        {
            dataService = new DataService(connection.ConnString(), connection.SqlType());
        }


        /// <summary>
        /// 查询前9项
        /// </summary>
        /// <param name="index">页数</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public ResponseModel<T4_House_Part> QueryPage(int index, int pagesize)
        {
            // 获取修改的列
            List<t4_code> fileds = new t4_code().Select<t4_code>().ToList();
            // 获取前9项实体属性
            Type typeInfo = typeof(T4_House_Part);
            var properties = typeInfo.GetProperties().ToList();
            // 获取房源
            List<T4_House_Part> houseList = new T4_House_Part().SelectPart<T4_House_Part>(typeof(T4_House).Name, index, pagesize, "column1");
            Parallel.ForEach(houseList, info =>
           {
               // 获取修改历史信息
               List<t4_json> jsonList = new t4_json(true).Select<t4_json>(info.column1, "houseid");
               Parallel.ForEach(jsonList, p =>
               {
                   List<T4_ModifyJsonModel> modifyInfoList = 
                   JsonConvert.DeserializeObject<List<T4_ModifyJsonModel>>(p.jsonstr);
                   T4_ModifyJsonModel mdify = modifyInfoList.OrderByDescending(a => a.Column207).FirstOrDefault();
                   t4_code columnName = fileds.Where(o => o.id == p.codeid).FirstOrDefault();
                   PropertyInfo proInfo = properties.Where(o => o.Name == columnName.name).FirstOrDefault();
                   if (proInfo != null)
                   {
                       proInfo.SetValue(info, mdify.Column205);
                   }
               });

           });
            ResponseModel<T4_House_Part> resModel = new ResponseModel<T4_House_Part>(houseList);
            return resModel;
        }

        /// <summary>
        /// 获取详情信息
        /// </summary>
        /// <param name="houseId">房屋ID</param>
        /// <returns></returns>
        public T4_House_Show QueryInformation(string houseId)
        {
            // 获取房屋信息
            List<T4_House> modelList = new T4_House().Select<T4_House>(houseId, "column1");
            if (modelList.Count > 0)
            {
                T4_House houseModel = modelList[0];
                // 获取房屋实体类属性
                Type typeInfo = typeof(T4_House);
                var properties = typeInfo.GetProperties().ToList();                
                string sql = "";
                // 获取可修改的列
                List<t4_code> modifyColumns = new t4_code().Select<t4_code>();
                Parallel.ForEach(modifyColumns, p =>
                {
                    sql = $"select * from t4_json where houseid='{houseId}' and codeid='{p.id}'";
                    // 获取列修改的信息
                    List<t4_json> jsonModel = new t4_json(true).Select<t4_json>(sql);
                    List<T4_ModifyJsonModel> jsonModify = JsonConvert.DeserializeObject<List<T4_ModifyJsonModel>>(jsonModel[0].jsonstr);
                    T4_ModifyJsonModel mdify = jsonModify.OrderByDescending(a => a.Column207).FirstOrDefault();
                    PropertyInfo proInfo = properties.Where(o => o.Name == p.name).FirstOrDefault();

                    if (proInfo != null)
                    {
                        // 给房屋信息赋值
                        proInfo.SetValue(houseModel, mdify.Column205);
                    }
                });
                // 获取电话信息
                List<t4_tel> telList = new t4_tel().Select<t4_tel>(houseModel.column10, "column202");
                T4_House_Show houseShowModel = new T4_House_Show();
                BaseModel.Mapper(houseShowModel, houseModel);
                houseShowModel.column10 = telList;
                return houseShowModel;
            }
            return null;
        }
    }
}
