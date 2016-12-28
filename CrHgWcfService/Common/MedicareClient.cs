using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrHgWcfService.Model;

namespace CrHgWcfService.Common
{
    public class MedicareClient : HgEngine
    {
        private const string HospitalId = "006010"; //医院代码
        public string OperNo = "CR"; //操作者ID
        public string Operator = "CR"; //操作者

        private string _errorMsg = string.Empty;
        private string _directory = string.Empty;

        public bool SetOper(string operNo,string opertor)
        {
            OperNo = operNo;
            Operator = opertor;
            return true;
        }

        public MedicareClient()
        {
            InitClient(ref _errorMsg);
        }

        //public static MedicareClient()
        //{

        //}

        public string GetError() => _errorMsg;

        /// <summary>
        /// 唯一接口号
        /// </summary>
        public int PInter { get; set; }


        /// <summary>
        /// 获取PInter并登陆
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool InitClient(ref string msg)
        {
            if (msg == null) throw new ArgumentNullException(nameof(msg));
            if (!Directory.Exists(Environment.CurrentDirectory + @"log\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"log\");
            }
            _directory = Environment.CurrentDirectory + @"log\";
            const string server = "http://192.168.6.9/HygeiaWebService/web/ProcessAll.asmx";
            const int port = 7001;
            const string servlet = "hygeia";
            var pInterface = NewInterface();
            PInter = pInterface;
            if (pInterface < 0)
            {
                _errorMsg = "创建接口失败";
            }
            var ret = Init(pInterface, server, port, servlet);
            if (ret < 0)
            {
                _errorMsg = "初始化接口失败";
            }
            if (RunService("0", ref _errorMsg, new Param("login_id", "hexu"), new Param("login_password", "hexu")))
            {
                msg = "执行成功";
                return true;
            }
            msg = _errorMsg;
            return false;
        }

        public bool Login()
        {
            //const string server = "http://192.168.6.9/HygeiaWebService/web/ProcessAll.asmx";
            //const int port = 7001;
            //const string servlet = "hygeia";
            //PInter = NewInterface();
            //var ret = Init(PInter, server, port, servlet);
            return RunService("0", ref _errorMsg, new Param("login_id", "hexu"), new Param("login_password", "hexu"));
        }

        /// <summary>
        /// 通过个人标识取人员信息
        /// </summary>
        /// <param name="pinType">个人标识类型</param>
        /// <param name="pin">标识号</param>
        /// <param name="personInfo">返回 个人信息</param>
        /// <param name="clinicapplyInfo"></param>
        /// <param name="errorMsg">返回 错误信息</param>
        /// <param name="freezeInfo"></param>
        /// <returns>是否成功完成业务</returns>
        public bool Bizh131001(PinType pinType, string pin, ref PersonInfo[] personInfo, ref FreezeInfo freezeInfo, ref ClinicApplyInfo clinicapplyInfo, ref string errorMsg)
        {
            if (errorMsg == null) throw new ArgumentNullException(nameof(errorMsg));
            pin = pin.Replace(" ", "");
            var args = new List<Param>();
            switch (pinType)
            {
                case PinType.IcNo:
                    args.Add(new Param("ic_no", pin));
                    break;
                case PinType.Name:
                    args.Add(new Param("name", pin));
                    break;
                case PinType.IndiId:
                    args.Add(new Param("indi_id", pin));
                    break;
                case PinType.Idcard:
                    args.Add(new Param("idcard", pin));
                    break;
                case PinType.SerialNo:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pinType), pinType, null);
            }
            args.AddRange(new[]
            {
                new Param("hospital_id", "006010"),
                new Param("treatment_type", "110"), new Param("biz_type", "11"),
                new Param("biz_date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
            });
            var s = string.Empty;
            if (!RunService("BIZH131001", ref s, args.ToArray()))
            {
                errorMsg = s;
                return false;
            }
            var size = GetRowCount(PInter);
            if (size == 1)
            {
                personInfo = new PersonInfo[1];
                personInfo[0] = GetInfo<PersonInfo>("personinfo");
                freezeInfo = GetInfo<FreezeInfo>("freezeinfo");
                clinicapplyInfo = GetInfo<ClinicApplyInfo>("clinicapplyinfo");
                errorMsg = "执行成功";
                return true;
            }
            if (size > 1)
            {
                personInfo = GetInfos<PersonInfo>("personinfo");
                //personInfo = new PersonInfo[size];
                //for (var i = 0; i < size; i++, NextRow(PInter))
                //    personInfo[i] = GetInfo<PersonInfo>("personinfo");
                errorMsg = "执行成功";
                return true;
            }
            errorMsg = size > 1 ? "数据多于一条,查询错误！" : "数据少于一条,查询错误！";
            return false;
        }

