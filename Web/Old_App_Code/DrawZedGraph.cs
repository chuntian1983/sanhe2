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
using System.IO;
using System.Drawing;
using ZedGraph;
using ZedGraph.Web;

/// <summary>
/// ZedGraph控件绘制类
/// </summary>
public class DrawZedGraph
{
    private ZedGraphWeb m_WebZedGraph;
    private string m_GraphType = string.Empty;
    private string m_Title = string.Empty;
    private string m_BarText = string.Empty;
    private string m_BarText2 = string.Empty;
    private string m_XAxisTitle = string.Empty;
    private string m_YAxisTitle = string.Empty;
    private int m_BarCount = 0;

    public ZedGraphWeb WebZedGraph
    {
        set { m_WebZedGraph = value; }
        get { return m_WebZedGraph; }
    }
    public string GraphType
    {
        set { m_GraphType = value; }
        get { return m_GraphType; }
    }
    public string BarText
    {
        set { m_BarText = value; }
        get { return m_BarText; }
    }
    public string BarText2
    {
        set { m_BarText2 = value; }
        get { return m_BarText2; }
    }
    public string Title
    {
        set { m_Title = value; }
        get { return m_Title; }
    }
    public string XAxisTitle
    {
        set { m_XAxisTitle = value; }
        get { return m_XAxisTitle; }
    }
    public string YAxisTitle
    {
        set { m_YAxisTitle = value; }
        get { return m_YAxisTitle; }
    }

    public Dictionary<string, double> data = new Dictionary<string, double>();
    public Dictionary<string, double> data2 = new Dictionary<string, double>();

    public DrawZedGraph()
    {
        // TODO: 在此处添加构造函数逻辑
    }

    public DrawZedGraph(ZedGraphWeb webZedGraph, string graphType, int barCount)
    {
        WebZedGraph = webZedGraph;
        GraphType = graphType;
        m_BarCount = barCount;
        switch (GraphType)
        {
            case "bar":
                int gWidth = 200 + m_BarCount * 80;
                WebZedGraph.Width = (gWidth > 700 ? gWidth : 700);
                WebZedGraph.Height = 400;
                break;
            case "barh":
                int gHeight = m_BarCount * 50;
                WebZedGraph.Width = 700;
                WebZedGraph.Height = (gHeight > 400 ? gHeight : 400);
                break;
            case "pie":
                WebZedGraph.Width = 700;
                WebZedGraph.Height = 400;
                break;
        }
    }

    public void Draw(ZedGraphWeb zgw, Graphics g, MasterPane masterPane)
    {
        GraphPane myPane = masterPane[0];
        myPane.Title.Text = Title;
        myPane.Title.FontSpec.Size = 20f;
        switch (GraphType)
        {
            case "bar":
                DrawBar(myPane);
                break;
            case "barh":
                DrawBarHorizontal(myPane);
                break;
            case "pie":
                DrawPie(myPane);
                break;
        }
        masterPane.AxisChange(g);
        if (GraphType == "bar")
        {
            BarItem.CreateBarLabels(myPane, false, "0");
        }
    }

    private void DrawBar(GraphPane myPane)
    {
        string[] labels = new string[m_BarCount];
        if (data.Count >= data2.Count)
        {
            data.Keys.CopyTo(labels, 0);
        }
        else
        {
            data2.Keys.CopyTo(labels, 0);
        }

        myPane.XAxis.Title.Text = XAxisTitle;
        myPane.XAxis.Type = AxisType.Text;
        myPane.XAxis.MajorGrid.IsVisible = true;
        myPane.XAxis.MajorTic.IsBetweenLabels = true;
        myPane.XAxis.Scale.TextLabels = labels;

        myPane.YAxis.Title.Text = YAxisTitle;
        myPane.YAxis.MajorGrid.IsVisible = true;
        myPane.YAxis.MajorTic.IsBetweenLabels = true;

        //柱1
        PointPairList list = new PointPairList();
        for (int x = 0; x < m_BarCount; x++)
        {
            if (data.ContainsKey(labels[x]))
            {
                list.Add(x, data[labels[x]]);
            }
            else
            {
                list.Add(x, 0);
            }
        }
        BarItem myCurve = myPane.AddBar(BarText, list, Color.Blue);
        myCurve.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);

