using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.ResManage
{
    public partial class ImportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImportExcelData.Attributes["onclick"] = "return confirm('您确定执行导入操作吗')";
                if (Session["isStartAccount"] != null)
                {
                    CheckBox2.Enabled = false;
                }
            }
        }

        protected void ImportExcelData_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string deptName = UserInfo.AccountName;
                string filepath = Server.MapPath("../BackupDB/" + Guid.NewGuid().ToString() + ".xls");
                FileUpload1.SaveAs(filepath);
                string[] sheets = ExcelProvider.GetExcelSheetNames(filepath);
                DataTable data = ExcelProvider.GetExcelDatas(filepath, sheets[0]);
                switch (RadioButtonList1.SelectedValue)
                {
                    case "11":
                        dziyuan(data);
                        break;
                    case "3":
                        dzichan1(data);
                        break;
                    case "4":
                        dzichan2(data);
                        break;
                    case "9":
                        daokemu(data);
                        break;
                }
                PageClass.ShowAlertMsg(this.Page, "数据导入完毕！");
            }
            else
            {
                PageClass.ShowAlertMsg(this.Page, "请选择Excel表格文件！");
            }
        }

        private void daokemu(DataTable data)
        {
            string ClassID = string.Empty;
            string ClassName = string.Empty;
            string pnos = CommClass.GetTableValue("cw_subject", "subjectno", "parentno='11302' and subjectno<>'113029999' order by subjectno desc", "113020001");
            int k = TypeParse.StrToInt(pnos, 113020001);
            for (int i = 6; i < data.Rows.Count; i++)
            {
                string firstCellText = data.Rows[i][0].ToString().Replace("\n", "").Trim();
                if (firstCellText == "小计" || firstCellText == "合计")
                {
                    break;
                }
                if (firstCellText.Length > 0)
                {
                    k++;
                    ClassID = k.ToString();
                    ClassName = firstCellText;
                    string qc = "0";
                    if (CheckBox2.Checked)
                    {
                        qc = TypeParse.StrToDecimal(data.Rows[i][5].ToString(), 0).ToString();
                    }
                    CommClass.ExecuteSQL("insert into cw_subject(id,parentno,subjectno,subjectname,subjecttype,"
                        + "AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct)values('"
                        + CommClass.GetRecordID("CW_Subject") + "','11302','"
                        + ClassID + "','" + ClassName + "','1','0','" + qc + "','0','1','0')");
                    CommClass.ExecuteSQL("update cw_subject set IsDetail='0' where subjectno='11302'");
                }
            }
        }

        private void dzichan1(DataTable data)
        {
            DateTime accountDate = MainClass.GetAccountDate();
            string AccountDate = accountDate.ToString("yyyy-MM-dd");
            string ClassID = "151019999";
            string ClassName = CommClass.GetFieldFromNo(ClassID, "SubjectName");
            string UseLife = "0.0";
            string SVRate = "0";
            string JingCZ = "0";
            string DeprMethod = "0";
            string DeprSubject = SysConfigs.MonthDeprSubject;
            string MUnit = "";
            string DeptName = string.Format("{0}.{1}", UserInfo.AccountID, UserInfo.AccountName);
            string pnos = CommClass.GetTableValue("cw_subject", "subjectno", "parentno='15101' and subjectno<>'151019999' order by subjectno desc", "151010001");
            int k = TypeParse.StrToInt(pnos, 151010001);
            for (int i = 7; i < data.Rows.Count; i++)
            {
                string firstCellText = data.Rows[i][0].ToString().Replace("\n", "").Trim();
                if (firstCellText == "小计" || firstCellText == "合计")
                {
                    break;
                }
                string secodCellText = data.Rows[i][1].ToString().Replace("\n", "").Replace(" ", "");
                if (firstCellText.Length == 0 || secodCellText == "小计" || secodCellText == "合计")
                {
                    continue;
                }
                //资产类别参数
                string OldPrice = data.Rows[i][11].ToString();
                decimal oldPrice = TypeParse.StrToDecimal(OldPrice, 0);
                if (oldPrice <= 0)
                {
                    continue;
                }
                string AAmount = data.Rows[i][10].ToString();
                decimal amount = TypeParse.StrToDecimal(AAmount, 0);
                decimal APrice = 0;
                if (amount != 0)
                {
                    APrice = oldPrice / amount;
                    APrice = Math.Round(APrice, 2);
                }
                if (CheckBox1.Checked)
                {
                    k++;
                    ClassID = k.ToString();
                    ClassName = firstCellText;
                    CommClass.ExecuteSQL("insert into cw_subject(id,parentno,subjectno,subjectname,subjecttype,"
                        + "AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct)values('"
                        + CommClass.GetRecordID("CW_Subject") + "','15101','"
                        + ClassID + "','" + ClassName + "','1','0','0','0','1','0')");
                    CommClass.ExecuteSQL("update cw_subject set IsDetail='0' where subjectno='15101'");
                }
                //创建资产卡片
                Dictionary<string, string> sql = new Dictionary<string, string>();
                sql.Add("ID", CommClass.GetRecordID("CW_AssetID"));
                sql.Add("CardID", CommClass.GetRecordID("CW_AssetCard"));
                sql.Add("AssetNo", CommClass.GetRecordID("CW_AssetNo"));
                sql.Add("AssetName", firstCellText);
                sql.Add("ClassID", ClassID);
                sql.Add("CName", ClassName);
                sql.Add("AssetModel", "");
                sql.Add("DeptName", DeptName);
                sql.Add("AddType", "");
                sql.Add("Depositary", "");
                sql.Add("UseState", "101");
                sql.Add("UseLife", UseLife);
                sql.Add("DeprMethod", DeprMethod);
                sql.Add("SUseDate", data.Rows[i][1].ToString());
                sql.Add("UsedMonths", "0");
                sql.Add("CurrencyType", "人民币");
                sql.Add("OldPrice", OldPrice);
                sql.Add("OldPrice0", OldPrice);
                sql.Add("JingCZL", SVRate);
                sql.Add("JingCZ", JingCZ);
                decimal zhejiu = TypeParse.StrToDecimal(data.Rows[i][12].ToString(), 0);
                sql.Add("ZheJiu", zhejiu.ToString());
                sql.Add("ZheJiu0", zhejiu.ToString());
                sql.Add("MonthZJL", "0");
                sql.Add("MonthZJE", "0");
                decimal newprice = TypeParse.StrToDecimal(data.Rows[i][13].ToString(), 0);
                sql.Add("NewPrice", newprice.ToString());
                sql.Add("DeprSubject", DeprSubject);
                sql.Add("AssetItem", "");
                sql.Add("AUnit", MUnit);
                sql.Add("AAmount", AAmount);
                sql.Add("HasAmount", "0");
                sql.Add("APrice", APrice.ToString());
                sql.Add("APicture", "");
                sql.Add("AssetAdmin", "");
                sql.Add("DeprState", "0");
                sql.Add("AssetState", "0");
                sql.Add("CVoucher", "0");
                sql.Add("BookMan", "nongyouautobook");
                sql.Add("BookTime", DateTime.Now.ToString("yyyy-MM-dd"));
                sql.Add("BookDate", AccountDate);
                CommClass.ExecuteSQL("cw_assetcard", sql);
            }
        }

        private void dzichan2(DataTable data)
        {
            DateTime accountDate = MainClass.GetAccountDate();
            string AccountDate = accountDate.ToString("yyyy-MM-dd");
            string ClassID = "";
            string ClassName = "";
            string UseLife = "0.0";
            string SVRate = "0";
            string JingCZ = "0";
            string DeprMethod = "0";
            string DeprSubject = SysConfigs.MonthDeprSubject;
            string MUnit = "";
            string DeptName = string.Format("{0}.{1}", UserInfo.AccountID, UserInfo.AccountName);
            int[] splist = { 15102, 15103, 15104, 15105 };
            int[] snolist = { 15102, 15103, 15104, 15105 };
            int sno = 0;
            if (CheckBox1.Checked)
            {
                for (int n = 0; n < splist.Length; n++)
                {
                    int def = splist[n] * 10000 + 1;
                    string pnos = CommClass.GetTableValue("cw_subject", "subjectno", "parentno='" + splist[n].ToString() + "' and subjectno<>'" + splist[n].ToString() + "9999' order by subjectno desc", def.ToString());
                    sno = TypeParse.StrToInt(pnos, def);
                    snolist[n] = sno;
                }
            }
            for (int i = 7; i < data.Rows.Count; i++)
            {
                string firstCellText = data.Rows[i][0].ToString().Replace("\n", "").Trim();
                if (firstCellText == "小计" || firstCellText == "合计")
                {
                    break;
                }
                string secodCellText = data.Rows[i][1].ToString().Replace("\n", "").Replace(" ", "");
                if (secodCellText.Length == 0 || secodCellText == "小计" || secodCellText == "合计")
                {
                    continue;
                }
                if ("一二三四五六七八九十".Contains(firstCellText))
                {
                    switch (secodCellText)
                    {
                        case "生产设施设备":
                            ClassID = "151029999";
                            sno = 0;
                            break;
                        case "运输工具":
                            ClassID = "151039999";
                            sno = 1;
                            break;
                        case "公益设施设备":
                            ClassID = "151049999";
                            sno = 2;
                            break;
                        case "办公设备":
                            ClassID = "151059999";
                            sno = 3;
                            break;
                        default:
                            ClassID = "15199";
                            continue;
                    }
                    if (ClassID.Length > 0)
                    {
                        ClassName = MainClass.GetTableValue("cw_subject", "subjectname", "subjectno='" + ClassID + "'");
                    }
                    continue;
                }
                if (ClassID.Length == 0)
                {
                    continue;
                }
                //资产类别参数
                string OldPrice = data.Rows[i][14].ToString();
                decimal oldPrice = TypeParse.StrToDecimal(OldPrice, 0);
                if (oldPrice <= 0)
                {
                    continue;
                }
                string AAmount = data.Rows[i][13].ToString();
                decimal amount = TypeParse.StrToDecimal(AAmount, 0);
                decimal APrice = 0;
                if (amount != 0)
                {
                    APrice = oldPrice / amount;
                    APrice = Math.Round(APrice, 2);
                }
                if (CheckBox1.Checked)
                {
                    snolist[sno] = snolist[sno] + 1;
                    ClassID = snolist[sno].ToString();
                    ClassName = secodCellText;
                    CommClass.ExecuteSQL("insert into cw_subject(id,parentno,subjectno,subjectname,subjecttype,"
                        + "AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct)values('"
                        + CommClass.GetRecordID("CW_Subject") + "','" + splist[sno].ToString() + "','"
                        + ClassID + "','" + ClassName + "','1','0','0','0','1','0')");
                    CommClass.ExecuteSQL("update cw_subject set IsDetail='0' where subjectno='" + splist[sno].ToString() + "'");
                }
                //创建资产卡片
                Dictionary<string, string> sql = new Dictionary<string, string>();
                sql.Add("ID", CommClass.GetRecordID("CW_AssetID"));
                sql.Add("CardID", CommClass.GetRecordID("CW_AssetCard"));
                sql.Add("AssetNo", CommClass.GetRecordID("CW_AssetNo"));
                sql.Add("AssetName", secodCellText);
                sql.Add("ClassID", ClassID);
                sql.Add("CName", ClassName);
                sql.Add("AssetModel", data.Rows[i][3].ToString());
                sql.Add("DeptName", DeptName);
                sql.Add("AddType", "");
                sql.Add("Depositary", data.Rows[i][2].ToString());
                sql.Add("UseState", "101");
                sql.Add("UseLife", UseLife);
                sql.Add("DeprMethod", DeprMethod);
                sql.Add("SUseDate", data.Rows[i][4].ToString());
                sql.Add("UsedMonths", "0");
                sql.Add("CurrencyType", "人民币");
                sql.Add("OldPrice", OldPrice);
                sql.Add("OldPrice0", OldPrice);
                sql.Add("JingCZL", SVRate);
                sql.Add("JingCZ", JingCZ);
                decimal zhejiu = TypeParse.StrToDecimal(data.Rows[i][15].ToString(), 0);
                sql.Add("ZheJiu", zhejiu.ToString());
                sql.Add("ZheJiu0", zhejiu.ToString());
                sql.Add("MonthZJL", "0");
                sql.Add("MonthZJE", "0");
                decimal newprice = TypeParse.StrToDecimal(data.Rows[i][16].ToString(), 0);
                sql.Add("NewPrice", newprice.ToString());
                sql.Add("DeprSubject", DeprSubject);
                sql.Add("AssetItem", "");
                sql.Add("AUnit", MUnit);
                sql.Add("AAmount", AAmount);
                sql.Add("HasAmount", "0");
                sql.Add("APrice", APrice.ToString());
                sql.Add("APicture", "");
                sql.Add("AssetAdmin", "");
                sql.Add("DeprState", "0");
                sql.Add("AssetState", "0");
                sql.Add("CVoucher", "0");
                sql.Add("BookMan", "nongyouautobook");
                sql.Add("BookTime", DateTime.Now.ToString("yyyy-MM-dd"));
                sql.Add("BookDate", AccountDate);
                CommClass.ExecuteSQL("cw_assetcard", sql);
            }
        }

        private void dziyuan(DataTable data)
        {
            string classID = string.Empty;
            string className = string.Empty;
            string DeptName = string.Format("{0}.{1}", UserInfo.AccountID, UserInfo.AccountName);
            for (int i = 6; i < data.Rows.Count; i++)
            {
                string firstCellText = data.Rows[i][0].ToString().Replace("\n", "").Trim();
                if (firstCellText == "小计" || firstCellText == "合计")
                {
                    break;
                }
                string secodCellText = data.Rows[i][1].ToString().Replace("\n", "").Replace(" ", "");
                if (secodCellText == "小计" || secodCellText == "合计")
                {
                    continue;
                }
                if ("一二三四五六七八九十".Contains(firstCellText))
                {
                    string cl = MainClass.GetTableValue("cw_resclass", "id", "ClassName='" + secodCellText + "'", "");
                    if (cl.Length > 0)
                    {
                        classID = cl;
                        className = secodCellText;
                    }
                    else
                    {
                        classID = "";
                    }
                    continue;
                }
                if (classID.Length == 0)
                {
                    continue;
                }
                decimal mianji = TypeParse.StrToDecimal(data.Rows[i][3].ToString(), 0);
                if (mianji <= 0)
                {
                    continue;
                }
                Dictionary<string, string> ResCard = new Dictionary<string, string>();
                ResCard.Add("ID", CommClass.GetRecordID("CW_ResourceID"));
                ResCard.Add("CardNo", CommClass.GetRecordID("CW_ResCard"));
                ResCard.Add("ResNo", CommClass.GetRecordID("CW_ResNo"));
                ResCard.Add("ResName", secodCellText);
                ResCard.Add("ClassID", classID);
                ResCard.Add("ClassName", className);
                ResCard.Add("ResUnit", "亩");
                ResCard.Add("ResAmount", mianji.ToString());
                ResCard.Add("HasAmount", "0");
                ResCard.Add("DeptName", DeptName);
                ResCard.Add("Locality", data.Rows[i][2].ToString());
                decimal area = TypeParse.StrToDecimal(data.Rows[i][4].ToString(), 0);
                if (area > 0)
                {
                    ResCard.Add("BookType", "发包");
                }
                else
                {
                    ResCard.Add("BookType", "其他");
                }
                ResCard.Add("UsedState", "0");
                ResCard.Add("Booker", "nongyouautobook");
                ResCard.Add("BookTime", DateTime.Now.ToString());
                ResCard.Add("BookDate", DateTime.Now.ToString("yyyy-MM-dd"));
                try
                {
                    for (int k = 4; k <= 18; k++)
                    {
                        ResCard.Add("name" + k.ToString(), data.Rows[i][k].ToString());
                    }
                }
                catch { }
                CommClass.ExecuteSQL("CW_ResCard", ResCard);
            }
        }
    }
}