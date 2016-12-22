using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    public class RefFeeInfo : FeeInfo
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
        ///  费用批次
        /// </summary>
        public string fee_batch { get; set; }
        /// <summary>
        ///  费用序号
        /// </summary>
        public string serial_fee { get; set; }
        /// <summary>
        ///  统计类别
        /// </summary>
        public string stat_type { get; set; }
        /// <summary>
        ///  中心药品项目编码
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        ///  中心药品项目名称
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        ///  可退金额
        /// </summary>
        public string reduce_money { get; set; }
        /// <summary>
        ///  用药标志
        /// 1：出院带药 2：抢救用药 3：急诊
        /// </summary>
        public string usage_flag { get; set; }
        /// <summary>
        ///  对应费用序列号
        /// </summary>
        public string opp_serial_fee { get; set; }
        /// <summary>
        ///  录入人工号
        /// </summary>
        public string input_staff { get; set; }
        /// <summary>
        ///  录入人姓名
        /// </summary>
        public string input_name { get; set; }
    }
}