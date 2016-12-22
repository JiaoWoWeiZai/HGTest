using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrHgWcfService.Common;

// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    /// <summary>
    /// 医疗费用支付组成信息
    /// </summary>
    public class FundInfo : BaseInfo
    {
        /// <summary>
        /// 统筹金支付总额
        /// </summary>
        public string fund_pay { get; set; }
        /// <summary>
        /// 个人自费金额
        /// </summary>
        public string self_pay { get; set; }
        /// <summary>
        /// 个人自付金额
        /// </summary>
        public string acct_pay { get; set; }
        /// <summary>
        /// 医院垫付金额
        /// </summary>
        public string hosp_pay { get; set; }


        public static FundInfo GetFundInfo(int pInter)
        {
            HgEngine.SetResultSet(pInter, "fund");
            var fundInfo = new FundInfo();

            foreach (var info in fundInfo.GetType().GetProperties())
            {
                var value = string.Empty;
                HgEngine.GetByName(pInter, info.Name, ref value);
                info.SetValue(fundInfo, value, null);
            }

            return fundInfo;
        }
    }
}