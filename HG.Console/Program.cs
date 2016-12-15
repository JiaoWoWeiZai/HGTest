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
// Project:    HG.Console
// File:       Program.cs
// Author:     www.wuleba.com
// CreateDate: 2016-12-07 13:41
// ModifyDate: 2016-12-07 13:47
// --------------------------------------------------------------------------------------------------------------------

#endregion

using System;
using HG.Console.Common;

namespace HG.Console
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            System.Console.WriteLine(InterfaceTest());
            System.Console.ReadKey();
        }

        public static string InterfaceTest()
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