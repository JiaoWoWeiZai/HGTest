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
using CrHgWcfService.Model;
// ReSharper disable InconsistentNaming

namespace CrHgWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CrHgService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 CrHgService.svc 或 CrHgService.svc.cs，然后开始调试。
    public class CrHgService : ICrHgService
    {
        private readonly MedicareClient client = new MedicareClient();
        private string errorMsg = string.Empty;

        /// <summary>
        /// 通过身份证查询医保病人信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idNum">身份证号</param>
        /// <returns>方法调用结果</returns>
        public string GetPersonInfoByIdNum(string name, string idNum)
        {
            client.Login();
            var obj = new object();
            client.Bizh131001(PinType.Idcard, idNum, name, ref obj);
            return Json(obj);
        }

        /// <summary>
        /// 挂号收费
        /// </summary>
        /// <param name="idNum">身份证号</param>
        /// <param name="registerId">处方号</param>
        /// <param name="totalAmt">总金额</param>
        /// <returns>方法调用结果</returns>
        public string Registration(string idNum, string registerId, string totalAmt)
        {
            client.Login();
            var personInfos = new PersonInfo[0];
            var freezeInfo = new FreezeInfo();
            var clinicApplyInfo = new ClinicApplyInfo();

            //查询患者信息
            if (!client.Bizh131001(PinType.Idcard, idNum, ref personInfos, ref freezeInfo,
                ref clinicApplyInfo, ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = errorMsg
                });
            //通过用户信息和His查询初始化挂号所需的信息
            var regInfo = RegisterInfo.GetInfoFromHis(registerId, new RegisterInfo(personInfos[0]));
            var payInfo = new PayInfo();
            //挂号
            if (!client.Bizh131104(regInfo, ref payInfo, ref errorMsg))
                return Json(new
                {
                    StatusCode = 0,
                    ResultMessage = "挂号失败,请重试"
                });
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            //查询系统中是否存在相应信息
            if (!client.Bizh131102("2", PinType.SerialNo, payInfo.serial_no, "110", ref infos, ref p1, "11",
                ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "挂号成功但服务器中不存在对应信息，请重试"
                });
            var serialNo = payInfo.serial_no;
            //初始化收费所需登记信息
            regInfo = RegisterInfo.GetInfoFromHis(registerId,
                new RegisterInfo(personInfos[0], new RegisterInfo { serial_no = serialNo }));
            payInfo = new PayInfo();
            var bizNo = string.Empty;
            //获得处方药品明细
            var feeInfos = FeeInfo.GetFeeInfoFromHis(registerId, totalAmt, ref errorMsg);
            //收费
            if (client.Bizh131104(regInfo, feeInfos, ref bizNo, ref payInfo, ref errorMsg))
            {
                return Json(new
                {
                    StatusCode = 0,
                    PayInfo = payInfo,
                    ResultMessage = "请求成功"
                });
            }
            //取消挂号
            if (client.CancellationRegister(serialNo, ref errorMsg))
            {
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "挂号成功但收费失败,已取消挂号"
                });
            }
            return Json(new
            {
                StatusCode = -1,
                ResultMessage = $"挂号成功但收费失败,取消挂号失败:{errorMsg}"
            });
        }

        /// <summary>
        /// 退费并取消挂号
        /// </summary>
        /// <param name="serialNo">业务序列号</param>
        /// <returns></returns>
        public string Unregister(string serialNo)
        {
            client.Login();
            if (!(client.ReturnPremium(serialNo, ref errorMsg) && client.CancellationRegister(serialNo, ref errorMsg)))
            {
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = errorMsg
                });
            }

            return Json(new
            {
                StatusCode = 0,
                ResultMessage = "取消成功"
            });
        }

        /// <summary>
        /// 收费试算
        /// </summary>
        /// <param name="idNum"></param>
        /// <param name="registerId"></param>
        /// <param name="totalAmt"></param>
        /// <returns></returns>
        public string TrialBalance(string idNum, string registerId, string totalAmt)
        {
            client.Login();
            var personInfos = new PersonInfo[0];
            var freezeInfo = new FreezeInfo();
            var clinicApplyInfo = new ClinicApplyInfo();

            //查询患者信息
            if (!client.Bizh131001(PinType.Idcard, idNum, ref personInfos, ref freezeInfo,
                ref clinicApplyInfo, ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = errorMsg
                });
            //通过用户信息和His查询初始化挂号所需的信息
            var regInfo = RegisterInfo.GetInfoFromHis(registerId, new RegisterInfo(personInfos[0]));
            var payInfo = new PayInfo();
            //挂号
            if (!client.Bizh131104(regInfo, ref payInfo, ref errorMsg))
                return Json(new
                {
                    StatusCode = 0,
                    ResultMessage = "挂号失败,请重试"
                });
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            //查询系统中是否存在相应信息
            if (!client.Bizh131102("2", PinType.SerialNo, payInfo.serial_no, "110", ref infos, ref p1, "11",
                ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "挂号成功但服务器中不存在对应信息，请重试"
                });
            var serialNo = payInfo.serial_no;
            //初始化收费所需登记信息
            regInfo = RegisterInfo.GetInfoFromHis(registerId,
                new RegisterInfo(personInfos[0], new RegisterInfo { serial_no = serialNo }));
            payInfo = new PayInfo();
            var bizNo = string.Empty;
            //获得处方药品明细
            var feeInfos = FeeInfo.GetFeeInfoFromHis(registerId, totalAmt, ref errorMsg);
            //试算
            if (client.Bizh131104(regInfo, feeInfos, ref bizNo, ref payInfo, ref errorMsg, "11", "2", "0"))
            {
                client.CancellationRegister(serialNo, ref errorMsg);
                return Json(new
                {
                    StatusCode = 0,
                    PayInfo = payInfo,
                    ResultMessage = "请求成功"
                });
            }
            //取消挂号
            if (client.CancellationRegister(serialNo, ref errorMsg))
            {
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "挂号成功但试算失败,已取消挂号"
                });
            }
            return Json(new
            {
                StatusCode = -1,
                ResultMessage = $"挂号成功但试算失败,取消挂号失败:{ errorMsg }"
            });
        }


        private static string Json(object obj) => JsonHelper.Serialize(obj);
    }
}
