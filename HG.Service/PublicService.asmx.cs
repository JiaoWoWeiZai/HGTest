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
// Project:    WcfService1
// File:       PublicService.asmx.cs
// Author:     www.wuleba.com
// CreateDate: 2016-12-07 14:02
// ModifyDate: 2016-12-07 14:05
// --------------------------------------------------------------------------------------------------------------------

#endregion

using System;
using System.ComponentModel;
using System.Web.Services;
using HG.Console.Common;

namespace HG.Service
{
    /// <summary>
    /// PublicService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PublicService : WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string InterfaceTest()
        {
            string result;

            try
            {
                result = HgEngine.newinterface().ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}