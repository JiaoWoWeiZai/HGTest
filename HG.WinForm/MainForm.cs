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
// Project:    HG.WinForm
// File:       MainForm.cs
// Author:     www.wuleba.com
// CreateDate: 2016-12-07 12:36
// ModifyDate: 2016-12-07 13:41
// --------------------------------------------------------------------------------------------------------------------

#endregion

using System;
using System.Windows.Forms;
using HG.WinForm.Common;

namespace HG.WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            MessageBox.Show(InterfaceTest(), "WinForm方法调用成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
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