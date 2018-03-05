using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model.Mongo
{
    public class table1
    {
        public ObjectId _id { get; set; }
        public string houseid { get; set; }
        public string estateid {get;set;}
        public string name { get; set; }
    }
}