        /// <summary>
        /// 通过个人标识取人员信息
        /// </summary>
        /// <param name="pinType">个人标识类型</param>
        /// <param name="pin">标识号</param>
        /// <param name="name">用户姓名</param>
        /// <param name="personObject">返回 个人信息</param>
        /// <returns></returns>
        public bool Bizh131001(PinType pinType, string pin, string name, ref object personObject)
        {
            if (personObject == null) throw new ArgumentNullException(nameof(personObject));
            var personInfos = new PersonInfo[1];
            var freezeInfo = new FreezeInfo();
            var clinicApplyInfo = new ClinicApplyInfo();
            var errorMsg = string.Empty;
            var isSuccess = Bizh131001(pinType, pin, ref personInfos, ref freezeInfo, ref clinicApplyInfo, ref errorMsg);
            if (isSuccess)
                if (personInfos[0].name == name)
                    if (personInfos.Length > 1)
                        personObject =
                            new
                            {
                                StatusCode = 0,
                                PersonInfos = personInfos,
                                ResultMessage = "请求成功"
                            };
                    else
                        personObject =
                            new
                            {
                                StatusCode = 0,
                                PersonInfo = personInfos[0],
                                FreezeInfo = freezeInfo,
                                ClinicApplyInfo = clinicApplyInfo,
                                ResultMessage = "请求成功"
                            };
                else
                    personObject =
                    new
                    {
                        StatusCode = -1,
                        ResultMessage = "姓名与身份证号码不匹配"
                    };
            else
                personObject =
                    new
                    {
                        StatusCode = -1,
                        ResultMessage = errorMsg
                    };
            return isSuccess;

        }

        /// <summary>
        /// 取消门诊挂号
        /// </summary>
        /// <param name="indiId"></param>
        /// <param name="serialNo"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool Bizh131105(string indiId, string serialNo, ref string errorMsg)
        {
            return RunService("BIZH131105", ref errorMsg, new Param("indi_id", indiId), new Param("hospital_id", HospitalId),
                new Param("serial_no", serialNo), new Param("fin_staff", "CR"), new Param("fin_man", "CR"));
        }

        /// <summary>
        /// 挂号
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="payInfo"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool Bizh131104(RegisterInfo regInfo, ref PayInfo payInfo, ref string errorMsg)
        {
            Param[] args =
            {
                new Param("save_flag", "1"),
                new Param("oper_flag", "1"),
                new Param("center_id", "620013"),
                new Param("hospital_id", HospitalId),
                new Param("serial_apply", "0"),
                new Param("patient_flag", "0"),
                new Param("diagnose_flag", "0"),
                new Param("reg_staff", OperNo),
                new Param("reg_man", Operator),
                new Param("indi_id", regInfo.indi_id),
                new Param("biz_type", "11"),
                new Param("treatment_type", regInfo.treatment_type),
                new Param("reg_flag", "1"),
                new Param("reg_date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")),
                new Param("diagnose_date",DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")),
                new Param("in_area", "001"),
                new Param("in_area_name","门诊部"),
                new Param("ic_flag", "0"),
                new Param("recipe_no", regInfo.recipe_no),
                new Param("fin_info", "0"),
                new Param("sex", regInfo.sex),
                new Param("diagnose", regInfo.in_disease),
                new Param("in_disease", regInfo.in_disease),
                new Param("patient_id", regInfo.patient_id),
                new Param("in_dept", regInfo.in_dept),
                new Param("in_dept_name", regInfo.in_dept_name),
                new Param("last_balance", "0"),
                new Param("diagnose_code", "1"),
                new Param("diagnose_sn", "1"),
                new Param("disease", regInfo.disease),
                new Param("icd", regInfo.in_disease)
            };
            if (!RunService("BIZH131104", ref errorMsg, args)) return false;
            SetResultSet(PInter, "bizinfo");
            var value = string.Empty;
            GetByName(PInter, "serial_no", ref value);
            payInfo.serial_no = value;
            return true;
        }

