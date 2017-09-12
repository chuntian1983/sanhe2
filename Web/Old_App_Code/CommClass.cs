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
using System.Collections.Generic;
using System.Collections;
using System.IO;

/// 模块名称：通用类
/// 程序版本：2.0
/// 程序作者：朱坤堂
/// 创建时间：2008-3-20
/// <summary>
/// 公用函数区域
/// </summary>
public class CommClass
{
    /////////////////////////////////////////////////////////
    //////全局参数
    /////////////////////////////////////////////////////////
    public static string ConnString
    {
        get
        {
            UserInfo.CheckSession();
            return SysConfigs.ConnectionTemplate.Replace("ConnectionTemplate", SysConfigs.DbPrefix + UserInfo.AccountID);
        }
    }

    ////////////////////////////////////////////////////////
    //////公用函数
    /////////////////////////////////////////////////////////
    public CommClass()
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

    /// 函数名称：InitSysPara
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 设置软件配置参数[CW_SysPara]
    /// </summary>
    /// <param name="ParaName"></param>
    /// <returns></returns>
    public static void InitSysPara(DropDownList ddl, string ParaType)
    {
        InitSysPara(ddl, ParaType, "");
    }
    public static void InitSysPara(DropDownList ddl, string ParaType, string firstItem)
    {
        DbHelper.InitSysPara(ddl, ParaType, firstItem, ConnString);
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

    /// <summary>
    /// 根据SQL语句初始化DropDownList控件
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="ddl"></param>
    public static void InitDropDownList(string sql, DropDownList ddl)
    {
        DataTable dt = GetDataTable(sql);
        foreach (DataRow row in dt.Rows)
        {
            ddl.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
        }
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
        return GetTableValue("cw_subject", FieldName, string.Format("subjectno='{0}'", SubjectNo));
    }

    /// 函数名称：GetDetailSubject
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-29
    /// <summary>
    /// 通过科目代码，获取科目下级所有级别名称
    /// </summary>
    /// <param name="SubjectNo">科目代码</param>
    /// <returns>查询值</returns>
    public static string GetDetailSubject(string SubjectNo)
    {
        string[] SubjectLevel = SysConfigs.SubjectLevel.Split(',');
        StringBuilder NameList = new StringBuilder();
        for (int i = 2; i < SubjectLevel.Length; i++)
        {
            int level = int.Parse(SubjectLevel[i]);
            if (level <= SubjectNo.Length)
            {
                NameList.Append("/");
                NameList.Append(GetFieldFromNo(SubjectNo.Substring(0, level), "SubjectName"));
            }
            else
            {
                break;
            }
        }
        if (NameList.Length > 0)
        {
            return NameList.ToString().Substring(1);
        }
        else
        {
            return "";
        }
    }

    /// 函数名称：GetSubjectNo2Name
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-29
    /// <summary>
    /// 通过科目代码，获取科目所有级别名称
    /// </summary>
    /// <param name="SubjectNo">科目代码</param>
    /// <returns>查询值</returns>
    public static string GetSubjectNo2Name(string SubjectNo)
    {
        string[] SubjectLevel = SysConfigs.SubjectLevel.Split(',');
        StringBuilder NameList = new StringBuilder();
        for (int i = 1; i < SubjectLevel.Length; i++)
        {
            int level = int.Parse(SubjectLevel[i]);
            if (level <= SubjectNo.Length)
            {
                NameList.Append("/");
                NameList.Append(GetFieldFromNo(SubjectNo.Substring(0, level), "SubjectName"));
            }
            else
            {
                break;
            }
        }
        if (NameList.Length > 0)
        {
            return NameList.ToString().Substring(1);
        }
        else
        {
            return "";
        }
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

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        DataRow row = MainClass.GetDataRow(string.Format("select * from cw_reportdesign where id='{0}'", ID));
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
            return PageClass.CreateGridView(row, BindTable, GV, sort);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////自定义表格设计
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// 函数名称：InitGridView
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-29
    /// <summary>
    /// 初始化表格
    /// </summary>
    /// <param name="DesignID"></param>
    /// <param name="ReportDate"></param>
    /// <param name="GV"></param>
    public static void InitGridView(string DesignID, string ReportDate, GridView GV)
    {
        string CountRowIndex = "";
        StringBuilder GridViewRowFlag = new StringBuilder();
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 8; i++)
        {
            BindTable.Columns.Add("F" + i.ToString("00"));
        }
        DataRow NewRow;
        DataRowCollection rows = GetDataRows(string.Format("select * from cw_reportrowitem where designid='{0}' order by id", DesignID));
        if (rows != null)
        {
            int RowID = 0;
            int CellStep = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                //行列分割设置
                if (rows[i]["rowinfo"].ToString() == "-")
                {
                    GridViewRowFlag.Append("|,");
                    CellStep += 4;
                    RowID = 0;
                    continue;
                }
                //合计行数据输出
                if (CellStep == 8)
                {
                    if (CountRowIndex.Length == 0)
                    {
                        CountRowIndex = BindTable.Rows.Count.ToString();
                    }
                    NewRow = BindTable.NewRow();
                    //////////////////////////////////////////////////////////////////////
                    //左栏合计行
                    if (rows[i]["rowinfo"].ToString().EndsWith("!_!SingleRow"))
                    {
                        //合计行默认数据输出
                        DataRow AnewRow = rows[i];
                        string[] Item = Regex.Split(rows[i]["rowinfo"].ToString(), "!_!");
                        NewRow[0] = Item[0];
                        NewRow[1] = Item[2];
                        NewRow[2] = Item[3];
                        NewRow[3] = Item[4];
                    }
                    else
                    {
                        NewRow[0] = rows[i]["itemname"].ToString();
                        NewRow[1] = rows[i]["rowno"].ToString();
                        NewRow[2] = rows[i]["itemexpr0"].ToString();
                        NewRow[3] = rows[i]["itemexpr1"].ToString();
                    }
                    GridViewRowFlag.Append(rows[i]["id"].ToString() + ",");
                    //////////////////////////////////////////////////////////////////////
                    //右栏合计行
                    i++;
                    if (rows[i]["rowinfo"].ToString().EndsWith("!_!SingleRow"))
                    {
                        //合计行默认数据输出
                        DataRow AnewRow = rows[i];
                        string[] Item = Regex.Split(rows[i]["rowinfo"].ToString(), "!_!");
                        NewRow[4] = Item[0];
                        NewRow[5] = Item[2];
                        NewRow[6] = Item[3];
                        NewRow[7] = Item[4];
                    }
                    else
                    {
                        NewRow[4] = rows[i]["itemname"].ToString();
                        NewRow[5] = rows[i]["rowno"].ToString();
                        NewRow[6] = rows[i]["itemexpr0"].ToString();
                        NewRow[7] = rows[i]["itemexpr1"].ToString();
                    }
                    GridViewRowFlag.Append(rows[i]["id"].ToString() + ",");
                    //////////////////////////////////////////////////////////////////////
                    BindTable.Rows.Add(NewRow);
                    continue;
                }
                //行列数据输出
                if (rows[i]["rowinfo"].ToString().EndsWith("!_!SingleRow"))
                {
                    //非自动行数据输出
                    DataRow AnewRow = rows[i];
                    string[] Item = Regex.Split(rows[i]["rowinfo"].ToString(), "!_!");
                    if (Item[1].Length == 0)
                    {
                        //含合计行的单行
                        AnewRow["itemname"] = Item[0];
                        AnewRow["rowno"] = Item[2];
                        AnewRow["itemexpr0"] = Item[3];
                        AnewRow["itemexpr1"] = Item[4];
                    }
                    else
                    {
                        //纯科目运算公式
                        string[] SList = Item[1].Split(new char[] { '+', '-', '*', '/' });
                        AnewRow["itemname"] = Item[0];
                        AnewRow["rowno"] = Item[2];
                        AnewRow["itemexpr0"] = Item[1];
                        AnewRow["itemexpr1"] = Item[1];
                        for (int m = 0; m < SList.Length; m++)
                        {
                            string NameNo = GetFieldFromNo(SList[m], "subjectname") + "[" + SList[m] + "]";
                            AnewRow["itemexpr0"] = AnewRow["itemexpr0"].ToString().Replace(SList[m], Item[3].Replace(":", NameNo + ":"));
                            AnewRow["itemexpr1"] = AnewRow["itemexpr1"].ToString().Replace(SList[m], Item[4].Replace(":", NameNo + ":"));
                        }
                    }
                    RowID = InsertDataRow(BindTable, RowID, CellStep, rows[i]);
                    GridViewRowFlag.Append(rows[i]["id"].ToString() + ",");
                }
                else if (rows[i]["rowinfo"].ToString().EndsWith("!_!AutoRow"))
                {
                    //自动行数据输出
                    string[] Item = Regex.Split(rows[i]["rowinfo"].ToString(), "!_!");
                    DataRowCollection arows = CommClass.GetDataRows("select subjectno,subjectname from cw_subject where parentno='" + Item[0] + "' order by subjectno");
                    if (arows != null && arows.Count > 0)
                    {
                        DataRow AnewRow = rows[i];
                        string[] ItemName = Regex.Split(rows[i]["itemname"].ToString(), "!_!");
                        string[] RowNo = Regex.Split(rows[i]["rowno"].ToString(), "!_!");
                        string[] ItemExpr0 = Regex.Split(rows[i]["itemexpr0"].ToString(), "!_!");
                        string[] ItemExpr1 = Regex.Split(rows[i]["itemexpr1"].ToString(), "!_!");
                        for (int k = 0; k < arows.Count; k++)
                        {
                            if (k < ItemName.Length && ItemName[k].Length > 0)
                            {
                                //插入已定义数据
                                AnewRow["itemname"] = ItemName[k];
                                AnewRow["rowno"] = RowNo[k];
                                AnewRow["itemexpr0"] = ItemExpr0[k];
                                AnewRow["itemexpr1"] = ItemExpr1[k];
                            }
                            else
                            {
                                //插入科目数据
                                AnewRow["rowno"] = "";
                                AnewRow["itemname"] = (k == 0 ? Item[1] : Item[2]) + (k + 1).ToString() + "．" + arows[k]["subjectname"].ToString();
                                AnewRow["itemexpr0"] = Item[3].Replace(":", arows[k]["subjectname"].ToString() + "[" + arows[k]["subjectno"].ToString() + "]:");
                                AnewRow["itemexpr1"] = Item[4].Replace(":", arows[k]["subjectname"].ToString() + "[" + arows[k]["subjectno"].ToString() + "]:");
                            }
                            RowID = InsertDataRow(BindTable, RowID, CellStep, AnewRow);
                            GridViewRowFlag.Append(rows[i]["id"].ToString() + ",");
                        }
                    }
                }
                else if (rows[i]["rowinfo"].ToString().EndsWith("!_!NoExpr"))
                {
                    //固定行数据输出（无科目）
                    DataRow AnewRow = rows[i];
                    if (rows[i]["itemname"].ToString().Length == 0)
                    {
                        AnewRow["itemname"] = rows[i]["rowinfo"].ToString().Replace("!_!NoExpr", "");
                    }
                    else
                    {
                        AnewRow["itemname"] = rows[i]["itemname"].ToString();
                    }
                    AnewRow["rowno"] = "";
                    AnewRow["itemexpr0"] = "";
                    AnewRow["itemexpr1"] = "";
                    RowID = InsertDataRow(BindTable, RowID, CellStep, rows[i]);
                    GridViewRowFlag.Append(rows[i]["id"].ToString() + ",");
                }
                else
                {
                    RowID = InsertDataRow(BindTable, RowID, CellStep, rows[i]);
                    GridViewRowFlag.Append(rows[i]["id"].ToString() + ",");
                }
            }
        }
        //创建空数据行
        for (int i = BindTable.Rows.Count - 1; i < 10; i++)
        {
            BindTable.Rows.Add(BindTable.NewRow());
        }
        BindTable.AcceptChanges();
        CreateGridView(DesignID, BindTable, GV);
        if (ReportDate.Length == 0) { return; }
        GV.Attributes["CountRowIndex"] = CountRowIndex;
        GV.Attributes["GridViewRowFlag"] = GridViewRowFlag.ToString();
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //计算报表公式
        ClsCalculate clsCalculate = new ClsCalculate();
        clsCalculate.DesignID = DesignID;
        clsCalculate.ReportDate = ReportDate;
        clsCalculate.GV = GV;
        for (int i = 1; i < GV.Rows.Count; i++)
        {
            GV.Rows[i].Attributes.Add("onclick", "OnRowClick(this.rowIndex,'" + GV.Rows[i].ClientID + "')");
            //科目设置
            GV.Rows[i].Cells[0].Attributes.Add("onclick", "OnCell0Click('" + GV.Rows[i].Cells[0].ClientID + "')");
            GV.Rows[i].Cells[4].Attributes.Add("onclick", "OnCell0Click('" + GV.Rows[i].Cells[4].ClientID + "')");
            GV.Rows[i].Cells[0].Text = GV.Rows[i].Cells[0].Text.Replace(" ", "&nbsp;&nbsp;");
            GV.Rows[i].Cells[4].Text = GV.Rows[i].Cells[4].Text.Replace(" ", "&nbsp;&nbsp;");
            //获取公式值
            int[] CellPos = new int[] { 2, 3, 6, 7 };
            for (int k = 0; k < CellPos.Length; k++)
            {
                GV.Rows[i].Cells[CellPos[k]].Attributes.Add("ondblclick", "OnCell1Click('" + GV.Rows[i].Cells[CellPos[k]].ClientID + "')");
                clsCalculate.CalculateExpr(GV.Rows[i].Cells[CellPos[k]]);
            }
            //行次编辑设置
            GV.Rows[i].Cells[1].Attributes.Add("onclick", "OnCell2Click('" + GV.Rows[i].Cells[1].ClientID + "')");
            GV.Rows[i].Cells[5].Attributes.Add("onclick", "OnCell2Click('" + GV.Rows[i].Cells[5].ClientID + "')");
        }
    }

