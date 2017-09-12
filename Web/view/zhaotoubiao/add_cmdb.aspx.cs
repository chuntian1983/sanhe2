using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;
using LTP.Common;
using Maticsoft.DBUtility;
using System.Data;
using System.IO;

namespace SanZi.Web.view.zhaotoubiao
{
    
    public partial class add_cmdb : System.Web.UI.Page
    {
       
        
        public Hashtable hs
        {
            get { return (Hashtable)ViewState["hs"]; }
            set { ViewState["hs"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    this.Label1.Text = Public.AccountName;
                    // SFUPC();
                    #region 标题
                    if (Request.QueryString["zt"]!=null)
                    {
                        Binddll();
                        switch (Request.QueryString["zt"].ToString())
                        {
                            case "1":
                                lbtitle.Text = "村民代表会议记录";
                                break;
                            case "2":
                                lbtitle.Text = "招标项目申请";
                                break;
                            case "3":
                                lbtitle.Text = "招标项目审批";
                                break;
                            case "4":
                                lbtitle.Text = "预算书录入";
                                break;
                            case "5":
                                lbtitle.Text = "招标文件录入";
                                break;
                            case "6":
                                lbtitle.Text = "投标公告";
                                break;
                            case "7":
                                lbtitle.Text = "参投登记";
                                break;
                            case "8":
                                lbtitle.Text = "竞价招投标";
                                break;
                            case "9":
                                lbtitle.Text = "中标公告";
                                break;
                            case "10":
                                lbtitle.Text = "签订合同";
                                break;
                        }
                    }
                    #endregion
                }
                else
                {

                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }

        }
        protected void Binddll()
        {
            if (Request.QueryString["zt"] != null)
            {
                string strzid = Request.QueryString["zt"];
                if (strzid == "1")
                {
                    this.DropDownList1.Visible = false;
                }
                else
                {
                    this.txtProjectName.Visible = false;
                    this.DropDownList1.Visible = true;
                    string strsql = "select * from cw_ztbxm where xmacount='" + HttpContext.Current.Session["AccountID"] + "'";
                    DataTable dt = MainClass.GetDataTable(strsql);
                    this.DropDownList1.DataSource = dt;
                    this.DropDownList1.DataTextField = "xmname";
                    this.DropDownList1.DataValueField = "id";
                    this.DropDownList1.DataBind();
                }
            }
        }
        #region 该方法是将页面中的上传文件的控件保存到
         
        private void SFUPC()
        {
            //声明一个ArrayList用于存放上传文件的控件
           ArrayList AL = new ArrayList();
           // Hashtable AL = new Hashtable();
            foreach (Control C in this.Tab_UpDownFile.Controls)
            {
                if (C.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlTableRow")
                {
                    HtmlTableCell HTC = (HtmlTableCell)C.Controls[0];
                    foreach (Control FUC in HTC.Controls)
                    {
                        if (FUC.GetType().ToString() == "System.Web.UI.WebControls.FileUpload")
                        {
                            fileuploaduser FU = (fileuploaduser)FUC;
                           // FileUpload FU = (FileUpload)FUC;
                            //FU.BorderColor = System.Drawing.Color.DimGray;
                            //FU.BorderWidth = 1;
                            AL.Add(FU);
                        }
                    }
                }
            }
            if (hs==null)
            {
                hs = new Hashtable();
            }
            hs.Add("FilesControls", AL);
           // Session["FilesControls"]= AL;
        }
        #endregion
        #region 该方法用于添加一个上传文件的控件
        private void InsertC()
        {
            //实例化一个ArrayList
            //ArrayList AL = new ArrayList();
            Hashtable AL = new Hashtable();
            //清除表里的所有行
         
            //获得Session里存放的上传文件的控件
           // GetInfo();
            //实例化表格的行
            if (this.TextBox1.Text.Length > 0)
            {
                try
                {
                    int shu = int.Parse(this.TextBox1.Text.Trim());
                    this.Tab_UpDownFile.Rows.Clear();
                    for (int i = 0; i < shu; i++)
                    {
                        HtmlTableRow HTR = new HtmlTableRow();
                        //实例化表格的列
                        HtmlTableCell HTC = new HtmlTableCell();
                        //向列里添加上传控件
                        HTC.Controls.Add(new FileUpload());
                        HTR.Controls.Add(HTC);
                        this.Tab_UpDownFile.Rows.Add(HTR);
                    }
                }
                catch
                {
                    MessageBox.Show(this, "文件个数只能输入数字！");
                    return;
                }
            }
           
           // SFUPC();
        }
        #endregion
        #region 该方法将上传控件集添加的表格中
        private void GetInfo()
        {
            ArrayList AL = new ArrayList();
          //  Hashtable AL = new Hashtable();
            if (hs["FilesControls"] != null)
            {
                AL = (ArrayList)hs["FilesControls"];
                foreach (fileuploaduser FU in AL)
                {
                    
               
                    HtmlTableRow HTR = new HtmlTableRow();
                    HtmlTableCell HTC = new HtmlTableCell();
                    
                    HTC.Controls.Add(FU);
                    HTR.Controls.Add(HTC);
                    this.Tab_UpDownFile.Rows.Add(HTR);
                }
            }
        }
        #endregion
        #region 该方法用于执行文件上传操作
        private void UpFile(string zid,string zt)
        {
            //获取文件夹路径
            string filepath = Server.MapPath("../../") + "UploadFile\\ztb";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
               // File.Create(filepath);
            }
            //获取客服端上载文件的集合
            HttpFileCollection HFC = Request.Files;
            for (int i = 0; i < HFC.Count; i++)
            {
                HttpPostedFile UserHPF = (HttpPostedFile)HFC[i];
                try
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        string kzm = System.IO.Path.GetExtension(UserHPF.FileName).ToLower();
                        if (kzm == ".jpg" || kzm == ".gif" || kzm == ".jpeg" || kzm == ".png")
                        {


                            string strsfile = Guid.NewGuid().ToString("N") + System.IO.Path.GetExtension(UserHPF.FileName);
                            string strsname = System.IO.Path.GetFileName(UserHPF.FileName).Substring(0, System.IO.Path.GetFileName(UserHPF.FileName).LastIndexOf('.'));
                            UserHPF.SaveAs(filepath + "\\" + strsfile);
                            string strsql = "insert into cw_ztbfj(zbid,xmpath,xmname,xmzt,xmcjsj) values('" + zid + "','" + strsfile + "','" + strsname + "','" + zt + "','" + System.DateTime.Now.ToString() + "')";
                            MainClass.ExecuteSQL(strsql);
                        }
                        else
                        {
                            MessageBox.Show(this, "只能上传图片！");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(this, "上传失败！");
             
                }

            }
            if (Session["FilesControls"] != null)
            {

                Session.Remove("FILEsCOntrols");
                //Session.Remove("FILEsCOntrols");
            }
            MessageBox.Show(this, "上传成功！");
            
        }
        #endregion
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            
            InsertC();
            
        }
        protected void BtnUpFile_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["zt"] != null)
            {
                string zt = Request.QueryString["zt"].ToString();
                if (zt == "1")
                {
                    string strsql = "insert into cw_ztbxm(xmname,xmzt,xmacount,xmqueryid,xmcjsj) values('" + this.txtProjectName.Text + "','" + zt + "','" + HttpContext.Current.Session["AccountID"] + "','','" + System.DateTime.Now.ToString() + "');SELECT LAST_INSERT_ID()";
                    DataTable dt = MainClass.GetDataTable(strsql);
                    if (dt.Rows.Count > 0)
                    {
                        string zid = dt.Rows[0][0].ToString();
                        UpFile(zid, zt);
                    }

                }
                else
                {
                    UpFile(this.DropDownList1.SelectedValue, zt);
                }
            }
            else
            {
                string strsql = "insert into cw_ztbxm(xmname,xmzt,xmacount,xmqueryid,xmcjsj) values('" + this.txtProjectName.Text + "','1','" + HttpContext.Current.Session["AccountID"] + "','','" + System.DateTime.Now.ToString() + "');SELECT LAST_INSERT_ID()";
                DataTable dt = MainClass.GetDataTable(strsql);
                if (dt.Rows.Count > 0)
                {
                    string zid = dt.Rows[0][0].ToString();
                    UpFile(zid, "1");
                }
            }
            
          
        }

    }



}