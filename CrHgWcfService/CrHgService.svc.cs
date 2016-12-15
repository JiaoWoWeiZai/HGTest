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

using CrHgWcfService.Common;

namespace CrHgWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CrHgService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 CrHgService.svc 或 CrHgService.svc.cs，然后开始调试。
    public class CrHgService : ICrHgService
    {
        public int NewInterface()
        {
            return HgEngine.newinterface();
        }

        public int NewInterfaceWithInit(string addr, int port, string servlet)
        {
            return HgEngine.newinterfacewithinit(addr, port, servlet);
        }

        public int Init(int print, string addr, int port, string servlet)
        {
            return HgEngine.init(print, addr, port, servlet);
        }

        public void DestoryInterface(int print)
        {
            HgEngine.destoryinterface(print);
        }

        public int Start(int print, string id)
        {
            return HgEngine.start(print, id);
        }

        public int Put(int print, int row, string pname, string pvalue)
        {
            return HgEngine.put(print, row, pname, pvalue);
        }

        public int PutCol(int print, string pname, string pvalue)
        {
            return HgEngine.putcol(print, pname, pvalue);
        }

        public int Run(int print)
        {
            return HgEngine.run(print);
        }

        public int SetResultSet(int print, string resultName)
        {
            return HgEngine.setresultset(print, resultName);
        }

        public int RunXml(int print, string xml)
        {
            return HgEngine.runxml(print, xml);
        }

        public int GetXmlStr_t(int print,ref string xml)
        {
            return HgEngine.getxmlstr_t(print,ref xml);
        }

        public int GetXmlStr(int print, ref string xml)
        {
            return HgEngine.getxmlstr(print, ref xml);
        }

        public int GetByName(int print, string pname, ref string pvalue)
        {
            return HgEngine.getbyname(print, pname, ref pvalue);
        }

        public int GetByIndex(int print, int index,ref string pname, ref string pvalue)
        {
            return HgEngine.getbyindex(print, index, pname, ref pvalue);
        }

        public int GetMessage(int print, ref string msg)
        {
            return HgEngine.getmessage(print, ref msg);
        }

        public int GetException(int print, ref string msg)
        {
            return HgEngine.getexception(print, ref msg);
        }

        public int GetRowCount(int print)
        {
            return HgEngine.getrowcount(print);
        }

        public int GetCode(int print, ref string msg)
        {
            return HgEngine.getcode(print, ref msg);
        }

        public int GeterrType(int print)
        {
            return HgEngine.geterrtype(print);
        }

        public int FirstRow(int print)
        {
            return HgEngine.firstrow(print);
        }

        public int NextRow(int print)
        {
            return HgEngine.nextrow(print);
        }

        public int PrevRow(int print)
        {
            return HgEngine.prevrow(print);
        }

        public int LastRow(int print)
        {
            return HgEngine.lastrow(print);
        }

        public int SetIcCommport(int pinter, int comm)
        {
            return HgEngine.set_ic_commport(pinter, comm);
        }

        public int GetResultNameByIndex(int pinter, int index, string resultname)
        {
            return HgEngine.getresultnamebyindex(pinter, index, resultname);
        }

        public int SetDebug(int print, int flag, string direct)
        {
            return HgEngine.setdebug(print, flag, direct);
        }
    }
}
