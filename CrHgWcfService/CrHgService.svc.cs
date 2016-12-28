#region Header

//-------------------------------------------------------------------------------------------------------------------
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
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml;
using CrHgWcfService.Common;
using CrHgWcfService.Model;
using log4net;
using Newtonsoft.Json;

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
            //var context = OperationContext.Current.RequestContext.RequestMessage.ToString();
            //ar doc = new XmlDocument();
            //doc.LoadXml(context);
            //var json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            //var logger = LogManager.GetLogger("errorMsg");
            //logger.Error();

            //进行登录业务
            if (!client.Login())
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "登录失败,请重试!"
                });
            //接收查询个人信息业务的返回值
            var obj = new object();
            //查询个人业务
            client.Bizh131001(PinType.Idcard, idNum, name, ref obj);
            //序列化查询信息并返回
            return Json(obj);
        }

        /// <summary>
        /// 挂号收费
        /// </summary>
        /// <param name="idNum">身份证号</param>
        /// <param name="registerId">处方号</param>
        /// <param name="totalAmt">总金额</param>
        /// <param name="operNo"></param>
        /// <param name="opertor"></param>
        /// <returns>方法调用结果</returns>
        public string Registration(string idNum, string registerId, string totalAmt, string operNo, string opertor)
        {
            //进行登录业务
            if (!client.Login())
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "登录失败,请重试!"
                });
            //设置操作者信息
            client.SetOper(operNo, opertor);

            //初始化变量
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

            //通过用户信息和HIS查询初始化挂号所需的信息
            var regInfo = new RegisterInfo();
            if (!RegisterInfo.GetInfoFromHis(registerId, ref regInfo, new RegisterInfo(personInfos[0])))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "查询HIS流水信息失败,请检查流水号是否正确!"
                });
            var payInfo = new PayInfo();
            //挂号
            if (!client.Bizh131104(regInfo, ref payInfo, ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"挂号失败,请重试,{errorMsg}"
                });
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            //查询系统中是否存在相应信息
            if (!client.Bizh131102("2", PinType.SerialNo, payInfo.serial_no, "110", ref infos, ref p1, "11",
                ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"挂号成功但服务器中不存在对应信息，请重试,{errorMsg}"
                });
            var serialNo = payInfo.serial_no;
            //初始化收费所需登记信息
            if (!RegisterInfo.GetInfoFromHis(registerId, ref regInfo, new RegisterInfo(personInfos[0], new RegisterInfo { serial_no = serialNo })))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "查询HIS流水信息失败,请检查流水号是否正确!"
                });
            payInfo = new PayInfo();
            var bizNo = string.Empty;
            //获得处方药品明细
            var feeInfos = new FeeInfo[1];
            if(!FeeInfo.GetFeeInfoFromHis(registerId, totalAmt,ref feeInfos, ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"获取处方明细失败,请检查流水号与金额的正确性与匹配性:{errorMsg}"
                });
            //收费
            if (client.Bizh131104(regInfo, feeInfos, ref bizNo, ref payInfo, ref errorMsg))
            {
                if (payInfo.SaveToHis(registerId, 0, operNo, ref errorMsg))
                    return Json(new
                    {
                        StatusCode = 0,
                        PayInfo = payInfo,
                        ResultMessage = "请求成功"
                    });
                var sqlErrorMsg = errorMsg;
                if (!client.CancellationRegister(serialNo, ref errorMsg))
                    return Json(new
                    {
                        StatusCode = -1,
                        ResultMessage = $"数据库访问失败，取消挂号失败，数据库错误信息{ sqlErrorMsg}，请重试！"
                    });
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"数据库访问失败，已取消挂号，数据库错误信息{sqlErrorMsg}"
                });
            }
            //取消挂号
            if (client.CancellationRegister(serialNo, ref errorMsg))
            {
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"挂号成功但收费失败,已取消挂号,{errorMsg}"
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
        /// <param name="registerId">处方号</param>
        /// <param name="operNo"></param>
        /// <param name="opertor"></param>
        /// <returns></returns>
        public string Unregister(string serialNo, string registerId, string operNo, string opertor)
        {
            //进行登录业务
            if (!client.Login())
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "登录失败,请重试!"
                });
            var payInfo = new PayInfo();
            client.SetOper(operNo, opertor);
            if (!(client.ReturnPremium(serialNo, ref payInfo, ref errorMsg) && client.CancellationRegister(serialNo, ref errorMsg)))
            {
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = errorMsg
                });
            }
            if (payInfo.SaveToHis(registerId, 9, operNo, ref errorMsg))
                return Json(new
                {
                    StatusCode = 0,
                    PayInfo = payInfo,
                    ResultMessage = "请求成功"
                });
            var sqlErrorMsg = errorMsg;
            if (!client.CancellationRegister(serialNo, ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"数据库访问失败，取消挂号失败，数据库错误信息{sqlErrorMsg}，请重试！"
                });
            return Json(new
            {
                StatusCode = -1,
                ResultMessage = $"数据库访问失败，已取消挂号，数据库错误信息{sqlErrorMsg}"
            });
        }

        /// <summary>
        /// 收费试算
        /// </summary>
        /// <param name="idNum"></param>
        /// <param name="registerId"></param>
        /// <param name="totalAmt"></param>
        /// <returns></returns>
        public string TrialBalance(string idNum, string registerId, string totalAmt, string operNo, string opertor)
        {
            //进行登录业务
            if (!client.Login())
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "登录失败,请重试!"
                });
            //设置操作者信息
            client.SetOper(operNo, opertor);

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
            //通过用户信息和HIS查询初始化挂号所需的信息
            var regInfo = new RegisterInfo();
            if (!RegisterInfo.GetInfoFromHis(registerId, ref regInfo, new RegisterInfo(personInfos[0])))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "查询HIS流水信息失败,请检查流水号是否正确!"
                });
            var payInfo = new PayInfo();
            //挂号
            if (!client.Bizh131104(regInfo, ref payInfo, ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"挂号失败,请重试,{errorMsg}"
                });
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            //查询系统中是否存在相应信息
            if (!client.Bizh131102("2", PinType.SerialNo, payInfo.serial_no, "110", ref infos, ref p1, "11",
                ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"挂号成功但服务器中不存在对应信息，请重试,{errorMsg}"
                });
            var serialNo = payInfo.serial_no;
            //初始化收费所需登记信息
            if (!RegisterInfo.GetInfoFromHis(registerId, ref regInfo, new RegisterInfo(personInfos[0], new RegisterInfo { serial_no = serialNo })))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = "查询HIS流水信息失败,请检查流水号是否正确!"
                });
            payInfo = new PayInfo();
            var bizNo = string.Empty;
            //获得处方药品明细
            var feeInfos = new FeeInfo[1];
            if (!FeeInfo.GetFeeInfoFromHis(registerId, totalAmt, ref feeInfos, ref errorMsg))
                return Json(new
                {
                    StatusCode = -1,
                    ResultMessage = $"获取处方明细失败,请检查流水号与金额的正确性与匹配性:{errorMsg}"
                });
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
                    ResultMessage = $"挂号成功但试算失败,已取消挂号,{errorMsg}"
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
