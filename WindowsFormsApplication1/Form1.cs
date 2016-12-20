using System;
using System.Data;
using System.Windows.Forms;
using CrHgWcfService.Model;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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
            RegisterInfo info = new RegisterInfo();
            foreach (var variable in info.GetType().GetProperties())
            {
                Console.WriteLine(variable.Name + @"	" + variable.GetValue(info));
            }
            ;
        }
    }
}
