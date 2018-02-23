using Dapper;
using DemoService.Services.Interface.Json;
using Microsoft.Extensions.Caching.Distributed;
using Models.Model;
using Newtonsoft.Json;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Services.Implements.Json
{
    public class TestService : ITestService
    {
        IDistributedCache cache = null;
        public TestService(IDistributedCache cache)
        {
            this.cache = cache;
        }
        
        //新增b_house_basic_attribute
        //public string Test()
        //{
        //    List<b_house_basic_attribute> house = new b_house_basic_attribute().Select<b_house_basic_attribute>().ToList();
        //    int res = 0;
        //    house.FirstOrDefault().BeginTran();
        //    try
        //    {
        //        foreach (var h in house)
        //        {
        //            h.id = Guid.NewGuid();
        //            new_resource_info info = new new_resource_info {
        //                id=h.id,
        //                createtime=h.createtime,
        //                modifytime=h.modifytime,
        //                accountid =h.accountid,
        //                isdelete=false,
        //                merge_ids=null
        //            };
        //            if (1 != info.Insert()) throw new Exception();


        //            if (h.InsertEx() == 1) res++;
        //            else throw new Exception();
        //        }
        //        house.FirstOrDefault().CommitTran();
        //    }
        //    catch(Exception e)
        //    {
        //        house.FirstOrDefault().RollbackTran();
        //    }
        //    return $"{house.Count.ToString()} success {res.ToString()}";
        //}

        public string Test()
        {
            List<new_resource_json_info> infos = new new_resource_json_info().Select<new_resource_json_info>();
            //Parallel.ForEach(infos, info => {
            //    cache.SetString(info.id.ToString(), info.jsonvalue);
            //});
            foreach(var info in infos)
            {
                //cache.SetString(info.id.ToString(), info.jsonvalue);
            }
            return infos.ToString();
        }

        public string Test1()
        {
            List<b_house_basic_attribute> house = new b_house_basic_attribute().Select<b_house_basic_attribute>().ToList();
            int res = 0;
            house.FirstOrDefault().BeginTran();
            try
            {
                foreach (var h in house)
                {
                    h.id = Guid.NewGuid();
                    new_resource_info info = new new_resource_info
                    {
                        id = h.id,
                        createtime = h.createtime,
                        modifytime = h.modifytime,
                        accountid = h.accountid,
                        isdelete = false,
                        merge_ids = null
                    };
                    if (1 != info.Insert()) throw new Exception();


                    if (h.InsertEx() == 1) res++;
                    else throw new Exception();
                }
                house.FirstOrDefault().CommitTran();
            }
            catch (Exception e)
            {
                house.FirstOrDefault().RollbackTran();
            }
            return $"{house.Count.ToString()} success {res.ToString()}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void TestJson(b_house_basic_attribute model)
        {
            new_resource_json_info info = new new_resource_json_info
            {
                //id = h.id,
                jsonvalue = JsonConvert.SerializeObject(model)
            };
            int i=info.Insert();
            i++;
        }

        /// <summary>
        /// 
        /// </summary>
        public async void TestJson()
        {
            List<b_house_basic_attribute> house = new b_house_basic_attribute().Select<b_house_basic_attribute>().ToList();
            house.FirstOrDefault().BeginTran();
            int res = 0;
            try
            {
                //Parallel.For(0, 1000, i =>
                //      {
                //          Parallel.ForEach(house, h =>
                //          {
                //              h.id = Guid.NewGuid();
                //              h.createtime = DateTime.Now;
                //              h.modifytime = DateTime.Now;
                //              new_resource_json info = new new_resource_json
                //              {
                //                  id = h.id,
                //                  jsonvalue = JsonConvert.SerializeObject(h)
                //              };
                //              info.InsertAsync();
                //          });
                //      });

                while(res<1000)
                {
                    foreach(var h in house)
                    {
                        h.id = Guid.NewGuid();
                        h.createtime = DateTime.Now;
                        h.modifytime = DateTime.Now;
                        new_resource_json_info info = new new_resource_json_info
                        {
                            //id = h.id,
                            jsonvalue = JsonConvert.SerializeObject(h)
                        };
                        info.Insert();
                    }
                    res++;
                }
                house.FirstOrDefault().CommitTran();

            }
            catch (Exception e)
            {
                house.FirstOrDefault().RollbackTran();
            }
            //return $"{house.Count.ToString()} success {res.ToString()}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseModel<b_house_basic_attribute> GetHouse(int index, int pageSize)
        {
            List<b_house_basic_attribute> hList = new b_house_basic_attribute().SelectEx<b_house_basic_attribute>(index, pageSize); 
            ResponseModel<b_house_basic_attribute> resModel = new ResponseModel<b_house_basic_attribute>(hList);
            return resModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResponseModel<b_house_basic_attribute> GetJsonHouse(int index, int pagesize)
        {
            List<b_house_basic_attribute> hList = new b_house_basic_attribute().GetJson<b_house_basic_attribute>(index,pagesize); 
            ResponseModel<b_house_basic_attribute> resModel = new ResponseModel<b_house_basic_attribute>(hList);
            return resModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseModel<b_house_basic_attribute1> GetHouseByExecuteStoredProcedure(int pageIndex, int pageSize)
        {
            string storedProcedureName = "ROW2COL";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@resourceTableName", "b_house_basic_attribute", System.Data.DbType.String);
            parameters.Add("@pageIndex", pageIndex,System.Data.DbType.Int32);
            parameters.Add("@pageSize", pageSize, System.Data.DbType.Int32);
            List<b_house_basic_attribute1> hList = new b_house_basic_attribute().StoredProcedure<b_house_basic_attribute1>(storedProcedureName, parameters);
            ResponseModel<b_house_basic_attribute1> resModel = new ResponseModel<b_house_basic_attribute1>(hList);
            return resModel;
        }

    }
}
