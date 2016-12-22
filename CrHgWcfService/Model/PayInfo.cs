using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrHgWcfService.Common;

// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    public class PayInfo : BaseInfo
    {
        /// <summary>
        /// 医院编号
        /// </summary>
        public string hospital_id { get; set; }
        /// <summary>
        /// 就医登记号
        /// </summary>
        public string serial_no { get; set; }
        /// <summary>
        /// 收费序号
        /// </summary>
        public string pay_no { get; set; }
        /// <summary>
        /// 收费日期
        /// </summary>
        public string pay_date { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public string zyzje { get; set; }
        /// <summary>
        /// 社保支付金额
        /// </summary>
        public string sbzfje { get; set; }
        /// <summary>
        /// 帐户支付金额
        /// </summary>
        public string zhzfje { get; set; }
        /// <summary>
        /// 部分项目自付金额
        /// </summary>
        public string bfxmzfje { get; set; }
        /// <summary>
        /// 个人起付金额
        /// </summary>
        public string qfje { get; set; }
        /// <summary>
        /// 个人自费项目金额
        /// </summary>
        public string grzfje1 { get; set; }
        /// <summary>
        /// 个人自付金额
        /// </summary>
        public string grzfje2 { get; set; }
        /// <summary>
        /// 个人自负金额
        /// </summary>
        public string grzfje3 { get; set; }
        /// <summary>
        /// 超统筹支付限额个人自付金额
        /// </summary>
        public string cxzfje { get; set; }
        /// <summary>
        /// 医院垫付金额
        /// </summary>
        public string yyfdje { get; set; }
        /// <summary>
        /// 个人自付现金部分
        /// </summary>
        public string cash_pay_com { get; set; }
        /// <summary>
        /// 个人自付个人帐户部分
        /// </summary>
        public string acct_pay_com { get; set; }
        /// <summary>
        /// 个人自费现金部分
        /// </summary>
        public string cash_pay_own { get; set; }
        /// <summary>
        /// 个人自费个人帐户部分
        /// </summary>
        public string acct_pay_own { get; set; }

        public static PayInfo GetPayInfo(int pInter)
        {
            HgEngine.SetResultSet(pInter, "payinfo");
            var payInfo = new PayInfo();

            foreach (var info in payInfo.GetType().GetProperties())
            {
                var value = string.Empty;
                HgEngine.GetByName(pInter, info.Name, ref value);
                info.SetValue(payInfo, value, null);
            }

            return payInfo;
        }
    }
}