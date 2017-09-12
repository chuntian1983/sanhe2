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
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.ServiceProcess;

public partial class SysManage_MDatabase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            InitWebControl();
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            Button2.Attributes.Add("onclick", "return BackupConfirm('您确定需要完全备份数据吗？','完全备份数据')");
        }
    }
    protected void InitWebControl()
    {
        DataSet ds = MainClass.GetDataSet("select * from cw_backupdb order by backupdate desc");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
            ddl.Items.Clear();
            for (int i = 0; i < GridView1.PageCount; i++)
            {
                ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
            }
            ddl.SelectedIndex = GridView1.PageIndex;
        }
    }
    protected void JumpPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridView1.PageIndex = Convert.ToInt32(ddl.SelectedValue);
        InitWebControl();
    }
    protected void FirstPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        InitWebControl();
    }
    protected void PreviousPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex -= 1;
            InitWebControl();
        }
    }
    protected void NextPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount)
        {
            GridView1.PageIndex += 1;
            InitWebControl();
        }
    }
    protected void LastPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridView1.PageCount;
        InitWebControl();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        try
        {
            DirectoryInfo directory = new DirectoryInfo(PageClass.PathCombine("../BackupDB", btn.CommandArgument));
            if (directory.Exists) { directory.Delete(true); }
            MainClass.ExecuteSQL("delete from cw_backupdb where id='" + btn.CommandArgument + "'");
        }
        catch
        {
            ExeScript.Text = "<script language=javascript>alert('备份文件删除失败，请手动删除！')</script>";
        }
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "return BackupConfirm('您确定需要删除吗？','删除备份数据')");
            LinkButton btnBackup = (LinkButton)e.Row.FindControl("LinkButton1");
            if (Directory.Exists(PageClass.PathCombine("../BackupDB", e.Row.Cells[0].Text)))
            {
                btnBackup.Attributes.Add("onclick", "return BackupConfirm('您确定需要完全还原数据吗？','还原数据')");
            }
            else
            {
                btnBackup.Enabled = false;
            }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        InitWebControl();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        MainClass.ExecuteSQL("update cw_backupdb set notes='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("Notes"))).Text.ToString().Trim()
            + "' where id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //备份系统数据库
        string BackupID = "MainDB" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        string BackupDir = PageClass.PathCombine("../BackupDB", BackupID);
        try
        {
            if (!Directory.Exists(BackupDir))
            {
                Directory.CreateDirectory(BackupDir);
            }
            DirectoryInfo MainDbDir = new DirectoryInfo(Path.Combine(DefConfigs.MySQL_DataDir, SysConfigs.MainDBName));
            foreach (FileInfo file in MainDbDir.GetFiles())
            {
                file.CopyTo(Path.Combine(BackupDir, file.Name), true);
            }
            MainClass.ExecuteSQL("insert cw_backupdb(id,backuppath,notes,backupdate)values('"
                + BackupID + "','" + BackupID + "','" + Notes.Text + "','" + DateTime.Now.ToString() + "')");
            ExeScript.Text = "<script language=javascript>alert('数据完全备份成功！')</script>";
        }
        catch
        {
            throw;
            ExeScript.Text = "<script language=javascript>alert('数据完全备份失败！')</script>";
        }
        Notes.Text = "";
        InitWebControl();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        //暂时保留备份记录，还原后更新
        DataSet ds = MainClass.GetDataSet("select id,backuppath,notes,backupdate from cw_backupdb");
        //还原系统数据库
        string DataDirPath = Path.Combine(DefConfigs.MySQL_DataDir, SysConfigs.MainDBName);
        try
        {
            DirectoryInfo MainDbDir = new DirectoryInfo(PageClass.PathCombine("../BackupDB", btn.CommandArgument));
            foreach (FileInfo file in MainDbDir.GetFiles())
            {
                if (!file.Name.StartsWith("cw_account", StringComparison.OrdinalIgnoreCase))
                {
                    file.CopyTo(Path.Combine(DataDirPath, file.Name), true);
                }
            }
            ExeScript.Text = "<script language=javascript>alert('数据完全还原成功！');</script>";
        }
        catch
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全还原失败！');</script>";
        }
        //修复数据库索引表
        MySQLClass.MySQL_CheckDB(SysConfigs.MainDBName);
        //写回备份记录，防止覆盖
        MainClass.ExecuteSQL("delete from cw_backupdb");
        StringBuilder sqlString = new StringBuilder();
        sqlString.Append("insert cw_backupdb(id,backuppath,notes,backupdate)values");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            sqlString.Append("('" + row[0].ToString() + "','" + row[1].ToString() + "','" + row[2].ToString() + "','" + row[3].ToString() + "'),");
        }
        if (sqlString.ToString().EndsWith(","))
        {
            sqlString.Remove(sqlString.Length - 1, 1);
            MainClass.ExecuteSQL(sqlString.ToString());
        }
        sqlString.Length = 0;
        //-
        InitWebControl();
    }
}