        /// <summary>
        /// 校验并保存门诊登记信息和费用明细信息  
        /// </summary>
        /// <param name="regInfo">门诊登记  参数部分</param>
        /// <param name="feeInfos"></param>
        /// <param name="serialNo">返回的就医登记号</param>
        /// <param name="payInfo">返回数据中的payinfo部分</param>
        /// <param name="errorMsg"></param>
        /// <param name="bizType"></param>
        /// <param name="operFlag">收费处理:2,退费处理:3</param>
        /// <param name="saveFlag"></param>
        /// <returns></returns>
        public bool Bizh131104(RegisterInfo regInfo, FeeInfo[] feeInfos, ref string serialNo, ref PayInfo payInfo, ref string errorMsg, string bizType = "11", string operFlag = "2", string saveFlag = "1")
        {
            Param[] args =
            {
                new Param("biz_type", bizType),
                new Param("oper_flag", operFlag),
                new Param("center_id", "620013"),
                new Param("hospital_id", HospitalId),
                new Param("serial_no", regInfo.serial_no),
                new Param("indi_id", regInfo.indi_id),
                new Param("in_disease", regInfo.in_disease),
                new Param("reg_staff", OperNo),
                new Param("reg_man", Operator),
                new Param("in_dept", regInfo.in_dept),
                new Param("in_dept_name", regInfo.in_dept_name),
                new Param("patient_id", regInfo.patient_id),
                new Param("serial_apply", "0"),
                new Param("last_balance", "0"),
                new Param("save_flag", saveFlag),
                new Param("end_flag", "0"),
                new Param("fee_batch",regInfo.fee_batch),
                new Param("reg_date",DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
            };

            if (Start(PInter, "BIZH131104") < 0)
            {
                errorMsg = "方法BIZH131104初始化失败";
                return false;
            }
            if (args.Any(param => Put(PInter, param.Row, param.Name, param.Value) < 0))
            {
                errorMsg = "参数添加失败";
                return false;
            }
            if (feeInfos == null || feeInfos.Length <= 0)
            {
                errorMsg = "费用明细长度异常,请重试";
                return false;
            }


            var pList = new List<Param>();


            for (var i = 0; i < feeInfos.Length; i++)
            {
                var t = feeInfos[i];

                pList.AddRange(new[]
                {
                    new Param("medi_item_type", t.medi_item_type,i+1),

                    new Param("serial_apply", "0",i+1),
                    new Param("serial_fee", "0",i+1),
                    new Param("serial_no", regInfo.serial_no,i+1),
                    new Param("input_man", Operator,i+1),
                    new Param("input_staff", OperNo,i+1),
                    new Param("calc_flag", "0",i+1),
                    new Param("his_item_code", t.his_item_code,i+1),
                    new Param("his_item_name", t.his_item_name,i+1),

                    new Param("self_scale", "0",i+1),
                    new Param("standard", t.standard,i+1),
                    new Param("fee_date", t.fee_date,i+1),

                    new Param("input_date", t.input_date,i+1),
                    new Param("unit", t.unit,i+1),
                    new Param("price", t.price,i+1),
                    new Param("dosage", t.dosage,i+1),
                    new Param("recipe_no", t.recipe_no,i+1),
                    new Param("staple_flag", t.staple_flag,i+1),
                    new Param("reduce_money", "0",i+1),
                });
                if (operFlag == "2")
                {
                    pList.AddRange(new[]
                    {
                        new Param("opp_serial_fee", "0", i + 1),
                        new Param("money", t.money,i+1),
                        new Param("fee_batch", "1",i+1),
                        new Param("old_money", "0",i+1),
                        new Param("input_money", t.money,i+1),
                        new Param("hos_serial", t.hos_serial,i+1),
                    });
                }
                if (operFlag == "3")
                {
                    var s = (RefFeeInfo)t;
                    pList.AddRange(new[]
                   {
                        new Param("opp_serial_fee", s.serial_fee, i + 1),
                        new Param("money", "0",i+1),
                        new Param("fee_batch", s.fee_batch,i+1),
                        new Param("old_money", t.money,i+1),
                        new Param("input_money", "-"+s.money,i+1),
                        new Param("cancel_flag", "1",i+1),
                        new Param("hos_serial", i.ToString(),i+1),

                    });

                }
            }

            //pList.AddRange(feeInfos[0].GetType().GetProperties().Select(info => new Param(info.Name, info.GetValue(feeInfos[0], null).ToString(), 1)));
            SetResultSet(PInter, "feeinfo");
            if (pList.Any(param => Put(PInter, param.Row, param.Name, param.Value) < 0))
            {
                errorMsg = "参数添加失败";
                return false;
            }

            if (Run(PInter) < 0)
            {
                GetMessage(PInter, ref errorMsg);
                return false;
            }
            SetDebug();

            SetResultSet(PInter, "bizinfo");
            var value = string.Empty;
            GetByName(PInter, "serial_no", ref value);//就医登记号
            serialNo = value;
            payInfo = PayInfo.GetPayInfo(PInter);
            return true;
        }

        //public bool Bizh131104( RegisterInfo regInfo,  FeeInfo[] feeInfos, ref string refBizinfo, ref PayInfo payInfo, string bizType, string operType);//校验并保存门诊登记信息和费用明细信息  退费处理 as_oper_type = '3'

        /// <summary>
        /// 提取计算结果(BIZH000106)
        /// </summary>
        /// <param name="serialNo">医疗机构编码</param>
        /// <param name="payNo">就医登记号</param>
        /// <param name="payInfo">收费序号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public bool Bizh000106(string serialNo, string payNo, ref PayInfo payInfo, ref string errorMsg)
        {
            if (!RunService("BIZH000106", ref errorMsg, new Param("hospital_id", HospitalId), new Param("serial_no", serialNo), new Param("pay_no", payNo))) return false;
            payInfo = PayInfo.GetPayInfo(PInter);
            return true;
        }

        /// <summary>
        /// 提取门诊结算单(BIZH200102)
        /// </summary>
        /// <param name="serialNo">业务序列号</param>
        /// <param name="refAcctPay">返回 医疗费用组成信息</param>
        /// <param name="errorMsg">返回 错误信息</param>
        /// <returns></returns>
        public bool Bizh200102(string serialNo, ref string refAcctPay, ref string errorMsg)
        {
            if (!RunService("BIZH200102", ref errorMsg, new Param("hospital_id", HospitalId), new Param("serial_no", serialNo))) return false;
            var fundInfo = FundInfo.GetFundInfo(PInter);
            refAcctPay = fundInfo.ToString();
            return true;
        }

        /// <summary>
        /// 根据门慢申请序号获取门诊慢性病申请信息（BIZH120101）
        /// </summary>
        /// <param name="serialApply"></param>
        /// <param name="spInfo"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool Bizh120101(string serialApply, ref SpInfo spInfo, ref string errorMsg)
        {
            if (!RunService("BIZH120101", ref errorMsg, new Param("serial_apply", serialApply)))
                return false;
            spInfo = SpInfo.GetSpInfo(PInter);
            return true;
        }


        /// <summary>
        /// 通过就医登记号或个人标识提取门诊信息 （BIZH131102）
        /// </summary>
        /// <param name="operFlag">操作标志
        /// “ 2”：收费
        /// “ 3”： 退费
        /// </param>
        /// <param name="pinType">标识类型</param>
        /// <param name="pin">标识码</param>
        /// <param name="treatmentType">待遇类型 "110":门诊</param>
        /// <param name="bizeInfos"></param>
        /// <param name="perInfo"></param>
        /// <param name="bizType">业务类型</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool Bizh131102(string operFlag, PinType pinType, string pin, string treatmentType,
            ref BizInfo[] bizeInfos, ref PersonInfo perInfo, string bizType, ref string errorMsg)
        {
            var args = new List<Param>();
            switch (pinType)
            {
                case PinType.SerialNo:
                    args.Add(new Param("serial_no", pin));
                    break;
                case PinType.Name:
                    args.Add(new Param("name", pin));
                    break;
                case PinType.IndiId:
                    args.Add(new Param("indi_id", pin));
                    break;
                case PinType.Idcard:
                    args.Add(new Param("idcard", pin));
                    break;
                case PinType.IcNo:
                    args.Add(new Param("ic_no", pin));
                    break;
                default:
                    args.Add(new Param("ic_no", pin));
                    break;
            }
            args.AddRange(new[]
            {
                    new Param("biz_type", bizType),
                    new Param("oper_flag", operFlag),
                    new Param("hospital_id", HospitalId),
                    new Param("treatment_type", treatmentType)
                });

            var s = string.Empty;
            if (!RunService("BIZH131102", ref s, args.ToArray()))
            {
                errorMsg = s;
                return false;
            }
            var rowCount = GetRowCount(PInter);
            if (rowCount < 1)
                return false;
            if (rowCount > 1)
            {
                bizeInfos = GetInfos<BizInfo>("bizinfo");
                //bizeInfos = new BizInfo[rowCount];
                //for (var i = 0; i < rowCount; i++, NextRow(PInter))
                //    bizeInfos[i] = GetInfo<BizInfo>( "bizinfo");
                return true;
            }
            bizeInfos = new[] { GetInfo<BizInfo>("bizinfo") };
            perInfo = GetInfo<PersonInfo>("personinfo");
            return true;
        }

