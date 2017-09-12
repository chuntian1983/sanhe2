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
using System.IO;

namespace SanZi.Web.view.GongKai
{
    public partial class addlb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBind();
                lbssdw.Text = UserInfo.AccountName;
            }
        }
        protected void GetBind()
        {
            string strsql = "select * from cwgk_lbb";
            DataSet ds = MainClass.GetDataSet(strsql);
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string gcid = e.CommandArgument.ToString();
            if (e.CommandName == "Del")
            {
                string strsql = "delete from cwgk_lbb where id='" + gcid + "'";
                MainClass.ExecuteSQL(strsql);
                GetBind();
            }

        }
        //上传公开样表
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string path = null;
            string fileforename = null;
            string filename = string.Empty;
            if (this.FileUpload1.HasFile == true)
            {
                //文件上传
                int filesize = this.FileUpload1.PostedFile.ContentLength;
                if (filesize > 1024 * 1024)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "cc", "<script language='javascript'>alert('你上传的文件大于1MB！')</script>");
                }
                else
                {
                    string[] filetypes = { "application/msword", "doc" };
                    //string[] filetypes ={ ".bmp", ".jpg", ".gif",".iso",".jpeg",".png" };
                    bool isPic = true;
                    string filetype = this.FileUpload1.PostedFile.ContentType;
                    //string filetype = Path.GetExtension(this.FileUpload1.FileName).ToLower();
                    for (int i = 0; i < filetypes.Length; i++)
                    {
                        if (filetype == filetypes[i])
                        {
                            isPic = true;
                            break;
                        }
                        else
                        {
                            isPic = false;
                        }
                    }
                    if (isPic) //上传的是jpg文件
                    {
                        //执行上传
                        path = Server.MapPath("../gongkai");
                        try
                        {
                            fileforename = DateTime.Now.Ticks.ToString();
                            filename=fileforename+ Path.GetExtension(this.FileUpload1.FileName);
                            this.FileUpload1.SaveAs(path +"\\"+ fileforename + Path.GetExtension(this.FileUpload1.FileName));
                            
                            //this.Label1.Text = path + fileforename + Path.GetExtension(this.FileUpload1.FileName);

                            string strsql = "insert into cwgk_lbb(name,filename,lbid,creattime) values('"+txtname.Text.Trim()+"','"+filename+"','"+ddllbmc.Text+"','"+System.DateTime.Now.ToString()+"')";
                            MainClass.ExecuteSQL(strsql);
                            //CommClass.ExecuteSQL(strsql);
                            PageClass.ShowAlertMsg(this.Page, "文件上传成功！");
                        }
                        catch
                        {
                            PageClass.ShowAlertMsg(this.Page, "因某种网络原因，文件上传失败！");
                        }
                    }
                    else
                    {
                        PageClass.ShowAlertMsg(this.Page, "请选择word文件上传！");
                    }
                }
            }
            else
            {
                PageClass.ShowAlertMsg(this.Page, "请选择上传的文件！");
            }
            GetBind();
        }
    }
}
