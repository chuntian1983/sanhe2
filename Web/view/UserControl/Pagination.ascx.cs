using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SanZi.Web.UserControl
{
    public class PageIndexChangedEventArguments : EventArgs
    {
        private int _newPageIndex;
        public PageIndexChangedEventArguments(int pageIndex)
        {
            this._newPageIndex = pageIndex;
        }
        public int NewPageIndex
        {
            get { return this._newPageIndex; }
        }
    }

    public delegate void PageIndexChangedEventHander(object sender, PageIndexChangedEventArguments e);
    public partial class Pagination : System.Web.UI.UserControl
    {
        private int _totalPages = 1;
        public int PageSize
        {
            get { return Convert.ToInt32(ViewState["_recordPerPage"]); }
            set { ViewState["_recordPerPage"] = value; }
        }
        public int RecordCount
        {
            get { return Convert.ToInt32(ViewState["_totalRecords"]); }
            set { ViewState["_totalRecords"] = value; }
        }
        public int PageIndex
        {
            get { return Convert.ToInt32(ViewState["_currentPageIndex"]); }
            set { ViewState["_currentPageIndex"] = value; }
        }
        public int PageCount
        {
            get
            {
                if (this._totalPages == 1)
                {
                    if (RecordCount != 0)
                    {
                        this._totalPages = RecordCount / PageSize;
                        if (RecordCount % PageSize != 0) this._totalPages++;
                    }
                }
                return this._totalPages;
            }
        }

        public event PageIndexChangedEventHander PageIndexChanged;
        protected void OnPageIndexChanged(PageIndexChangedEventArguments e)
        {
            if (PageIndexChanged != null)
            {
                PageIndexChanged(this, e);
            }
        }
        protected void Page_Load(object sender, EventArgs e) { }
        protected void PagerButtonClick(object sender, System.EventArgs e)
        {
            //获得LinkButton的参数值
            string arg = string.Empty;
            if (sender is LinkButton)
                arg = ((LinkButton)sender).CommandArgument;
            else if (sender is Button)
                arg = ((Button)sender).CommandArgument;
            switch (arg)
            {
                case ("Next"):
                    if (this.PageIndex < this.PageCount - 1)
                        this.PageIndex++;
                    break;
                case ("Previous"):
                    if (this.PageIndex > 0)
                        this.PageIndex--;
                    break;
                case ("First"):
                    this.PageIndex = 0;
                    break;
                case ("Last"):
                    this.PageIndex = this.PageCount - 1;
                    break;
                case "Jump":
                    int jumpToIndex = int.Parse(JumpNum.Text.Trim());
                    if (jumpToIndex > this.PageCount) jumpToIndex = this.PageCount;
                    else if (jumpToIndex < 1) jumpToIndex = 1;
                    this.PageIndex = jumpToIndex - 1;
                    break;
                default:
                    break;
            }
            this.OnPageIndexChanged(new PageIndexChangedEventArguments(this.PageIndex));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.RecordCount == 0)
            {
                this.__tb_pager.Visible = false;
                this.__tb_norecord.Visible = true;
            }
            else
            {
                this.__tb_pager.Visible = true;
                this.__tb_norecord.Visible = false;

                this.RecCount.Text = this.RecordCount.ToString();
                this.CurPage.Text = (this.PageIndex + 1).ToString();
                this.PagCount.Text = this.PageCount.ToString();
                this.JumpNum.Text = (this.PageIndex + 1).ToString();

                this.FirstPageStatic.Visible = false;
                this.PreviousPageStatic.Visible = false;
                this.FirstPage.Visible = true;
                this.PreviousPage.Visible = true;
                this.NextPage.Visible = true;
                this.LastPage.Visible = true;
                this.NextPageStatic.Visible = false;
                this.LastPageStatic.Visible = false;

                if (this.PageIndex == 0)
                {
                    this.FirstPageStatic.Visible = true;
                    this.PreviousPageStatic.Visible = true;
                    this.FirstPage.Visible = false;
                    this.PreviousPage.Visible = false;
                }
                if (this.PageIndex == this.PageCount - 1)
                {
                    this.NextPage.Visible = false;
                    this.LastPage.Visible = false;
                    this.NextPageStatic.Visible = true;
                    this.LastPageStatic.Visible = true;
                }
            }
        }
    }
}
