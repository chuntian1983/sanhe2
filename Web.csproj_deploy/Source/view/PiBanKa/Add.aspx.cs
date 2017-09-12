using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SanZi.Web
{
    public partial class PiBanKaAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HiddenField1.Value = Request.QueryString["a1"].ToString();
                HiddenField2.Value = Request.QueryString["a2"].ToString();
                HiddenField3.Value = Request.QueryString["a3"].ToString();
                HiddenField4.Value = Request.QueryString["a4"].ToString();
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string strFileName = this.FileUpload1.PostedFile.FileName.Trim();
            string subID = "";
            if (strFileName != null && strFileName.LastIndexOf(".") > 0)
            {
                string strUploadType = ",jpg,jpeg,bmp,gif,png,psd,";
                int intLastIndex = strFileName.LastIndexOf(".");//取得文件名中最后一个"."的索引
                string strExName = strFileName.Substring(intLastIndex).Trim();//上传附件扩展名
                if (intLastIndex < strFileName.Length)
                {
                    if (strUploadType.IndexOf("," + strFileName.Substring(intLastIndex + 1).ToLower().Trim() + ",") == -1)
                    {
                        this.lblResult.Text = "请选择图片文件！";
                    }
                    else
                    {
                        string strNewFileName = System.Guid.NewGuid().ToString() + "_" + this.FileUpload1.PostedFile.ContentLength.ToString() + strExName;
                        this.FileUpload1.PostedFile.SaveAs(Server.MapPath("~/view/Images/" + strNewFileName));
                        Dictionary<string, string> d = new Dictionary<string, string>();
                        d.Add(HiddenField2.Value, HiddenField1.Value);
                        Dictionary<string, string> c = new Dictionary<string, string>();
                        c.Add(HiddenField3.Value, HiddenField1.Value);
                        subID = CommOutCall.CreateNewVoucher("RY", HiddenField4.Value, d, c, "/view/Images/" + strNewFileName, true, true);
                    }

                }
            }
            else
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add(HiddenField2.Value, HiddenField1.Value);
                Dictionary<string, string> c = new Dictionary<string, string>();
                c.Add(HiddenField3.Value, HiddenField1.Value);
                subID = CommOutCall.CreateNewVoucher("RY", HiddenField4.Value, d, c, "", true, true);
            }
            SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
            SanZi.Model.PiBanKa model = new SanZi.Model.PiBanKa();
            model.PID = int.Parse(Request.QueryString["a5"].ToString());
            model.subid = subID;
            bll.Add1(model);
            Response.Redirect("Index.aspx");
        }
    }
}
