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

using System.Text.RegularExpressions;

public partial class DefaultSanZi : System.Web.UI.Page
{
    private int nodeDepth = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Session.Abandon();
            AccountID.Attributes["onclick"] = "SetObjectPos('DoOverLay')";
            AccountName.Attributes["onclick"] = "SetObjectPos('DoOverLay')";
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            UserList.Attributes["onchange"] = "$('Password').focus();";
            /////////////////////////////////////版权校验//////////////////////////////////////
            DataSet RegInfoDS = ValidateClass.GetRegDataSet();
            if (RegInfoDS == null) { return; }
            ///////////////////////////////////////////////////////////////////////////////////
            AccountID.Attributes["readonly"] = "readonly";
            AccountName.Attributes["readonly"] = "readonly";
            ImageButton1.Attributes.Add("onclick", "return CheckLogin();");
            //初始化行政区划树/////////////////////////////////////////////////////////////////
            TotalLevel.Value = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "LastLevel");
            string TopUnitID = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "TopUnitID");
            if (TopUnitID.Length == 0)
            {
                TopUnitID = "000000";
            }
            nodeDepth = int.Parse(TotalLevel.Value);
            DataTable UnitTable = RegInfoDS.Tables["CUnits"];
            InitUnitList(TreeView1.Nodes[0], TopUnitID, UnitTable);
            TreeView1.Nodes[0].Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + TopUnitID + "']", "UnitName");
            TreeView1.Nodes[0].Value = TopUnitID;
            TreeView1.Nodes[0].ImageUrl = "Images/dopen.gif";
            if (TotalLevel.Value == "0")
            {
                DataTable dt = MainClass.GetDataTable("select id,accountname,accountdate from cw_account where unitid='" + TreeView1.Nodes[0].Value + "' order by levelid,id");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode tn = new TreeNode(dt.Rows[i]["accountname"].ToString(), "$" + dt.Rows[i]["id"].ToString());
                    tn.SelectAction = TreeNodeSelectAction.Select;
                    TreeView1.Nodes[0].ChildNodes.Add(tn);
                }
            }
        }
        nodeDepth = int.Parse(TotalLevel.Value);
        if (TreeView1.SelectedNode != null)
        {
            if (TreeView1.SelectedNode.Value.StartsWith("$"))
            {
                DivShowState.Value = "0";
                Password.Focus();
            }
            else
            {
                if (TreeView1.SelectedNode.Parent == null)
                {
                    DivShowState.Value = "0";
                }
                else
                {
                    TreeView1.SelectedNode.ToggleExpandState();
                    if ((bool)TreeView1.SelectedNode.Expanded.Value)
                    {
                        TreeView1.SelectedNode.ImageUrl = "Images/dopen.gif";
                        DivShowState.Value = "1";
                    }
                    else
                    {
                        TreeView1.SelectedNode.ImageUrl = "Images/dclose.gif";
                        DivShowState.Value = "0";
                    }
                }
            }
        }
    }
    protected void InitUnitList(TreeNode _TreeNode, string ParentID, DataTable UnitTable)
    {
        _TreeNode.ImageUrl = "Images/dclose.gif";
        _TreeNode.SelectAction = TreeNodeSelectAction.Select;
        DataView DV = new DataView(UnitTable, "parentid='" + ParentID + "'", "", DataViewRowState.CurrentRows);
        for (int i = 0; i < DV.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(DV[i]["unitname"].ToString(), DV[i]["id"].ToString()));
            InitUnitList(_TreeNode.ChildNodes[_TreeNode.ChildNodes.Count - 1], DV[i]["id"].ToString(), UnitTable);
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        UserList.Items.Clear();
        AccountName.Text = TreeView1.SelectedNode.Text;
        if (TreeView1.SelectedNode.Value.StartsWith("$"))
        {
            AccountID.Attributes["isAccount"] = "1";
            AccountID.Text = TreeView1.SelectedNode.Value.Substring(1);
            DataTable users = MainClass.GetDataTable("select username,realname from cw_users where accountid like '%" + AccountID.Text
                + "%' and unitid='" + TreeView1.SelectedNode.Parent.Value + "'");
            foreach (DataRow row in users.Rows)
            {
                string username = row["username"].ToString();
                UserList.Items.Add(new ListItem(string.Format("{0}（{1}）", row["realname"].ToString(), username), username));
            }
            LoginType.SelectedIndex = 1;
            LoginType.Items[0].Enabled = false;
            for (int k = 1; k < LoginType.Items.Count; k++)
            {
                LoginType.Items[k].Enabled = true;
            }
        }
        else
        {
            AccountID.Attributes["isAccount"] = "0";
            AccountID.Text = TreeView1.SelectedNode.Value;
            DataRow[] rows = ValidateClass.GetRegRows("CAdmins", "unitid='" + TreeView1.SelectedNode.Value + "'", "usertype desc");
            for (int i = 0; i < rows.Length; i++)
            {
                string RealName = string.Empty;
                string username = rows[i]["username"].ToString();
                switch (rows[i]["usertype"].ToString())
                {
                    case "3":
                        RealName = "领导查询";
                        break;
                    case "4":
                        RealName = "审计查询";
                        break;
                    default:
                        RealName = "管理员";
                        break;
                }
                UserList.Items.Add(new ListItem(string.Format("{0}（{1}）", RealName, username), username));
            }
            ///////////////////////////////////////////////////////////////////////////////////
            if (TotalLevel.Value != "0") { InitTreeNode(TreeView1.Nodes[0]); }
            if (TreeView1.SelectedNode.Depth == nodeDepth && TreeView1.SelectedNode.ChildNodes.Count == 0)
            {
                DataTable dt = MainClass.GetDataTable("select id,accountname,accountdate from cw_account where unitid='" + TreeView1.SelectedNode.Value + "' order by levelid,id");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode tn = new TreeNode(dt.Rows[i]["accountname"].ToString(), "$" + dt.Rows[i]["id"].ToString());
                    tn.SelectAction = TreeNodeSelectAction.Select;
                    TreeView1.SelectedNode.ChildNodes.Add(tn);
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////
            if (TreeView1.SelectedNode.ChildNodes.Count == 0)
            {
                DivShowState.Value = "0";
            }
            LoginType.SelectedIndex = 0;
            LoginType.Items[0].Enabled = true;
            for (int k = 1; k < LoginType.Items.Count; k++)
            {
                LoginType.Items[k].Enabled = false;
            }
        }
        if (UserList.Items.Count == 0)
        {
            UserList.Items.Add(new ListItem("未查询到登录用户", "000000"));
        }
    }
    private void InitTreeNode(TreeNode _TreeNode)
    {
        foreach (TreeNode node in _TreeNode.ChildNodes)
        {
            if (TreeView1.SelectedNode.ValuePath.IndexOf(node.ValuePath) == -1 && node.Depth == nodeDepth && node.ChildNodes.Count > 0)
            {
                node.ChildNodes.Clear();
            }
            else
            {
                InitTreeNode(node);
            }
            if (TreeView1.SelectedNode.ValuePath.IndexOf(node.Value) == -1 && node.Depth <= nodeDepth)
            {
                node.ImageUrl = "Images/dclose.gif";
                node.CollapseAll();
            }
        }
    }
    protected void LoginType_SelectedIndexChanged(object sender, EventArgs e)
    {
        UserList.Items.Clear();
        string Q = LoginType.SelectedIndex == 1 ? "=" : "<>";
        DataTable dt = MainClass.GetDataTable("select username,realname from cw_users where accountid like '%" + AccountID.Text
            + "%' and unitid" + Q + "'" + TreeView1.SelectedNode.Parent.Value + "'");
        foreach (DataRow row in dt.Rows)
        {
            string username = row["username"].ToString();
            UserList.Items.Add(new ListItem(string.Format("{0}（{1}）", row["realname"].ToString(), username), username));
        }
        if (UserList.Items.Count == 0)
        {
            UserList.Items.Add(new ListItem("未查询到登录用户", "000000"));
        }
        DivShowState.Value = "0";
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DivShowState.Value = "0";
        if (Regex.IsMatch(Password.Text, "select|insert|update|where|=|'", RegexOptions.IgnoreCase))
        {
            ExeScript.Text = "<script>alert('密码中含有非法数据，请核实！')</script>";
            return;
        }
        if (TreeView1.SelectedNode.Value.StartsWith("$"))
        {
            DataRow row = MainClass.GetDataRow("select * from cw_users where username='" + UserList.SelectedValue + "'");
            if (row != null)
            {
                if (Password.Text == row["password"].ToString())
                {
                    /////////////////////////////////////版权校验//////////////////////////////////////
                    if (!ValidateClass.ValidateUserList(this.Page)) { return; }
                    ///////////////////////////////////////////////////////////////////////////////////
                    UserInfo.SessionFlag = "SessionFlag";
                    UserInfo.AccountID = TreeView1.SelectedValue.Substring(1);
                    Session["UserID"] = row["id"].ToString();
                    Session["RealName"] = row["realname"].ToString();
                    Session["UserName"] = row["username"].ToString();
                    Session["Powers"] = row["powers"].ToString();
                    Session["MyAccount"] = row["accountid"].ToString();
                    Session["UserType"] = "0";
                    MainClass.ExecuteSQL("update cw_users set LoginCounts=LoginCounts+1,LastLogin='" + DateTime.Now.ToString() + "' where id='" + row["id"].ToString() + "'");
                    if (row["unitid"].ToString().StartsWith("999999"))
                    {
                        Session["UnitID"] = row["unitid"].ToString().Substring(6);
                        if (LoginType.SelectedValue == "2")
                        {
                            Response.Redirect("QueryFrame.aspx");
                        }
                        else
                        {
                            Response.Redirect("gongkai.aspx");
                        }
                    }
                    else
                    {
                        Session["UnitID"] = row["unitid"].ToString();
                        //写入操作日志
                        CommClass.WriteCTL_Log("100005", "登录账务处理平台");
                        //--
                        Response.Redirect("BusinessList.aspx");
                    }
                }
                else
                {
                    ExeScript.Text = "<script>alert('密码错误，请核实！')</script>";
                }
            }
            else
            {
                ExeScript.Text = "<script>alert('无此操作员，请核实！')</script>";
            }
        }
        else
        {
            string UserID = ValidateClass.ReadXMLNodeText("FinancialDB/CAdmins[UserName='" + UserList.SelectedValue + "']", "ID");
            if (UserID.Length > 0)
            {
                DataRow AdminRow = MainClass.GetDataRow("select realname,password,LoginCounts from cw_users where id='" + UserID + "'");
                if (AdminRow == null)
                {
                    ExeScript.Text = "<script>alert('授权文件用户信息与数据库用户信息不一致，请重新导入授权文件！')</script>";
                }
                else
                {
                    if (Password.Text == AdminRow["password"].ToString())
                    {
                        UserInfo.SessionFlag = "SessionFlag";
                        Session["UserID"] = UserID;
                        Session["RealName"] = AdminRow["realname"].ToString();
                        Session["UserName"] = UserList.SelectedValue;
                        string unitID = TreeView1.SelectedValue;
                        Session["UnitID"] = unitID;
                        if (unitID != "000000")
                        {
                            //初始化默认参数
                            MainClass.CheckInit(unitID);
                        }
                        Session["Powers"] = "000000";
                        MainClass.ExecuteSQL("update cw_users set LoginCounts=LoginCounts+1,LastLogin='" + DateTime.Now.ToString() + "' where id='" + UserID + "'");
                        string UserType = ValidateClass.ReadXMLNodeText("FinancialDB/CAdmins[UserName='" + UserList.SelectedValue + "']", "UserType");
                        Session["UserType"] = UserType;
                        /////////////////////////////////////版权校验//////////////////////////////////////
                        if (!ValidateClass.ValidateUser(this.Page, "FinancailDefaultAdmin", 0))
                        {
                            Response.Redirect("SysManage/UserManage.aspx", true);
                            return;
                        }
                        ///////////////////////////////////////////////////////////////////////////////////
                        switch (UserType)
                        {
                            case "1":
                            case "2":
                                Response.Redirect("AdminFrame.aspx");
                                break;
                            case "3":
                            case "4":
                                if (unitID == "000000")
                                {
                                    Response.Redirect("LeadQFrame.aspx");
                                }
                                else
                                {
                                    Response.Redirect("LeadQFrame2.aspx");
                                }
                                //Response.Redirect("LeadQFrame2.aspx");
                                break;
                        }
                    }
                    else
                    {
                        ExeScript.Text = "<script>alert('密码错误，请核实！')</script>";
                    }
                }
            }
            else
            {
                ExeScript.Text = "<script>alert('无此管理员，请核实！')</script>";
            }
        }
    }
}
