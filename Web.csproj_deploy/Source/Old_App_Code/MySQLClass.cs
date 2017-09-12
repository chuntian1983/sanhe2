using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using System.Text;

/// <summary>
/// MySQL数据库操作类
/// </summary>
public class MySQLClass
{
	public MySQLClass()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 数据库完全备份
    /// </summary>
    /// <param name="Notes">备份日志</param>
    public static bool BackupAllTable(string Notes)
    {
        StringBuilder tableNames = new StringBuilder();
        DataTable Tables = CommClass.GetDataTable("SHOW TABLES");
        foreach (DataRow row in Tables.Rows)
        {
            tableNames.AppendFormat("{0},", row[0].ToString());
        }
        return DbBackup(Notes, tableNames.ToString());
    }

    /// <summary>
    /// 数据库备份
    /// </summary>
    /// <param name="Notes">备份日志</param>
    /// <param name="BackupTable">备份表</param>
    public static bool DbBackup(string Notes, string BackupTable)
    {
        string MySQL_DataDir = DefConfigs.MySQL_DataDir;
        string DbName = SysConfigs.DbPrefix + UserInfo.AccountID;
        string BackupID = DbName + DateTime.Now.ToString("yyyyMMddHHmmss");
        string BackupDir = PageClass.PathCombine("../BackupDB", BackupID);
        List<string> FilterFile = new List<string>();
        FilterFile.Add("cw_viewsubjectsum");
        FilterFile.Add("cw_voucherentry");
        FilterFile.Add("cw_voucherentrydata");
        FilterFile.Add("cw_recordid");
        FilterFile.Add("cw_backupdb");
        FilterFile.Add("cw_logs");
        try
        {
            if (!Directory.Exists(BackupDir))
            {
                Directory.CreateDirectory(BackupDir);
            }
            string[] TableList = BackupTable.ToLower().Split(',');
            for (int i = 0; i < TableList.Length; i++)
            {
                if (TableList[i].Length == 0 || FilterFile.Contains(TableList[i]))
                {
                    continue;
                }
                string BackupDirPath = Path.Combine(BackupDir, TableList[i]);
                string DataDirPath = Path.Combine(MySQL_DataDir, string.Format("{0}/{1}", DbName, TableList[i]));
                File.Copy(DataDirPath + ".frm", BackupDirPath + ".frm", true);
                File.Copy(DataDirPath + ".myd", BackupDirPath + ".myd", true);
                File.Copy(DataDirPath + ".myi", BackupDirPath + ".myi", true);
            }
            if (BackupTable.IndexOf("cw_subjectsum,", StringComparison.OrdinalIgnoreCase) != -1)
            {
                string updateColumns = "AccountDate,AccountYear,StartAccountDate,LastCarryDate,RealLastCarry,YearCarryVoucher,YearCarryFlag";
                DataTable Account = MainClass.GetDataTable(string.Format("select {0} from cw_account where id='{1}'", updateColumns, UserInfo.AccountID));
                StreamWriter AccountFile = File.CreateText(Path.Combine(BackupDir, "CW_Account"));
                AccountFile.Write("update cw_account set ");
                for (int i = 0; i < Account.Columns.Count; i++)
                {
                    AccountFile.Write(string.Concat(Account.Columns[i].ColumnName, "='", Account.Rows[0][i].ToString(), "',"));
                }
                AccountFile.Write(string.Concat("id=id where id='", UserInfo.AccountID, "'"));
                AccountFile.Flush();
                AccountFile.Close();
            }
            CommClass.ExecuteSQL("insert into cw_backupdb(id,backuppath,notes,backupdate)values('"
                + BackupID + "','" + BackupID + "','" + Notes + "','" + DateTime.Now.ToString() + "')");
            //写入操作日志
            CommClass.WriteCTL_Log("100011", "数据备份编号：" + BackupID);
            //--
            return true;
        }
        catch
        {
            //写入操作日志
            CommClass.WriteCTL_Log("100011", "数据备份失败！");
            //--
            throw;
            return false;
        }
    }

