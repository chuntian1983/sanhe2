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
using System.IO;

/// <summary>
/// 全局参数变量
/// </summary>
public class SysConfigs
{
    public SysConfigs()
    {
        //--
    }

    public static string baseDirectory
    {
        get { return AppDomain.CurrentDomain.BaseDirectory; }
    }

    public static string GetAppDataFilePath(string fileName)
    {
        return string.Format("{0}App_Data\\{1}", baseDirectory, fileName);
    }

    /// 函数名称：GetValueFromWebConfig
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 获取软件配置参数[Web.Config]
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetValueFromWebConfig(string key)
    {
        string value = ConfigurationManager.AppSettings[key];
        if (value == null)
        {
            return "";
        }
        else
        {
            return value;
        }
    }

    /// <summary>
    /// 获取科目长度参数
    /// </summary>
    /// <returns></returns>
    public static string[] GetSubjectLevel()
    {
        string[] SubjectLevel = SysConfigs.SubjectLevel.Split(',');
        if (SubjectLevel.Length <= 1)
        {
            PageClass.UrlRedirect("科目长度限制参数不正确！", 3);
        }
        return SubjectLevel;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //web.config配置参数
    ///////////////////////////////////////////////////////////////////////////////////////////////

    public static string DefaultPageUrl
    {
        get { return GetValueFromWebConfig("DefaultPageUrl"); }
    }

    public static string ErrShowPageUrl
    {
        get { return GetValueFromWebConfig("ErrShowPageUrl"); }
    }

    public static string SaConnection
    {
        get { return GetValueFromWebConfig("SaConnection"); }
    }

    public static string DbConnection
    {
        get { return GetValueFromWebConfig("DbConnection"); }
    }

    public static string ConnectionTemplate
    {
        get { return GetValueFromWebConfig("ConnectionTemplate"); }
    }

    public static string DbServer
    {
        get { return GetValueFromWebConfig("DbServer"); }
    }

    public static string SaPassword
    {
        get { return GetValueFromWebConfig("SaPassword"); }
    }

    public static string UID
    {
        get { return GetValueFromWebConfig("UID"); }
    }

    public static string PWD
    {
        get { return GetValueFromWebConfig("PWD"); }
    }

    public static string MainDBName
    {
        get { return GetValueFromWebConfig("MainDBName"); }
    }

    public static string DbPrefix
    {
        get { return GetValueFromWebConfig("DbPrefix"); }
    }

    public static string SubjectLevel
    {
        get { return GetValueFromWebConfig("SubjectLevel"); }
    }

    public static string AccReveivable
    {
        get { return GetValueFromWebConfig("AccReveivable"); }
    }

    public static string AccPayable
    {
        get { return GetValueFromWebConfig("AccPayable"); }
    }

    public static string InternalDemand
    {
        get { return GetValueFromWebConfig("InternalDemand"); }
    }

    public static string GroupingSubject
    {
        get { return GetValueFromWebConfig("GroupingSubject"); }
    }

    public static string FixedAssetSubject
    {
        get { return GetValueFromWebConfig("FixedAssetSubject"); }
    }

    public static string MergeVoucherSLevel
    {
        get { return GetValueFromWebConfig("MergeVoucherSLevel"); }
    }

    public static string AccumulatedDeprSubject
    {
        get { return GetValueFromWebConfig("AccumulatedDeprSubject"); }
    }

    public static string WelfarismSubject
    {
        get { return GetValueFromWebConfig("WelfarismSubject"); }
    }

    public static string WelfarismIncrease
    {
        get { return GetValueFromWebConfig("WelfarismIncrease"); }
    }

    public static string YearProfitSubject
    {
        get { return GetValueFromWebConfig("YearProfitSubject"); }
    }

    public static string UndistributedProfit
    {
        get { return GetValueFromWebConfig("UndistributedProfit"); }
    }

    public static string ManageSubject
    {
        get { return GetValueFromWebConfig("ManageSubject"); }
    }

    public static string MonthDeprSubject
    {
        get { return GetValueFromWebConfig("MonthDeprSubject"); }
    }

    public static string IncomeSubject
    {
        get { return GetValueFromWebConfig("IncomeSubject"); }
    }

    public static string ExpenseSubject
    {
        get { return GetValueFromWebConfig("ExpenseSubject"); }
    }

    public static string DebitSubject
    {
        get { return GetValueFromWebConfig("DebitSubject"); }
    }

    public static string CreditSubject
    {
        get { return GetValueFromWebConfig("CreditSubject"); }
    }

    public static string SBR_ShowType
    {
        get { return GetValueFromWebConfig("SBR_ShowType"); }
    }

    public static string NotDeleteSuject
    {
        get { return GetValueFromWebConfig("NotDeleteSuject"); }
    }

    public static string LockYearCarry
    {
        get { return GetValueFromWebConfig("LockYearCarry"); }
    }

    public static string LockFirstSubjectPower
    {
        get { return GetValueFromWebConfig("LockFirstSubjectPower"); }
    }

    public static string OnBackupUpdateTableStruct
    {
        get { return GetValueFromWebConfig("OnBackupUpdateTableStruct"); }
    }

    public static string AutoBackupData
    {
        get { return GetValueFromWebConfig("AutoBackupData"); }
    }

    public static string CheckFixedAssetModel
    {
        get { return GetValueFromWebConfig("CheckFixedAssetModel"); }
    }

    public static string AutoCreateAssetCard
    {
        get { return GetValueFromWebConfig("AutoCreateAssetCard"); }
    }

    public static string LastSystemUpdateDate
    {
        get { return GetValueFromWebConfig("LastSystemUpdateDate"); }
    }

    public static string ImportTables
    {
        get { return GetValueFromWebConfig("ImportTables"); }
    }

    public static string ForbidFolders
    {
        get { return GetValueFromWebConfig("ForbidFolders"); }
    }

    public static string ForbidExtensions
    {
        get { return GetValueFromWebConfig("ForbidExtensions"); }
    }
}
