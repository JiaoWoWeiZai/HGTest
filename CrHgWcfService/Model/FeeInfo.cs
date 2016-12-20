// ReSharper disable InconsistentNaming
namespace CrHgWcfService.Model
{
    public class FeeInfo
    {
        /// <summary>
        /// 项目药品类型
        /// "0"：诊疗项目
        /// "1"：西药
        /// "2"：中成药
        /// "3"：中草药
        /// </summary>
        public string medi_item_type { get; set; }
        /// <summary>
        /// 医院药品项目编码
        /// </summary>
        public string his_item_code { get; set; }
        /// <summary>
        /// 医院药品项目名称
        /// </summary>
        public string his_item_name { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        public string factory { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string standard { get; set; }
        /// <summary>
        /// 费用发生时间
        /// </summary>
        public string fee_date { get; set; }
        /// <summary>
        /// 费用录入日期
        /// </summary>
        public string input_date { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        public string dosage { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string recipe_no { get; set; }
        /// <summary>
        /// 处方医生编号
        /// </summary>
        public string doctor_no { get; set; }
        /// <summary>
        /// 处方医生姓名
        /// </summary>
        public string doctor_name { get; set; }
        /// <summary>
        /// 医院费用序列号
        /// </summary>
        public string hos_serial { get; set; }
    }
}