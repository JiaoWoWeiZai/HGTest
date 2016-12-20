using CrHgWcfService.Common;
// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    public class PersonInfo
    {
        public PersonInfo(int interfaceId)
        {
            //获取信息记录行数
            var count = HgEngine.GetRowCount(interfaceId);
            //设置结果集为基本信息PersonInfo
            HgEngine.setresultset(interfaceId, "PersonInfo");

            var pvalue = "";
            HgEngine.GetByName(interfaceId, "name", ref pvalue);
            name = pvalue;
            HgEngine.GetByName(interfaceId, "indi_id", ref pvalue);
            indi_id = pvalue;
            HgEngine.GetByName(interfaceId, "sex", ref pvalue);
            sex = pvalue;
            HgEngine.GetByName(interfaceId, "pers_identity", ref pvalue);
            pers_identity = pvalue;
            HgEngine.GetByName(interfaceId, "pers_status", ref pvalue);
            pers_status = pvalue;
            HgEngine.GetByName(interfaceId, "office_grade", ref pvalue);
            office_grade = pvalue;
            HgEngine.GetByName(interfaceId, "idcard", ref pvalue);
            idcard = pvalue;
            HgEngine.GetByName(interfaceId, "telephone", ref pvalue);
            telephone = pvalue;
            HgEngine.GetByName(interfaceId, "birthday", ref pvalue);
            birthday = pvalue;
            HgEngine.GetByName(interfaceId, "post_code", ref pvalue);
            post_code = pvalue;
            HgEngine.GetByName(interfaceId, "corp_id", ref pvalue);
            corp_id = pvalue;
            HgEngine.GetByName(interfaceId, "corp_name", ref pvalue);
            corp_name = pvalue;
            HgEngine.GetByName(interfaceId, "last_balance", ref pvalue);
            last_balance = pvalue;


            if (count != 1) return;
            HgEngine.setresultset(interfaceId, "freezeinfo");
            HgEngine.GetByName(interfaceId, "fund_id", ref pvalue);
            fund_id = pvalue;
            HgEngine.GetByName(interfaceId, "fund_name", ref pvalue);
            fund_name = pvalue;
            HgEngine.GetByName(interfaceId, "indi_freeze_status", ref pvalue);
            indi_freeze_status = pvalue;
            HgEngine.setresultset(interfaceId, "clinicapplyinfo");
            HgEngine.GetByName(interfaceId, "serial_apply", ref pvalue);
            serial_apply = pvalue;
        }
        /// <summary>
        /// 个人电脑号
        /// </summary>
        public string indi_id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public string pers_identity { get; set; }
        /// <summary>
        /// 人员状态
        /// </summary>
        public string pers_status { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string office_grade { get; set; }
        /// <summary>
        /// 公民身份号码
        /// </summary>
        public string idcard { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 地区编码
        /// </summary>
        public string post_code { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string corp_id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string corp_name { get; set; }
        /// <summary>
        /// 个人帐户余额
        /// </summary>
        public string last_balance { get; set; }
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
        /// </summary>
        public string indi_freeze_status { get; set; }
        /// <summary>
        /// 门诊选点申请序列号
        /// </summary>
        public string serial_apply { get; set; }
    }
}