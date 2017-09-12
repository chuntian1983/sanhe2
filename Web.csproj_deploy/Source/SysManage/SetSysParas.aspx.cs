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
using System.Collections.Generic;

public partial class SysManage_SetSysParas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return confirm('您确定保存参数设置吗？')");
            if (MainClass.GetSysPara("CTLLogManageRead") == "1")
            {
                rizhi.Items[0].Selected = true;
            }
            if (MainClass.GetSysPara("CTLLogManageDel") == "1")
            {
                rizhi.Items[1].Selected = true;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (rizhi.Items[0].Selected)
        {
            MainClass.SetSysPara("CTLLogManageRead", "1");
        }
        else
        {
            MainClass.SetSysPara("CTLLogManageRead", "0");
        }
        if (rizhi.Items[1].Selected)
        {
            MainClass.SetSysPara("CTLLogManageDel", "1");
        }
        else
        {
            MainClass.SetSysPara("CTLLogManageDel", "0");
        }
        PageClass.ShowAlertMsg(this.Page, "保存成功！");
    }
}
