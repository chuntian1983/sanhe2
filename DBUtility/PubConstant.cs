using System;
using System.Configuration;
using System.Text;
using System.Web;
namespace Maticsoft.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString1
        {
            get
            {
                return ConfigurationManager.AppSettings["DbConnection"];
            }
        }
        public static string ConnectionString
        {
            
            get
            {
                return ConfigurationManager.AppSettings["ConnectionTemplate"].Replace("ConnectionTemplate", ConfigurationManager.AppSettings["DbPrefix"] + HttpContext.Current.Session["AccountID"].ToString());
            }
        }
      
        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }


    }
}
