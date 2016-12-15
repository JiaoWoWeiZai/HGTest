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

using System.Runtime.InteropServices;

namespace CrHgWcfService.Common
{
    /// <summary>
    /// 医保卡引擎接口
    /// </summary>
    public static class HgEngine
    {
        private const string Dllname = @"Libraries\\HG_Interface.dll";

        /// <summary>
        /// 该函数建立一个新的接口实例，但这个函数没有初始化接口，必须再调用 init 函
        /// 数初始化接口，此函数返回接口指针 p_inter，它将作为其他函数入口参数。 
        /// </summary>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "newinterface", CallingConvention = CallingConvention.Cdecl)]
        public static extern int newinterface();

        /// <summary>
        /// 该函数建立一个新的接口实例并将接口初始化，不需要再调用init函数。参数Addr
        /// 为应用服务器 IP 地址， Port 为应用服务器端口号，Servlet 为应用服务器入口
        /// Servlet 的名称，此函数返回接口指针 p_inter，它将作为其他函数入口参数。 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        /// <param name="servlet"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int newinterfacewithinit(string addr, int port, string servlet);

        /// <summary>
        /// 初始化接口。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit 的
        /// 返回值，参数 Addr 为应用服务器 IP 地址，Port 为应用服务器端口号，Servlet 为应用
        /// 服务器入口 Servlet 的名称。返回-1 表示没有 Start 成功，返回 1 表示调用成功。 
        /// </summary>
        /// <param name="print"></param>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        /// <param name="servlet"></param>
        /// <returns></returns>
        [DllImport(Dllname, EntryPoint = "init", CallingConvention = CallingConvention.Cdecl)]
        public static extern int init(int print, string addr, int port, string servlet);

        /// <summary>
        /// 从内存中释放接口的实例。 
        /// </summary>
        /// <param name="print"></param>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern void destoryinterface(int print);

        /// <summary>
        /// 该函数为一次接口调用的开始，入口参数 p_inter 为函数 newinterface()或者
        /// newinterfacewithinit 的返回值，参数 FUNC_ID 为要进行的业务的功能号，在上一次
        /// Start 的业务没有进行完之前不能进行下一次 Start。返回-1 表示没有 Start 成功，
        /// 返 回 1 表示调用成功。 
        /// </summary>
        /// <param name="print"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int start(int print, string id);

        /// <summary>
        /// 该函数用来在一次接口调用中传入业务所需的参数，参数 p_inter 为函数
        /// newinterface()或者 newinterfacewithinit 的返回值，row 为多行参数的行号， p_name
        /// 为参数名称，以字符串小写表示，p_value 为参数值，可以是字符串和数值型。返回-1
        /// 表示没有 Put 成功，返回大于零表示 Put 成功 ，此值同时为当前的行号。如果入参有
        /// 多个记录集，可用 setresultset 函数设置要传参数的记录集。
        /// </summary>
        /// <param name="print"></param>
        /// <param name="row"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int put(int print, int row, string pname, string pvalue);

        /// <summary>
        /// 该函数用来在一次接口调用中传入业务所需的参数，参数 p_inter 为函数
        /// newinterface()或者 newinterfacewithinit 的返回值，在当前的行，p_name 为参数名
        /// 称，以字符串小写表示，p_value 为参数值，可以是字符串和数值型。返回-1 表示没有
        /// Put 成功，返回大于零表示 Put 成功，此值同时为当前的行号。 
        /// </summary>
        /// <param name="print"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int putcol(int print, string pname, string pvalue);

        /// <summary>
        /// 该函数开始一次接口运行，直接将参数打包成送往 Servlet，如果出错，将返回一
        /// 个错误。返回-1 表示没有 Run 成功，返回大于零的值为返回参数的记录条数。参数 
        /// p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int run(int print);

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
        /// <param name="print"></param>
        /// <param name="result_name"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int setresultset(int print, string result_name);

        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int runxml(int print, string xml);

        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getxmlstr_t(int print,ref string xml);

        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getxmlstr(int print,ref string xml);

        /// <summary>
        /// 该函数用来从接口取得返回的参数值。返回值小于零, 表示没有 Get 成功，返回大
        /// 于零表示为当前记录集的第几条记录。用 getmessage 可以取得最近一次出错的错误信
        /// 息。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。参数
        /// p_name 为需要接口返回的字段名，需要用小写表示。参数 p_value 为接口返回的数值，
        /// 必须在客户端分配足够大的内存，长度单位为 sizeof(char)。 
        /// </summary>
        /// <param name="print"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getbyname(int print, string pname,ref string pvalue);

