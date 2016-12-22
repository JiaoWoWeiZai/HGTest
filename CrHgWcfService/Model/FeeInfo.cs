// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CrHgWcfService.Common;
using Oracle.ManagedDataAccess.Client;

namespace CrHgWcfService.Model
{
    public class FeeInfo : BaseInfo
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

        public string staple_flag { get; set; }

        public static FeeInfo[] GetFeeInfoFromHis(string registerId, string totalAmt, ref string errorMsg)
        {
            var param0 = Database.CreateParameter("registerId", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, registerId);
            var param1 = Database.CreateParameter("totalAmt", OracleDbType.Varchar2, 10,
                ParameterDirection.Input, totalAmt);
            var param2 = Database.CreateParameter("resultCode", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, "");
            var param3 = Database.CreateParameter("resultMessage", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, "");
            var param4 = Database.CreateParameter("feeInfoList", OracleDbType.RefCursor, 20,
                ParameterDirection.Output, "");
            OracleParameter[] array = { param0, param1, param2, param3, param4 };

            var db = Database.RunProcRetDataSet("patientInterface.getPatientFeeInfo", ref array);
            errorMsg = param3.Value.ToString();
            if (param2.Value.ToString() != "0") return null;
            return (from DataRow row in db.Tables[0].Rows
                    select new FeeInfo
                    {
                        medi_item_type = row["MEDI_ITEM_TYPE"].ToString(),
                        his_item_code = row["HIS_ITEM_CODE"].ToString(),
                        his_item_name = row["HIS_ITEM_NAME"].ToString(),
                        model = row["MODEL"].ToString(),
                        factory = row["FACTORY"].ToString(),
                        standard = row["STANDARD"].ToString(),
                        fee_date = row["FEE_DATE"].ToString(),
                        input_date = row["INPUT_DATE"].ToString(),
                        unit = row["UNIT"].ToString(),
                        price = row["PRICE"].ToString(),
                        dosage = row["DOSAGE"].ToString(),
                        money = row["MONEY"].ToString(),
                        recipe_no = row["RECIPE_NO"].ToString(),
                        doctor_no = row["DOCTOR_NO"].ToString(),
                        doctor_name = row["DOCTOR_NAME"].ToString(),
                        hos_serial = row["HOS_SERIAL"].ToString(),
                        staple_flag = row["STAPLE_FLAG"].ToString(),
                    }).ToArray();
        }
    }
}