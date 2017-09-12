using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using System.IO;

/// <summary>
/// 数据库操作辅助类
/// </summary>
public class DbHelper
{
	public DbHelper()
	{
        //构造函数
	}

    /// 函数名称：WriteCTL_Log
    /// 函数作者：朱坤堂
    /// 创建时间：2008-12-02
    /// <summary>
    /// 写入操作日志
    /// </summary>
    /// <param name="LogContent">操作日志内容</param>
    public static void WriteCTL_Log(string LogID, string LogContent, string ConnString)
    {
        StringBuilder LogSQL = new StringBuilder();
        LogSQL.Append("insert into cw_logs(id,logcontent,loguser,logname,loguid,logpid,logtime,logdefine1,logdefine2)values(");
        LogSQL.AppendFormat("'{0}',", GetRecordID("CW_Logs", ConnString));
        LogSQL.AppendFormat("'{0}',", LogContent);
        LogSQL.AppendFormat("'{0}',", UserInfo.UserName);
        LogSQL.AppendFormat("'{0}',", UserInfo.RealName);
        if (UserInfo.AccountID == null || UserInfo.UserType != "0")
        {
            LogSQL.Append("'000000',");
        }
        else
        {
            LogSQL.AppendFormat("'{0}',", UserInfo.AccountID);
        }
        LogSQL.AppendFormat("'{0}',", UserInfo.UnitID);
        LogSQL.AppendFormat("'{0}',", DateTime.Now.ToString());
        LogSQL.AppendFormat("'{0}',", LogID);
        LogSQL.AppendFormat("'{0}')", HttpContext.Current.Request.UserHostAddress);
        ExecuteSQL(LogSQL.ToString(), ConnString);
    }

    /// 函数名称：GetSysPara
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 获取软件配置参数[CW_SysPara]
    /// </summary>
    /// <param name="ParaName"></param>
    /// <returns></returns>
    public static string GetSysPara(string ParaName, string ConnString)
    {
        return GetTableValue("cw_syspara", "ParaValue", string.Format("ParaName='{0}'", ParaName), "", ConnString);
    }

    /// 函数名称：InitSysPara
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 设置软件配置参数[CW_SysPara]
    /// </summary>
    /// <param name="ParaName"></param>
    /// <returns></returns>
    public static void InitSysPara(DropDownList ddl, string ParaType, string ConnString)
    {
        InitSysPara(ddl, ParaType, "", ConnString);
    }
    public static void InitSysPara(DropDownList ddl, string ParaType, string firstItem, string ConnString)
    {
        DataTable dt = GetDataTable(string.Concat("select ParaName,ParaValue from cw_syspara where ParaType='", ParaType, "'"), ConnString);
        if (firstItem.Length > 0)
        {
            DataRow newRow = dt.NewRow();
            newRow[0] = "000000";
            newRow[1] = firstItem;
            dt.Rows.InsertAt(newRow, 0);
        }
        ddl.DataSource = dt.DefaultView;
        ddl.DataTextField = "ParaValue";
        ddl.DataValueField = "ParaName";
        ddl.DataBind();
    }

    /// 函数名称：SetSysPara
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 设置软件配置参数[CW_SysPara]
    /// </summary>
    /// <param name="ParaName"></param>
    /// <returns></returns>
    public static void SetSysPara(string ParaName, string ParaValue, string ConnString)
    {
        if (CheckExist("cw_syspara", string.Format("ParaName='{0}'", ParaName), ConnString))
        {
            ExecuteSQL(string.Format("update cw_syspara set ParaValue='{0}' where ParaName='{1}'", ParaValue, ParaName), ConnString);
        }
        else
        {
            ExecuteSQL(string.Format("insert into cw_syspara(ParaName,ParaValue)values('{1}','{0}')", ParaValue, ParaName), ConnString);
        }
    }

    /// 函数名称：GetTableValue
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 获取表中某个值
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <param name="FieldName">获取值的字段名</param>
    /// <param name="whstring">查询条件</param>
    /// <returns>查询值</returns>
    public static string GetTableValue(string TableName, string FieldName, string whstring, string ConnString)
    {
        return GetTableValue(TableName, FieldName, whstring, "NoDataItem", ConnString);
    }
    public static string GetTableValue(string TableName, string FieldName, string whstring, string defaultValue, string ConnString)
    {
        string sqlString = string.Empty;
        if (whstring.Length == 0)
        {
            sqlString = string.Format("select {0} from {1}", FieldName, TableName);
        }
        else
        {
            sqlString = string.Format("select {0} from {1} where {2}", FieldName, TableName, whstring);
        }
        return ExecuteScalar(sqlString, defaultValue, ConnString);
    }

