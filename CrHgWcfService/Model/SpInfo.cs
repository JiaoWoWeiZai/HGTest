using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrHgWcfService.Common;

// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    /// <summary>
    /// 门慢申请信息
    /// </summary>
    public class SpInfo : BaseInfo
    {
        /// <summary>
        /// 门慢申请序号
        /// </summary>
        public string serial_apply { get; set; }
        /// <summary>
        /// 待遇类型名称
        /// </summary>
        public string treatment_name { get; set; }
        /// <summary>
        /// 申请医院名称
        /// </summary>
        public string apply_hospital_name { get; set; }
        /// <summary>
        /// 开始生效时间
        /// </summary>
        public string admit_effect { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public string admit_date { get; set; }

        public static SpInfo GetSpInfo(int pInter)
        {
            HgEngine.SetResultSet(pInter, "spinfo");
            var spInfo = new SpInfo();

            foreach (var info in spInfo.GetType().GetProperties())
            {
                var value = string.Empty;
                HgEngine.GetByName(pInter, info.Name, ref value);
                info.SetValue(spInfo, value, null);
            }

            return spInfo;
        }
    }
}