using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Threading;
using System.Configuration;
using System.Data;

namespace sdql.Old_App_Code
{
    public class ExportToExcel
    {
        private static HSSFWorkbook hssfworkbook;
        protected static string mainPath = ConfigurationManager.AppSettings["uploadFilePath"];
        protected static string yangbiaopath = ConfigurationManager.AppSettings["yangbiaoFilePath"];
        private  string templates = "TemplatesCity.xls";
        private  Dictionary<string, int> dic = new Dictionary<string, int>();
        public  string Templates
        {
            set { templates = value; }
            get { return templates; }
        }
        public void InitializeWorkbook()
        {
            string templateFile = Templates; //yangbiaopath + "/" + Templates;
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(@"" + templateFile), FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "山东农友软件有限公司";
            hssfworkbook.DocumentSummaryInformation = dsi;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "农村集体经济组织清查清产核资管理系统";
            hssfworkbook.SummaryInformation = si;
            AddCommArgs();
        }

        public void AddCommArgs()
        {
            dic.Add("1", 65000);
            dic.Add("2", 19);
            dic.Add("3", 20);
            dic.Add("4", 15);
            dic.Add("5", 23);
            dic.Add("6", 19);
            dic.Add("7", 18);
            dic.Add("8", 16);
            dic.Add("9", 15);
            dic.Add("10", 17);
            dic.Add("11", 28);
            dic.Add("12", 37);
            dic.Add("13", 24);
            dic.Add("14", 21);
            dic.Add("15", 21);
            dic.Add("16", 18);
            dic.Add("17", 20);
            dic.Add("18", 30);
            dic.Add("19", 16);
            dic.Add("20", 20);
        }

        private void ProExcelHeader(Sheet sheetHeader, int rowIndex,int mergeColumnCount,string strname,DateTime sj,string ExcelIndex)
        {
            HSSFFont font = (HSSFFont)hssfworkbook.CreateFont();
            font.Underline = HSSFFontFormatting.U_NONE;
            font.FontHeight = 240;
            HSSFCellStyle style1 = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            style1.Alignment = HorizontalAlignment.CENTER;
            style1.VerticalAlignment = VerticalAlignment.CENTER;
            style1.SetFont(font);
            sheetHeader.AddMergedRegion(new CellRangeAddress(rowIndex-1, rowIndex-1, 0, mergeColumnCount));
            string headerTitle = string.Empty;
            if ((int.Parse(ExcelIndex).CompareTo(11) > 0 && int.Parse(ExcelIndex).CompareTo(19) < 0) || int.Parse(ExcelIndex).CompareTo(1) == 0)
               headerTitle = strname.PadLeft(10, ' ') + " ".ToString().PadRight(20, ' ') + sj.ToString("yyyy年MM月dd日") + "单位：元".PadLeft(20, ' ');
            else
               headerTitle = strname.PadLeft(30, ' ') + " ".ToString().PadRight(80, ' ') + sj.ToString("yyyy年MM月dd日") + "单位：元".PadLeft(74, ' ');
            if(int.Parse(ExcelIndex).CompareTo(7)==0)
                headerTitle = strname.PadLeft(30, ' ') + " ".ToString().PadRight(80, ' ') + sj.ToString("yyyy年MM月dd日") ;
            HSSFRow hssRow = (HSSFRow)sheetHeader.GetRow(rowIndex - 1);
            hssRow.GetCell(0).SetCellValue(headerTitle.ToString());
            hssRow.GetCell(0).CellStyle = style1;
        }

