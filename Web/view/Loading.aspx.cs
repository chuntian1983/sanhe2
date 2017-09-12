using System;

namespace SanZi.Web
{
    public partial class Loading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Loading));
        }

        [AjaxPro.AjaxMethod]
        public string GetServerTime()
        {
            //System.Threading.Thread.Sleep(2000);
            return DateTime.Now.ToString();
        }
    }
}
