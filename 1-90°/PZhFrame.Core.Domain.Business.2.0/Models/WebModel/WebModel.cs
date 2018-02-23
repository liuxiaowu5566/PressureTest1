using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model
{
    public class ResponseModel<T>
    {
        public List<T> dateList { get; set; }
        public int Count { get; set; }
        public ResponseModel(List<T> tList)
        {
            dateList = tList;
            Count = dateList.Count;
        }
    }
}
