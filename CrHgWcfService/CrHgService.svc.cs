#region Header

// --------------------------------------------------------------------------------------------------------------------
//  ┏┓　　　┏┓
// ┏┛┻━━━┛┻┓
// ┃　　　　　　　┃ 　
// ┃　　　━　　　┃
// ┃　┳┛　┗┳　┃
// ┃　　　　　　　┃
// ┃　　　┻　　　┃
// ┃　　　　　　　┃
// ┗━┓　　　┏━┛
//     ┃　　　┃   神兽保佑　　　　　　　　　
//     ┃　　　┃   代码无BUG！
//     ┃　　　┗━━━┓
//     ┃　　　　　　　┣┓
//     ┃　　　　　　　┏┛
//     ┗┓┓┏━┳┓┏┛
//       ┃┫┫　┃┫┫
//       ┗┻┛　┗┻┛
// 
// Copyirght:  Copyright (C) 2016 - FStudio All rights reserved
// Solution:   HG.WinForm
// Project:    HG.Service2
// File:       HgEngine.cs
// Author:     www.wuleba.com
// CreateDate: 2016-12-07 13:41
// ModifyDate: 2016-12-07 13:43
// --------------------------------------------------------------------------------------------------------------------

#endregion

using System;
using System.Linq;
using CrHgWcfService.Common;
using CrHgWcfService.Model;

