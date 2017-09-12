using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace SanZi.Web.view.Users
{
    public partial class barcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UtilsPage.SetTextBoxAutoValue(TextBox1, "0");
                SanZi.BLL.Users bll = new SanZi.BLL.Users();
                DataTable users = bll.GetList("").Tables[0];
                foreach (DataRow urow in users.Rows)
                {
                    CheckBoxList1.Items.Add(new ListItem(urow["TrueName"].ToString(), urow["UserID"].ToString() + "." + urow["TitleName"].ToString()));
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string moban = Server.MapPath("../Images/file/三资监管平台权利人信息.xls");
            string fileName = Server.MapPath("../Images/file/三资监管平台权利人信息" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            File.Copy(moban, fileName, true);
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            Sheet sheet1 = hssfworkbook.GetSheetAt(0);

            int k = 1;
            int count = int.Parse(TextBox1.Text);
            string aname = UserInfo.AccountName;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                foreach (ListItem li in CheckBoxList1.Items)
                {
                    if (li.Selected)
                    {
                        string[] arr = li.Value.Split('.');
                        for (int m = 0; m < count; m++)
                        {
                            HSSFRow hssRow = (HSSFRow)sheet1.CreateRow(k);
                            HSSFCell cell0 = (HSSFCell)hssRow.CreateCell(0);
                            cell0.SetCellValue(aname);
                            HSSFCell cell1 = (HSSFCell)hssRow.CreateCell(1);
                            cell1.SetCellValue(arr[1]);
                            HSSFCell cell2 = (HSSFCell)hssRow.CreateCell(2);
                            cell2.SetCellValue(li.Text);
                            byte[] buffer = Guid.NewGuid().ToByteArray();
                            int bcode = BitConverter.ToInt32(buffer, 0);
                            bcode = Math.Abs(bcode);
                            HSSFCell cell3 = (HSSFCell)hssRow.CreateCell(3);
                            cell3.SetCellValue(bcode.ToString());
                            CommClass.ExecuteSQL(string.Concat("insert into cw_barcode(ID,UserID,barcode,usestate)values('",
                                CommClass.GetRecordID("cw_barcode"), "','", arr[0], "','", bcode.ToString(), "','0')"));
                            k++;
                        }
                    }
                }
            }
            else
            {
                foreach (ListItem li in CheckBoxList1.Items)
                {
                    if (li.Selected)
                    {
                        string[] arr = li.Value.Split('.');
                        DataTable bars = CommClass.GetDataTable("select barcode from cw_barcode where UserID='" + arr[0] + "' and usestate='0'");
                        for (int m = 0; m < bars.Rows.Count; m++)
                        {
                            HSSFRow hssRow = (HSSFRow)sheet1.CreateRow(k);
                            HSSFCell cell0 = (HSSFCell)hssRow.CreateCell(0);
                            cell0.SetCellValue(aname);
                            HSSFCell cell1 = (HSSFCell)hssRow.CreateCell(1);
                            cell1.SetCellValue(arr[1]);
                            HSSFCell cell2 = (HSSFCell)hssRow.CreateCell(2);
                            cell2.SetCellValue(li.Text);
                            HSSFCell cell3 = (HSSFCell)hssRow.CreateCell(3);
                            cell3.SetCellValue(bars.Rows[m]["barcode"].ToString());
                            k++;
                        }
                    }
                }
            }

            file = new FileStream(fileName, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();

            file = new FileStream(fileName, FileMode.Open);
            byte[] bytes = new byte[(int)file.Length];
            file.Read(bytes, 0, bytes.Length);
            file.Close();

            string fname = System.IO.Path.GetFileName(fileName);
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fname, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}