using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CrHgWcfService.Common;
using Oracle.ManagedDataAccess.Client;

// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    public class RegisterInfo
    {

        /// <summary>
        /// 计算保存标志
        /// "0"： 试算
        /// "1"： 收费
        /// </summary>
        public string save_flag { get; set; } = "1";
        /// <summary>
        /// 操作标志
        /// “ 1”：门诊挂号
        /// </summary>
        public string oper_flag { get; set; } = "1";
        /// <summary>
        /// 中心编码
        /// 默认： ‘620013'
        /// </summary>
        public string center_id { get; set; } = "620013";
        /// <summary>
        /// 医疗机构编码
        /// </summary>
        public string hospital_id { get; set; } = "006010";
        /// <summary>
        /// 
        /// </summary>
        public string serial_apply { get; set; } = "0";
        /// <summary>
        /// 
        /// </summary>
        public string patient_flag { get; set; } = "0";
        /// <summary>
        /// 
        /// </summary>
        public string diagnose_flag { get; set; } = "0";
        /// <summary>
        /// 个人帐户支付金额
        /// 个人帐户余额，可传 0
        /// </summary>
        public string last_balance { get; set; } = "0";
        /// <summary>
        /// 登记人员工号
        /// </summary>
        public string reg_staff { get; set; } = "CR";
        /// <summary>
        /// 登记人姓名
        /// </summary>
        public string reg_man { get; set; } = "CR";
        /// <summary>
        /// 
        /// </summary>
        public string biz_type { get; set; } = "11";
        /// <summary>
        /// 
        /// </summary>
        public string reg_flag { get; set; } = "1";
        /// <summary>
        /// 病区编码
        /// </summary>
        public string in_area { get; set; } = "001";
        /// <summary>
        /// 病区名称
        /// </summary>
        public string in_area_name { get; set; } = "门诊部";
        /// <summary>
        /// 用卡标志
        /// "0"： 不使用卡
        /// "1"： 使用卡
        /// </summary>
        public string ic_flag { get; set; } = "0";
        /// <summary>
        /// 
        /// </summary>
        public string fin_info { get; set; } = "0";
        /// <summary>
        /// 
        /// </summary>
        public string diagnose_code { get; set; } = "1";
        /// <summary>
        /// 
        /// </summary>
        public string diagnose_sn { get; set; } = "1";
        /// <summary>
        /// 个人电脑号
        /// </summary>
        public string indi_id { get; set; }

        /// <summary>
        /// 待遇类型
        /// </summary>
        public string treatment_type { get; set; } = "110";
        /// <summary>
        /// 登记日期
        /// </summary>
        public string reg_date { get; set; }
        /// <summary>
        /// 就诊日期
        /// </summary>
        public string diagnose_date { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string recipe_no { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// =诊断编码
        /// </summary>
        public string diagnose { get; set; }
        /// <summary>
        /// 诊断编码
        /// </summary>
        public string in_disease { get; set; }
        /// <summary>
        /// 挂号
        /// </summary>
        public string patient_id { get; set; }
        /// <summary>
        /// 就诊科室
        /// </summary>
        public string in_dept { get; set; }
        /// <summary>
        /// 就诊科室名称
        /// </summary>
        public string in_dept_name { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string disease { get; set; }
        /// <summary>
        /// =诊断编码
        /// </summary>
        public string icd { get; set; }

        /// <summary>
        /// 门诊费用明细信息
        /// </summary>
        public List<FeeInfo> FeeInfos { get; set; }


        public RegisterInfo(PersonInfo person)
        {
            indi_id = person.indi_id;
            sex = person.sex;
            reg_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            diagnose_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// 调用存储过程,以处方号获得属性并初始化
        /// </summary>
        /// <param name="registerId"></param>
        public void GetInfoFromHis(string registerId)
        {
            recipe_no = registerId;

            var resultCode = string.Empty;//错误代码
            var resultMessage = string.Empty;//错误描述
            var patientId = string.Empty;//病人院内ID
            var inDisease = string.Empty;//诊断编码 对应医保的in_disease
            var diseases = string.Empty;//诊断名称
            var inDept = string.Empty;//就诊科室代码
            var inDeptName = string.Empty;//就诊科室名称

            var param0 = Database.CreateParameter("registerId", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, "2016121992997");
            var param1 = Database.CreateParameter("resultCode", OracleDbType.Varchar2, 10,
                ParameterDirection.Output, resultCode);
            var param2 = Database.CreateParameter("resultMessage", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, resultMessage);
            var param3 = Database.CreateParameter("patientId", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, patientId);
            var param4 = Database.CreateParameter("inDisease", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, inDisease);
            var param5 = Database.CreateParameter("disease", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, diseases);
            var param6 = Database.CreateParameter("inDept", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, inDept);
            var param7 = Database.CreateParameter("inDeptName", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, inDeptName);
            OracleParameter[] array = { param0, param1, param2, param3, param4, param5, param6, param7 };

            Database.RunProcRetNone("patientInterface.getPatientRegisterInfo", ref array);

            if (array[1].Value.ToString() != "0") return;
            patient_id = patientId;
            in_disease = inDisease;
            diagnose = inDisease;
            icd = inDisease;
            disease = diseases;
            in_dept = inDept;
            in_dept_name = inDeptName;
        }

    }
}