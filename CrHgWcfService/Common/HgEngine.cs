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

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CrHgWcfService.Common
{
    /// <summary>
    /// 医保卡引擎接口
    /// </summary>
    public class HgEngine
    {
        private const string Dllname = @"Libraries\\HG_Interface.dll";

        /// <summary>
        /// 该函数建立一个新的接口实例，但这个函数没有初始化接口，必须再调用 init 函
        /// 数初始化接口，此函数返回接口指针 p_inter，它将作为其他函数入口参数。 
        /// </summary>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "newinterface", CallingConvention = CallingConvention.StdCall)]
        //public static extern int newinterface();
        static extern IntPtr newinterface();
        public static int NewInterface()
        {
            return (int)newinterface();
        }


        /// <summary>
        /// 该函数建立一个新的接口实例并将接口初始化，不需要再调用init函数。参数Addr
        /// 为应用服务器 IP 地址， Port 为应用服务器端口号，Servlet 为应用服务器入口
        /// Servlet 的名称，此函数返回接口指针 p_inter，它将作为其他函数入口参数。 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        /// <param name="servlet"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "newinterfacewithinit", CallingConvention = CallingConvention.StdCall)]
        //public static extern int newinterfacewithinit(string addr, int port, string servlet);
        static extern IntPtr newinterfacewithinit(string addr, int port, string servlet);
        public static int NewInterfaceWithInit(string addr, int port, string servlet)
        {
            return (int)newinterfacewithinit(addr, port, servlet);
        }

        /// <summary>
        /// 初始化接口。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit 的
        /// 返回值，参数 Addr 为应用服务器 IP 地址，Port 为应用服务器端口号，Servlet 为应用
        /// 服务器入口 Servlet 的名称。返回-1 表示没有 Start 成功，返回 1 表示调用成功。 
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        /// <param name="servlet"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "init", CallingConvention = CallingConvention.StdCall)]
        //public static extern int init(int pint, string addr, int port, string servlet);
        static extern int init(IntPtr pint, string addr, int port, string servlet);
        public static int Init(int pint, string addr, int port, string servlet)
        {
            return init(new IntPtr(pint), addr, port, servlet);
        }


        /// <summary>
        /// 从内存中释放接口的实例。 
        /// </summary>
        /// <param name="pint"></param>
        [DllImport(Dllname, EntryPoint = "destoryinterface", CallingConvention = CallingConvention.StdCall)]
        //public static extern void destoryinterface(int pint);
        static extern void destoryinterface(IntPtr pint);
        public static void DestoryInterface(int pint)
        {
            destoryinterface(new IntPtr(pint));
        }


        /// <summary>
        /// 该函数为一次接口调用的开始，入口参数 p_inter 为函数 newinterface()或者
        /// newinterfacewithinit 的返回值，参数 FUNC_ID 为要进行的业务的功能号，在上一次
        /// Start 的业务没有进行完之前不能进行下一次 Start。返回-1 表示没有 Start 成功，
        /// 返 回 1 表示调用成功。 
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "start", CallingConvention = CallingConvention.StdCall)]
        //public static extern int start(int pint, string id);
        static extern IntPtr start(IntPtr pint, IntPtr id);
        public static int Start(int pint, string id)
        {
            return (int)start(new IntPtr(pint), Marshal.StringToHGlobalAnsi(id));
        }


        /// <summary>
        /// 该函数用来在一次接口调用中传入业务所需的参数，参数 p_inter 为函数
        /// newinterface()或者 newinterfacewithinit 的返回值，row 为多行参数的行号， p_name
        /// 为参数名称，以字符串小写表示，p_value 为参数值，可以是字符串和数值型。返回-1
        /// 表示没有 Put 成功，返回大于零表示 Put 成功 ，此值同时为当前的行号。如果入参有
        /// 多个记录集，可用 setresultset 函数设置要传参数的记录集。
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="row"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "put", CallingConvention = CallingConvention.StdCall)]
        //public static extern int put(int pint, int row, string pname, string pvalue);
        static extern IntPtr put(IntPtr pint, IntPtr row, IntPtr pname, IntPtr pvalue);
        public static int Put(int pint, int row, string pname, string pvalue)
        {
            return (int)put(new IntPtr(pint), new IntPtr(row), Marshal.StringToHGlobalAnsi(pname), Marshal.StringToHGlobalAnsi(pvalue));
        }


        /// <summary>
        /// 该函数用来在一次接口调用中传入业务所需的参数，参数 p_inter 为函数
        /// newinterface()或者 newinterfacewithinit 的返回值，在当前的行，p_name 为参数名
        /// 称，以字符串小写表示，p_value 为参数值，可以是字符串和数值型。返回-1 表示没有
        /// Put 成功，返回大于零表示 Put 成功，此值同时为当前的行号。 
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "putcol", CallingConvention = CallingConvention.StdCall)]
        //public static extern int putcol(int pint, string pname, string pvalue);
        static extern int putcol(IntPtr pint, IntPtr pname, IntPtr pvalue);
        public static int PutCol(int pint, string pname, string pvalue)
        {
            return putcol(new IntPtr(pint), Marshal.StringToHGlobalAnsi(pname), Marshal.StringToHGlobalAnsi(pvalue));
        }


        /// <summary>
        /// 该函数开始一次接口运行，直接将参数打包成送往 Servlet，如果出错，将返回一
        /// 个错误。返回-1 表示没有 Run 成功，返回大于零的值为返回参数的记录条数。参数 
        /// p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="pint"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "run", CallingConvention = CallingConvention.StdCall)]
        //public static extern int run(int pint);
        static extern int run(IntPtr pint);
        public static int Run(int pint)
        {
            return run(new IntPtr(pint));
        }



        /// <summary>
        /// 参数 p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。 
        /// 当取结果时： 
        /// 将当前记录集设置为由 result_name 指定的记录集，如果指的记录集不存在，则不
        /// 会改变当前记录集。返回－1 表示不成功，返回大于等于零的值为记录集记录数。 
        /// 当设置入参时： 
        /// 将当前记录集设置为由 result_name 指定的记录集，如果指的记录集存在，则改变
        /// 当前记录集为存在的记录集，其中有个特殊的记录集 Parameters, 它是个参数集，没
        /// 有记录行，其他都有记录行，通过 nextrow, prevrow, firstrow, lastrow。返回－1
        /// 表示不成功，返回大于等于零的值为记录集记录数。 
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="resultName"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "setresultset", CallingConvention = CallingConvention.StdCall)]
        static extern int setresultset(IntPtr pint, string resultName);
        public static int SetResultSet(int pint, string resultName)
        {
            return setresultset(new IntPtr(pint), resultName);
        }



        [DllImport(Dllname, EntryPoint = "runxml", CallingConvention = CallingConvention.StdCall)]
        public static extern int runxml(int pint, string xml);

        [DllImport(Dllname, EntryPoint = "getxmlstr_t", CallingConvention = CallingConvention.StdCall)]
        public static extern int getxmlstr_t(int pint, ref string xml);

        [DllImport(Dllname, EntryPoint = "getxmlstr", CallingConvention = CallingConvention.StdCall)]
        public static extern int getxmlstr(int pint, ref string xml);

        /// <summary>
        /// 该函数用来从接口取得返回的参数值。返回值小于零, 表示没有 Get 成功，返回大
        /// 于零表示为当前记录集的第几条记录。用 getmessage 可以取得最近一次出错的错误信
        /// 息。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。参数
        /// p_name 为需要接口返回的字段名，需要用小写表示。参数 p_value 为接口返回的数值，
        /// 必须在客户端分配足够大的内存，长度单位为 sizeof(char)。 
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "getbyname", CallingConvention = CallingConvention.StdCall)]
        //public static extern int getbyname(int pint, string pname,ref string pvalue);
        static extern int getbyname(IntPtr pint, string pname, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pvalue);
        public static int GetByName(int pint, string name, ref string pvalue)
        {
            //if (pvalue == null) throw new ArgumentNullException(nameof(pvalue));
            var sb = new StringBuilder(1024);
            var ret = getbyname(new IntPtr(pint), name, sb);
            pvalue = sb.ToString();
            return ret;
        }

        /// <summary>
        /// 该函数用来从接口取得返回的参数值。返回值小于零, 表示没有调用成功，返回值
        /// 大于零, 表示调用成功。用 getmessage 可以取得最近一次出错的错误信息。参数
        /// p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。参数年第亿升
        /// 为需要接口返回的字段名的索引值。参数 p_value 为接口返回的数值，必须在客户端分
        /// 配足够大的内存，长度单位为 sizeof(char)。
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="index"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "getbyindex", CallingConvention = CallingConvention.StdCall)]
        //public static extern int getbyindex(int pint, int index, string pname,ref string pvalue);
        static extern int getbyindex(IntPtr pint, long index, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pname, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pvalue);
        public static int GetByIndex(int pint, long index, ref string pname, ref string pvalue)
        {
            //if (pname == null) throw new ArgumentNullException(nameof(pname));
            //if (pvalue == null) throw new ArgumentNullException(nameof(pvalue));
            var sb = new StringBuilder(1024);
            var sb1 = new StringBuilder(1024);
            var ret = getbyindex(new IntPtr(pint), index, sb, sb1);
            pname = sb.ToString();
            pvalue = sb1.ToString();
            return ret;
        }



        /// <summary>
        /// 该函数在所有函数出错时，调用它，将得到一个错误信息，错误信息存放在 err
        /// 指向的一片内存空间中，当入参 err 为空指针(NULL)时，将返回 message 的长度。调用
        /// 此函数应保证 err 指向的内存有足够的长度存放返回的错误信息。函数返回值小于零
        /// 时，函数执行不成功。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit
        /// 的返回值。 
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "getmessage", CallingConvention = CallingConvention.StdCall)]
        //public static extern int getmessage(int pint,ref string msg);
        static extern int getmessage(IntPtr pint, [MarshalAs(UnmanagedType.LPStr)] StringBuilder msg);
        public static int GetMessage(int pint, ref string msg)
        {
            //if (msg == null) throw new ArgumentNullException(nameof(msg));
            var sb = new StringBuilder(1024);
            var ret = getmessage(new IntPtr(pint), sb);
            msg = sb.ToString();
            return ret;
        }


        /// <summary>
        /// 该函数在所有函数出错时，调用它，将得到一个详细的错误信息，通过 exception
        /// 串返回，当 exception 为 NULL 时，将返回 message 的长度。函数返回值小于零时，函
        /// 数执行不成功。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit 的返
        /// 回值。
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "getexception", CallingConvention = CallingConvention.StdCall)]
        //public static extern int getexception(int pint,ref string msg);
        static extern int getexception(IntPtr pint, [MarshalAs(UnmanagedType.LPStr)] StringBuilder msg);
        public static int GetException(int pint, ref string msg)
        {
            //if (msg == null) throw new ArgumentNullException(nameof(msg));
            var sb = new StringBuilder(1024);
            var ret = getexception(new IntPtr(pint), sb);
            msg = sb.ToString();
            return ret;
        }


        /// <summary>
        /// 该函数用来从接口取得返回的当前记录集的记录行数。返回值小于零, 表示没有Get 成功，
        /// 返回值大于零, 表示当前记录集的记录行数。参数 p_inter 为函数 newinterface()或者
        ///  newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="pint"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "getrowcount", CallingConvention = CallingConvention.StdCall)]
        //public static extern int getrowcount(int pint);   
        static extern int getrowcount(IntPtr pint);
        public static int GetRowCount(int pint)
        {
            return getrowcount(new IntPtr(pint));
        }


        [DllImport(Dllname, EntryPoint = "getcode", CallingConvention = CallingConvention.StdCall)]
        //public static extern int getcode(int pint,ref string msg);
        static extern int getcode(IntPtr pint, [MarshalAs(UnmanagedType.LPStr)] StringBuilder msg);
        public static int GetCode(int pint, ref string msg)
        {
            //if (msg == null) throw new ArgumentNullException(nameof(msg));
            var sb = new StringBuilder(1024);
            var ret = getcode(new IntPtr(pint), sb);
            msg = sb.ToString();
            return ret;
        }

        [DllImport(Dllname, EntryPoint = "geterrtype", CallingConvention = CallingConvention.StdCall)]
        public static extern int geterrtype(int pint);

        /// <summary>
        /// 跳到结果集第一行记录，返回－1 表示调用不成功，返回 1 表示调 用成功。参数 p_inter 为
        /// 函数 newinterface()或者 newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="pint"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "firstrow", CallingConvention = CallingConvention.StdCall)]
        //public static extern int firstrow(int pint);
        static extern int firstrow(IntPtr pint);
        public static int FirstRow(int pint)
        {
            return firstrow(new IntPtr(pint));
        }


        /// <summary>
        /// 跳到结果集后一行记录，返回－1 表示调用不成功，返回大于零表示调用成功，同
        /// 时此值为当前的行号。参数p_inter为函数newinterface()或者newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="pint"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "nextrow", CallingConvention = CallingConvention.StdCall)]
        //public static extern int nextrow(int pint);
        static extern int nextrow(IntPtr pint);
        public static int NextRow(int pint)
        {
            return nextrow(new IntPtr(pint));
        }


        /// <summary>
        /// 跳到结果集前一行记录，返回－1 表示调用不成功，返回大于零表示调用成功，同
        /// 时此值为当前的行号。参数p_inter为函数newinterface()或者newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="pint"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "prevrow", CallingConvention = CallingConvention.StdCall)]
        //public static extern int prevrow(int pint);
        static extern int prevrow(IntPtr pint);
        public static int PrevRow(int pint)
        {
            return prevrow(new IntPtr(pint));
        }



        /// <summary>
        /// 跳到结果集最后一行记录，返回－1 表示调用不成功，返回大于零表示为当前记录
        /// 集记录数。参数 p_inter为函数 newinterface()或者newinterfacewithinit 的返回值。
        /// </summary>
        /// <param name="pint"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "lastrow", CallingConvention = CallingConvention.StdCall)]
        //public static extern int lastrow(int pint);
        static extern int lastrow(IntPtr pint);
        public static int LastRow(int pint)
        {
            return lastrow(new IntPtr(pint));
        }


        /// <summary>
        /// 该函数用来设置 IC 卡设备的串口号。返回值小于零, 表示没有成功，返回值大于
        /// 等 于 零 , 表 示 调 用 成 功 。 参 数 p_inter 为函数 newinterface() 或者
        ///newinterfacewithinit 的返回值。参数 comm 为与 IC 卡连接的串口号，com1 表示
        ///  1， com2 表示 2„。 
        /// </summary>
        /// <param name="pinter"></param>
        /// <param name="comm"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "set_ic_commport", CallingConvention = CallingConvention.StdCall)]
        //public static extern int set_ic_commport(int pinter, int comm);
        static extern int set_ic_commport(IntPtr pint, int comm);
        public static int SetIcCommport(int pint, int comm)
        {
            return set_ic_commport(new IntPtr(pint), comm);
        }


        /// <summary>
        /// 该函数用来从接口取得第 index 的记录集名。返回值小于零, 表示没有成功，返回
        /// 值大于零, 表示调用成功。用 getmessage 可以取得最近一次出错的错误信息。参数
        /// p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。
        /// </summary>
        /// <param name="pinter"></param>
        /// <param name="index"></param>
        /// <param name="resultname"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "getresultnamebyindex", CallingConvention = CallingConvention.StdCall)]
        //public static extern int getresultnamebyindex(int pinter, int index,ref string resultname);
        static extern int getresultnamebyindex(IntPtr pint, long index, [MarshalAs(UnmanagedType.LPStr)] StringBuilder resultname);
        public static int GetResultNameByIndex(int pint, long index, ref string resultname)
        {
            var sb1 = new StringBuilder(1024);
            var ret = getresultnamebyindex(new IntPtr(pint), index, sb1);
            resultname = sb1.ToString();
            return ret;
        }

        /// <summary>
        /// 该函数用来设置接口的运行模式，当 flag 为 1 时将产生调试信息并且写入指定目
        /// 录 direct 下的日志文件中。返回值小于零, 表示没有成功，返回值大于等于零, 表示
        /// 成功。参数 pinter 为函数 newinterface()或者 newinterfacewithinit 的返回值，flag 
        /// 为调试标志， 0 表示不作调试，其它为可调试，direct 为存放调试信息日志文件的目录，
        /// 注意此目录必须是存在的。 
        /// </summary>
        /// <param name="pint"></param>
        /// <param name="flag"></param>
        /// <param name="direct"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "setdebug", CallingConvention = CallingConvention.StdCall)]
        //public static extern int setdebug(int pint, int flag, string direct);
        static extern int setdebug(IntPtr pint, int flag, string direct);
        public static int SetDebug(int pint, int flag, string direct)
        {
            return setdebug(new IntPtr(pint), flag, direct);
        }


        [DllImport(Dllname, EntryPoint = "decode64", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr decode64(string psrc, long size, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pdesc);

        public static int Decode64(string psrc, long size, ref string pdesc)
        {
            //if (pdesc == null) throw new ArgumentNullException(nameof(pdesc));
            var sb = new StringBuilder(1024);
            var ret = encode64(psrc, size, sb);
            pdesc = sb.ToString();
            return (int)ret;
        }


        [DllImport(Dllname, EntryPoint = "decodesize", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr decodesize(long size);
        public static int DecodeSize(long size)
        {
            return (int)decodesize(size);
        }


        [DllImport(Dllname, EntryPoint = "decode64_tofile", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr decode64_tofile(string psrc, long size, string docname);
        public static int Decode64ToFile(string psrc, long size, string docname)
        {
            return (int)decode64_tofile(psrc, size, docname);
        }


        [DllImport(Dllname, EntryPoint = "encode64", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr encode64(string psrc, long size, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pdesc);
        public static int Encode64(string psrc, long size, ref string pdesc)
        {
            //if (pdesc == null) throw new ArgumentNullException(nameof(pdesc));
            var sb = new StringBuilder(1024);
            var ret = encode64(psrc, size, sb);
            pdesc = sb.ToString();
            return (int)ret;
        }


        [DllImport(Dllname, EntryPoint = "encodesize", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr encodesize(long size);
        public static int EncodeSize(long size)
        {
            return (int)encodesize(size);
        }


        //[DllImport(Dllname, EntryPoint = "init", CallingConvention = CallingConvention.StdCall)]
        //public static extern int set_ic_commport(int pint, int comm);

        //[DllImport(Dllname, EntryPoint = "init", CallingConvention = CallingConvention.StdCall)]
        //public static extern int setdebug(int pinter, int flag, string direct);
    }
}