using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;
using Oracle.ManagedDataAccess.Client;

namespace CrHgWcfService.Common
{
    //数据库相关操作类
    public class Database
    {
        public static OracleConnection Connectdb()
        {
            // string connString =ConfigurationManager.ConnectionStrings["OraConnectionString"].ConnectionString;// System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];//ConfigurationSettings.AppSettings["connString"];

            var conn = new OracleConnection
            {
                ConnectionString = "Data Source=192.168.6.202:1521/test;User ID=healthcare;Password=healthcare"
            };
            //OracleConnection conn = new OracleConnection(connString);
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
            }
            return conn;
        }   
        public static readonly string Logfile = ConfigurationManager.AppSettings["logfileDir"];
        /// <summary>写日志文件(写结果，记录调用XML串)
        /// </summary>
        /// <param name="logtxt"></param>
        public static void WriteLogResult(string logtxt)
        {
            string uip;

            if (!Directory.Exists(Logfile))
            {
                Directory.CreateDirectory(Logfile);
            }

            var lsFileName = Logfile + string.Format("{0:yyyyMMdd}", DateTime.Today) + ".log";
            //ls_file_name = logfile.Substring(0, logfile.Length - 4) + string.Format("{0:yyyyMMdd}", DateTime.Today) + ".log";
            //ls_file_name = logfile + "\\" + ls_file_name;
            uip = System.Web.HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
            StreamWriter sw = new StreamWriter(lsFileName, true);
            try
            {
                sw.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " " + uip + " " + logtxt);
                sw.Flush();
            }
            finally
            {
                sw.Close();
            }
        }


        /// <summary>获取Xml节点的值
        /// </summary>
        /// <param name="node">要获取值的Xml节点对象</param>
        /// <returns>字符串</returns>
        public static string GetXmlNodeValue(XmlNode node)
        {
            if (node.FirstChild != null)
            {
                return node.FirstChild.Value;
            }
            else
            {
                return "";
            }
        }


        //获取XML节点值
        public static string GetXmlNodeValue(XmlNode node, string parth)
        {
            string lsResult = string.Empty;//返回值
            string lsParth = parth;
            foreach (XmlNode node1 in node.ChildNodes)
            {
                if (node1.Name == parth)
                {
                    lsResult = node1.Value;
                }
            }
            return lsResult;
        }


        /// <summary>Oracle参数生成函数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="oracleType">参数类型</param>
        /// <param name="size">大小</param>
        /// <param name="pDirection">输入输出</param>
        /// <param name="value">参数值</param>
        /// <returns>Oracle参数对象</returns>
        public static OracleParameter CreateParameter(string name, OracleDbType oracleType, int size, ParameterDirection pDirection, string value)
        {
            OracleParameter pm = new OracleParameter(name, oracleType, size)
            {
                Direction = pDirection,
                Value = value
            };
            return pm;
        }


        /// <summary>运行存储过程,填充出参并返回dataset
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="pm">参数数组</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcRetDataSet(string procName, ref OracleParameter[] pm)
        {
            DataSet ds = new DataSet();

            try
            {
                using (OracleConnection conn = Connectdb())
                {
                    // conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(pm);

                    var oda = new OracleDataAdapter(cmd);
                    oda.Fill(ds);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return ds;
        }


        /// <summary>运行存储过程,填充出参
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="pm">参数数组</param>
        public static void RunProcRetNone(string procName, ref OracleParameter[] pm)            
        {
            using (OracleConnection conn = Connectdb())
            {
                try
                {
                    //conn.Open();
                    OracleCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procName;
                    cmd.Parameters.AddRange(pm);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }


        /// <summary>运行存储过程,填充出参(使用事务操作)
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="pm">参数数组</param>
        public static void RunProcUseOraTrans(string procName, ref OracleParameter[] pm)
        {
            using (var conn = Connectdb())
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    throw e;
                }
                OracleTransaction ot;
                try
                {
                    ot = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                }
                catch (Exception e)
                {
                    throw e;
                }
                OracleCommand cmd = conn.CreateCommand();
                cmd.Transaction = ot;
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procName;
                    cmd.Parameters.AddRange(pm);
                    cmd.ExecuteNonQuery();
                    ot.Commit();
                }
                catch (Exception e)
                {
                    ot.Rollback();
                    throw e;
                }
            }
        }


        private DataTable DBExecStoredProcedure(string storeureName, OracleParameter[] sqlParme)
        {
            try
            {
                //使用微软的ORACLE访问接口                 
                if (Connectdb().State == ConnectionState.Closed)//获取数据连接
                    Connectdb().Open();
                OracleCommand oraCmd = new OracleCommand(storeureName, Connectdb());
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.Parameters.Clear();//先清空   
                foreach (OracleParameter parme in sqlParme)
                {
                    oraCmd.Parameters.Add(parme);
                }
                DataTable table = new DataTable();
                DateTime BegTime = System.DateTime.Now;
                OracleDataAdapter da1 = new OracleDataAdapter(oraCmd);//取出数据
                da1.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Connectdb().Close();
            }
        }
    }


}