        /// <summary>
        /// 该函数用来从接口取得返回的参数值。返回值小于零, 表示没有调用成功，返回值
        /// 大于零, 表示调用成功。用 getmessage 可以取得最近一次出错的错误信息。参数
        /// p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。参数年第亿升
        /// 为需要接口返回的字段名的索引值。参数 p_value 为接口返回的数值，必须在客户端分
        /// 配足够大的内存，长度单位为 sizeof(char)。
        /// </summary>
        /// <param name="print"></param>
        /// <param name="index"></param>
        /// <param name="pname"></param>
        /// <param name="pvalue"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getbyindex(int print, int index, string pname,ref string pvalue);

        /// <summary>
        /// 该函数在所有函数出错时，调用它，将得到一个错误信息，错误信息存放在 err
        /// 指向的一片内存空间中，当入参 err 为空指针(NULL)时，将返回 message 的长度。调用
        /// 此函数应保证 err 指向的内存有足够的长度存放返回的错误信息。函数返回值小于零
        /// 时，函数执行不成功。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit
        /// 的返回值。 
        /// </summary>
        /// <param name="print"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getmessage(int print,ref string msg);

        /// <summary>
        /// 该函数在所有函数出错时，调用它，将得到一个详细的错误信息，通过 exception
        /// 串返回，当 exception 为 NULL 时，将返回 message 的长度。函数返回值小于零时，函
        /// 数执行不成功。参数 p_inter 为函数 newinterface()或者 newinterfacewithinit 的返
        /// 回值。
        /// </summary>
        /// <param name="print"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getexception(int print,ref string msg);

        /// <summary>
        /// 该函数用来从接口取得返回的当前记录集的记录行数。返回值小于零, 表示没有Get 成功，
        /// 返回值大于零, 表示当前记录集的记录行数。参数 p_inter 为函数 newinterface()或者
        ///  newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getrowcount(int print);       

        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getcode(int print,ref string msg);

        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int geterrtype(int print);

        /// <summary>
        /// 跳到结果集第一行记录，返回－1 表示调用不成功，返回 1 表示调 用成功。参数 p_inter 为
        /// 函数 newinterface()或者 newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int firstrow(int print);

        /// <summary>
        /// 跳到结果集后一行记录，返回－1 表示调用不成功，返回大于零表示调用成功，同
        /// 时此值为当前的行号。参数p_inter为函数newinterface()或者newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int nextrow(int print);

        /// <summary>
        /// 跳到结果集前一行记录，返回－1 表示调用不成功，返回大于零表示调用成功，同
        /// 时此值为当前的行号。参数p_inter为函数newinterface()或者newinterfacewithinit 的返回值。 
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prevrow(int print);

        /// <summary>
        /// 跳到结果集最后一行记录，返回－1 表示调用不成功，返回大于零表示为当前记录
        /// 集记录数。参数 p_inter为函数 newinterface()或者newinterfacewithinit 的返回值。
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lastrow(int print);

        /// <summary>
        /// 该函数用来设置 IC 卡设备的串口号。返回值小于零, 表示没有成功，返回值大于
        /// 等 于 零 , 表 示 调 用 成 功 。 参 数 p_inter 为函数 newinterface() 或者
        ///newinterfacewithinit 的返回值。参数 comm 为与 IC 卡连接的串口号，com1 表示
        ///  1， com2 表示 2„。 
        /// </summary>
        /// <param name="pinter"></param>
        /// <param name="comm"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int set_ic_commport(int pinter, int comm);

        /// <summary>
        /// 该函数用来从接口取得第 index 的记录集名。返回值小于零, 表示没有成功，返回
        /// 值大于零, 表示调用成功。用 getmessage 可以取得最近一次出错的错误信息。参数
        /// p_inter 为函数 newinterface()或者 newinterfacewithinit 的返回值。
        /// </summary>
        /// <param name="pinter"></param>
        /// <param name="index"></param>
        /// <param name="resultname"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getresultnamebyindex(int pinter, int index, string resultname);

        /// <summary>
        /// 该函数用来设置接口的运行模式，当 flag 为 1 时将产生调试信息并且写入指定目
        /// 录 direct 下的日志文件中。返回值小于零, 表示没有成功，返回值大于等于零, 表示
        /// 成功。参数 pinter 为函数 newinterface()或者 newinterfacewithinit 的返回值，flag 
        /// 为调试标志， 0 表示不作调试，其它为可调试，direct 为存放调试信息日志文件的目录，
        /// 注意此目录必须是存在的。 
        /// </summary>
        /// <param name="print"></param>
        /// <param name="flag"></param>
        /// <param name="direct"></param>
        /// <returns></returns>
        [DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int setdebug(int print, int flag, string direct);

        //[DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int set_ic_commport(int print, int comm);

        //[DllImport(Dllname, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int setdebug(int pinter, int flag, string direct);
    }
}