        private void MyInsertRow(HSSFSheet sheet, int InsertRowNum, int InsertRowCount, HSSFRow sourceRow)
        {
            //批量移动行 开始行 结束行 移动大小(行数)往下移动 是否复制行高 是否重置行高 是否移动批注
            sheet.ShiftRows(InsertRowNum,sheet.LastRowNum,InsertRowCount,true,false, true);

            #region 对批量移动后空出的空行插，创建相应的行，并以InsertRowNum的上一行为格式源(即：InsertRowNum-1的那一行)
            for (int i = InsertRowNum; i < InsertRowNum + InsertRowCount ; i++)//- 1
            {
                HSSFRow targetRow = null;
                HSSFCell sourceCell = null;
                HSSFCell targetCell = null;

                targetRow = (HSSFRow)sheet.CreateRow(i + 1);
                targetRow.Height = sourceRow.Height;
                for (int m = sourceRow.FirstCellNum; m < sourceRow.LastCellNum; m++)
                {
                    sourceCell =(HSSFCell)sourceRow.GetCell(m);
                    if (sourceCell == null)
                        continue;
                    targetCell = (HSSFCell)targetRow.CreateCell(m);
                    targetCell.CellStyle = sourceCell.CellStyle;
                    
                    targetCell.SetCellType(sourceCell.CellType);
                }
            }
            HSSFRow firstTargetRow = (HSSFRow)sheet.GetRow(InsertRowNum);
            HSSFCell firstSourceCell = null;
            HSSFCell firstTargetCell = null;
            for (int m = sourceRow.FirstCellNum; m < sourceRow.LastCellNum; m++)
            {
                firstSourceCell = (HSSFCell)sourceRow.GetCell(m);
                if (firstSourceCell == null)
                    continue;
                firstTargetCell = (HSSFCell)firstTargetRow.CreateCell(m);
                firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                firstTargetCell.SetCellType(firstSourceCell.CellType);
            }
            #endregion
        }
        //非通用
        public void ExportExcel_NoComm(string str_FilePath, int RowIndex, int ColumnIndex, DataTable dt, string yblb, string strname, DateTime sj, string[] args)
        {
            string chengpath = string.Empty;
            try
            {
                InitializeWorkbook();
                Sheet sheet1 = hssfworkbook.GetSheetAt(0);
                sheet1.DisplayGridlines = true;
                ProExcelHeader(sheet1, int.Parse(args[0]), int.Parse(args[1]), strname, sj, yblb);
                HSSFRow hssRow = (HSSFRow)sheet1.GetRow(4);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][0]));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][1]));
                hssRow = (HSSFRow)sheet1.GetRow(5);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][2] + "   元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][3] + "   元"));
                hssRow = (HSSFRow)sheet1.GetRow(6);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][4].ToString() + " 笔" + dt.Rows[0][5].ToString()+" 元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][6].ToString() + " 笔" + dt.Rows[0][7].ToString() + " 元"));
                hssRow = (HSSFRow)sheet1.GetRow(7);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][8].ToString() + " 笔" + dt.Rows[0][9].ToString() + " 元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][10].ToString() + " 笔" + dt.Rows[0][11].ToString() + " 元"));
                hssRow = (HSSFRow)sheet1.GetRow(8);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][12].ToString() + "   元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][13].ToString() + "   元"));
                hssRow = (HSSFRow)sheet1.GetRow(9);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][14].ToString() + "   元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][15].ToString() + "   元"));
                hssRow = (HSSFRow)sheet1.GetRow(10);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][16].ToString() + " 张" + dt.Rows[0][17].ToString() + " 元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][18].ToString() + " 张" + dt.Rows[0][19].ToString() + " 元"));
                hssRow = (HSSFRow)sheet1.GetRow(11);
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][20].ToString() + " 笔" + dt.Rows[0][21].ToString() + " 元"));
                hssRow = (HSSFRow)sheet1.GetRow(12);
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][22].ToString() + "   元"));
                hssRow = (HSSFRow)sheet1.GetRow(13);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][23].ToString() + "   元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][24].ToString() + "   元"));
                hssRow = (HSSFRow)sheet1.GetRow(14);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][25].ToString() + "   元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][26].ToString() + "   元"));
                hssRow = (HSSFRow)sheet1.GetRow(15);
                hssRow.GetCell(2).SetCellValue(Convert.ToString(dt.Rows[0][27].ToString() + "   元"));
                hssRow.GetCell(5).SetCellValue(Convert.ToString(dt.Rows[0][28].ToString() + "   元"));
                WriteToFile(str_FilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExportExcel_Comm1(string str_FilePath, int RowIndex, int ColumnIndex, DataTable dt, string yblb, string strname, DateTime sj, string[] args)
        {
            string chengpath = string.Empty;
            try
            {
                InitializeWorkbook();
                Sheet sheet1 = hssfworkbook.GetSheetAt(0);
                sheet1.DisplayGridlines = true;
                int rowIndex = RowIndex - 1;
                int columnIndex = ColumnIndex - 1;
                int WriteRowCurrent = 0;
                int tempPos = 0;
                string threeorthirteen = string.Empty;
                bool OutStack = false;
                int i0 = int.Parse(args[3].Split(',')[0]) - 1;
                int i1 = int.Parse(args[3].Split(',')[1]) - 1;
                int i2 = int.Parse(args[4].Split(',')[0]) - 1;
                int i3 = int.Parse(args[4].Split(',')[1]) - 1;
                if (args[2] != null && args[2].Length > 0)
                {
                    tempPos = dic[yblb];
                    dic[yblb] = int.Parse(args[2]);     
                }
                ProExcelHeader(sheet1, int.Parse(args[0]), int.Parse(args[1]), strname, sj, yblb);
                if (tempPos == 0)
                {
                    tempPos = dic[yblb];
                }
                if ((tempPos - rowIndex).CompareTo(dt.Rows.Count) < 0)
                {
                    WriteRowCurrent = tempPos - rowIndex;
                    OutStack = true;
                }
                else
                {
                    WriteRowCurrent = dt.Rows.Count;
                }              
                int k = WriteRowCurrent - 1;
                for (int i = 0; i < WriteRowCurrent - 1; i++)
                {
                    HSSFRow hssRow = (HSSFRow)sheet1.GetRow(rowIndex);
                    columnIndex = ColumnIndex - 1;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string tempstring = Convert.ToString(dt.Rows[i][columnIndex]);
                        hssRow.GetCell(columnIndex).SetCellValue(tempstring);
                        columnIndex++;
                    }
                    rowIndex++;
                }
                if (OutStack)
                {
                    HSSFRow hssRow1 = (HSSFRow)sheet1.GetRow(rowIndex - 1);
                    MyInsertRow((HSSFSheet)sheet1, rowIndex, dt.Rows.Count - WriteRowCurrent, hssRow1);//rowIndex
                    for (int i = WriteRowCurrent - 1; i < dt.Rows.Count; i++)
                    {
                        HSSFRow hssRow = (HSSFRow)sheet1.GetRow(rowIndex);
                        columnIndex = ColumnIndex - 1;
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            hssRow.GetCell(columnIndex).SetCellValue(Convert.ToString(dt.Rows[i][columnIndex]));
                            columnIndex++;
                        }
                        rowIndex++;
                    }
                }
                else
                {
                    HSSFRow hssRow = (HSSFRow)sheet1.GetRow(dic[yblb] - 1);
                    columnIndex = ColumnIndex - 1;
                    switch (yblb)
                    {
                        case "2":
                        case "6":
                            columnIndex = 4;
                            break;
                        case "5":
                        case "11":
                            columnIndex = 5;
                            break;
                        case "7":
                            columnIndex = 2;
                            break;
                        case "3":
                        case "12":
                            columnIndex = 3;
                            break;
                        default:
                            for (int j = columnIndex; j < dt.Columns.Count; j++)
                            {
                                hssRow.GetCell(columnIndex).SetCellValue(Convert.ToString(dt.Rows[k][columnIndex]));
                                columnIndex++;
                            }
                            rowIndex++;
                            columnIndex = 10000;
                            break;
                    }
                    if (columnIndex < 10000)
                    {
                        hssRow.GetCell(0).SetCellValue(Convert.ToString(dt.Rows[k][0]));
                        for (int j = columnIndex; j < dt.Columns.Count; j++)
                        {
                            hssRow.GetCell(columnIndex).SetCellValue(Convert.ToString(dt.Rows[k][columnIndex]));
                            columnIndex++;
                        }
                        rowIndex++;
                    }
                }
                sheet1.AddMergedRegion(new CellRangeAddress(i0, i1, 0, 0));
                sheet1.AddMergedRegion(new CellRangeAddress(i2, i3, 0, 0));
                //sheet1.ForceFormulaRecalculation = true;//强制要求Excel在打开时重新计算的属性 ,对公式有用
                WriteToFile(str_FilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //通用 4、8、9、10、14、15、16 ok
        //通用 2、5、6、7、11、12、 ok
        //通用 3、13 
        //1、17死表 
        public void ExportExcel_Comm(string str_FilePath, int RowIndex, int ColumnIndex, DataTable dt, string yblb, string strname, DateTime sj, string[] args)
        {
            string chengpath = string.Empty;
            InitializeWorkbook();
            Sheet sheet1 = hssfworkbook.GetSheetAt(0);
            sheet1.DisplayGridlines = true;
            int rowIndex = RowIndex - 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HSSFRow hssRow = (HSSFRow)sheet1.CreateRow(rowIndex);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string tempstring = Convert.ToString(dt.Rows[i][j]);
                    HSSFCell sourceCell = (HSSFCell)hssRow.CreateCell(j);
                    sourceCell.SetCellValue(tempstring);
                }
                rowIndex++;
            }
            WriteToFile(str_FilePath);
        }

        private static void WriteToFile(string OutputFileName)
        {
            //string templateFile = mainPath + "/" + OutputFileName;
            string fileName = HttpContext.Current.Server.MapPath(OutputFileName);
            FileStream file = new FileStream(fileName, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
            
            FileStream fs = new FileStream(fileName, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            string fname = System.IO.Path.GetFileName(fileName);
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fname, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            //Thread thread = new Thread(new ParameterizedThreadStart(del));
            //thread.Start(file.Name);
        }

        //------------------------------------------------------以下未用---------------------------------------------------
        #region 未用
        public HSSFCellStyle StyleReturn()
        {
            HSSFCellStyle style = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.BorderLeft = NPOI.SS.UserModel.CellBorderType.THIN;
            style.BorderTop = NPOI.SS.UserModel.CellBorderType.THIN;
            style.BorderRight = NPOI.SS.UserModel.CellBorderType.THIN;
            style.BorderBottom = NPOI.SS.UserModel.CellBorderType.THIN;
            style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.BLACK.index;
            style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.BLACK.index;
            style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.BLACK.index;
            style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.BLACK.index;
            return style;
        }

        public string Checkstring(string scheck)
        {
            scheck = scheck.Replace("&nbsp;", "").Replace("<center>", "").Replace("</center>", "");
            return scheck;
        }

        public string MapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用 
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    //strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\'); 
                    strPath = strPath.TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        private void del(object spath)
        {
            Thread.Sleep(150000);//1.5秒就删除 晕 加两个0 1500秒，二分钟
            string DirectoryPath = MapPath("Templates");
            DirectoryInfo MainDbDir = new DirectoryInfo(DirectoryPath);
            FileInfo f = new FileInfo(spath.ToString());
            if (MainDbDir.Exists)
            {
                foreach (FileInfo file in MainDbDir.GetFiles())
                {
                    if (file.Name != f.Name && file.Name != "no.txt" && file.Name != "EXCEL.xls" && file.Name != "template.xls" && file.Name != "TemplatesCity.xls" && file.Name != "TemplatesCountry.xls" && file.Name != "TemplatesCounty.xls")
                    {
                        file.Delete();
                    }
                }
            }
        }
        #endregion
    }
}