        public bool Bizh131102(string operFlag, PinType pinType, string pin, string treatmentType,
            ref BizInfo[] bizeInfos, ref PersonInfo perInfo, string bizType, ref RefFeeInfo[] refFeeInfo, ref FeeBatchInfo[] feeBatchInfos, string feeBatch, ref string errorMsg)
        {
            var args = new List<Param>();
            switch (pinType)
            {
                case PinType.SerialNo:
                    args.Add(new Param("serial_no", pin));
                    break;
                case PinType.Name:
                    args.Add(new Param("name", pin));
                    break;
                case PinType.IndiId:
                    args.Add(new Param("indi_id", pin));
                    break;
                case PinType.Idcard:
                    args.Add(new Param("idcard", pin));
                    break;
                case PinType.IcNo:
                    args.Add(new Param("ic_no", pin));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(pinType), pinType, null);
            }
            args.AddRange(new[]
            {
                    new Param("biz_type", bizType),
                    new Param("oper_flag", operFlag),
                    new Param("hospital_id", HospitalId),
                    new Param("treatment_type", treatmentType),
                    new Param("fee_batch", feeBatch)
                });

            var s = string.Empty;
            if (!RunService("BIZH131102", ref s, args.ToArray()))
            {
                errorMsg = s;
                return false;
            }
            SetResultSet(PInter, "bizinfo");
            var rowCount = GetRowCount(PInter);
            if (rowCount > 1)
            {
                bizeInfos = GetInfos<BizInfo>("bizinfo");
                return true;
            }
            if (rowCount != 1) return false;
            SetResultSet(PInter, "feebatchinfo");
            var feeBatchCount = GetRowCount(PInter);
            if (feeBatchCount > 1)
            {
                bizeInfos = new BizInfo[1];
                bizeInfos[0] = GetInfo<BizInfo>("bizinfo");
                perInfo = GetInfo<PersonInfo>("personinfo");
                feeBatchInfos = GetInfos<FeeBatchInfo>("feebatchinfo");
                return true;
            }
            feeBatchInfos = new FeeBatchInfo[1];
            feeBatchInfos[0] = GetInfo<FeeBatchInfo>("feebatchinfo");
            bizeInfos = new BizInfo[1];
            bizeInfos[0] = GetInfo<BizInfo>("bizinfo");
            perInfo = GetInfo<PersonInfo>("personinfo");
            refFeeInfo = GetInfos<RefFeeInfo>("feeinfo");
            return true;
        }

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool ReturnPremium(string serialNo,ref PayInfo payInfo, ref string errorMsg)
        {
            //查询登记信息
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            var refFeeInfo = new RefFeeInfo[1];
            var feeBatchInfo = new FeeBatchInfo[1];
            var feeBatchInfo1 = new FeeBatchInfo[1];
            if (!Bizh131102("3", PinType.SerialNo, serialNo, "110", ref infos, ref p1, "11",
                ref refFeeInfo, ref feeBatchInfo,
                "0", ref errorMsg))
            {
                return false;
            }
            foreach (var t in feeBatchInfo)
            {
                if (!Bizh131102("3", PinType.SerialNo, serialNo, "110", ref infos, ref p1, "11",
                    ref refFeeInfo, ref feeBatchInfo1,
                    t.fee_batch, ref errorMsg))
                {
                    return false;
                }
                foreach (var t1 in infos)
                {
                    var serNo = string.Empty;
                    var regInfo = RegisterInfo.GetInfoFromBizInfo(t1,
                        new RegisterInfo(p1,
                            new RegisterInfo { serial_no = serialNo, fee_batch = t.fee_batch }));
                    ;
                    if (!Bizh131104(regInfo, refFeeInfo, ref serNo, ref payInfo, ref errorMsg, "11", "3"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool CancellationRegister(string serialNo, ref string errorMsg)
        {
            var infos = new BizInfo[1];
            var p1 = new PersonInfo();
            return Bizh131102("2", PinType.SerialNo, serialNo, "110", ref infos, ref p1, "11", ref errorMsg) && Bizh131105(p1.indi_id, serialNo, ref errorMsg);
        }

        private void SetDebug()
        {
            SetDebug(PInter, 1, _directory);
        }

        private bool RunService(string funId, ref string errorInfo, params Param[] pars)
        {
            if (errorInfo == null) throw new ArgumentNullException(nameof(errorInfo));
            if (Start(PInter, funId) < 0)
            {
                errorInfo = $"方法{funId}初始化失败";
                return false;
            }
            if (pars.Any(param => Put(PInter, param.Row, param.Name, param.Value) < 0))
            {
                errorInfo = "参数添加失败";
                return false;
            }
            if (Run(PInter) < 0)
            {
                GetMessage(PInter, ref _errorMsg);
                errorInfo = _errorMsg;
                return false;
            }
            errorInfo = "执行成功";
            SetDebug();
            return true;
        }

        private T GetInfo<T>(string resultSet)
        {
            SetResultSet(PInter, resultSet);
            var info = Activator.CreateInstance<T>();

            foreach (var proInfo in typeof(T).GetProperties())
            {
                var value = string.Empty;
                GetByName(PInter, proInfo.Name, ref value);
                proInfo.SetValue(info, value, null);
            }
            return info;
        }

        private T[] GetInfos<T>(string resultSet)
        {
            SetResultSet(PInter, resultSet);
            var rowCount = GetRowCount(PInter);
            var infos = new T[rowCount];
            for (var i = 0; i < rowCount; i++, NextRow(PInter))
            {
                var info = Activator.CreateInstance<T>();
                foreach (var proInfo in typeof(T).GetProperties())
                {
                    var value = string.Empty;
                    GetByName(PInter, proInfo.Name, ref value);
                    proInfo.SetValue(info, value, null);
                }
                infos[i] = info;
            }
            return infos;
        }
    }
}