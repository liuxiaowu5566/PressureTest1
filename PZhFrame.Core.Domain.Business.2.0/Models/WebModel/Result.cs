using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model
{
    public class Result
    {
        [ExplicitKey]
        [Head("column1", "-")]
        public int column1 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column2", "-")]
        public string column2 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column3", "-")]
        public string column3 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column4", "-")]
        public string column4 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column5", "-")]
        public string column5 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column6", "-")]
        public string column6 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column7", "-")]
        public string column7 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column8", "-")]
        public string column8 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("column9", "-")]
        public string column9 { get; set; }
    }
}