    public static int InsertDataRow(DataTable BindTable, int RowID, int CellStep, DataRow DRow)
    {
        DataRow NewRow;
        if (CellStep == 0)
        {
            NewRow = BindTable.NewRow();
        }
        else
        {
            if (RowID == BindTable.Rows.Count)
            {
                //右排行多于左排
                BindTable.Rows.Add(BindTable.NewRow());
            }
            NewRow = BindTable.Rows[RowID];
        }
        NewRow[0 + CellStep] = DRow["itemname"].ToString();
        NewRow[1 + CellStep] = DRow["rowno"].ToString();
        NewRow[2 + CellStep] = DRow["itemexpr0"].ToString();
        NewRow[3 + CellStep] = DRow["itemexpr1"].ToString();
        if (CellStep == 0)
        {
            BindTable.Rows.Add(NewRow);
        }
        return ++RowID;
    }

    public struct RowColumn
    {
        public string ItemName;
        public string RowNo;
        public string ItemExpr0;
        public string ItemExpr1;
    }
    public static void SaveGridView(string DesignID, string RowItemText, string GridViewRowFlag)
    {
        string[] RowFlag = GridViewRowFlag.Split(',');
        string[] RowItem = Regex.Split(RowItemText, "!_0_!");
        DataSet ds = GetDataSet("select * from cw_reportrowitem where designid='" + DesignID + "' order by id");
        if (ds.Tables[0].Rows.Count > 0)
        {
            Dictionary<string, RowColumn> allRows = new Dictionary<string, RowColumn>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                RowColumn _RowColumn = new RowColumn();
                allRows.Add(ds.Tables[0].Rows[i]["id"].ToString(), _RowColumn);
            }
            int k = 0;
            for (int i = 0; i < RowItem.Length - 1; i++)
            {
                if (RowFlag[k] == "|")
                {
                    while (RowItem[i++] != "|") { }
                    k++;
                }
                string[] Item = Regex.Split(RowItem[i], "!_1_!");
                RowColumn _RowColumn = allRows[RowFlag[k++]];
                if (_RowColumn.ItemName == null)
                {
                    _RowColumn.ItemName = Item[0];
                    _RowColumn.RowNo = Item[1];
                    _RowColumn.ItemExpr0 = Item[2];
                    _RowColumn.ItemExpr1 = Item[3];
                }
                else
                {
                    _RowColumn.ItemName += "!_!" + Item[0];
                    _RowColumn.RowNo += "!_!" + Item[1];
                    _RowColumn.ItemExpr0 += "!_!" + Item[2];
                    _RowColumn.ItemExpr1 += "!_!" + Item[3];
                }
                allRows[RowFlag[k - 1]] = _RowColumn;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["itemname"].ToString() != "-")
                {
                    RowColumn _RowColumn = allRows[ds.Tables[0].Rows[i]["id"].ToString()];
                    ds.Tables[0].Rows[i]["itemname"] = _RowColumn.ItemName;
                    ds.Tables[0].Rows[i]["rowno"] = _RowColumn.RowNo;
                    ds.Tables[0].Rows[i]["itemexpr0"] = _RowColumn.ItemExpr0;
                    ds.Tables[0].Rows[i]["itemexpr1"] = _RowColumn.ItemExpr1;
                    if (ds.Tables[0].Rows[i]["rowinfo"].ToString().IndexOf("!_!SingleRow") != -1)
                    {
                        ds.Tables[0].Rows[i]["rowinfo"] = ds.Tables[0].Rows[i]["rowinfo"].ToString().Replace("!_!SingleRow", "!_!HasModify");
                    }
                }
            }
            UpdateDataSet(ds);
        }
    }

    public static void GetTemplate(string DesignID)
    {
        DataSet ds = GetDataSet("select * from cw_reportrowitem where designid='" + DesignID + "' order by id");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["itemname"] = "";
                ds.Tables[0].Rows[i]["rowno"] = "";
                ds.Tables[0].Rows[i]["itemexpr0"] = "";
                ds.Tables[0].Rows[i]["itemexpr1"] = "";
                if (ds.Tables[0].Rows[i]["rowinfo"].ToString().IndexOf("!_!HasModify") != -1)
                {
                    ds.Tables[0].Rows[i]["rowinfo"] = ds.Tables[0].Rows[i]["rowinfo"].ToString().Replace("!_!HasModify", "!_!SingleRow");
                }
            }
            UpdateDataSet(ds);
        }
    }
}
