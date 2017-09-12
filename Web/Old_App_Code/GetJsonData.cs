using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 获取Json数据
/// </summary>
public class GetJsonData
{
	public GetJsonData()
	{
		//--
	}

    public static void GetSubjectList(string gtype, ref StringBuilder json)
    {
        try
        {
            HttpRequest request = HttpContext.Current.Request;
            DataTable subjectList = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append("select subjectno,subjectname,isdetail,islock,parentno from cw_subject where ");
            if (request.QueryString["root"] == "source")
            {
                sql.Append("parentno='000' ");
                string filter = request.QueryString["filter"];
                if (filter != null && filter.Length > 0)
                {
                    sql.AppendFormat("and (subjectno like '{0}') ", filter.Replace("|", "' or subjectno like '"));
                }
            }
            else
            {
                sql.AppendFormat("parentno='{0}' ", request.QueryString["root"]);
            }
            if (request.QueryString["hidelock"] == "1")
            {
                sql.Append("and (islock='0' or islock is null) ");
            }
            switch (request.QueryString["f"])
            {
                case "2":
                    if (UserInfo.UserType == "0")
                    {
                        sql.Append("order by subjectno");
                        subjectList = CommClass.GetDataTable(sql.ToString());
                    }
                    else
                    {
                        sql.AppendFormat("and unitid='{0}' order by subjectno", UserInfo.UnitID);
                        subjectList = MainClass.GetDataTable(sql.ToString());
                    }
                    break;
                case "0":
                    sql.AppendFormat("and unitid='{0}' order by subjectno", UserInfo.UnitID);
                    subjectList = MainClass.GetDataTable(sql.ToString());
                    break;
                default:
                    sql.Append("order by subjectno");
                    subjectList = CommClass.GetDataTable(sql.ToString());
                    break;
            }
            json.Append("[");
            switch (gtype)
            {
                case "0":
                    //可选所有科目
                    foreach (DataRow row in subjectList.Rows)
                    {
                        string sno = row[0].ToString();
                        string sname = row[1].ToString();
                        string text = string.Concat(sno, ".", sname);
                        text = string.Format("<a href=### onclick=\\\"OnTreeClick(\\'{0}\\',\\'{1}\\')\\\">{2}</a>", new string[] { sname, sno, text });
                        if (row["isdetail"].ToString() == "1")
                        {
                            if (row["islock"].ToString() == "1")
                            {
                                text = string.Concat("<del>", text, "</del>");
                            }
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'file',expanded:false},");
                        }
                        else
                        {
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'folder',expanded:false,hasChildren:true},");
                        }
                    }
                    break;
                case "1":
                    //只可选明细科目
                    foreach (DataRow row in subjectList.Rows)
                    {
                        string sno = row[0].ToString();
                        string sname = row[1].ToString();
                        string text = string.Concat(sno, ".", sname);
                        if (row["isdetail"].ToString() == "1")
                        {
                            text = string.Format("<a href=### onclick=\\\"OnTreeClick(\\'{0}\\',\\'{1}\\')\\\">{2}</a>", new string[] { sname, sno, text });
                            if (row["islock"].ToString() == "1")
                            {
                                text = string.Concat("<del>", text, "</del>");
                            }
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'file',expanded:false},");
                        }
                        else
                        {
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'folder',expanded:false,hasChildren:true},");
                        }
                    }
                    break;
                case "2":
                    //只可选一级科目
                    foreach (DataRow row in subjectList.Rows)
                    {
                        string sno = row[0].ToString();
                        string sname = row[1].ToString();
                        string text = string.Concat(sno, ".", sname);
                        if (row["parentno"].ToString() == "000")
                        {
                            text = string.Format("<a href=### onclick=\\\"OnTreeClick(\\'{0}\\',\\'{1}\\')\\\">{2}</a>", new string[] { sname, sno, text });
                        }
                        if (row["isdetail"].ToString() == "1")
                        {
                            if (row["islock"].ToString() == "1")
                            {
                                text = string.Concat("<del>", text, "</del>");
                            }
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'file',expanded:false},");
                        }
                        else
                        {
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'folder',expanded:false,hasChildren:true},");
                        }
                    }
                    break;
                case "3":
                    //只可选非明细科目
                    foreach (DataRow row in subjectList.Rows)
                    {
                        string sno = row[0].ToString();
                        string sname = row[1].ToString();
                        string text = string.Concat(sno, ".", sname);
                        if (row["isdetail"].ToString() == "1")
                        {
                            if (row["islock"].ToString() == "1")
                            {
                                text = string.Concat("<del>", text, "</del>");
                            }
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'file',expanded:false},");
                        }
                        else
                        {
                            text = string.Format("<a href=### onclick=OnTreeClick(\\'{0}\\',\\'{1}\\')>{2}</a>", new string[] { sname, sno, text });
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'folder',expanded:false,hasChildren:true},");
                        }
                    }
                    break;
                case "4":
                    //可选任意科目
                    foreach (DataRow row in subjectList.Rows)
                    {
                        string sno = row[0].ToString();
                        string sname = row[1].ToString();
                        string text = string.Concat(sno, ".", sname);
                        text = string.Format("<a href=### onclick=\\\"OnTreeClick(\\'{0}[{1}]\\')\\\">{2}</a>", new string[] { sname, sno, text });
                        if (row["isdetail"].ToString() == "1")
                        {
                            if (row["islock"].ToString() == "1")
                            {
                                text = string.Concat("<del>", text, "</del>");
                            }
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'file',expanded:false},");
                        }
                        else
                        {
                            json.Append("{text:'" + text + "',id:'" + sno + "',classes:'folder',expanded:false,hasChildren:true},");
                        }
                    }
                    break;
            }
            json.Append("]");
            json = json.Replace(",]", "]");
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("D:\\err.txt", HttpContext.Current.Request.QueryString["f"] + "***" + ex.ToString());
        }
        
    }

    public static void GetSubject_Choose(ref StringBuilder json)
    {
        HttpRequest request = HttpContext.Current.Request;
        StringBuilder sql = new StringBuilder();
        sql.Append("select subjectno,subjectname,isdetail from cw_subject where ");
        if (request.QueryString["root"] == "source")
        {
            if (request.QueryString["qt"] != "0")
            {
                sql.AppendFormat("subjecttype='{0}' and ", request.QueryString["qt"]);
            }
            bool hasQ = false;
            if (request.QueryString["qn"] != "")
            {
                hasQ = true;
                sql.AppendFormat("(subjectno like '{0}%' or (subjectno like '{1}%' and subjectno like '%{0}')) and ", request.QueryString["qn"], SysConfigs.GroupingSubject);
            }
            if (request.QueryString["qg"] != "000000")
            {
                hasQ = true;
                string GroupID = CommClass.GetFieldFromID(request.QueryString["qg"], "subjectno", "cw_subjectgroup");
                sql.AppendFormat("subjectno in ('{0}') and ", GroupID.Replace("$", "','"));
            }
            if (request.QueryString["qm"] != "")
            {
                hasQ = true;
                sql.AppendFormat("subjectname like '%{0}%' and ", HttpUtility.UrlDecode(request.QueryString["qm"]));
            }
            if (hasQ)
            {
                sql.Append("1=1 ");
            }
            else
            {
                sql.Append("parentno='000' ");
            }
        }
        else
        {
            sql.AppendFormat("parentno='{0}' ", request.QueryString["root"]);
        }
        bool isJump = false;
        switch (request.QueryString["st"])
        {
            case "1":
                isJump = true;
                break;
            case "2":
                sql.Append("and isdetail='1' ");
                break;
        }
        sql.Append("and (islock is null or islock='0') order by subjectno");
        DataTable subjectList = CommClass.GetDataTable(sql.ToString());
        json.Append("[");
        ListPredicate listPredicate = new ListPredicate();
        foreach (DataRow row in subjectList.Rows)
        {
            string sno = row[0].ToString();
            if (isJump)
            {
                if (listPredicate.CheckStartsWith(sno))
                {
                    continue;
                }
                else
                {
                    listPredicate.AddListItem(sno);
                }
            }
            string text = string.Concat(sno, ".", row[1].ToString());
            if (row["isdetail"].ToString() == "1")
            {
                string Name0 = CommClass.GetFieldFromNo(sno.Substring(0, 3), "SubjectName");
                string Name1 = sno.Length > 3 ? CommClass.GetDetailSubject(sno) : "";
                text = string.Format("<a href=### onclick=\\\"OnTreeClick(\\'{0}\\',\\'{1}\\',\\'{2}\\')\\\">{3}</a>", new string[] { Name0, Name1, sno, text });
                json.Append("{text:'" + text + "',id:'" + sno + "',classes:'file',expanded:false},");
            }
            else
            {
                json.Append("{text:'" + text + "',id:'" + sno + "',classes:'folder',expanded:false,hasChildren:true},");
            }
        }
        json.Append("]");
        json = json.Replace(",]", "]");
    }

    public static void GetJsonUnit(ref StringBuilder json)
    {
        json.Append("[");
        string myid = string.Empty;
        HttpRequest request = HttpContext.Current.Request;
        if (request.QueryString["root"] == "source")
        {
            myid = request.QueryString["topid"];
        }
        else
        {
            myid = request.QueryString["root"];
        }
        string TotalLevel = request.QueryString["l"];
        string mylevel = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + myid + "']", "UnitLevel");
        if (mylevel == TotalLevel)
        {
            DataTable accounts = MainClass.GetDataTable(string.Concat("select id,accountname from cw_account where unitid='", myid, "'"));
            foreach (DataRow row in accounts.Rows)
            {
                string aid = row["id"].ToString();
                string text = row["accountname"].ToString();
                if (request.QueryString["f"] == "1")
                {
                    if (MainClass.GetTableValue("cw_account", "accountdate", string.Concat("id='", aid, "'")).Length == 0)
                    {
                        text = string.Format("<a href=### onclick=\\\"alert(\\'{1}\\')\\\">{0}</a>", new string[] { text, "该账套尚未启用！" });
                    }
                    else
                    {
                        text = string.Format("<a href=### onclick=\\\"OnTreeClick(\\'{0}\\',\\'{1}\\')\\\">{0}</a>", new string[] { text, aid });
                    }
                }
                json.Append("{text:'" + text + "',id:'_" + aid + "',pid:'" + myid + "',classes:'file',expanded:false},");
            }
        }
        else
        {
            DataRow[] units = ValidateClass.GetRegRows("CUnits", string.Concat("ParentID='", myid, "'"));
            foreach (DataRow row in units)
            {
                json.Append("{text:'" + row["UnitName"].ToString() + "',id:'" + row["id"].ToString() + "',pid:'" + myid + "',classes:'folder',expanded:false,hasChildren:true},");
            }
        }
        json.Append("]");
        json = json.Replace(",]", "]");
    }
}
