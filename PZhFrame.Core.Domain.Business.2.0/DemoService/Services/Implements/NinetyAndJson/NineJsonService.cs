/*
 作者:徐章壹
 */
using DemoService.Services.Interface.Vertical;
using Models.Model;
using Models.Model.t1;
using Models.Model.t3;
using Newtonsoft.Json;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;
using PZhFrame.ModelLayer.Models.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<T3_Part> hList = selectNineJson<T3_Part>(index, pageSize);
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
            List<T3> hList = nineJsonModel<T3>(id);
            ResponseModel<T3> resModel = new ResponseModel<T3>(hList);
            return resModel;
        }

        /// <summary>
        /// 90°+json格式分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private List<T> selectNineJson<T>(int index, int pageSize) where T : class, new()
        {
            List<t1_code> fileds = new t1_code().Select<t1_code>().ToList();
            List<t1_house> infos = new t1_house().Select<t1_house>(index, pageSize, "createtime ");
            ConcurrentBag<T> res = new ConcurrentBag<T>();
            Type typeInfo = typeof(T);
            var properties = typeInfo.GetProperties().ToList();
            ParallelOptions opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };
            Parallel.ForEach(infos, opt, info =>
            {
                T model = new T();
                List<t3_json> filedValues = new t3_json(true).Select<t3_json>(info.id, "houseid").ToList();
                Parallel.ForEach(properties, opt, p =>
                {
                    t1_code filed = fileds.Where(o => o.name == p.Name).FirstOrDefault();
                    var value = filedValues.Where(o => o.codeid == filed.id).FirstOrDefault().jsonstr;
                    if (value.Substring(0, 1) == "[")
                    {
                        List<T3_ModifyJsonModel> arrayjson = JsonConvert.DeserializeObject<List<T3_ModifyJsonModel>>(value);
                        T3_ModifyJsonModel column = arrayjson.OrderByDescending(a => a.Column207).FirstOrDefault();
                        Task.Run(() => { p.SetValue(model, column.Column205); }).ConfigureAwait(false);
                    }
                    if (value.Substring(0, 1) == "{")
                    {
                        var json = JsonConvert.DeserializeObject<T3_ModifyJsonModel>(value);
                        Task.Run(() => { p.SetValue(model, json.Column205); }).ConfigureAwait(false);
                    }
                });
                res.Add(model);
            });
            return res.ToList();
        }


        // <summary>
        /// 90°+json格式单值查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private List<T> nineJsonModel<T>(string id) where T : class, new()
        {
            List<t1_code> fileds = new t1_code().Select<t1_code>().ToList();
            ConcurrentBag<T> res = new ConcurrentBag<T>();
            Type typeInfo = typeof(T);
            var properties = typeInfo.GetProperties().ToList();
            ParallelOptions opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };
            T model = new T();
            List<t3_json> filedValues = new t3_json(true).Select<t3_json>(id, "houseid").ToList();
            Parallel.ForEach(properties, opt, p =>
            {
                t1_code filed = fileds.Where(o => o.name == p.Name).FirstOrDefault();
                var value = filedValues.Where(o => o.codeid == filed.id).FirstOrDefault().jsonstr;
                if (value.Substring(0, 1) == "[")
                {
                    if (p.Name == "Column10")
                    {
                        List<T3_ModifyTelModel> stel = JsonConvert.DeserializeObject<List<T3_ModifyTelModel>>(value);
                        T3_ModifyTelModel columntel = stel.OrderByDescending(a => a.Column204).FirstOrDefault();
                        Task.Run(() => { p.SetValue(model, columntel); }).ConfigureAwait(false);
                    }
                    else
                    {
                        List<T3_ModifyJsonModel> ss = JsonConvert.DeserializeObject<List<T3_ModifyJsonModel>>(value);
                        T3_ModifyJsonModel column = ss.OrderByDescending(a => a.Column207).FirstOrDefault();
                        Task.Run(() => { p.SetValue(model, column.Column205); }).ConfigureAwait(false);
                    }
                }
                if (value.Substring(0, 1) == "{")
                {
                    var aa = JsonConvert.DeserializeObject<T3_ModifyJsonModel>(value);
                    Task.Run(() => { p.SetValue(model, aa.Column205); }).ConfigureAwait(false);
                }
            });
            res.Add(model);
            return res.ToList();
        }
    }
    
}
