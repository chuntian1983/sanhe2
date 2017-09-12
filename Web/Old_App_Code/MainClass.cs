using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Collections;
using System.IO;

using System.Xml;

/// 模块名称：通用类
/// 程序版本：2.0
/// 程序作者：朱坤堂
/// 创建时间：2008-3-20
/// <summary>
/// 公用函数区域
/// </summary>
public class MainClass
{
    /////////////////////////////////////////////////////////
    //////全局参数
    /////////////////////////////////////////////////////////
    public static string ConnString = SysConfigs.DbConnection;

    ////////////////////////////////////////////////////////
    //////公用函数
    /////////////////////////////////////////////////////////
    public MainClass()
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
    public static void WriteCTL_Log(string LogID, string LogContent)
    {
        DbHelper.WriteCTL_Log(LogID, LogContent, ConnString);
    }

    /// 函数名称：GetSysPara
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 获取软件配置参数[CW_SysPara]
    /// </summary>
    /// <param name="ParaName"></param>
    /// <returns></returns>
    public static string GetSysPara(string ParaName)
    {
        return DbHelper.GetSysPara(ParaName, ConnString);
    }

    /// 函数名称：SetSysPara
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 设置软件配置参数[CW_SysPara]
    /// </summary>
    /// <param name="ParaName"></param>
    /// <returns></returns>
    public static void SetSysPara(string ParaName, string ParaValue)
    {
        DbHelper.SetSysPara(ParaName, ParaValue, ConnString);
    }

