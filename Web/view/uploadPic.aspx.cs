using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace SanZi.Web
{
    public partial class uploadPic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnCheck.Enabled = false;
            this.lblResult.Text = "";
            AjaxPro.Utility.RegisterTypeForAjax(typeof(uploadPic)); //注册ajaxPro,括号中的参数是当前的类名
        }

        [AjaxPro.AjaxMethod] //申明是ajaxPro方法
        public string checkPhoto()
        {
            string strFileName = this.FileUpload1.PostedFile.FileName.Trim();
            return "ok|" + strFileName;
            string strResult = string.Empty;
            if (strFileName == "")
            {
                strResult = "error|请选择要识别的票据图片！";
            }
            else
            {
                string strUploadType = ",jpg,jpeg,bmp,gif,png,psd,";
                int intLastIndex = strFileName.LastIndexOf(".");//取得文件名中最后一个"."的索引
                string strExName = strFileName.Substring(intLastIndex).Trim();//上传附件扩展名
                return "ok|" + strFileName;
                if (intLastIndex < strFileName.Length)
                {
                    if (strUploadType.IndexOf("," + strFileName.Substring(intLastIndex + 1).ToLower().Trim() + ",") == -1)
                    {
                        strResult = "error|请选择图片文件！";
                    }
                    else
                    {
                        //string strNewFileName = System.Guid.NewGuid().ToString() + "_" + this.FileUpload1.PostedFile.ContentLength.ToString() + strExName;
                        //this.FileUpload1.PostedFile.SaveAs(Server.MapPath("Images/PiaoJu/" + strNewFileName));
                        //if (checkPhoto(strNewFileName))
                        //{
                        strResult = "ok|恭喜，票据符合要求！";
                        //}
                        //else
                        //{
                        //    strResult = "error|对不起，票据图片不符合要求！";
                        //}
                    }

                }
            }

            return strResult;
        }

        protected void btnCheckPic_Click(object sender, EventArgs e)
        {
            string strFileName = this.FileUpload1.PostedFile.FileName.Trim();
            if (strFileName == "")
            {
                this.lblResult.Text = "请选择要识别的票据图片！";
            }
            else
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
                        this.lblResult.Text = "恭喜，票据符合要求！";
                        this.btnCheck.Enabled = true;
                        this.btnCheckPic.Enabled = false;
                        this.HiddenField3.Value = strNewFileName;
                        //if (checkPhoto(strNewFileName))
                        //{
                        //    this.lblResult.Text = "恭喜，票据符合要求！";
                        //    this.btnCheck.Enabled = true;
                        //    this.HiddenField3.Value = strNewFileName;
                        //    //this.page.ClientScript.RegisterStartupScript(this.page, "message", "<script language='javascript' defer>alert('票据符合要求！');parentWindow.document.getElementById('hidCheckPhoto').value=1;</script>");
                        //}
                        //else
                        //{
                        //    this.lblResult.Text = "恭喜，票据符合要求！";
                        //    this.btnCheck.Enabled = true;
                        //    this.HiddenField3.Value = strNewFileName;
                        //    //this.lblResult.Text = "对不起，票据图片不符合要求！";
                        //}
                    }
                }
            }
        }

        public bool checkPhoto(string fileName)
        {
           
            string picAddr = Server.MapPath("~/view/Images/" + fileName).ToString();
            string filename = System.IO.Path.GetFileName(picAddr);//文件名 
            string extension = System.IO.Path.GetExtension(picAddr);//扩展名 
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(picAddr);// 没有扩展名的文件名 “Default”

            int picWidth = 0;//图片长度
            int picHeight = 0;//图片高度
            System.Drawing.Image img = System.Drawing.Image.FromFile(picAddr);
            picWidth = img.Width;
            picHeight = img.Height;

            int checkPointNum = 10;//取点数量 取10个点，有3个符合及为合法票据
            int successNum = 0;//符合要求的点数
            int xAddr = 0, yAddr = 0;//坐标位置

            //for (int i = 0; i < checkPointNum; i++)
            //{
            //    xAddr = getRandNumInt(100, picWidth - 100);//像素点坐标值100<x<图片宽度
            //    yAddr = getRandNumInt(140, picHeight - 140);//像素点坐标值140<y<图片高度

            //    using (Bitmap bmp = new Bitmap(picAddr))
            //    {
            //        Color pixelColor = bmp.GetPixel(xAddr, yAddr);
            //        int alpha = pixelColor.A;//像素点颜色的 Alpha 值
            //        int red = pixelColor.R;//颜色的 RED 分量值
            //        int green = pixelColor.G;//颜色的 GREEN 分量值
            //        int blue = pixelColor.B;//颜色的 BLUE 分量值
            //        if (checkPicRGB(red, green, blue))
            //        {
            //            successNum = successNum + 1;
            //        }
            //    }
            //}

            //if (successNum > 3)
            //{
            //    //LTP.Common.MessageBox.Show(this.Page, "票据图片符合要求！");
            //    return true;
            //}
            //else
            //{
            //    //LTP.Common.MessageBox.Show(this.Page, "票据图片不符合要求");
            //    return false;
            //}

            xAddr = picWidth / 2;
            yAddr = 50;
            //Response.Write(yAddr);
            //Response.Write("+"+picHeight);
            int yAddr2 = picHeight / 2;
            while (successNum < 1 && yAddr < 550 && picHeight > 550)
            {
                yAddr = yAddr + 5;
                using (Bitmap bmp = new Bitmap(picAddr))
                {
                    Color pixelColor = bmp.GetPixel(xAddr, yAddr);
                    int alpha = pixelColor.A;//像素点颜色的 Alpha 值
                    int red = pixelColor.R;//颜色的 RED 分量值
                    int green = pixelColor.G;//颜色的 GREEN 分量值
                    int blue = pixelColor.B;//颜色的 BLUE 分量值
                    if (checkPicRGB(red, green, blue))
                    {
                        successNum = successNum + 1;
                    }
                }
            }

            if (successNum > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
          
        }

        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <returns></returns>
        public static int getRandNumInt(int startNum, int endNum)
        {
            int num = 0;
            Random rand = new Random();
            num = rand.Next(startNum, endNum);
            return num;

        }

        /// <summary>
        /// 判断图片颜色
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public bool checkPicRGB(int red, int green, int blue)
        {
            bool retResult = false;
            //票据判断标准 120<red<200  190<green<210  180<blue<210
            //if ((red > 120 && red < 200) && (green > 190 && green < 210) && (blue > 180 && blue < 210))
            if ((red > 10 && red < 190) && (green > 210 && green < 250) && (blue > 210 && blue < 250))
            {
                retResult = true;
            }
            else
            {
                retResult = false;
            }
            return retResult;
        }
        //public static string CreateNewVoucher(string vfalg, string vsummary, Dictionary<string, string> DebitSubject, Dictionary<string, string> CreditSubject)
        //public static string CreateNewVoucher(string vfalg, string vsummary, Dictionary<string, string> DebitSubject, Dictionary<string, string> CreditSubject, bool isAudit, bool isRecord)
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            //string vfalg;//凭证标示，统一定为RY
            //string vsummary;//分录摘要
            //string DebitSubject;//借方科目代码
            //string CreditSubject;//贷方科目代码
            //bool isAudit;//是否设为已审核状态
            //bool isRecord;//是否设为已记账状态

            if (string.IsNullOrEmpty(this.HiddenField1.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(),"dd","<script>alert('请选择借方科目')</script>");
                return;
            }
            if (string.IsNullOrEmpty(this.HiddenField2.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(),"dd","<script>alert('请选择贷方科目')</script>");
                return;
            }
            //调用示例：
            string img = "/view/Images/" + this.HiddenField3.Value;
            string a = this.TextBox1.Text;
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add(HiddenField1.Value, txtDebit.Text);
            Dictionary<string, string> c = new Dictionary<string, string>();
            c.Add(HiddenField2.Value, txtDebit.Text);
            CommOutCall.CreateNewVoucher("RY", a, d, c, img, true, true);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "dds", "<script>alert('生成成功！')</script>");
            this.TextBox1.Text = "";
            this.txtDebit.Text = "";
            this.HiddenField3.Value = "";

            this.btnCheckPic.Enabled = true;
            this.btnCheck.Enabled = false;
        }
    }
}
