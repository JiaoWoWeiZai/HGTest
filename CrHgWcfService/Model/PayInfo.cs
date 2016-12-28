using System;
using System.Data;
using CrHgWcfService.Common;
using Oracle.ManagedDataAccess.Client;

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

        public bool SaveToHis(string registerId, int flag,string channel,ref string errorMsg)
        {
            if (errorMsg == null) throw new ArgumentNullException(nameof(errorMsg));
            var resultCode = string.Empty;//错误代码
            var resultMessage = string.Empty;//错误描述

            var param0 = Database.CreateParameter("registerId", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, registerId);
            var param2 = Database.CreateParameter("v_serial_no", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, serial_no);
            var param3 = Database.CreateParameter("n_zyzje", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, zyzje);
            var param4 = Database.CreateParameter("n_sbzfje", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, sbzfje);
            var param5 = Database.CreateParameter("n_zhzfje", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, zhzfje);
            var param6 = Database.CreateParameter("n_bfxmzfje", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, bfxmzfje);
            var param7 = Database.CreateParameter("n_qfje", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, qfje);
            var param8 = Database.CreateParameter("n_grzfje1", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, grzfje1);
            var param9 = Database.CreateParameter("n_grzfje2", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, grzfje2);
            var param10 = Database.CreateParameter("n_grzfje3", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, grzfje3);
            var param11 = Database.CreateParameter("n_cxzfje", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, cxzfje);
            var param12 = Database.CreateParameter("n_yyfdje", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, yyfdje);
            var param13 = Database.CreateParameter("n_cash_pay_com", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, cash_pay_com);
            var param14 = Database.CreateParameter("n_acct_pay_com", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, acct_pay_com);
            var param15 = Database.CreateParameter("n_cash_pay_own", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, cash_pay_own);
            var param16 = Database.CreateParameter("n_acct_pay_own", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, acct_pay_own);
            var param17 = Database.CreateParameter("flag", OracleDbType.Int32, 20,
                ParameterDirection.Input, flag.ToString());
            var param20 = Database.CreateParameter("channel", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, channel);
            var param18 = Database.CreateParameter("resultCode", OracleDbType.Varchar2, 10,
                ParameterDirection.Output, resultCode);
            var param19 = Database.CreateParameter("resultMessage", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, resultMessage);


            OracleParameter[] array = { param0, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, param20, param18, param19 };

            try
            {
                Database.RunProcRetNone("patientInterface.insertInsurSettleData", ref array);
                if (param18.Value.ToString() != "0")
                {
                    errorMsg = param19.Value.ToString();
                    return false;
                }
                errorMsg = "数据保存成功!";
                return true;
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return false;
            }
        }
    }
}