    /// 函数名称：CreateDatabase
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 创建账套数据库
    /// </summary>
    /// <param name="AccountID"></param>
    public static void CreateDatabase(string AccountID, string AccountType)
    {
        using (MySqlConnection conn = new MySqlConnection(SysConfigs.SaConnection))
        {
            conn.Open();
            try
            {
                DataSet ds = new DataSet();
                string TableColumns = string.Empty;
                string DbName = SysConfigs.DbPrefix + AccountID;
                string myExecuteQuery = string.Format("DROP DATABASE IF EXISTS {0};CREATE DATABASE {0}", DbName);
                MySqlCommand myCommand = new MySqlCommand(myExecuteQuery, conn);
                myCommand.ExecuteNonQuery();
                UserInfo.AccountID = AccountID;
                CommClass.ExecuteSqlInFile(SysConfigs.GetAppDataFilePath("cw_accountdb_mysql.sql"));
                //复制科目库
                if (AccountType == "1")
                {
                    string[] lines = File.ReadAllLines(SysConfigs.GetAppDataFilePath("cw_subject2.sql"), Encoding.Default);
                    StringBuilder SQLString = new StringBuilder();
                    SQLString.AppendFormat("insert into {0} ({1})values", "cw_subject", lines[0].Replace("'", ""));
                    for (int i = 1; i < lines.Length; i++)
                    {
                        SQLString.AppendFormat("({0}),", lines[i]);
                    }
                    if (SQLString.ToString().EndsWith(","))
                    {
                        SQLString.Remove(SQLString.Length - 1, 1);
                        CommClass.ExecuteSQL(SQLString.ToString());
                    }
                    SQLString.Length = 0;
                }
                else
                {
                    TableColumns = "ParentNo,SubjectNo,SubjectName,SubjectType,AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct,AccountFlag";
                    ds = MainClass.GetDataSet("select " + TableColumns + " from cw_subject where unitid='" + UserInfo.UnitID + "'");
                    ImportDefaultData("CW_Subject", TableColumns, ds);
                }
                //复制报表设计参数
                ds = MainClass.GetDataSet("select ID,TableName,HeadString,GatherCells,ColumnWidth,HAlign from cw_reportdesign");
                DataSet NewUnit = ds.Clone();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    NewUnit.Tables[0].Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                }
                CommClass.UpdateDataSet(NewUnit);
                //复制自定义报表设计行数据
                TableColumns = "LevelID,DefineID,ItemName0,RowNo0,ItemExpr0,ItemExpr1,ItemName1,RowNo1,ItemExpr2,ItemExpr3";
                ds = MainClass.GetDataSet("select " + TableColumns + " from cw_definerowitem where unitid='000000' order by id");
                ImportDefaultData("CW_DefineRowItem", TableColumns, ds);
                //复制报表设计行数据
                TableColumns = "DesignID,RowInfo";
                ds = MainClass.GetDataSet("select " + TableColumns + " from cw_reportrowitem where unitid='000000' order by id");
                ImportDefaultData("CW_ReportRowItem", TableColumns, ds);
            }
            catch { throw; }
        }
    }

    private static void ImportDefaultData(string TableName, string TableColumns, DataSet ImData)
    {
        StringBuilder SQLString = new StringBuilder();
        SQLString.AppendFormat("insert into {0} (ID,{1})values", TableName, TableColumns);
        for (int i = 0; i < ImData.Tables[0].Rows.Count; i++)
        {
            SQLString.AppendFormat("('{0}", CommClass.GetRecordID(TableName));
            for (int k = 0; k < ImData.Tables[0].Columns.Count; k++)
            {
                SQLString.AppendFormat("','{0}", ImData.Tables[0].Rows[i][k].ToString());
            }
            SQLString.Append("'),");
        }
        if (SQLString.ToString().EndsWith(","))
        {
            SQLString.Remove(SQLString.Length - 1, 1);
            CommClass.ExecuteSQL(SQLString.ToString());
        }
        SQLString.Length = 0;
    }

    /// 函数名称：DropDatabase
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 删除账套数据库
    /// </summary>
    /// <param name="AccountID"></param>
    public static void DropDatabase(string AccountID)
    {
        using (MySqlConnection conn = new MySqlConnection(SysConfigs.SaConnection))
        {
            conn.Open();
            try
            {
                MySqlCommand myCommand = new MySqlCommand(string.Format("Drop Database {0}{1}", SysConfigs.DbPrefix, AccountID), conn);
                myCommand.ExecuteNonQuery();
            }
            catch { }
        }
    }

    /// 函数名称：CheckInit
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 检测单位是否初始化
    /// </summary>
    /// <param name="UnitID"></param>
    public static void CheckInit(string UnitID)
    {
        StringBuilder SQLString = new StringBuilder();
        if (CheckExist("cw_subject", string.Format("unitid='{0}'", UnitID)) == false)
        {
            //复制科目模板库
            DataSet NewSubject = GetDataSet("select * from cw_subject where unitid='000000'");
            SQLString.Append("insert into cw_subject(id,unitid,parentno,subjectno,subjectname,subjecttype,AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct)values");
            for (int k = 0; k < NewSubject.Tables[0].Rows.Count; k++)
            {
                SQLString.Append("('");
                SQLString.Append(GetRecordID("CW_Subject") + "','");
                SQLString.Append(UnitID + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["parentno"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["subjectno"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["subjectname"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["subjecttype"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["AccountType"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["BeginBalance"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["IsEntryData"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["IsDetail"].ToString() + "','");
                SQLString.Append(NewSubject.Tables[0].Rows[k]["AccountStruct"].ToString() + "'),");
            }
            if (SQLString.ToString().EndsWith(","))
            {
                SQLString.Remove(SQLString.Length - 1, 1);
                ExecuteSQL(SQLString.ToString());
            }
            SQLString.Length = 0;
        }
        if (CheckExist("cw_collectrowitem", string.Format("unitid='{0}'", UnitID)) == false)
        {
            //复制汇总报表
            DataSet NewCollect = GetDataSet("select * from cw_collectrowitem where unitid='000000'");
            SQLString.Append("insert into cw_collectrowitem(id,unitid,levelid,DesignID,ItemName0,RowNo0,ItemExpr0,ItemExpr1,ItemName1,RowNo1,ItemExpr2,ItemExpr3)values");
            for (int k = 0; k < NewCollect.Tables[0].Rows.Count; k++)
            {
                SQLString.Append("('");
                SQLString.Append(GetRecordID("CW_CollectRowItem") + "','");
                SQLString.Append(UnitID + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["levelid"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["DesignID"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["ItemName0"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["RowNo0"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["ItemExpr0"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["ItemExpr1"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["ItemName1"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["RowNo1"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["ItemExpr2"].ToString() + "','");
                SQLString.Append(NewCollect.Tables[0].Rows[k]["ItemExpr3"].ToString() + "'),");
            }
            if (SQLString.ToString().EndsWith(","))
            {
                SQLString.Remove(SQLString.Length - 1, 1);
                ExecuteSQL(SQLString.ToString());
            }
            SQLString.Length = 0;
        }
    }

    /// 函数名称：GetAccountDate
    /// 函数作者：朱坤堂
    /// 创建时间：2009-04-11
    /// <summary>
    /// 获取账套财务日期
    /// </summary>
    /// <returns>财务日期</returns>
    public static DateTime GetAccountDate()
    {
        if (UserInfo.AccountID == null)
        {
            return Convert.ToDateTime("1900年01月01日");
        }
        else
        {
            string AccountDate = GetFieldFromID(UserInfo.AccountID, "accountdate", "cw_account");
            if (AccountDate.Length == 0 || AccountDate == "NoDataItem")
            {
                return Convert.ToDateTime("1900年01月01日");
            }
            else
            {
                return Convert.ToDateTime(AccountDate);
            }
        }
    }

    /// 函数名称：InitAccountYear
    /// 函数作者：朱坤堂
    /// 创建时间：2009-04-11
    /// <summary>
    /// 初始化报表查询年度控件
    /// </summary>
    /// <param name="ddl">年度控件</param>
    public static void InitAccountYear(DropDownList ddl)
    {
        string AccountYear = GetFieldFromID(UserInfo.AccountID, "accountyear", "cw_account");
        if (AccountYear == "NoDataItem")
        {
            ddl.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
        }
        else
        {
            string[] _AccountYear = AccountYear.Split('|');
            for (int i = 0; i < _AccountYear.Length; i++)
            {
                if (_AccountYear[i].Length == 0) { continue; }
                ddl.Items.Add(new ListItem(_AccountYear[i] + "年", _AccountYear[i]));
            }
        }
        ddl.Text = GetAccountDate().Year.ToString();
    }

    /// 函数名称：GetFieldFromNo
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-29
    /// <summary>
    /// 获取科目表某个字段值，通过科目代码
    /// </summary>
    /// <param name="SubjectNo">科目代码</param>
    /// <param name="FieldName">字段名</param>
    /// <returns>查询值</returns>
    public static string GetFieldFromNo(string SubjectNo, string FieldName)
    {
        return GetTableValue("cw_subject", FieldName, string.Format("subjectno='{0}' and unitid='{1}'", SubjectNo, UserInfo.UnitID));
    }

    /// 函数名称：GetDataTable
    /// 函数作者：朱坤堂
    /// 创建时间：2008-10-16
    /// <summary>
    /// 获取数据表GetDataTable
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>数据表</returns>
    public static DataTable GetDataTable(string sqlString)
    {
        return DbHelper.GetDataTable(sqlString, ConnString);
    }

    /// 函数名称：GetDataSet
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 获取数据集DataSet
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>记录集DataSet</returns>
    public static DataSet GetDataSet(string sqlString)
    {
        return DbHelper.GetDataSet(sqlString, ConnString);
    }

    /// 函数名称：GetDataRow
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-21
    /// <summary>
    /// 获取数据集的单条数据行
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>记录集的所有行的集合</returns>
    public static DataRow GetDataRow(string sqlString)
    {
        return DbHelper.GetDataRow(sqlString, ConnString);
    }

    /// 函数名称：GetDataRows
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-29
    /// <summary>
    /// 获取数据集的所有数据行
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>记录集的所有行的集合</returns>
    public static DataRowCollection GetDataRows(string sqlString)
    {
        return DbHelper.GetDataRows(sqlString, ConnString);
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
    public static string GetTableValue(string TableName, string FieldName, string whstring)
    {
        return GetTableValue(TableName, FieldName, whstring, "NoDataItem");
    }
    public static string GetTableValue(string TableName, string FieldName, string whstring, string defaultValue)
    {
        return DbHelper.GetTableValue(TableName, FieldName, whstring, defaultValue, ConnString);
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
    public static string GetFieldFromID(string ID, string FieldName)
    {
        return GetFieldFromID(ID, FieldName, "cw_subject");
    }
    public static string GetFieldFromID(string ID, string FieldName, string TableName)
    {
        return DbHelper.GetFieldFromID(ID, FieldName, TableName, ConnString);
    }

    /// 函数名称：UpdateDataSet
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 更新改变后的记录集
    /// </summary>
    /// <param name="ds"></param>
    public static void UpdateDataSet(DataSet ds)
    {
        UpdateDataSet(ds, "*");
    }
    public static void UpdateDataSet(DataSet ds, string columns)
    {
        DbHelper.UpdateDataSet(ds, columns, ConnString);
    }

    /// 函数名称：UpdateDataTable
    /// 函数作者：朱坤堂
    /// 创建时间：2009-06-17
    /// <summary>
    /// 更新改变后的记录集
    /// <summary>
    /// <param name="dt"></param>
    public static void UpdateDataTable(DataTable dt)
    {
        UpdateDataTable(dt, "*");
    }
    public static void UpdateDataTable(DataTable dt, string columns)
    {
        DbHelper.UpdateDataTable(dt, columns, ConnString);
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
    public static Boolean CheckExist(string TableName, string wh)
    {
        return DbHelper.CheckExist(TableName, wh, ConnString);
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
    public static int CountRecord(string TableName, string wh)
    {
        return DbHelper.CountRecord(TableName, wh, ConnString);
    }

    /// 函数名称：ExecuteSQL
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 执行单条SQL语句
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>产生影响的记录数（int）</returns>
    public static int ExecuteSQL(string sqlString)
    {
        return DbHelper.ExecuteSQL(sqlString, ConnString);
    }
    public static int ExecuteSQL(string TableName, Dictionary<string, string> dbParameters)
    {
        return DbHelper.ExecuteSQL(TableName, dbParameters, ConnString);
    }
    public static int ExecuteSQL(string TableName, Dictionary<string, string> dbParameters, string wh)
    {
        return DbHelper.ExecuteSQL(TableName, dbParameters, wh, ConnString);
    }

    /// 函数名称：ExecuteTransaction
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 执行事务处理多条语句
    /// </summary>
    /// <param name="sqlString">SQL查询语句</param>
    /// <returns>返回操作标志（bool），true成功，false失败</returns>
    public static bool ExecuteTransaction(string sqlString)
    {
        return DbHelper.ExecuteTransaction(sqlString, ConnString);
    }

    /// 函数名称：ExecuteSqlInFile
    /// 函数作者：朱坤堂
    /// 创建时间：2008-06-18
    /// <summary>
    /// 执行SQL脚本文件
    /// </summary>
    /// <param name="pathToScriptFile">SQL脚本文件路径</param>
    /// <returns>返回操作标志（bool），true成功，false失败</returns>
    public static bool ExecuteSqlInFile(string pathToScriptFile)
    {
        return DbHelper.ExecuteSqlInFile(pathToScriptFile, ConnString);
    }

    /// <summary>
    /// 批量执行SQL语句文件
    /// </summary>
    /// <param name="SQLFilePath"></param>
    public static void ExecuteBatchSQL(string SQLFilePath)
    {
        DbHelper.ExecuteBatchSQL(SQLFilePath, ConnString);
    }

    /// 函数名称：GetRecordID
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 获得记录编号
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <returns>记录编号（string）</returns>
    public static string GetRecordID(string TableName)
    {
        return DbHelper.GetRecordID(TableName, ConnString);
    }

    /// 函数名称：CreateGridView
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-29
    /// <summary>
    /// 初始化报表表格结构
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="BindTable"></param>
    /// <param name="GV"></param>
    /// <returns></returns>
    public static int CreateGridView(string ID, DataTable BindTable, GridView GV)
    {
        return CreateGridView(ID, BindTable, GV, null);
    }
    public static int CreateGridView(string ID, DataTable BindTable, GridView GV, string sort)
    {
        DataRow row = GetDataRow(string.Format("select * from cw_reportdesign where id='{0}'", ID));
        if (row == null)
        {
            DataSet design = new DataSet("Design");
            design.ReadXml(SysConfigs.GetAppDataFilePath("ReportDesign.xml"));
            DataRow[] rows = design.Tables[0].Select(string.Concat("id='", ID, "'"));
            if (rows.Length == 0)
            {
                return -1;
            }
            else
            {
                return PageClass.CreateGridView(rows[0], BindTable, GV, sort);
            }
        }
        else
        {
            return PageClass.CreateGridView(row, BindTable, GV);
        }
    }
}
