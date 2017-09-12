using System;
using System.Data;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Web;

namespace SanZi.Web.Users
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtDeptName.Text = Public.AccountName;
                initForm();
            }
        }

        #region
        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void initForm()
        {
            this.dplUserTitle.Items.Add(new ListItem("请选择", "0"));
            DataTable dt = MainClass.GetDataTable("select paraname,paratype,paravalue from cw_syspara where paratype='100001'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem lt = new ListItem(dt.Rows[i]["paravalue"].ToString(), dt.Rows[i]["paraname"].ToString());
                this.dplUserTitle.Items.Add(lt);
            }
        }
        #endregion

        protected void btnAddUser_Click(object sender, EventArgs e)
        {

            string strErr = "";
            string DeptName = this.txtDeptName.Text.Trim();//单位名称
            int DeptID = int.Parse(Request.Form["hidDeptID"].ToString());//单位名称ID
            string UserTitleName = this.dplUserTitle.SelectedItem.ToString();//个人职务
            int UserTitleID = this.dplUserTitle.SelectedIndex;//个人职务ID
            string TrueName = this.txtTrueName.Text.Trim();//姓名
            string TelPhone = this.txtTelPhone.Text.Trim();//电话
            //string Address = this.txtAddress.Text.Trim();//住址

            if (TrueName == "")
            {
                strErr += "姓名不能为空！\\n";
            }
            if (UserTitleID == 0)
            {
                strErr += "请选择个人职务！\\n";
            }
            if (TelPhone == "")
            {
                strErr += "电话不能为空！\\n";
            }
            //if (Address == "")
            //{
            //    strErr += "住址不能为空！\\n";
            //}
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            System.DateTime time = System.DateTime.Now;

            SanZi.Model.Users model = new SanZi.Model.Users();

            model.TitleID = UserTitleID;
            model.TitleName = UserTitleName;
            model.DeptID = DeptID;
            model.DeptName = DeptName;
            model.TrueName = TrueName;
            model.TelPhone = TelPhone;
            model.Address = "";
            model.SubTime = (int)LTP.Common.TimeParser.ConvertDateTimeInt(time);
            if (HttpContext.Current.Session["UserID"] != null)
            {
                model.SubUID = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            }
            else
            {
                model.SubUID = 0;
            }
            SanZi.BLL.Users bll = new SanZi.BLL.Users();
            int maxUserID = bll.GetMaxId() + 1;
            //model.BarCode = LTP.Common.DEncrypt.DESEncrypt.Encrypt(maxUserID.ToString());
            model.BarCode = this.GetRandomCode(3) + GetStringByNum(maxUserID);
            //Response.Write(" <script language=\"javascript\">window.top.opener = null; window.close(); </script>");
            //Response.Write(UserTitleID + UserTitleName + DeptID + DeptName + TrueName + TelPhone + Address);
            bll.AddUser(model);
            //Response.Write(bll.AddUsers(model));
            Response.Redirect("index.aspx");
        }

        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
        /// </summary>
        /// <param name="allChar"></param>
        /// <param name="CodeCount"></param>
        /// <returns></returns>
        public string GetRandomCode(int CodeCount)
        {
            //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            string allChar = "1,2,3,4,5,6,7,8,9,0";
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }

        public string GetStringByNum(int maxID)
        {
            string strID = maxID.ToString();
            int strLength = strID.Length;
            string strResult = string.Empty;
            if (strLength < 5)
            {
                strLength = 5 - strLength;
                for (int i = 0; i < strLength; i++)
                {
                    strResult += "0";
                }
            }

            return strResult + strID;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