    /// <summary>
    /// 数据库恢复
    /// </summary>
    /// <param name="BackupName">备份文件名</param>
    public static bool DbRestore(string BackupID)
    {
        string DbName = SysConfigs.DbPrefix + UserInfo.AccountID;
        string DataDirPath = Path.Combine(DefConfigs.MySQL_DataDir, DbName);
        string logsLastID = CommClass.GetTableValue("cw_recordid", "LastID", "TableName='CW_Logs'");
        //暂时保留备份记录，还原后更新
        DataTable dt = CommClass.GetDataTable("select id,backuppath,notes,backupdate from cw_backupdb");
        //还原系统数据库
        DirectoryInfo MainDbDir = new DirectoryInfo(PageClass.PathCombine("../BackupDB", BackupID));
        foreach (FileInfo file in MainDbDir.GetFiles())
        {
            if (string.Compare(file.Name, "cw_account", true) == 0)
            {
                string sqlText = File.ReadAllText(file.FullName);
                string[] sqlarr = System.Text.RegularExpressions.Regex.Split(sqlText, " where ");
                string sql = string.Concat(sqlarr[0], " where id='", UserInfo.AccountID, "'");
                MainClass.ExecuteSQL(sql);
                MainClass.ExecuteSQL(string.Concat("update cw_account set CTLDateTime=NULL where id='", UserInfo.AccountID, "'"));
            }
            else
            {
                file.CopyTo(Path.Combine(DataDirPath, file.Name), true);
            }
        }
        //修复数据库索引表
        MySQL_CheckDB(DbName);
        //写回备份记录，防止覆盖
        CommClass.ExecuteSQL("delete from cw_backupdb");
        StringBuilder sqlString = new StringBuilder();
        sqlString.Append("insert cw_backupdb(id,backuppath,notes,backupdate)values");
        foreach (DataRow row in dt.Rows)
        {
            sqlString.Append(string.Format("('{0}','{1}','{2}','{3}'),", row.ItemArray));
        }
        if (sqlString.ToString().EndsWith(","))
        {
            sqlString.Remove(sqlString.Length - 1, 1);
            CommClass.ExecuteSQL(sqlString.ToString());
        }
        sqlString.Length = 0;
        //升级数据库表结构
        DateTime dtBackupDate;
        if (DateTime.TryParse(CommClass.GetTableValue("cw_backupdb", "BackupDate", string.Format("id='{0}'", BackupID)), out dtBackupDate))
        {
            string backupDate = dtBackupDate.ToString("yyyy-MM-dd");
            if (SysConfigs.OnBackupUpdateTableStruct == "1" || string.Compare(backupDate, SysConfigs.LastSystemUpdateDate) <= 0)
            {
                DirectoryInfo updates = new DirectoryInfo(Path.Combine(SysConfigs.baseDirectory, "App_Data\\UpdateSQL_Files"));
                foreach (FileInfo file in updates.GetFiles())
                {
                    if (string.Compare(file.Name, backupDate) >= 0)
                    {
                        CommClass.ExecuteSqlInFile(file.FullName);
                    }
                }
            }
            else
            {
                //string dbPath = string.Concat(SysConfigs.baseDirectory, "BackupDB\\", Guid.NewGuid().ToString());
                //Directory.CreateDirectory(dbPath);
                //OutputData(dbPath);
                //ImportData(dbPath);
                //Directory.Delete(dbPath, true);
            }
        }
        //修复超限凭证状态
        RepairAlarmVoucher();
        //写入操作日志
        CommClass.ExecuteSQL(string.Concat("update cw_recordid set LastID='", logsLastID, "' where TableName='CW_Logs'"));
        CommClass.WriteCTL_Log("100011", "数据恢复编号：" + BackupID);
        //--
        //try
        //{

        //}
        //catch (Exception ex)
        //{
        //    //修复数据库索引表
        //    MySQL_CheckDB(DbName);
        //    //写入操作日志
        //    CommClass.ExecuteSQL(string.Concat("update cw_recordid set LastID='", logsLastID, "' where TableName='CW_Logs'"));
        //    CommClass.WriteCTL_Log("100011", "数据恢复失败，错误信息：" + ex.Message);
        //    //--
        //    return false;
        //}
        return true;
    }

