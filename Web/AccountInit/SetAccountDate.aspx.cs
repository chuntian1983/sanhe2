using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AccountInit_SetAccountDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            curDate.Attributes["readonly"] = "readonly";
            SetCarry.Attributes["onclick"] = "return _SetCarry();";
            BackupDate.Attributes["onclick"] = "return confirm('您确定需要进行数据完全备份吗？');";
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        DateTime AccountDate = MainClass.GetAccountDate();
        if (AccountDate.Year.ToString() == "1900")
        {
            Response.Clear();
            Response.Write("<script>window.close();alert('当前账套尚未启用，不能设置财务日期！');</script>");
            Response.End();
        }
        curDate.Text = AccountDate.ToString("yyyy年MM月dd日");
        CurSetDate.Value = curDate.Text;
        //创建数据表
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        ((BoundField)GridView1.Columns[0]).DataField = "id";
        for (int k = 0; k < 7; k++)
        {
            dt.Rows.Add(new string[] { "" });
        }
        //绑定表格
        GridView1.DataSource = dt.DefaultView;
        GridView1.DataBind();
        string WeekDay = "日一二三四五六";
        for (int i = 0; i < 7; i++)
        {
            GridView1.Rows[0].Cells[i].Text = WeekDay.Substring(i, 1);
            GridView1.Rows[0].Style["background"] = "#f6f6f6";
        }
        int ShowDay = 1;
        bool StartShowFlag = false;
        int FirstDayWeek = (int)Convert.ToDateTime(AccountDate.ToString("yyyy-MM-01")).DayOfWeek;
        int _DaysInMonth = DateTime.DaysInMonth(AccountDate.Year, AccountDate.Month);
        LastMonthDay.Value = AccountDate.ToString("yyyy年MM月") + _DaysInMonth .ToString("00")+ "日";
        for (int i = 1; i < 7; i++)
        {
            for (int k = 0; k < 7; k++)
            {
                if (ShowDay > _DaysInMonth)
                {
                    break;
                }
                if (FirstDayWeek != k && !StartShowFlag)
                {
                    continue;
                }
                else
                {
                    StartShowFlag = true;
                }
                GridView1.Rows[i].Cells[k].Text = ShowDay.ToString("00");
                GridView1.Rows[i].Cells[k].Attributes["onclick"] = "OnCellClick('" + GridView1.Rows[i].Cells[k].ClientID + "','" + ShowDay.ToString("00") + "')";
                GridView1.Rows[i].Cells[k].Attributes["onmouseover"] = "this.className='mouseover2';";
                GridView1.Rows[i].Cells[k].Attributes["onmouseout"] = "this.className='';";
                if (ShowDay == AccountDate.Day)
                {
                    curRowID.Value = GridView1.Rows[i].Cells[k].ClientID;
                    GridView1.Rows[i].Cells[k].Attributes["style"] = "background:red";
                }
                ShowDay++;
            }
        }
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CurSetDate.Value = curDate.Text;
        MainClass.ExecuteSQL("update cw_account set AccountDate='" + curDate.Text + "' where id='" + UserInfo.AccountID + "'");
        ExeScript.Text = "<script language=javascript>alert('财务日期设置成功！');</script>";
        InitWebControl();
        //写入操作日志
        CommClass.WriteCTL_Log("100003", "设置财务日期：" + curDate.Text);
        //--
    }
    protected void SetCarry_Click(object sender, EventArgs e)
    {
        CurSetDate.Value = curDate.Text;
        MainClass.ExecuteSQL("update cw_account set AccountDate='" + curDate.Text + "' where id='" + UserInfo.AccountID + "'");
        ExeScript.Text = "<script>$('IsMonthCarry').value='1';window.close();</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100003", "设置财务日期：" + curDate.Text);
        //--
    }
    protected void BackupDate_Click(object sender, EventArgs e)
    {
        if (MySQLClass.BackupAllTable("设置财务日期备份"))
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份成功！');</script>";
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份失败！');</script>";
        }
        InitWebControl();
    }
}
