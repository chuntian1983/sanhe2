using System;
using System.Data;

namespace LTP.Common
{
    public class LogicLayer
    {
        public void OutputExcel(DataView dv,string str)
         {
          //
          // TODO: 在此处添加构造函数逻辑
          //
          Excel.Application excel;
          int rowIndex=4;
          int colIndex=1;

          Excel._Workbook xBk;
          Excel._Worksheet xSt;

          excel = new Excel.Application(); ;
          xBk = excel.Workbooks.Add(true);
          xSt = (Excel._Worksheet)xBk.ActiveSheet;

          //
          //取得标题
          //
          foreach(DataColumn col in dv.Table.Columns)
          {
           colIndex++;
           excel.Cells[4,colIndex] = col.ColumnName;
           xSt.get_Range(excel.Cells[4,colIndex],excel.Cells[4,colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置标题格式为居中对齐
          }

          //
          //取得表格中的数据
          //
          foreach(DataRowView row in dv)
          {
           rowIndex ++;
           colIndex = 1;
           foreach(DataColumn col in dv.Table.Columns)
           {
            colIndex ++;
            if(col.DataType == System.Type.GetType("System.DateTime"))
            {
             excel.Cells[rowIndex,colIndex] = (Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
             xSt.get_Range(excel.Cells[rowIndex,colIndex],excel.Cells[rowIndex,colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置日期型的字段格式为居中对齐
            }
            else
            if(col.DataType == System.Type.GetType("System.String"))
            {
             excel.Cells[rowIndex,colIndex] = "'"+row[col.ColumnName].ToString();
             xSt.get_Range(excel.Cells[rowIndex,colIndex],excel.Cells[rowIndex,colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置字符型的字段格式为居中对齐
            }
            else
            {
             excel.Cells[rowIndex,colIndex] = row[col.ColumnName].ToString();
            }
           }
          }
          //
          //加载一个合计行
          //
          int rowSum = rowIndex + 1;
          int colSum = 2;
          excel.Cells[rowSum,2] = "合计";
          xSt.get_Range(excel.Cells[rowSum,2],excel.Cells[rowSum,2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
          //
          //设置选中的部分的颜色
          //
          xSt.get_Range(excel.Cells[rowSum,colSum],excel.Cells[rowSum,colIndex]).Select();
          xSt.get_Range(excel.Cells[rowSum,colSum],excel.Cells[rowSum,colIndex]).Interior.ColorIndex = 19;//设置为浅黄色，共计有56种
          //
          //取得整个报表的标题
          //
          excel.Cells[2,2] = str;
          //
          //设置整个报表的标题格式
          //
          xSt.get_Range(excel.Cells[2,2],excel.Cells[2,2]).Font.Bold = true;
          xSt.get_Range(excel.Cells[2,2],excel.Cells[2,2]).Font.Size = 22;
          //
          //设置报表表格为最适应宽度
          //
          xSt.get_Range(excel.Cells[4,2],excel.Cells[rowSum,colIndex]).Select();
          xSt.get_Range(excel.Cells[4,2],excel.Cells[rowSum,colIndex]).Columns.AutoFit();
          //
          //设置整个报表的标题为跨列居中
          //
          xSt.get_Range(excel.Cells[2,2],excel.Cells[2,colIndex]).Select();
          xSt.get_Range(excel.Cells[2,2],excel.Cells[2,colIndex]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenterAcrossSelection;
          //
          //绘制边框
          //
          xSt.get_Range(excel.Cells[4,2],excel.Cells[rowSum,colIndex]).Borders.LineStyle = 1;
          xSt.get_Range(excel.Cells[4,2],excel.Cells[rowSum,2]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;//设置左边线加粗
          xSt.get_Range(excel.Cells[4,2],excel.Cells[4,colIndex]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;//设置上边线加粗
          xSt.get_Range(excel.Cells[4,colIndex],excel.Cells[rowSum,colIndex]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;//设置右边线加粗
          xSt.get_Range(excel.Cells[rowSum,2],excel.Cells[rowSum,colIndex]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;//设置下边线加粗
          //
          //显示效果
          //
          excel.Visible=true;
         }


    }
}