        //柱2
        if (data2.Count > 0)
        {
            PointPairList list2 = new PointPairList();
            for (int x = 0; x < m_BarCount; x++)
            {
                if (data2.ContainsKey(labels[x]))
                {
                    list2.Add(x, data2[labels[x]]);
                }
                else
                {
                    list2.Add(x, 0);
                }
            }
            BarItem myCurve2 = myPane.AddBar(BarText2, list2, Color.Green);
            myCurve2.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green);
        }

        //填充Pane背景颜色和倾斜度
        myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);
        //背景颜色
        myPane.Chart.Fill = new Fill(Color.Red, Color.LightGoldenrodYellow, 45.0f);
    }

    private void DrawBarHorizontal(GraphPane myPane)
    {
        string[] labels = new string[m_BarCount];
        if (data.Count >= data2.Count)
        {
            data.Keys.CopyTo(labels, 0);
        }
        else
        {
            data2.Keys.CopyTo(labels, 0);
        }
        Array.Reverse(labels);

        myPane.BarSettings.Base = BarBase.Y;
        myPane.BarSettings.Type = BarType.Cluster;

        myPane.YAxis.Title.Text = XAxisTitle;
        myPane.YAxis.Type = AxisType.Text;
        myPane.YAxis.MajorGrid.IsVisible = true;
        myPane.YAxis.MajorTic.IsBetweenLabels = true;
        myPane.YAxis.Scale.TextLabels = labels;

        myPane.XAxis.Title.Text = YAxisTitle;
        myPane.XAxis.MajorGrid.IsVisible = true;
        myPane.XAxis.MajorTic.IsBetweenLabels = true;

        //柱1
        PointPairList list = new PointPairList();
        for (int x = 0; x < m_BarCount; x++)
        {
            if (data.ContainsKey(labels[x]))
            {
                list.Add(data[labels[x]], x);
            }
            else
            {
                list.Add(0, x);
            }
        }
        BarItem myCurve = myPane.AddBar(BarText, list, Color.Blue);
        myCurve.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue, 90F);

        //柱2
        if (data2.Count > 0)
        {
            PointPairList list2 = new PointPairList();
            for (int x = 0; x < m_BarCount; x++)
            {
                if (data2.ContainsKey(labels[x]))
                {
                    list2.Add(data2[labels[x]], x);
                }
                else
                {
                    list2.Add(0, x);
                }
            }
            BarItem myCurve2 = myPane.AddBar(BarText2, list2, Color.Green);
            myCurve2.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green, 90F);
        }

        //填充Pane背景颜色和倾斜度
        myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 90F);
        //背景颜色
        myPane.Chart.Fill = new Fill(Color.Red, Color.LightGoldenrodYellow, 90F);
    }

    private void DrawPie(GraphPane myPane)
    {
        Random rand = new Random();
        //填充Pane背景颜色和倾斜度
        myPane.Fill = new Fill(Color.White, Color.Goldenrod, 45.0f);
        //背景填充类型
        myPane.Chart.Fill.Type = FillType.None;
        //设置位置
        myPane.Legend.Position = LegendPos.Float;
        myPane.Legend.Location = new Location(0.95f, 0.15f, CoordType.PaneFraction, AlignH.Right, AlignV.Top);
        //文字大小
        myPane.Legend.FontSpec.Size = 12f;
        myPane.Legend.IsHStack = false;
        //添加饼图片值
        PieItem[] segment = new PieItem[data.Count];
        string[] labels = new string[data.Count];
        data.Keys.CopyTo(labels, 0);
        for (int x = 0; x < data.Count; x++)
        {
            segment[x] = myPane.AddPieSlice(data[labels[x]], Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)), Color.White, 45f, 0, labels[x]);
            segment[x].LabelType = PieLabelType.Name_Value;
            segment[x].LabelDetail.FontSpec.Size = 12f;
            segment[x].LabelDetail.FontSpec.FontColor = Color.Red;
        }
    }
}
