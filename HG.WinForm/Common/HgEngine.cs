﻿#region Header

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
// Project:    HG.WinForm
// File:       HgEngine.cs
// Author:     www.wuleba.com
// CreateDate: 2016-12-07 12:40
// ModifyDate: 2016-12-07 13:41
// --------------------------------------------------------------------------------------------------------------------

#endregion

using System.Runtime.InteropServices;

namespace HG.WinForm.Common
{
    /// <summary>
    /// 医保卡引擎接口
    /// </summary>
    public class HgEngine
    {
        [DllImport("Libraries\\HG_Interface.dll", EntryPoint = "newinterface", CallingConvention = CallingConvention.Cdecl)]
        public static extern int newinterface();
    }
}