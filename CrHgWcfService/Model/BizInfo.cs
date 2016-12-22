using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ReSharper disable InconsistentNaming

namespace CrHgWcfService.Model
{
    public class BizInfo:BaseInfo
    {
        /// <summary>
        ///  医疗机构编号
        /// </summary>
        public string hospital_id { get; set; }
        /// <summary>
        ///  就医登记号
        /// </summary>
        public string serial_no { get; set; }
        /// <summary>
        ///  业务类型
        /// </summary>
        public string biz_type { get; set; }
        /// <summary>
        ///  中心编码
        /// </summary>
        public string center_id { get; set; }
        /// <summary>
        ///  个人编号
        /// </summary>
        public string indi_id { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        ///  公民身份号码
        /// </summary>
        public string idcard { get; set; }
        /// <summary>
        ///  医保卡号
        /// </summary>
        public string ic_no { get; set; }
        /// <summary>
        ///  出生日期
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        ///  联系电话
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        ///  单位编码
        /// </summary>
        public string corp_id { get; set; }
        /// <summary>
        ///   单位名称
        /// </summary>
        public string corp_name { get; set; }
        /// <summary>
        ///  待遇类别
        /// </summary>
        public string treatment_type { get; set; }
        /// <summary>
        ///  业务登记日期
        /// </summary>
        public string reg_date { get; set; }
        /// <summary>
        ///  登记人工号
        /// </summary>
        public string reg_staff { get; set; }
        /// <summary>
        ///   登记人
        /// </summary>
        public string reg_man { get; set; }
        /// <summary>
        ///   登记标志
        /// </summary>
        public string reg_flag { get; set; }
        /// <summary>
        ///   业务开始时间
        ///   日期格式 YYYY-MM-DD
        /// </summary>
        public string begin_date { get; set; }
        /// <summary>
        ///  业务开始情况
        /// </summary>
        public string reg_info { get; set; }
        /// <summary>
        ///  入院科室
        /// </summary>
        public string in_dept { get; set; }
        /// <summary>
        ///  入院科室名称
        /// </summary>
        public string in_dept_name { get; set; }
        /// <summary>
        ///  入院病区
        /// </summary>
        public string in_area { get; set; }
        /// <summary>
        ///  入院病区名称
        /// </summary>
        public string in_area_name { get; set; }
        /// <summary>
        ///  入院床位号
        /// </summary>
        public string in_bed { get; set; }
        /// <summary>
        ///  医院业务号(挂号)
        /// </summary>
        public string patient_id { get; set; }
        /// <summary>
        ///  入院疾病诊断（ icd 码）
        /// </summary>
        public string in_disease { get; set; }
        /// <summary>
        ///  疾病名称
        /// </summary>
        public string disease { get; set; }
        /// <summary>
        ///  用卡标志 
        /// </summary>
        public string ic_flag { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///  总费用
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        ///  诊次结束标志
        /// “ 0”——诊次未结束
        /// “ 1”——诊次结束
        /// </summary>
        public string end_flag { get; set; }
        ///// <summary>
        /////  门慢申请序号
        ///// </summary>
        //public string serial_apply { get; set; }
    }
}