using System;
using System.Configuration;
using System.Text;
using System.Web;
namespace Maticsoft.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// ��ȡ�����ַ���
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
        /// �õ�web.config������������ݿ������ַ�����
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
