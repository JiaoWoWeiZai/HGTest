using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrHgWcfService.Common;
using CrHgWcfService.Model;
using Oracle.ManagedDataAccess.Client;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MedicareClient client = new MedicareClient();
        private string errorMsg = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {

            var resultCode = string.Empty;//错误代码
            var resultMessage = string.Empty;//错误描述

            var parameter1 = Database.CreateParameter("resultCode", OracleDbType.Varchar2, 10,
                ParameterDirection.Output, resultCode);
            var parameter2 = Database.CreateParameter("resultMessage", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, resultCode);
            OracleParameter[] array = { parameter1, parameter2 };

            Database.RunProcRetNone("patientInterface.appTest", ref array);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var resultCode = string.Empty;//错误代码
            var resultMessage = string.Empty;//错误描述
            var patientId = string.Empty;//病人院内ID
            var inDisease = string.Empty;//诊断编码 对应医保的in_disease
            var disease = string.Empty;//诊断名称
            var inDept = string.Empty;//就诊科室代码
            var inDeptName = string.Empty;//就诊科室名称

            var parameter0 = Database.CreateParameter("registerId", OracleDbType.Varchar2, 20,
                ParameterDirection.Input, "2016121992997");
            var parameter1 = Database.CreateParameter("resultCode", OracleDbType.Varchar2, 10,
                ParameterDirection.Output, resultCode);
            var parameter2 = Database.CreateParameter("resultMessage", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, resultMessage);
            var parameter3 = Database.CreateParameter("patientId", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, patientId);
            var parameter4 = Database.CreateParameter("inDisease", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, inDisease);
            var parameter5 = Database.CreateParameter("disease", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, disease);
            var parameter6 = Database.CreateParameter("inDept", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, inDept);
            var parameter7 = Database.CreateParameter("inDeptName", OracleDbType.Varchar2, 20,
                ParameterDirection.Output, inDeptName);
            OracleParameter[] array = { parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7 };

            Database.RunProcRetNone("patientInterface.getPatientRegisterInfo", ref array);

            if (array[1].Value.ToString() == "0")
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //var errorMsg = string.Empty;
            //var infos = FeeInfo.GetFeeInfoFromHis("2016121992997", "62.75", ref errorMsg);
            //RegisterInfo info =new RegisterInfo(new PersonInfo(123));
            var p1 = new Param("邵玮", "男", 22);
            foreach (var info in p1.GetType().GetProperties())
            {
                Console.WriteLine(info.Name + @"	" + info.GetValue(p1, null));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var person = new PersonInfo();
            //if (client.Bizh131001(PinType.Idcard, "445221 19801025 4537", ref person, ref errorMsg))
            //    AppendText(person);
            //else
            //    AppendText(errorMsg);

        }

        void AppendText(object s)
        {
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(s.ToString());
            textBox1.AppendText(Environment.NewLine);
        }

        void AppendJsonText(object o)
        {
            AppendText(JsonHelper.Serialize(o));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //var obj = new object();
            //if (client.Bizh131001(PinType.Idcard, "445221198010254537", ref obj))
            //    AppendText(obj);

            var personInfos = new PersonInfo[0];
            var freezeInfo = new FreezeInfo();
            var clinicApplyInfo = new ClinicApplyInfo();

            if (client.Bizh131001(PinType.Idcard, "445221198010254537", ref personInfos, ref freezeInfo, ref clinicApplyInfo, ref errorMsg))
                AppendText(personInfos[0]);

            var regInfo = RegisterInfo.GetInfoFromHis("2016121992997", new RegisterInfo(personInfos[0]));
            var payInfo = new PayInfo();
            //if (client.Bizh131104(regInfo, ref payInfo, ref errorMsg))
            //    AppendText(payInfo);
            //else
            //    AppendText(errorMsg);
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            if (client.Bizh131102("2", PinType.SerialNo, "0060101612146557", "110", ref infos, ref p1, "11",
                ref errorMsg))
            {
                AppendText(infos.Length);
                AppendText(JsonHelper.Serialize(infos));
                AppendText(p1);
                var all = infos.All(i => i.ToString() == infos[0].ToString());
            }
            else
                AppendText(errorMsg);
            regInfo = RegisterInfo.GetInfoFromHis("2016121992997", new RegisterInfo(personInfos[0], new RegisterInfo { serial_no = "0060101612146557" }));
            payInfo = new PayInfo();
            var bizNo = string.Empty;
            var feeInfos = FeeInfo.GetFeeInfoFromHis("2016121992997", "62.75", ref errorMsg);
            AppendJsonText(feeInfos);
            if (client.Bizh131104(regInfo, feeInfos, ref bizNo, ref payInfo, ref errorMsg))
            {
                AppendText(regInfo);
                AppendText(payInfo);
                AppendText(bizNo);
            }
            else
                AppendText(errorMsg);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //查询登记信息
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            var refFeeInfo = new RefFeeInfo[1];
            var feeBatchInfo = new FeeBatchInfo[1];
            if (client.Bizh131102("3", PinType.SerialNo, "0060101612146557", "110", ref infos, ref p1, "11", ref refFeeInfo, ref feeBatchInfo,
                "0", ref errorMsg))
            {
                AppendJsonText(infos);
                AppendText(p1);
                AppendJsonText(refFeeInfo);
                AppendJsonText(feeBatchInfo);
            }
            else
            {
                AppendText(errorMsg);
            }
            ;


        }
    }
}
