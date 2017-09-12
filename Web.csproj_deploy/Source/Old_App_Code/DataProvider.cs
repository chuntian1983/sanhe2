using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;

/// <summary>
/// 数据库操作类
/// </summary>
public class DataProvider : IDisposable
{
    public MySqlConnection a;
    public DataTable dataTable;
    public MySqlDataAdapter dataAdapter;

    public DataProvider()
        : this(SysConfigs.DbConnection)
    {
        //--
    }

    public DataProvider(string ConnString)
    {
        this.a = new MySqlConnection(ConnString);
        this.a.Open();
    }

    public void Close()
    {
        if (this.a.State != ConnectionState.Closed)
        {
            this.a.Close();
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
            if (this.a.State != ConnectionState.Closed)
            {
                this.a.Close();
            }
        }
        catch { }
    }

    public string GetConnString(string dbName)
    {
        return SysConfigs.ConnectionTemplate.Replace("ConnectionTemplate", dbName);
    }

    public DataSet ExecuteDataSet(string CommandTxt)
    {
        DataSet dataSet = new DataSet();
        using (MySqlDataAdapter adapter = new MySqlDataAdapter(CommandTxt, this.a))
        {
            adapter.Fill(dataSet);
        }
        return dataSet;
    }

    public DataTable ExecuteDataTable(string CommandTxt)
    {
        DataTable dataTable = new DataTable();
        using (MySqlDataAdapter adapter = new MySqlDataAdapter(CommandTxt, this.a))
        {
            adapter.Fill(dataTable);
        }
        return dataTable;
    }

    public void ExecuteNonQuery(string CommandTxt)
    {
        using (MySqlCommand command = new MySqlCommand(CommandTxt, this.a))
        {
            command.ExecuteNonQuery();
        }
    }

    public MySqlDataReader ExecuteReader(string CommandTxt)
    {
        using (MySqlCommand command = new MySqlCommand(CommandTxt, this.a))
        {
            return command.ExecuteReader();
        }
    }

    public object ExecuteScalar(string CommandTxt)
    {
        using (MySqlCommand command = new MySqlCommand(CommandTxt, this.a))
        {
            return command.ExecuteScalar();
        }
    }

    public DataTable U_CreateDataTable(string CommandTxt)
    {
        dataTable = new DataTable();
        dataAdapter = new MySqlDataAdapter(CommandTxt, this.a);
        dataAdapter.Fill(dataTable);
        return dataTable;
    }

    public void U_UpdateDataTable()
    {
        MySqlCommandBuilder SCB = new MySqlCommandBuilder(dataAdapter);
        dataAdapter.Update(dataTable);
    }
}
