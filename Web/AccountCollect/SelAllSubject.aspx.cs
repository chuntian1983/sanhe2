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

public partial class AccountCollect_SelAllSubject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["slist"]))
            {
                InitSubject(TreeView1.Nodes[0], "000");
            }
            else
            {
                string[] sno = Request.QueryString["slist"].Split('|');
                for (int i = 0; i < sno.Length; i++)
                {
                    string subjectName = MainClass.GetFieldFromNo(sno[i], "subjectname");
                    if (subjectName != "NoDataItem")
                    {
                        TreeNode node = new TreeNode(subjectName, sno[i]);
                        TreeView1.Nodes[0].ChildNodes.Add(node);
                        InitSubject(node, sno[i]);
                    }
                }
            }
        }
    }

    protected void InitSubject(TreeNode _TreeNode, string ParentNo)
    {
        DataSet ds = MainClass.GetDataSet("select subjectno,subjectname from cw_subject where unitid='" + Session["UnitID"].ToString() + "' and parentno='" + ParentNo + "' order by subjectno");
        if (_TreeNode.Value != "000")
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            string ClickStr = "OnTreeClick('" + _TreeNode.Text.Replace(_TreeNode.Value + "&nbsp;.&nbsp;", "") + "','" + _TreeNode.Value + "');";
            _TreeNode.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">" + _TreeNode.Text + "</a>";
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(ds.Tables[0].Rows[i]["subjectno"].ToString() + "&nbsp;.&nbsp;"
                + ds.Tables[0].Rows[i]["subjectname"].ToString(), ds.Tables[0].Rows[i]["subjectno"].ToString()));
            InitSubject(_TreeNode.ChildNodes[i], ds.Tables[0].Rows[i]["subjectno"].ToString());
        }
    }
}
