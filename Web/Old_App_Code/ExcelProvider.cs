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
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

/// <summary>
/// Excel操作类
/// </summary>
public class ExcelProvider : IDisposable
{
    private string a;
    private OleDbConnection conn;

    public string[] strTableNames;
    public string filePath
    {
        set
        {
            a = value;
            InitProvider();
        }
        get
        {
            return a;
        }
    }

    public ExcelProvider()
    {

    }

    public ExcelProvider(string _filePath)
	{
        this.filePath = _filePath;
	}

    private void InitProvider()
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + a + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=2;\";";
        conn = new OleDbConnection(strConn);
        conn.Open();

        //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等  
        DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
        strTableNames = new string[dtSheetName.Rows.Count];
        for (int k = 0; k < dtSheetName.Rows.Count; k++)
        {
            strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
        }
    }

    public DataTable GetExcelDatas(string tableName)
    {
        DataTable dataTable = new DataTable();
        string strExcel = "select * from [" + tableName + "]";
        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(strExcel, conn);
        dataAdapter.Fill(dataTable);
        return dataTable;
    }

    public void Close()
    {
        if (this.conn.State != ConnectionState.Closed)
        {
            this.conn.Close();
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
    }

    protected void Dispose(bool Diposing)
    {
        try
        {
            if (this.conn.State != ConnectionState.Closed)
            {
                this.conn.Close();
            }
        }
        catch
        {
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //静态类区

    public static string[] GetExcelSheetNames(string filePath)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\";";
        using (OleDbConnection conn = new OleDbConnection(strConn))
        {
            conn.Open();
            //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等  
            DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            string[] strTableNames = new string[dtSheetName.Rows.Count];
            for (int k = 0; k < dtSheetName.Rows.Count; k++)
            {
                strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
            }
            return strTableNames;
        }
    }

    public static DataTable GetExcelDatas(string filePath, string tableName)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\";";
        using (OleDbConnection conn = new OleDbConnection(strConn))
        {
            conn.Open();
            DataTable dt = new DataTable();
            string strExcel = "select * from [" + tableName + "]";
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, conn);
            myCommand.Fill(dt);
            return dt;
        }
    }

    public static void DataToExcel(DataTable dataTable)
    {
        GridView GView = new GridView();
        GView.DataSource = dataTable.DefaultView;
        GView.DataBind();
        PageClass.ToExcel(GView);
    }

    public static void DataToExcel(DataTable dataTable, string filePath, bool isDown)
    {
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
        sw.Write(dataTable.Columns[0].ColumnName);
        for (int k = 1; k < dataTable.Columns.Count; k++)
        {
            sw.Write("\t" + dataTable.Columns[k].ColumnName);
        }
        sw.WriteLine();
        foreach (DataRow row in dataTable.Rows)
        {
            sw.Write(row[0].ToString());
            for (int k = 1; k < dataTable.Columns.Count; k++)
            {
                sw.Write("\t" + row[k].ToString());
            }
            sw.WriteLine();
        }
        sw.Flush();
        sw.Close();
        fs.Close();
        if (isDown)
        {
            DataToExcel(dataTable);
        }
    }

    private static void CloseExcel()
    {
        //强行销毁创建的应用程序
        GC.Collect();
        //强行杀死最近打开的Excel进程
        System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL.EXE");
        System.DateTime startTime = new DateTime();
        int killId = -1;
        for (int m = 0; m < excelProc.Length; m++)
        {
            if (startTime < excelProc[m].StartTime)
            {
                startTime = excelProc[m].StartTime;
                killId = m;
            }
        }
        if (killId != -1 && excelProc[killId].HasExited == false)
        {
            excelProc[killId].Kill();
        }
    }

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

    public static void KillExcel(int hwnd)
    {
        int k = 0;
        IntPtr t = new IntPtr(hwnd);
        GetWindowThreadProcessId(t, out k);
        System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
        p.Kill();
        GC.Collect();
    }
}
