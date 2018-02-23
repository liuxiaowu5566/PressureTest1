using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;

namespace Models.Model
{
    public partial class b_house_basic_attribute
    {
           public b_house_basic_attribute(){


           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
	   [ExplicitKey]    
           [Head("id","主键ID")]
           public Guid id {get;set;}

           /// <summary>
           /// Desc:房源ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_basic_code","房源ID")]
           public Guid? house_basic_code {get;set;}

           /// <summary>
           /// Desc:小区ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_estate_code","小区ID")]
           public Guid? house_estate_code {get;set;}

           /// <summary>
           /// Desc:房源编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_code","房源编号")]
           public string house_code {get;set;}

           /// <summary>
           /// Desc:房源标题
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_title","房源标题")]
           public List<ModifyLogModel> house_title {get;set;}

           /// <summary>
           /// Desc:房源标签
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_tag","房源标签")]
           public string house_tag {get;set;}

           /// <summary>
           /// Desc:楼号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_build","楼号")]
           public List<ModifyLogModel> house_build {get;set;}

           /// <summary>
           /// Desc:单元
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_unit","单元")]
           public List<ModifyLogModel> house_unit {get;set;}

           /// <summary>
           /// Desc:户号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_num","户号")]
           public List<ModifyLogModel> house_num {get;set;}

           /// <summary>
           /// Desc:室
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("house_bedroom","室")]
           public int? house_bedroom {get;set;}

           /// <summary>
           /// Desc:厅
           /// Default:0
           /// Nullable:False
           /// </summary>           
           [Head("house_livingroom","厅")]
           public int house_livingroom {get;set;}

           /// <summary>
           /// Desc:厨
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("house_kitchen","厨")]
           public int? house_kitchen {get;set;}

           /// <summary>
           /// Desc:明卫
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("house_bathroom_light","明卫")]
           public int? house_bathroom_light {get;set;}

           /// <summary>
           /// Desc:暗卫
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("house_bathroom_dark","暗卫")]
           public int? house_bathroom_dark {get;set;}

           /// <summary>
           /// Desc:房源描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_describ","房源描述")]
           public string house_describ {get;set;}

           /// <summary>
           /// Desc:阳台
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("house_balcony","阳台")]
           public int? house_balcony {get;set;}

           /// <summary>
           /// Desc:楼型
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_build_type","楼型")]
           public string house_build_type {get;set;}

           /// <summary>
           /// Desc:总楼层
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_total_floor","总楼层")]
           public string house_total_floor {get;set;}

           /// <summary>
           /// Desc:房屋用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_purpose","房屋用途")]
           public string house_purpose {get;set;}

           /// <summary>
           /// Desc:房屋结构
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_construction","房屋结构")]
           public string house_construction {get;set;}

           /// <summary>
           /// Desc:建成年代
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_build_year","建成年代")]
           public string house_build_year {get;set;}

           /// <summary>
           /// Desc:梯
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_lift_num","梯")]
           public string house_lift_num {get;set;}

           /// <summary>
           /// Desc:户
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_house_count","户")]
           public string house_house_count {get;set;}

           /// <summary>
           /// Desc:所在楼层
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("house_floor","所在楼层")]
           public List<ModifyLogModel> house_floor {get;set;}

           /// <summary>
           /// Desc:朝向
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_toward","朝向")]
           public string house_toward {get;set;}

           /// <summary>
           /// Desc:建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_build_area","建筑面积")]
           public List<ModifyLogModel> house_build_area {get;set;}

           /// <summary>
           /// Desc:各部分面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_build_detail_area","各部分面积")]
           public string house_build_detail_area {get;set;}

           /// <summary>
           /// Desc:标记房屋的唯一状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_status","标记房屋的唯一状态")]
           public string house_status {get;set;}

           /// <summary>
           /// Desc:前状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("super_house_status","前状态")]
           public string super_house_status {get;set;}

           /// <summary>
           /// Desc:房屋地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_address","房屋地址")]
           public string house_address {get;set;}

           /// <summary>
           /// Desc:类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("house_type","类型")]
           public string house_type {get;set;}

           /// <summary>
           /// Desc:删除标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("isdelete","删除标识")]
           public bool? isdelete {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("createtime","创建时间")]
           public DateTime? createtime {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("modifytime","修改时间")]
           public DateTime? modifytime {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:True
           /// </summary>  
           [AccountId]
           [Head("accountid","用户ID")]
           public Guid? accountid {get;set;}

           /// <summary>
           /// Desc:是否置顶，0不置顶，1置顶
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("istop","是否置顶，0不置顶，1置顶")]
           public int? istop {get;set;}

           /// <summary>
           /// Desc:是否需要仲裁，0不需要，1需要
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("is_arbitrate","是否需要仲裁，0不需要，1需要")]
           public bool? is_arbitrate {get;set;}

           /// <summary>
           /// Desc:标记是否为公共数据，0私有数据，1公共数据
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [Head("is_public","标记是否为公共数据，0私有数据，1公共数据")]
           public bool? is_public {get;set;}

           /// <summary>
           /// Desc:类型 SaleHouse RentHouse
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("utype","类型 SaleHouse RentHouse")]
           public string utype {get;set;}

           /// <summary>
           /// Desc:操作人ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("operatorid","操作人ID")]
           public Guid? operatorid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("founder","-")]
           public Guid? founder {get;set;}

    }
}
