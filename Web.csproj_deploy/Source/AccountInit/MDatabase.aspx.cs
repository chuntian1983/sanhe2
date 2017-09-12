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

public partial class AccountInit_MDatabase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            InitTableList();
            InitWebControl();
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            Button3.Attributes.Add("onclick", "return CheckUpload()");
            Button2.Attributes.Add("onclick", "return ShowConfirm('您确定需要完全备份数据吗？','完全备份数据')");
            Button1.Attributes.Add("onclick", "return ShowConfirm('您确定需要备份选择的数据表吗？','备份选择的数据表')");
            //检测是否可继续进行操作
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            if (PageClass.CheckAccountUsed(true))
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
                Button3.Enabled = false;
                Lightbox.InnerHtml = "<br />已有用户正在使用，请30秒后再尝试刷新！";
                PageClass.ExcuteScript(this.Page, "LimitControl();");
            }
            else
            {
                PageClass.ExcuteScript(this.Page, "RefreshD();");
            }
        }
    }

    protected void InitTableList()
    {
        TableList.Items.Add(new ListItem("科目表相关", "CW_Subject,CW_SubjectData,CW_SubjectGroup,cw_subjectbudget,cw_subjectrec"));
        TableList.Items.Add(new ListItem("凭证相关", "CW_Voucher,CW_Entry,CW_EntryData"));
        TableList.Items.Add(new ListItem("科目余额表", "CW_SubjectSum,cw_amountsum,cw_lastmonthsum"));
        TableList.Items.Add(new ListItem("汇总报表相关", "CW_ReportDesign,CW_ReportRowItem"));
        TableList.Items.Add(new ListItem("自定义报表", "CW_DefineReport,CW_DefineRowItem"));
        TableList.Items.Add(new ListItem("摘要库", "CW_Summary"));
        TableList.Items.Add(new ListItem("系统相关", "CW_Syspara,cw_logs,cw_department"));
        TableList.Items.Add(new ListItem("资产相关", "CW_AssetCard,CW_Assetda,CW_AssetClass,CW_Ditype"));
        TableList.Items.Add(new ListItem("资源相关", "CW_ResCard,cw_resclass,cw_resleasecard,cw_respayperiod"));
        for (int i = 0; i < TableList.Items.Count; i++)
        {
            TableList.Items[i].Selected = true;
        }
    }

    protected void InitWebControl()
    {
        DataSet ds = CommClass.GetDataSet("select * from cw_backupdb order by backupdate desc");
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
            string BackupPath = Server.MapPath("../BackupDB") + "\\" + btn.CommandArgument;
            DirectoryInfo directory = new DirectoryInfo(BackupPath);
            if (directory.Exists)
            {
                directory.Delete(true);
            }
            if (File.Exists(BackupPath + ".zip"))
            {
                File.Delete(BackupPath + ".zip");
            }
            CommClass.ExecuteSQL("delete from cw_backupdb where id='" + btn.CommandArgument + "'");
            //写入操作日志
            CommClass.WriteCTL_Log("100011", "删除数据备份：" + btn.CommandArgument);
            //--
        }
        catch
        {
            throw;
            ExeScript.Text = "<script language=javascript>alert('备份文件删除失败，请手动删除！')</script>";
        }
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "return ShowConfirm('您确定需要删除吗？','删除备份数据')");
            LinkButton btnBackup = (LinkButton)e.Row.FindControl("LinkButton1");
            if (Directory.Exists(Server.MapPath(string.Format("../BackupDB/{0}", e.Row.Cells[0].Text))))
            {
                btnBackup.Attributes.Add("onclick", "return ShowConfirm('您确定需要完全还原数据吗？','还原数据')");
            }
            else
            {
                btnBackup.Enabled = false;
            }
            LinkButton btnDown = (LinkButton)e.Row.FindControl("LinkButton2");
            if (File.Exists(Server.MapPath(string.Format("../BackupDB/{0}.zip", e.Row.Cells[0].Text))))
            {
                btnDown.Attributes["onclick"] = string.Format("return ShowZipInfo('{0}')", btnDown.CommandArgument);
            }
            else
            {
                if (btnBackup.Enabled)
                {
                    btnDown.Attributes["onclick"] = "return ShowConfirm('当前备份文件尚未压缩，必须压缩后才可下载。\\n\\n您确定需要压缩备份数据吗？','压缩备份数据')";
                }
                else
                {
                    btnDown.Enabled = false;
                }
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
        CommClass.ExecuteSQL("update cw_backupdb set notes='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("Notes"))).Text.ToString().Trim()
            + "' where id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder BackupTable = new StringBuilder();
        StringBuilder BackupName = new StringBuilder();
        foreach (ListItem li in TableList.Items)
        {
            if (li.Selected)
            {
                BackupTable.Append(li.Value + ",");
                BackupName.Append(li.Text + ",");
            }
        }
        if (MySQLClass.DbBackup(BackupName.ToString(), BackupTable.ToString()))
        {
            ExeScript.Text = "<script language=javascript>alert('数据表备份成功！')</script>";
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('数据表备份失败！')</script>";
        }
        InitWebControl();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (MySQLClass.BackupAllTable(Notes.Text))
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份成功！')</script>";
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份失败！')</script>";
        }
        Notes.Text = "";
        InitWebControl();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        if (MySQLClass.DbRestore(btn.CommandArgument))
        {
            string UADate = string.Empty;
            DateTime ADate = MainClass.GetAccountDate();
            if (ADate.Year == 1900)
            {
                Session["isStartAccount"] = null;
                UADate = "('ctl00_LeftFrame1_CurAccountDate').innerText='财务日期：" + DateTime.Now.ToString("yyyy年MM月dd日") + "';";
            }
            else
            {
                Session["isStartAccount"] = "";
                UADate = "('ctl00_LeftFrame1_CurAccountDate').innerText='财务日期：" + ADate.ToString("yyyy年MM月dd日") + "';";
            }
            ExeScript.Text = "<script language=javascript>parent.document.getElementById" + UADate + "alert('数据完全还原成功！');</script>";
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全还原失败！');</script>";
        }
        InitWebControl();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton lbn = (LinkButton)sender;
        string BackupDBPath = AppDomain.CurrentDomain.BaseDirectory + "BackupDB\\";
        PageClass.DoZipSingleFile(BackupDBPath + lbn.CommandArgument, BackupDBPath + lbn.CommandArgument + ".zip");
        ExeScript.Text = "<script language=javascript>ShowZipInfo('" + lbn.CommandArgument + "')</script>";
        InitWebControl();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string fileName = string.Empty;
        string DbName = SysConfigs.DbPrefix + UserInfo.AccountID;
        string uploadDir = HttpContext.Current.Server.MapPath("../BackupDB/");
        string tempDir = Path.Combine(uploadDir, Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        HttpPostedFile file = FileUpload1.PostedFile;
        if (file.ContentLength > 0)
        {
            fileName = Path.Combine(tempDir, "upload" + Path.GetExtension(file.FileName));
            file.SaveAs(fileName);
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('未接收到上传数据！');</script>";
            return;
        }
        if (fileName.Length != 0)
        {
            try
            {
                PageClass.DeZipDirectory(fileName, tempDir);
                foreach (string dir in Directory.GetDirectories(tempDir))
                {
                    string BackupID = DbName + DateTime.Now.ToString("yyyyMMddHHmmss");
                    PageClass.CopyDirectory(dir, Path.Combine(uploadDir, BackupID));
                    CommClass.ExecuteSQL("insert cw_backupdb(id,backuppath,notes,backupdate)values('"
                        + BackupID + "','" + BackupID + "','上传备份编号：" + dir.Substring(dir.LastIndexOf("\\") + 1) + "','" + DateTime.Now.ToString() + "')");
                    //写入操作日志
                    CommClass.WriteCTL_Log("100011", "上传备份编号：" + BackupID);
                    //--
                }
                ExeScript.Text = "<script language=javascript>alert('数据上传成功！');</script>";
            }
            catch (Exception ex)
            {
                if (ex.Message == "Invalid password")
                {
                    ExeScript.Text = "<script language=javascript>alert('您上传的数据不是本账套数据！');</script>";
                }
                else
                {
                    ExeScript.Text = "<script language=javascript>alert('数据上传失败！');</script>";
                }
            }
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('未接收到上传数据！');</script>";
        }
        PageClass.DelDirectory(tempDir);
        InitWebControl();
    }
}
