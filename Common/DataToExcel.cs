using System;
using System.Diagnostics;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace LTP.Common
{
    /// <summary>
    /// 操作EXCEL导出数据报表的类
    /// </summary>
    public class DataToExcel
    {
        public DataToExcel()
        {
            Excel.Application app = new Excel.Application();
        }

        #region 操作EXCEL的一个类(需要Excel.dll支持)

        private int titleColorindex = 15;
        /// <summary>
        /// 标题背景色
        /// </summary>
        public int TitleColorIndex
        {
            set { titleColorindex = value; }
            get { return titleColorindex; }
        }

        private DateTime beforeTime;			//Excel启动之前时间
        private DateTime afterTime;				//Excel启动之后时间

        #region 创建一个Excel示例
        /// <summary>
        /// 创建一个Excel示例
        /// </summary>
        public static void CreateExcel()
        {
            Excel.Application excel = new Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Cells[1, 1] = "第1行第1列";
            excel.Cells[1, 2] = "第1行第2列";
            excel.Cells[2, 1] = "第2行第1列";
            excel.Cells[2, 2] = "第2行第2列";
            excel.Cells[3, 1] = "第3行第1列";
            excel.Cells[3, 2] = "第3行第2列";

            //保存
            
            excel.ActiveWorkbook.SaveAs(@"E:\tt.xls", Excel.XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            //打开显示
            excel.Visible = true;
            //			excel.Quit();
            //			excel=null;            
            //			GC.Collect();//垃圾回收
        }
        #endregion

        #region 将DataTable的数据导出显示为报表
        /// <summary>
        /// 将DataTable的数据导出显示为报表
        /// </summary>
        /// <param name="dt">要导出的数据</param>
        /// <param name="strTitle">导出报表的标题</param>
        /// <param name="FilePath">保存文件的路径</param>
        /// <returns></returns>
        public string OutputExcel(System.Data.DataTable dt, string strTitle)
        {
            //string FilePath = "";//路径
            //FilePath = "./";//GetUserDesktopPath();
            beforeTime = DateTime.Now;

            Excel.Application excel;
            Excel._Workbook xBk;
            Excel._Worksheet xSt;

            int rowIndex = 1;
            int colIndex = 1;

            excel = new Excel.Application();
            xBk = excel.Workbooks.Add(true);
            xSt = (Excel._Worksheet)xBk.ActiveSheet;

            //取得列标题			
            foreach (DataColumn col in dt.Columns)
            {
                excel.Cells[1, colIndex] = col.ColumnName;

                //设置标题格式为居中对齐
                xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex]).Font.Bold = true;
                xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex]).Select();
                xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[1, colIndex]).Interior.ColorIndex = titleColorindex;//19;//设置为浅黄色，共计有56种
                colIndex++;
            }


            //取得表格中的数据			
            foreach (DataRow row in dt.Rows)
            {
                rowIndex++;
                colIndex = 1;
                foreach (DataColumn col in dt.Columns)
                {
                    
                    if (col.DataType == System.Type.GetType("System.DateTime"))
                    {
                        excel.Cells[rowIndex, colIndex] = (Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
                        xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置日期型的字段格式为居中对齐
                    }
                    else
                        if (col.DataType == System.Type.GetType("System.String"))
                        {
                            excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                            xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置字符型的字段格式为居中对齐
                        }
                        else
                        {
                            excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                        }

                    colIndex++;
                }
            }

            //加载一个合计行			
            int rowSum = rowIndex ;
            //int colSum = 2;
            //excel.Cells[rowSum, 2] = "合计";
            //xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //设置选中的部分的颜色			
            //xSt.get_Range(excel.Cells[rowSum, colSum], excel.Cells[rowSum, colIndex]).Select();
            //xSt.get_Range(excel.Cells[rowSum,colSum],excel.Cells[rowSum,colIndex]).Interior.ColorIndex =Assistant.GetConfigInt("ColorIndex");// 1;//设置为浅黄色，共计有56种

            //取得整个报表的标题			
            //excel.Cells[2, 2] = strTitle;

            //设置整个报表的标题格式			
            //xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Bold = true;
            //xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Size = 22;

            //设置报表表格为最适应宽度			
            //xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Select();
            //xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Columns.AutoFit();

            //设置整个报表的标题为跨列居中			
            //xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).Select();
            //xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenterAcrossSelection;

            //绘制边框	
            colIndex = colIndex - 1;
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowSum, colIndex]).Borders.LineStyle = 1;
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowSum, 1]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;//设置左边线加粗
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[4, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;//设置上边线加粗
            xSt.get_Range(excel.Cells[1, colIndex], excel.Cells[rowSum, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;//设置右边线加粗
            xSt.get_Range(excel.Cells[rowSum, 1], excel.Cells[rowSum, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;//设置下边线加粗



            afterTime = DateTime.Now;

            //显示效果			
            //excel.Visible=true;			
            //excel.Sheets[0] = "Users";

            //ClearFile(FilePath);
            string filename = strTitle + DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss") + ".xls";//FilePath + filename
            excel.ActiveWorkbook.SaveAs(HttpContext.Current.Server.MapPath(strTitle), Excel.XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);

            //wkbNew.SaveAs strBookName;
            //excel.Save(strExcelFileName);

            #region  结束Excel进程

            //需要对Excel的DCOM对象进行配置:dcomcnfg


            //excel.Quit();
            //excel=null;            

            xBk.Close(null, null, null);
            excel.Workbooks.Close();
            excel.Quit();


            //注意：这里用到的所有Excel对象都要执行这个操作，否则结束不了Excel进程
            //			if(rng != null)
            //			{
            //				System.Runtime.InteropServices.Marshal.ReleaseComObject(rng);
            //				rng = null;
            //			}
            //			if(tb != null)
            //			{
            //				System.Runtime.InteropServices.Marshal.ReleaseComObject(tb);
            //				tb = null;
            //			}
            if (xSt != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
                xSt = null;
            }
            if (xBk != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
                xBk = null;
            }
            if (excel != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                excel = null;
            }
            GC.Collect();//垃圾回收
            #endregion
            KillExcelProcess();
            return filename;

        }
        #endregion

        #region Kill Excel进程

        /// <summary>
        /// 结束Excel进程
        /// </summary>
        public void KillExcelProcess()
        {
            Process[] myProcesses;
            DateTime startTime;
            myProcesses = Process.GetProcessesByName("Excel");

            //得不到Excel进程ID，暂时只能判断进程启动时间
            foreach (Process myProcess in myProcesses)
            {
                startTime = myProcess.StartTime;
                if (startTime > beforeTime && startTime < afterTime)
                {
                    myProcess.Kill();
                }
            }
        }
        #endregion

        #endregion

        //#region 将DataTable的数据导出显示为报表(不使用Excel对象，使用COM.Excel)

        //#region 使用示例
        ///*使用示例：
        // * DataSet ds=(DataSet)Session["AdBrowseHitDayList"];
        //    string ExcelFolder=Assistant.GetConfigString("ExcelFolder");
        //    string FilePath=Server.MapPath(".")+"\\"+ExcelFolder+"\\";
			
        //    //生成列的中文对应表
        //    Hashtable nameList = new Hashtable();
        //    nameList.Add("ADID", "广告编码");
        //    nameList.Add("ADName", "广告名称");
        //    nameList.Add("year", "年");
        //    nameList.Add("month", "月");
        //    nameList.Add("browsum", "显示数");
        //    nameList.Add("hitsum", "点击数");
        //    nameList.Add("BrowsinglIP", "独立IP显示");
        //    nameList.Add("HitsinglIP", "独立IP点击");
        //    //利用excel对象
        //    DataToExcel dte=new DataToExcel();
        //    string filename="";
        //    try
        //    {			
        //        if(ds.Tables[0].Rows.Count>0)
        //        {
        //            filename=dte.DataExcel(ds.Tables[0],"标题",FilePath,nameList);
        //        }
        //    }
        //    catch
        //    {
        //        //dte.KillExcelProcess();
        //    }
			
        //    if(filename!="")
        //    {
        //        Response.Redirect(ExcelFolder+"\\"+filename,true);
        //    }
        // * 
        // * */

        //#endregion

        ///// <summary>
        ///// 将DataTable的数据导出显示为报表(不使用Excel对象)
        ///// </summary>
        ///// <param name="dt">数据DataTable</param>
        ///// <param name="strTitle">标题</param>
        ///// <param name="FilePath">生成文件的路径</param>
        ///// <param name="nameList"></param>
        ///// <returns></returns>
        //public string DataExcel(System.Data.DataTable dt, string strTitle, string FilePath, Hashtable nameList)
        //{
        //    Excel.Application excel = new Excel.Application();
        //    ClearFile(FilePath);
        //    string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
        //    excel.CreateFile(FilePath + filename);
        //    excel.PrintGridLines = false;

        //    COM.Excel.cExcelFile.MarginTypes mt1 = COM.Excel.cExcelFile.MarginTypes.xlsTopMargin;
        //    COM.Excel.cExcelFile.MarginTypes mt2 = COM.Excel.cExcelFile.MarginTypes.xlsLeftMargin;
        //    COM.Excel.cExcelFile.MarginTypes mt3 = COM.Excel.cExcelFile.MarginTypes.xlsRightMargin;
        //    COM.Excel.cExcelFile.MarginTypes mt4 = COM.Excel.cExcelFile.MarginTypes.xlsBottomMargin;

        //    double height = 1.5;
        //    excel.SetMargin(ref mt1, ref height);
        //    excel.SetMargin(ref mt2, ref height);
        //    excel.SetMargin(ref mt3, ref height);
        //    excel.SetMargin(ref mt4, ref height);

        //    COM.Excel.cExcelFile.FontFormatting ff = COM.Excel.cExcelFile.FontFormatting.xlsNoFormat;
        //    string font = "宋体";
        //    short fontsize = 9;
        //    excel.SetFont(ref font, ref fontsize, ref ff);

        //    byte b1 = 1,
        //        b2 = 12;
        //    short s3 = 12;
        //    excel.SetColumnWidth(ref b1, ref b2, ref s3);

        //    string header = "页眉";
        //    string footer = "页脚";
        //    excel.SetHeader(ref header);
        //    excel.SetFooter(ref footer);


        //    COM.Excel.cExcelFile.ValueTypes vt = COM.Excel.cExcelFile.ValueTypes.xlsText;
        //    COM.Excel.cExcelFile.CellFont cf = COM.Excel.cExcelFile.CellFont.xlsFont0;
        //    COM.Excel.cExcelFile.CellAlignment ca = COM.Excel.cExcelFile.CellAlignment.xlsCentreAlign;
        //    COM.Excel.cExcelFile.CellHiddenLocked chl = COM.Excel.cExcelFile.CellHiddenLocked.xlsNormal;

        //    // 报表标题
        //    int cellformat = 1;
        //    //			int rowindex = 1,colindex = 3;					
        //    //			object title = (object)strTitle;
        //    //			excel.WriteValue(ref vt, ref cf, ref ca, ref chl,ref rowindex,ref colindex,ref title,ref cellformat);

        //    int rowIndex = 1;//起始行
        //    int colIndex = 0;



        //    //取得列标题				
        //    foreach (DataColumn colhead in dt.Columns)
        //    {
        //        colIndex++;
        //        string name = colhead.ColumnName.Trim();
        //        object namestr = (object)name;
        //        IDictionaryEnumerator Enum = nameList.GetEnumerator();
        //        while (Enum.MoveNext())
        //        {
        //            if (Enum.Key.ToString().Trim() == name)
        //            {
        //                namestr = Enum.Value;
        //            }
        //        }
        //        excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref namestr, ref cellformat);
        //    }

        //    //取得表格中的数据			
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        rowIndex++;
        //        colIndex = 0;
        //        foreach (DataColumn col in dt.Columns)
        //        {
        //            colIndex++;
        //            if (col.DataType == System.Type.GetType("System.DateTime"))
        //            {
        //                object str = (object)(Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd"); ;
        //                excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref str, ref cellformat);
        //            }
        //            else
        //            {
        //                object str = (object)row[col.ColumnName].ToString();
        //                excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref str, ref cellformat);
        //            }
        //        }
        //    }
        //    int ret = excel.CloseFile();

        //    //			if(ret!=0)
        //    //			{
        //    //				//MessageBox.Show(this,"Error!");
        //    //			}
        //    //			else
        //    //			{
        //    //				//MessageBox.Show(this,"请打开文件c:\\test.xls!");
        //    //			}
        //    return filename;

        //}

        //#endregion

        #region  清理过时的Excel文件

        private void ClearFile(string FilePath)
        {
            String[] Files = System.IO.Directory.GetFiles(FilePath);
            if (Files.Length > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        System.IO.File.Delete(Files[i]);
                    }
                    catch
                    {
                    }

                }
            }
        }
        #endregion

        public static void DataSetToExcel(DataSet ds, string FileName)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            //string strFileName = ConvertUTF8ToGb2312(FileName);

            try
            {
                //Web页面定义 
                //System.Web.UI.Page mypage=new System.Web.UI.Page(); 

                HttpResponse resp;
                resp = HttpContext.Current.Response;
                resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                resp.AppendHeader("Content-disposition", "attachment;filename=" + FileName + ".xls");
                resp.ContentType = "application/ms-excel";

                //变量定义 
                string colHeaders = null;
                string Is_item = null;

                //显示格式定义//////////////// 


                //文件流操作定义 
                //FileStream fs=new FileStream(FileName,FileMode.Create,FileAccess.Write); 
                //StreamWriter sw=new StreamWriter(fs,System.Text.Encoding.GetEncoding("GB2312")); 

                StringWriter sfw = new StringWriter();
                //定义表对象与行对象，同时用DataSet对其值进行初始化 
                System.Data.DataTable dt = ds.Tables[0];
                DataRow[] myRow = dt.Select();
                int i = 0;
                int cl = dt.Columns.Count;

                //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符 
                for (i = 0; i < cl; i++)
                {
                    //if(i==(cl-1))  //最后一列，加\n 
                    // colHeaders+=dt.Columns[i].Caption.ToString(); 
                    //else 
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                }
                sfw.WriteLine(colHeaders);
                //sw.WriteLine(colHeaders); 

                //逐行处理数据 
                foreach (DataRow row in myRow)
                {
                    //当前数据写入 
                    for (i = 0; i < cl; i++)
                    {
                        //if(i==(cl-1)) 
                        //   Is_item+=row[i].ToString()+"\n"; 
                        //else 
                        Is_item += row[i].ToString() + "\t";
                    }
                    sfw.WriteLine(Is_item);
                    //sw.WriteLine(Is_item); 
                    Is_item = null;
                }
                resp.Write(sfw);
                //resp.Clear(); 
                resp.End();
            }
            catch (Exception e)
            {
                throw e;
            } 

        }

        public static string ConvertUTF8ToGb2312(string str)
        {
            byte[] utf8bytes = Encoding.Default.GetBytes(str);
            byte[] gb2312bytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("GB2312"), utf8bytes);
            string result = Encoding.GetEncoding("GB2312").GetString(gb2312bytes);
            return result;
        }

        /// <summary>
        /// 取用户桌面路径
        /// </summary>
        /// <returns></returns>
        private string GetUserDesktopPath()
        {
            RegistryKey folders;
            folders = OpenRegistryPath(Registry.CurrentUser, @"\software\microsoft\windows\currentversion\explorer\shell folders");
            // Windows用户桌面路径
            string desktopPath = folders.GetValue("Desktop").ToString();
            return desktopPath;
        }
        private RegistryKey OpenRegistryPath(RegistryKey root, string s)
        {
            s = s.Remove(0, 1) + @"\";
            while (s.IndexOf(@"\") != -1)
            {
                root = root.OpenSubKey(s.Substring(0, s.IndexOf(@"\")));
                s = s.Remove(0, s.IndexOf(@"\") + 1);
            }
            return root;
        }


    }
}
