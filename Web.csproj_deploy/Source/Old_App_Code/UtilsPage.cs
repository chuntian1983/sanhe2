using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

/// <summary>
/// 页面常用工具集合类
/// </summary>
public class UtilsPage : Page
{
    public UtilsPage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static bool IsNullOrEmpty(string s)
    {
        return (s == null || s.Trim().Length == 0);
    }

    public static string GetThisMonthLastDay(string format)
    {
        return DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString(format);
    }

    public static void SetTextBoxReadOnly(TextBox tbox)
    {
        tbox.BorderWidth = 1;
        tbox.Attributes["readonly"] = "readonly";
        tbox.Style["background-color"] = "#F6F6F6";
    }

    public static void SetTextBoxBorderEffect(TextBox tbox)
    {
        tbox.Style["border"] = "1px solid white;";
        tbox.Attributes["onmouseover"] = "this.style.border='1px solid blue';";
        tbox.Attributes["onmouseout"] = "this.style.border='1px solid white';";
    }

    public static void SetTextBoxCalendar(TextBox tbox)
    {
        SetTextBoxCalendar(tbox, "yyyy-MM-dd");
    }

    public static void SetTextBoxCalendar(TextBox tbox, string formate)
    {
        SetTextBoxReadOnly(tbox);
        if (formate.Length > 0)
        {
            tbox.Text = DateTime.Now.ToString(formate);
        }
        else
        {
            formate = "yyyy-MM-dd";
        }
        tbox.Attributes["onclick"] = string.Concat("popUpCalendar(this,document.forms[0].", tbox.ClientID, ",'", formate.ToLower(), "')");
    }

    public static void SetTextBoxAutoValue(TextBox tbox)
    {
        SetTextBoxAutoValue(tbox, "[自动编号]");
    }

    public static void SetTextBoxAutoValue(TextBox tbox, string defValue)
    {
        if (tbox.Text.Length == 0)
        {
            tbox.Text = defValue;
        }
        tbox.Attributes.Add("onfocus", string.Concat("if(this.value=='", defValue, "')this.value='';"));
        tbox.Attributes.Add("onblur", string.Concat("if(this.value.length==0)this.value='", defValue, "';"));
        if (defValue == "0")
        {
            SetTextBoxOnlyNumber(tbox);
        }
    }

    public static void SetTextBoxAutoValue(TextBox tbox, decimal defValue)
    {
        string dv = defValue.ToString("#0.00");
        if (tbox.Text.Length == 0)
        {
            tbox.Text = dv;
        }
        tbox.Attributes.Add("onfocus", string.Concat("if(eval(this.value)==eval('", dv, "'))this.value='';"));
        tbox.Attributes.Add("onblur", string.Concat("if(this.value.length==0||eval(this.value)==eval('", dv, "'))this.value='", dv, "';"));
        SetTextBoxOnlyNumber(tbox);
    }

    public static void SetTextBoxAutoValue2(TextBox tbox, decimal defValue)
    {
        string dv = defValue.ToString("#0.00");
        tbox.Attributes.Add("onfocus", string.Concat("if(eval(this.value)==eval('", dv, "'))this.value='';"));
        tbox.Attributes.Add("onblur", string.Concat("if(this.value.length==0||eval(this.value)==eval('", dv, "'))this.value='';"));
        SetTextBoxOnlyNumber(tbox);
    }

