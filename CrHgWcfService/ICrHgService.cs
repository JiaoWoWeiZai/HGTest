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
        string GetPersonInfoByIdNum(string name, string idNum);

        [OperationContract]
        string Registration(string idNum, string registerId, string totalAmt);

        [OperationContract]
        string Unregister(string serialNo);

        [OperationContract]
        string TrialBalance(string idNum, string registerId, string totalAmt);

    }

}
