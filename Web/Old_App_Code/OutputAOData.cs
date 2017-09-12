using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// 导出审计AO格式数据
/// </summary>
public class OutputAOData
{
	public OutputAOData()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static void OutputData(string AccountYear, string OutDbFilePath, DataProvider AccDbProvider)
    {
        string[] ATypeList ={ "一般金额账", "数量金额账" };
        string[] STypeList ={ "资产", "负债", "权益", "成本", "损益" };
        string[] SLevel = SysConfigs.SubjectLevel.Split(',');
        using (OleDbConnection conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source = " + OutDbFilePath))
        {
            conn.Open();
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.Connection = conn;
            //导出科目表
            DataTable subject = AccDbProvider.ExecuteDataTable("select subjectno,subjectname,subjecttype,accounttype,isdetail from cw_subject");
            foreach (DataRow row in subject.Rows)
            {
                int SType = int.Parse(row["subjecttype"].ToString());
                int AType = int.Parse(row["accounttype"].ToString());
                int IGrade = 1;
                for (int i = 1; i < SLevel.Length; i++)
                {
                    if (SLevel[i] == row["subjectno"].ToString().Length.ToString())
                    {
                        IGrade = i;
                        break;
                    }
                }
                command.CommandText = "insert into code(cclass,cclass_engl,ccode,ccode_name,igrade,cbook_type,cbook_type_engl,bend)values('"
                + STypeList[SType - 1] + "','0','"
                + row["subjectno"].ToString() + "','"
                + row["subjectname"].ToString() + "','"
                + IGrade.ToString() + "','"
                + ATypeList[AType] + "','0','"
                + row["isdetail"].ToString() + "')";
                command.ExecuteNonQuery();
            }
            //导出科目余额表
            DataTable subjectsum = AccDbProvider.ExecuteDataTable("select subjectno,yearmonth,beginbalance,lead,onloan,FinalBalance from cw_viewsubjectsum where yearmonth like '"
                + AccountYear + "%' order by yearmonth,subjectno");
            foreach (DataRow row in subjectsum.Rows)
            {
                string iperiod = row["yearmonth"].ToString().Substring(5, 2);
                if (iperiod.StartsWith("0"))
                {
                    iperiod = iperiod.Substring(1);
                }
                string BeginBalance = row["beginbalance"].ToString();
                string FinalBalance = row["FinalBalance"].ToString();
                string CBeginD_C = string.Empty;
                string CEndD_C = string.Empty;
                if (BeginBalance == "0")
                {
                    CBeginD_C = "平";
                }
                else if (BeginBalance.StartsWith("-"))
                {
                    CBeginD_C = "贷";
                    BeginBalance = BeginBalance.Substring(1);
                }
                else
                {
                    CBeginD_C = "借";
                }
                if (FinalBalance == "0")
                {
                    CEndD_C = "平";
                }
                else if (FinalBalance.StartsWith("-"))
                {
                    CEndD_C = "贷";
                    FinalBalance = FinalBalance.Substring(1);
                }
                else
                {
                    CEndD_C = "借";
                }
                command.CommandText = "insert into gl_accsum(ccode,iperiod,cbegind_c,cbegind_c_engl,mb,md,mc,cendd_c,cendd_c_engl,me)values('"
                    + row["subjectno"].ToString() + "',"
                    + iperiod + ",'"
                    + CBeginD_C + "','0',"
                    + BeginBalance + ","
                    + row["lead"].ToString() + ","
                    + row["onloan"].ToString() + ",'"
                    + CEndD_C + "','0',"
                    + FinalBalance + ")";
                command.ExecuteNonQuery();
            }
            //导出凭证表
            DataTable voucher = AccDbProvider.ExecuteDataTable("select voucherno,subjectno,vsummary,summoney,voucherdate from cw_voucherentry where voucherdate like '"
                + AccountYear + "%' order by left(voucherdate,8),voucherno");
            foreach (DataRow row in voucher.Rows)
            {
                string iperiod = row["voucherdate"].ToString().Substring(5, 2);
                if (iperiod.StartsWith("0"))
                {
                    iperiod = iperiod.Substring(1);
                }
                bool isOnloan = row["summoney"].ToString().StartsWith("-");
                string vsummary = row["vsummary"].ToString();
                if (vsummary.Length == 0) { vsummary = "无摘要"; }
                command.CommandText = "insert into gl_accvouch(iperiod,csign,ino_id,inid,dbill_date,idoc,ibook,cdigest,ccode,md,mc)values("
                    + iperiod + ",'记','"
                    + row["voucherno"].ToString().Substring(1) + "','0','"
                    + row["voucherdate"].ToString() + "','"
                    + "0','1','"
                    + vsummary + "','"
                    + row["subjectno"].ToString() + "','"
                    + (isOnloan ? "0" : row["summoney"].ToString()) + "','"
                    + (isOnloan ? row["summoney"].ToString().Substring(1) : "0") + "')";
                command.ExecuteNonQuery();
            }
        }
    }
}
