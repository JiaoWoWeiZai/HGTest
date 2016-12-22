using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    public class FeeBatchInfo : BaseInfo
    {
        /// <summary>
        /// 医院编码
        /// </summary>
        public string hospital_id { get; set; }
        /// <summary>
        /// 就医登记号
        /// </summary>
        public string serial_no { get; set; }
        /// <summary>
        /// 个人电脑号
        /// </summary>
        public string indi_id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  费用批次
        /// </summary>
        public string fee_batch { get; set; }
        /// <summary>
        ///  批次费用
        /// </summary>
        public string sum_fee { get; set; }
    }
}