    /// 函数名称：GetFieldFromID
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-09
    /// <summary>
    /// 获取科目表某个字段值，通过表ID
    /// </summary>
    /// <param name="ID">表ID</param>
    /// <param name="FieldName">字段名</param>
    /// <returns>查询值</returns>
    public static string GetFieldFromID(string ID, string FieldName, string ConnString)
    {
        return GetFieldFromID(ID, FieldName, "cw_subject", ConnString);
    }
    public static string GetFieldFromID(string ID, string FieldName, string TableName, string ConnString)
    {
        return ExecuteScalar(string.Format("select {0} from {1} where id='{2}'", FieldName, TableName, ID), "NoDataItem", ConnString);
    }

    /// 函数名称：GetDataTable
    /// 函数作者：朱坤堂
    /// 创建时间：2008-10-16
    /// <summary>
    /// 获取数据表GetDataTable
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>数据表</returns>
    public static DataTable GetDataTable(string sqlString, string ConnString)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            conn.Open();
            try
            {
                string TableName = Regex.Replace(sqlString, "\\([^\\)]*\\)", "");
                TableName = Regex.Replace(TableName, "(.* from )|(( (where|order|group) ).*)", "").Trim();
                DataTable dataTable = new DataTable(TableName);
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, conn);
                da.Fill(dataTable);
                da.Dispose();
                conn.Close();
                return dataTable;
            }
            catch
            {
                conn.Close();
                throw;
                return null;
            }
        }
    }

    /// 函数名称：GetDataSet
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 获取数据集DataSet
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>记录集DataSet</returns>
    public static DataSet GetDataSet(string sqlString, string ConnString)
    {
        DataTable dt = GetDataTable(sqlString, ConnString);
        if (dt == null)
        {
            return null;
        }
        else
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
    }

    /// 函数名称：GetDataRow
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-21
    /// <summary>
    /// 获取数据集的单条数据行
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>记录集的所有行的集合</returns>
    public static DataRow GetDataRow(string sqlString, string ConnString)
    {
        DataTable dt = GetDataTable(sqlString, ConnString);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        else
        {
            return null;
        }
    }

    /// 函数名称：GetDataRows
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-29
    /// <summary>
    /// 获取数据集的所有数据行
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>记录集的所有行的集合</returns>
    public static DataRowCollection GetDataRows(string sqlString, string ConnString)
    {
        DataTable dt = GetDataTable(sqlString, ConnString);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows;
        }
        else
        {
            return null;
        }
    }

    /// 函数名称：UpdateDataSet
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 更新改变后的记录集
    /// </summary>
    /// <param name="ds"></param>
    public static void UpdateDataSet(DataSet ds, string ConnString)
    {
        UpdateDataSet(ds, "*", ConnString);
    }
    public static void UpdateDataSet(DataSet ds, string columns, string ConnString)
    {
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            UpdateDataTable(ds.Tables[i], columns, ConnString);
        }
    }

    /// 函数名称：UpdateDataTable
    /// 函数作者：朱坤堂
    /// 创建时间：2009-06-17
    /// <summary>
    /// 更新改变后的记录集
    /// <summary>
    /// <param name="dt"></param>
    public static void UpdateDataTable(DataTable dt, string ConnString)
    {
        UpdateDataTable(dt, "*", ConnString);
    }
    public static void UpdateDataTable(DataTable dt, string columns, string ConnString)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            conn.Open();
            try
            {
                DataTable xDataTable = dt.GetChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Deleted);
                if (!xDataTable.HasErrors && xDataTable.Rows.Count > 0)
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(string.Format("select {0} from {1} where 1=2", columns, dt.TableName), conn);
                    MySqlCommandBuilder sb = new MySqlCommandBuilder(da);
                    da.Update(xDataTable);
                    da.Dispose();
                }
                dt.AcceptChanges();
            }
            catch
            {
                //更新失败
            }
            conn.Close();
        }
    }

    /// 函数名称：CheckExist
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 判断是否已存在相同记录
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <param name="wh">查询条件</param>
    /// <returns>是否查询到标识</returns>
    public static Boolean CheckExist(string TableName, string wh, string ConnString)
    {
        return CountRecord(TableName, wh, ConnString) > 0;
    }

    /// 函数名称：CountRecord
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 统计需要查询的记录数目
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <param name="wh">查询条件</param>
    /// <returns>查询到的记录数</returns>
    public static int CountRecord(string TableName, string wh, string ConnString)
    {
        return TypeParse.StrToInt(ExecuteScalar(string.Format("select count(*) from {0} where {1}", TableName, wh), "0", ConnString), 0);
    }

    /// 函数名称：ExecuteScalar
    /// 函数作者：朱坤堂
    /// 创建时间：2010-06-30
    /// <summary>
    /// 执行ExecuteScalar
    /// </summary>
    public static string ExecuteScalar(string sqlString, string ConnString)
    {
        return ExecuteScalar(sqlString, null, ConnString);
    }
    public static string ExecuteScalar(string sqlString, string defaultV, string ConnString)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            conn.Open();
            object fValue = new MySqlCommand(sqlString, conn).ExecuteScalar();
            conn.Close();
            if (fValue == null)
            {
                return defaultV;
            }
            else
            {
                return fValue.ToString();
            }
        }
    }

    /// 函数名称：ExecuteSQL
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 执行单条SQL语句
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>产生影响的记录数（int）</returns>
    public static int ExecuteSQL(string sqlString, string ConnString)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            conn.Open();
            try
            {
                MySqlCommand command = new MySqlCommand(sqlString, conn);
                int rec = command.ExecuteNonQuery();
                conn.Close();
                return rec;
            }
            catch
            {
                conn.Close();
                throw;
                return 0;
            }
        }
    }
    public static int ExecuteSQL(string TableName, Dictionary<string, string> dbParameters, string ConnString)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            conn.Open();
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = conn;
                string[] keys = new string[dbParameters.Keys.Count];
                dbParameters.Keys.CopyTo(keys, 0);
                command.CommandText = string.Format("insert into {0}({1})values(@{2})", TableName, string.Join(",", keys), string.Join(",@", keys));
                foreach (KeyValuePair<string, string> para in dbParameters)
                {
                    command.Parameters.Add(para.Key, para.Value);
                }
                int rec = command.ExecuteNonQuery();
                conn.Close();
                return rec;
            }
            catch
            {
                conn.Close();
                throw;
                return 0;
            }
        }
    }
    public static int ExecuteSQL(string TableName, Dictionary<string, string> dbParameters, string wh, string ConnString)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            conn.Open();
            try
            {
                StringBuilder sql = new StringBuilder();
                MySqlCommand command = new MySqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = conn;
                sql.AppendFormat("update {0} set ", TableName);
                foreach (KeyValuePair<string, string> para in dbParameters)
                {
                    sql.AppendFormat("{0}=@{1},", para.Key, para.Key);
                    command.Parameters.Add(para.Key, para.Value);
                }
                sql.Remove(sql.Length - 1, 1);
                if (wh != null && wh.Length > 0)
                {
                    sql.Append(" where ");
                    sql.Append(wh);
                }
                command.CommandText = sql.ToString();
                int rec = command.ExecuteNonQuery();
                conn.Close();
                return rec;
            }
            catch
            {
                conn.Close();
                return 0;
            }
        }
    }

    /// 函数名称：ExecuteTransaction
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 执行事务处理多条语句
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>返回操作标志（bool），true成功，false失败</returns>
    public static bool ExecuteTransaction(string sqlString, string ConnString)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand();
            MySqlTransaction transaction = conn.BeginTransaction();
            try
            {
                string[] SqlList = Regex.Split(sqlString, "#sql#");
                command.Connection = conn;
                command.Transaction = transaction;
                foreach (string sql in SqlList)
                {
                    if (sql.Length > 0)
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
                conn.Close();
                return true;
            }
            catch
            {
                try
                {
                    //回滚操作
                    transaction.Rollback();
                }
                catch
                {
                    //回滚失败
                }
                conn.Close();
                return false;
            }
        }
    }

    /// 函数名称：ExecuteSqlInFile
    /// 函数作者：朱坤堂
    /// 创建时间：2008-06-18
    /// <summary>
    /// 执行SQL脚本文件
    /// </summary>
    /// <param name="pathToScriptFile">SQL脚本文件路径</param>
    /// <returns>返回操作标志（bool），true成功，false失败</returns>
    public static bool ExecuteSqlInFile(string pathToScriptFile, string ConnString)
    {
        if (File.Exists(pathToScriptFile) == false)
        {
            return false;
        }
        Stream stream = File.OpenRead(pathToScriptFile);
        StreamReader reader = new StreamReader(stream);
        using (MySqlConnection connection = new MySqlConnection(ConnString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            string sql = string.Empty;
            while ((sql = ReadNextStatementFromStream(reader)) != null)
            {
                if (sql.Length > 0)
                {
                    command.CommandText = sql;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch { }
                }
            }
            connection.Close();
        }
        reader.Close();
        return true;
    }
    private static string ReadNextStatementFromStream(StreamReader _reader)
    {
        string lineOfText;
        StringBuilder sb = new StringBuilder();
        while (true)
        {
            lineOfText = _reader.ReadLine();
            if (lineOfText == null)
            {
                if (sb.Length > 0)
                {
                    return sb.ToString();
                }
                else
                {
                    return null;
                }
            }
            if (lineOfText.TrimEnd().EndsWith(";"))
            {
                sb.AppendFormat("{0}\r\n", lineOfText);
                break;
            }
            if (!lineOfText.TrimStart().StartsWith("#"))
            {
                sb.AppendFormat("{0}\r\n", lineOfText);
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 批量执行SQL语句文件
    /// </summary>
    /// <param name="SQLFilePath"></param>
    public static void ExecuteBatchSQL(string SQLFilePath, string ConnString)
    {
        string ConnectionString = string.Empty;
        string ConnectionTemplate = SysConfigs.ConnectionTemplate;
        StringBuilder sqlString = new StringBuilder();
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand command = new MySqlCommand();
        command.CommandType = CommandType.Text;
        string[] DataList = File.ReadAllLines(SQLFilePath, Encoding.Default);
        for (int i = 0; i < DataList.Length; i++)
        {
            if (DataList[i].StartsWith("#")) { continue; }
            if (DataList[i].StartsWith("StartExcuteSQL"))
            {
                string dbName = DataList[i].Substring(15);
                conn = new MySqlConnection(ConnectionTemplate.Replace("ConnectionTemplate", dbName));
                conn.Open();
                continue;
            }
            if (DataList[i].StartsWith("EndExcuteSQL"))
            {
                conn.Close();
                conn = null;
                continue;
            }
            if (DataList[i].TrimEnd().EndsWith(";"))
            {
                sqlString.AppendFormat("{0}\r\n", DataList[i]);
                command.Connection = conn;
                command.CommandText = sqlString.ToString();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
                sqlString.Length = 0;
                continue;
            }
            else
            {
                sqlString.AppendFormat("{0}\r\n", DataList[i]);
            }
        }
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }

    /// 函数名称：GetRecordID
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 获得记录编号
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <returns>记录编号（string）</returns>
    public static string GetRecordID(string TableName, string ConnString)
    {
        string OLastID = ExecuteScalar(string.Concat("select LastID from CW_RecordID where TableName='", TableName, "'"), "NoLastID", ConnString);
        if (OLastID == "NoLastID")
        {
            ExecuteSQL(string.Concat("insert into CW_RecordID(TableName,LastID)values('", TableName, "','1000100001')"), ConnString);
            return "1000100000";
        }
        else
        {
            string LastID = OLastID;
            int idCount = LastID.Length;
            if (LastID.Replace("9", "").Length == 0)
            {
                LastID = "1".PadRight(idCount, '0');
            }
            else
            {
                decimal NextID = decimal.Parse(LastID) + 1;
                LastID = NextID.ToString().PadLeft(idCount, '0');
            }
            ExecuteSQL(string.Concat("update CW_RecordID set LastID = '", LastID, "' where TableName='", TableName, "'"), ConnString);
            return LastID;
        }
    }

    /// <summary>
    /// 授权文件合法性验证
    /// </summary>
    /// <returns></returns>
    public static bool ValidateVerifySignedHash()
    {
        if (File.Exists(ValidateClass.RegFilePath))
        {
            DataSet RegInfoDS = new DataSet("FinancialDB");
            RegInfoDS.ReadXml(ValidateClass.RegFilePath);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //服务器端验证
            string SignRegInfo = RegInfoDS.Tables["RegInfo"].Rows[0]["RegInfoSign"].ToString();
            string RegInfoHash = RegInfoDS.Tables["RegInfo"].Rows[0]["RegInfoHash"].ToString();
            RegInfoDS.Tables["RegInfo"].Rows[0]["RegInfoSign"] = "";
            RegInfoDS.Tables["RegInfo"].Rows[0]["RegInfoHash"] = "";
            string _RegInfoHash = FormsAuthentication.HashPasswordForStoringInConfigFile(RegInfoDS.GetXml(), "sha1");
            RegInfoDS.Tables["RegInfo"].Rows[0]["RegInfoHash"] = RegInfoHash;
            //授权文件完整性验证
            if (RegInfoHash == _RegInfoHash && ValidateClass.VerifySignedHash(RegInfoDS.GetXml(), SignRegInfo))
            {
                ////本机合法验证
                //string ClientHDid = RegInfoDS.Tables["RegInfo"].Rows[0]["ClientHDid"].ToString();
                //if (ClientHDid.IndexOf(ValidateClass.GetMachineCode()) == -1)
                //{
                //    //临时过渡使用，截止2013-12-31
                //    if (ClientHDid.IndexOf("FAD44BF1F402A308") == -1)
                //    {
                //        PageClass.UrlRedirect("您的授权文件不是本机的授权文件！", 0);
                //        return false;
                //    }
                //}
                return true;
            }
            else
            {
                PageClass.UrlRedirect("软件授权文件已被篡改，请致电我公司更换授权文件！", 0);
                return false;
            }
        }
        else
        {
            PageClass.UrlRedirect("授权文件不存在，请使用注册程序导入授权文件！", 0);
            return false;
        }
    }
}
