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
// File:       ClientForm.cs
// Author:     www.wuleba.com
// CreateDate: 2016-12-07 15:01
// ModifyDate: 2016-12-07 15:03
// --------------------------------------------------------------------------------------------------------------------

#endregion

using System.Windows.Forms;
using HG.WinForm.PublicService;

namespace HG.WinForm
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();

            MessageBox.Show(InterfaceTest(), "WCF接口调用成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string InterfaceTest()
        {
            using (var client = new PublicServiceClient())
            {
                return client.InterfaceTest();
            }
        }
    }
}