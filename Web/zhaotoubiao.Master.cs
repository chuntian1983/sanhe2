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
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace SanZi.Web
{
    public partial class zhaotoubiao : System.Web.UI.MasterPage
    {
        public StringBuilder SubMenuList = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UserInfo.CheckSession();
            XmlDocument menus = new XmlDocument();
            menus.Load(SysConfigs.GetAppDataFilePath("zhaotoubiao.xml"));
            XmlNodeList mainMenus = menus.SelectSingleNode("Menus").ChildNodes;
            foreach (XmlNode mainMenu in mainMenus)
            {
                string menuName = mainMenu.Attributes["Name"].Value;
                XmlNodeList subMenus = mainMenu.ChildNodes;
                if (subMenus.Count > 0)
                {
                    SubMenuList.Append("<li class=\"imatm\"  style=\"width:120px;\"><a href=\"Javascript:\"><span class=\"imea imeam\"><span></span></span>");
                    SubMenuList.Append(menuName);
                    SubMenuList.Append("</a><div class=\"imsc\"><div class=\"imsubc\" style=\"width:150px;top:0px;left:0px;\"><ul>");
                    foreach (XmlNode subMenu in subMenus)
                    {
                        SubMenuList.Append("<li style=\"height:20px;border-bottom: 1px solid buttonface;\"><a href=\"Javascript:MenuBarClick('");
                        SubMenuList.AppendFormat("{1}')\"><img src='Images/21.gif'>&nbsp;&nbsp;{0}</a></li>", subMenu.Attributes["Name"].Value, subMenu.Attributes["Src"].Value);
                    }
                    SubMenuList.Append("</ul></div></div></li>");
                }
                else
                {
                    SubMenuList.AppendFormat("<li class=\"imatm\"  style=\"width:120px;\"><a href=\"Javascript:MenuBarClick('{0}", mainMenu.Attributes["Src"].Value);
                    SubMenuList.AppendFormat("')\"><span class=\"imea imeam\"><span></span></span>{0}</a></li>", menuName);
                }
            }
        }
    }
}