    public static void SetTextBoxOnlyNumber(TextBox tbox)
    {
        tbox.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57)||(event.keyCode==46&&this.value.indexOf('.')==-1);");
    }

    public static string ConvertTextToHTML(string str)
    {
        return str.Replace(" ", "&nbsp;&nbsp;").Replace("\r\n", "<br>");
    }

    public static void FillTableCell(DataTable dt, HtmlTableCell cell, string condition)
    {
        DataView dv = new DataView(dt, condition, "", DataViewRowState.CurrentRows);
        cell.InnerHtml = dv.Count.ToString();
    }

    public static void InitDropDownList(DropDownList ddl, DataTable dt)
    {
        ddl.DataSource = dt.DefaultView;
        ddl.DataTextField = dt.Columns[1].ColumnName;
        ddl.DataValueField = dt.Columns[0].ColumnName;
        ddl.DataBind();
    }

    /// 函数名称：InitListBox
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 初始选定列表框的项
    /// </summary>
    /// <param name="ddl">列表框控件对象</param>
    /// <param name="selectValue">选择项的值</param>
    public static void InitListBox(ListBox ddl, string selectValue)
    {
        foreach (ListItem li in ddl.Items)
        {
            if (selectValue.IndexOf(li.Value) == -1)
            {
                li.Selected = false;
            }
            else
            {
                li.Selected = true;
            }
        }
    }

    /// 函数名称：InitCheckBoxList
    /// 函数作者：朱坤堂
    /// 创建时间：2008-03-20
    /// <summary>
    /// 初始选定CheckBoxList项
    /// </summary>
    /// <param name="cbl">CheckBoxList对象</param>
    /// <param name="selectValue">选择项的值</param>
    public static void InitCheckBoxList(CheckBoxList cbl, string selectValue)
    {
        foreach (ListItem li in cbl.Items)
        {
            if (selectValue.IndexOf(li.Value) == -1)
            {
                li.Selected = false;
            }
            else
            {
                li.Selected = true;
            }
        }
    }

    /// 函数名称：InitColorControl
    /// 函数作者：朱坤堂
    /// 创建时间：2008-10-25
    /// <summary>
    /// 初始化颜色选择器控件
    /// </summary>
    /// <param name="ddl"></param>
    public static void InitColorControl(DropDownList ddl)
    {
        string s = "EEEEEE FF00CC FF66FF FF00FF DDDDDD FFCCCC FF66CC FF00CC CCCCCC FFCC99 FF6699 FF0099 BBBBBB FFCC66 FF6666 " +
                   "FF0066 AAAAAA FFCC33 FF6633 FF0033 999999 FFCC00 FF6600 FF0000 888888 CCCCFF CC66FF CC00FF 777777 CCCCCC " +
                   "CC66CC CC00CC 666666 CCCC99 CC6699 CC0099 555555 CCCC66 CC6666 CC0066 444444 CCCC33 CC6633 CC0033 333333 " +
                   "CCCC00 CC6600 CC0000 222222 99CCFF 9966FF 9900FF 111111 99CCCC 9966CC 9900CC 000000 99CC99 996699 990099 " +
                   "FF0000 99CC66 996666 990066 EE0000 99CC33 996633 990033 DD0000 99CC00 996600 990000 CC0000 66CCFF 6666FF " +
                   "6600FF BB0000 66CCCC 6666CC 6600CC AA0000 66CC99 666699 660099 990000 66CC66 666666 660066 880000 66CC33 " +
                   "666633 660033 770000 66CC00 666600 660000 660000 33CCFF 3366FF 3300FF 550000 33CCCC 3366CC 3300CC 440000 " +
                   "33CC99 336699 330099 330000 33CC66 336666 330066 220000 33CC33 336633 330033 110000 33CC00 336600 330000 " +
                   "FFFFFF 00CCFF 0066FF 0000FF FFFFCC 00CCCC 0066CC 0000CC FFFF99 00CC99 006699 000099 FFFF66 00CC66 006666 " +
                   "000066 FFFF33 00CC33 006633 000033 FFFF00 00CC00 006600 000000 CCFFFF FF99FF FF33FF 00FF00 CCFFCC FF99CC " +
                   "FF33CC 00EE00 CCFF99 FF9999 FF3399 00DD00 CCFF66 FF9966 FF3366 00CC00 CCFF33 FF9933 FF3333 00BB00 CCFF00 " +
                   "FF9900 FF3300 00AA00 99FFFF CC99FF CC33FF 990099 99FFCC CC99CC CC33CC 008800 99FF99 CC9999 CC3399 007700 " +
                   "99FF66 CC9966 CC3366 006600 99FF33 CC9933 CC3333 005500 99FF00 CC9900 CC3300 004400 66FFFF 9999FF 9933FF " +
                   "003300 66FFCC 9999CC 9933CC 002200 66FF99 999999 993399 001100 66FF66 999966 993366 0000FF 66FF33 999933 " +
                   "993333 0000EE 66FF00 999900 993300 0000DD 33FFFF 6699FF 6633FF 0000CC 33FFCC 6699CC 6633CC 0000BB 33FF99 " +
                   "669999 663399 0000AA 33FF66 669966 663366 000099 33FF33 669933 663333 000088 33FF00 669900 663300 000077 " +
                   "00FFFF 3399FF 3333FF 000066 00FFCC 3399CC 3333CC 000055 00FF99 339999 333399 000044 00FF66 339966 333366 " +
                   "000033 00FF33 339933 333333 000022 00FF00 339900 333300 000011 0099FF 0033FF 0099CC 0033CC 009999 003399 " +
                   "009966 003366 009933 003333 009900 003300";
        string[] c = s.Split(' ');
        bool f = ddl.Items.Count > 0;
        for (int i = 0; i < c.Length; i++)
        {
            if (!f) { ddl.Items.Add(new ListItem("#" + c[i], "#" + c[i])); }
            ddl.Items[i].Attributes.Add("style", "BACKGROUND-COLOR: #" + c[i]);
        }
    }

    public static string UploadFiles()
    {
        try
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request.Files.Count > 0)
            {
                HttpPostedFile file = request.Files[0];
                if (file.ContentLength > 0)
                {
                    string ext = System.IO.Path.GetExtension(file.FileName);
                    string fileName = string.Format("../UploadFile/{0}{1}", string.Format(("{0:yyyyMMddHHmmssfff}"), DateTime.Now), ext);
                    file.SaveAs(HttpContext.Current.Server.MapPath(fileName));
                    return fileName;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }
}
