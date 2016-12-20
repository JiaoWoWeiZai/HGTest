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

using System.ServiceModel;

namespace CrHgWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ICrHgService”。
    [ServiceContract]
    public interface ICrHgService
    {

        [OperationContract]
        string GetPersonJsonByIdNum(string name, string idNum);

        [OperationContract]
        string Settlement(string idNum, string preNum);


        [OperationContract]
        string Login(string username, string password);

        [OperationContract]
        int InitNewInterface();

        [OperationContract]
        int NewInterface();

        [OperationContract]
        int NewInterfaceWithInit(string addr, int port, string servlet);

        [OperationContract]
        int Init(int pint, string addr, int port, string servlet);

        [OperationContract]
        void DestoryInterface(int pint);

        [OperationContract]
        int Start(int pint, string id);

        [OperationContract]
        int Put(int pint, int row, string pname, string pvalue);

        [OperationContract]
        int PutCol(int pint, string pname, string pvalue);

        [OperationContract]
        int Run(int pint);

        [OperationContract]
        int SetResultSet(int pint, string resultName);

        [OperationContract]
        int RunXml(int print, string xml);

        [OperationContract]
        int GetByName(int pint, string pname,ref string pvalue);

        [OperationContract]
        int GetXmlStr_t(int print, ref string xml);

        [OperationContract]
        int GetXmlStr(int print, ref string xml);

        [OperationContract]
        int GetByIndex(int pint, int index,ref string pname,ref string pvalue);

        [OperationContract]
        int GetMessage(int pint,ref string msg);

        [OperationContract]
        int GetException(int pint,ref string msg);

        [OperationContract]
        int GetRowCount(int pint);

        [OperationContract]
        int GetCode(int print, ref string msg);

        [OperationContract]
        int GeterrType(int print);

        [OperationContract]
        int FirstRow(int pint);

        [OperationContract]
        int NextRow(int pint);

        [OperationContract]
        int PrevRow(int pint);

        [OperationContract]
        int LastRow(int pint);

        [OperationContract]
        int SetIcCommport(int pinter, int comm);

        [OperationContract]
        int GetResultNameByIndex(int pinter, int index, ref string resultname);

        [OperationContract]
        int SetDebug(int pint, int flag, string direct);
    }

}