    /// <summary>
    /// 导出备份数据
    /// </summary>
    /// <param name="dbPath"></param>
    private static void OutputData(string dbPath)
    {
        List<string> FilterFile = new List<string>();
        FilterFile.Add("cw_viewsubjectsum");
        FilterFile.Add("cw_voucherentry");
        FilterFile.Add("cw_voucherentrydata");
        DataTable Tables = CommClass.GetDataTable("SHOW TABLES");
        foreach (DataRow table in Tables.Rows)
        {
            string TableName = table[0].ToString();
            if (TableName.Length == 0 || FilterFile.Contains(TableName))
            {
                continue;
            }
            using (StreamWriter sw = new StreamWriter(Path.Combine(dbPath, TableName)))
            {
                DataTable dataTable = CommClass.GetDataTable(string.Concat("select * from ", TableName));
                if (dataTable.Rows.Count > 0)
                {
                    StringBuilder ColumnsName = new StringBuilder();
                    int ColumnCount = dataTable.Columns.Count;
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        ColumnsName.Append("," + dataTable.Columns[i].ColumnName);
                    }
                    sw.WriteLine(ColumnsName.ToString());
                    foreach (DataRow data in dataTable.Rows)
                    {
                        for (int i = 0; i < ColumnCount; i++)
                        {
                            string d = data[i].ToString();
                            if (d.Length == 0)
                            {
                                sw.Write(",null");
                            }
                            else
                            {
                                sw.Write(",'");
                                sw.Write(d.Replace("\r\n", "{$rnrn$}").Replace("\"", "”").Replace("'", "’"));
                                sw.Write("'");
                            }
                        }
                        sw.WriteLine();
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
        }
    }

    /// <summary>
    /// 导入备份数据
    /// </summary>
    /// <param name="dbPath"></param>
    private static void ImportData(string dbPath)
    {
        CommClass.ExecuteSQL(string.Format("DROP DATABASE IF EXISTS {0}{1};CREATE DATABASE {0}{1}", SysConfigs.DbPrefix, UserInfo.AccountID));
        CommClass.ExecuteSqlInFile(SysConfigs.GetAppDataFilePath("cw_accountdb_mysql.sql"));
        DirectoryInfo OutputDataDir = new DirectoryInfo(dbPath);
        StringBuilder SQLString = new StringBuilder();
        foreach (FileInfo tableFile in OutputDataDir.GetFiles())
        {
            string[] DataList = File.ReadAllLines(tableFile.FullName);
            if (DataList.Length > 1)
            {
                int InsertCounts = 0;
                string insertHead = string.Format("insert into {0}({1})values", tableFile.Name, DataList[0].Substring(1));
                SQLString.Append(insertHead);
                for (int i = 1; i < DataList.Length; i++)
                {
                    if (DataList[i].Length == 0) { continue; }
                    InsertCounts++;
                    SQLString.AppendFormat("({0}),", DataList[i].Substring(1).Replace("{$rnrn$}", "\r\n"));
                    if (InsertCounts > 200)
                    {
                        SQLString.Remove(SQLString.Length - 1, 1);
                        CommClass.ExecuteSQL(SQLString.ToString());
                        SQLString.Length = 0;
                        SQLString.Append(insertHead);
                        InsertCounts = 0;
                    }
                }
                if (SQLString.ToString().EndsWith(","))
                {
                    SQLString.Remove(SQLString.Length - 1, 1);
                    CommClass.ExecuteSQL(SQLString.ToString());
                }
                SQLString.Length = 0;
            }
        }
        CommClass.ExecuteSQL("update cw_assetcard set CDate=null where CDate='0000-00-00 00:00:00'");
    }

    public static void RepairAlarmVoucher()
    {
        StringBuilder vid0 = new StringBuilder();
        StringBuilder vid1 = new StringBuilder();
        DataTable vouchers = MainClass.GetDataTable(string.Concat("select VoucherID,DoState from cw_balancealarm where AccountID='", UserInfo.AccountID, "' and AlarmType='0'"));
        foreach (DataRow row in vouchers.Rows)
        {
            if (row["DoState"].ToString() == "1")
            {
                vid1.AppendFormat("'{0}',", row["VoucherID"].ToString());
            }
            else
            {
                vid0.AppendFormat("'{0}',", row["VoucherID"].ToString());
            }
        }
        CommClass.ExecuteSQL(string.Format("update cw_voucher set IsHasAlarm='0' where id not in ({0}{1}'')", vid0.ToString(), vid1.ToString()));
        if (vid0.Length > 0)
        {
            CommClass.ExecuteSQL(string.Format("update cw_voucher set IsAuditing='0' where id in ({0}'')", vid0.ToString()));
        }
        if (vid1.Length > 0)
        {
            CommClass.ExecuteSQL(string.Format("update cw_voucher set IsAuditing='1' where id in ({0}'')", vid1.ToString()));
        }
    }

    /// <summary>
    /// 修复数据库索引表
    /// </summary>
    /// <param name="DbName"></param>
    public static void MySQL_CheckDB(string DbName)
    {
        CreateProcess(Path.Combine(DefConfigs.MySQL_BaseDir, "bin/mysqlcheck.exe"), string.Format("-r -s -uroot -p{0} {1}", SysConfigs.SaPassword, DbName));
    }

    /// 函数名称：CreateProcess
    /// 函数作者：朱坤堂
    /// 创建时间：2008-10-25
    /// <summary>
    /// 创建新进程执行程序外运行
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="arguments"></param>
    public static void CreateProcess(string fileName, string arguments)
    {
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.FileName = fileName;
        p.StartInfo.Arguments = arguments;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = false;
        p.StartInfo.RedirectStandardOutput = false;
        p.StartInfo.RedirectStandardError = false;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        p.WaitForExit();
        p.Close();
    }
}