namespace CrHgWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CrHgService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 CrHgService.svc 或 CrHgService.svc.cs，然后开始调试。
    public class CrHgService : ICrHgService
    {
        private const string Server = "http://192.168.6.9/HygeiaWebService/web/ProcessAll.asmx";
        private const int Port = 7001;
        private const string Servlet = "hygeia";
        private const string UserName = "hexu";
        private const string PassWord = "hexu";

        private int _interfaceId;
        private int InterfaceId
        {
            get
            {
                while (_interfaceId <= 0)
                {
                    _interfaceId = HgEngine.NewInterfaceWithInit(Server, Port, Servlet);
                }
                return _interfaceId;
            }
        }

        private bool RunService(string funId, ref string errorInfo, params Param[] pars)
        {
            if (errorInfo == null) throw new ArgumentNullException(nameof(errorInfo));
            if (InterfaceId <= 0)
            {
                InitNewInterface();
            }
            var interfaceId = InterfaceId; //NewInterfaceWithInit(Server, Port, Servlet);
            if (interfaceId < 0)
            {
                errorInfo = "创建接口失败";
                return false;
            }
            if (Init(interfaceId, Server, Port, Servlet) < 0)
            {
                errorInfo = "初始化接口失败";
                return false;
            }
            //var funId = "BIZH131001";
            if (Start(interfaceId, funId) < 0)
            {
                errorInfo = "方法初始化失败";
                return false;
            }
            if (pars.Any(param => Put(interfaceId, 1, param.Name, param.Value) < 0))
            {
                errorInfo = "参数添加失败";
                return false;
            }
            if (Run(interfaceId) < 0)
            {
                var errorMsg = string.Empty;
                GetMessage(interfaceId, ref errorMsg);

                errorInfo = errorMsg;
                return false;
            }
            errorInfo = "执行成功";
            return true;
        }

        //public T GetData<T>()


        public int InitNewInterface()
        {
            return InterfaceId;
        }

        public string Login(string username, string password)
        {
            var s = string.Empty;
            RunService("0", ref s, new Param("login_id", username), new Param("login_password", password));
            return s;
        }

        /// <summary>
        /// 通过身份证查询医保病人信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idNum">身份证号</param>
        /// <returns>方法调用结果</returns>
        public string GetPersonJsonByIdNum(string name, string idNum)
        {
            var errorMsg = string.Empty;
            var info = GetPersonInfoByIdNum(idNum, ref errorMsg);
            if (info == null) return errorMsg;
            return name == info.name ? JsonHelper.Serialize(info) : "姓名与身份证号不匹配！";
        }

        public PersonInfo GetPersonInfoByIdNum(string idNum, ref string errorMsg)
        {
            var s = string.Empty;
            Login(UserName, PassWord);
            if (!RunService("BIZH131001", ref s, new Param("idcard", idNum), new Param("hospital_id", "006010"),
                new Param("treatment_type", "110"), new Param("biz_type", "11"),
                new Param("biz_date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))))
            {
                errorMsg = s;
                return null;
            }
            SetResultSet(InterfaceId, "personinfo");
            var size = GetRowCount(InterfaceId);

            if (size == 1) return new PersonInfo(InterfaceId);
            errorMsg = size > 1 ? "数据多于一条,查询错误！" : "数据少于一条,查询错误！";
            return null;
        }

        /// <summary>
        /// 医保就医登记
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idNum">身份证号</param>
        /// <param name="preNum">处方号</param>
        /// <returns>方法调用结果</returns>
        public string Settlement(string idNum, string preNum)
        {
            var errorMsg = string.Empty;
            var person = GetPersonInfoByIdNum(idNum, ref errorMsg);
            if (person == null) return errorMsg;
            var register = new RegisterInfo(person);
            register = RegisterInfo.GetInfoFromHis(preNum, register);


            return "方法正在构造,敬请期待！";
        }

        #region Dll原生方法

        public int NewInterface()
        {
            return HgEngine.NewInterface();
        }

        public int NewInterfaceWithInit(string addr = Server, int port = Port, string servlet = Servlet)
        {
            return HgEngine.NewInterfaceWithInit(addr, port, servlet);
        }

        public int Init(int print, string addr = Server, int port = Port, string servlet = Servlet)
        {
            return HgEngine.Init(print, addr, port, servlet);
        }

        public void DestoryInterface(int print)
        {
            HgEngine.DestoryInterface(print);
        }

        public int Start(int print, string id)
        {
            return HgEngine.Start(print, id);
        }

        public int Put(int print, int row, string pname, string pvalue)
        {
            return HgEngine.Put(print, row, pname, pvalue);
        }

        public int PutCol(int print, string pname, string pvalue)
        {
            return HgEngine.PutCol(print, pname, pvalue);
        }

        public int Run(int print)
        {
            return HgEngine.Run(print);
        }

        public int SetResultSet(int print, string resultName)
        {
            return HgEngine.SetResultSet(print, resultName);
        }

        public int RunXml(int print, string xml)
        {
            return HgEngine.runxml(print, xml);
        }

        public int GetXmlStr_t(int print, ref string xml)
        {
            return HgEngine.getxmlstr_t(print, ref xml);
        }

        public int GetXmlStr(int print, ref string xml)
        {
            return HgEngine.getxmlstr(print, ref xml);
        }

        public int GetByName(int print, string pname, ref string pvalue)
        {
            return HgEngine.GetByName(print, pname, ref pvalue);
        }

        public int GetByIndex(int print, int index, ref string pname, ref string pvalue)
        {
            return HgEngine.GetByIndex(print, index, ref pname, ref pvalue);
        }

        public int GetMessage(int print, ref string msg)
        {
            return HgEngine.GetMessage(print, ref msg);
        }

        public int GetException(int print, ref string msg)
        {
            return HgEngine.GetException(print, ref msg);
        }

        public int GetRowCount(int print)
        {
            return HgEngine.GetRowCount(print);
        }

        public int GetCode(int print, ref string msg)
        {
            return HgEngine.GetCode(print, ref msg);
        }

        public int GeterrType(int print)
        {
            return HgEngine.geterrtype(print);
        }

        public int FirstRow(int print)
        {
            return HgEngine.FirstRow(print);
        }

        public int NextRow(int print)
        {
            return HgEngine.NextRow(print);
        }

        public int PrevRow(int print)
        {
            return HgEngine.PrevRow(print);
        }

        public int LastRow(int print)
        {
            return HgEngine.LastRow(print);
        }

        public int SetIcCommport(int pinter, int comm)
        {
            return HgEngine.SetIcCommport(pinter, comm);
        }

        public int GetResultNameByIndex(int pinter, int index, ref string resultname)
        {
            return HgEngine.GetResultNameByIndex(pinter, index, ref resultname);
        }

        public int SetDebug(int print, int flag, string direct)
        {
            return HgEngine.SetDebug(print, flag, direct);
        }


        #endregion
    }
}
