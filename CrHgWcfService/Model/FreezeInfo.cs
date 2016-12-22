// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    public class FreezeInfo
    {
        /// <summary>
        /// 基金编号
        /// </summary>
        public string fund_id { get; set; }
        /// <summary>
        /// 基金名称
        /// </summary>
        public string fund_name { get; set; }
        /// <summary>
        /// 基金状态标志
        /// "0"——"正常"
        /// "1"——"冻结"
        /// "2"——"暂停参保"
        /// "3"——"中止参保"
        /// "9"——"未参保"
        /// </summary>
        public string indi_freeze_status { get; set; }

    }
}