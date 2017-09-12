using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace SanZi.Web
{
    public partial class ScanPiBankaBarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ScanPiBankaBarcode)); //注册ajaxPro,括号中的参数是当前的类名
        }

        [AjaxPro.AjaxMethod] //申明是ajaxPro方法
        public string searchUserInfo(string barCode)
        {
            StringBuilder sbReturn = new StringBuilder();//返回信息
            Regex r = new Regex(@"\s+\'");
            string strBarCode = r.Replace(barCode,"");//barCode.Replace("'","")
            if (strBarCode == "")
            {
                sbReturn.Append("error|请扫描条形码！");
            }
            else
            {
                SanZi.BLL.Users bll = new SanZi.BLL.Users();
                //bool ifExist = bll.ExistsByBarCode(barCode);
                string uid = CommClass.GetTableValue("cw_barcode", "UserID", "barcode='" + barCode + "' and usestate='0'", "");
                if (uid.Length > 0)
                {
                    DataTable dt = bll.GetUserInfoByBarcode(uid).Tables[0];
                    if (dt.Rows.Count == 1)
                    {
                        sbReturn.Append("ok|" + dt.Rows[0]["TrueName"] + "|" + dt.Rows[0]["UserID"]);
                        sbReturn.Append("|" + dt.Rows[0]["TitleName"] + "|" + dt.Rows[0]["DeptName"]);
                        sbReturn.Append("|" + barCode);
                    }
                    else
                    {
                        sbReturn.Append("error|没有找到相关权利人信息！");
                    }
                }
                else
                {
                    sbReturn.Append("error|该条形码不存在或已被使用！");
                }
            }
            return sbReturn.ToString();
        }

        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
    }
}
