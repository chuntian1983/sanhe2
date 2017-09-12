using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用

namespace SanZi.DAL
{
    public class CunWuGongKai
    {
        public CunWuGongKai()
		{}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(string title, int deptid, string deptname, string filename, int uid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into CunWuGongKai(Title,DeptID,DeptName,FileName,SubTime,LastUpdate,SubUID) ");
            strSql.Append(" values(");
            strSql.Append("'" + title + "'," + deptid + ",'" + deptname + "',");
            strSql.Append("'" + filename + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "',");
            strSql.Append("'" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," + uid + " ");
            strSql.Append(" ) ");
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(int id, string title, int deptid, string deptname, string filename)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" update CunWuGongKai ");
            strSql.Append(" set title='"+title+"',");
            strSql.Append("deptid=" + deptid + ",DeptName='" + deptname + "',");
            strSql.Append("FileName='" + filename + "',LastUpdate='" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'");
            strSql.Append(" where id=" + id);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" update CunWuGongKai");
            strSql.Append(" set delflag=1");
            strSql.Append(" where id="+id);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 获得权利人数据列表
        /// </summary>
        public DataSet GetList(string strWhere,string subid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,DeptID,DeptName,FileName,SubTime,LastUpdate,SubUID,DelFlag ");
            strSql.Append(" FROM CunWuGongKai ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where DelFlag=0 and subuid='"+subid+"' and (Title like '%" + strWhere + "%' or DeptName like '%" + strWhere + "%')");
            }
            else
            {
                strSql.Append(" where DelFlag=0 and subuid='" + subid + "'  ");
            }
            strSql.Append(" order by ID desc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        public DataSet getInfoByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,DeptID,DeptName,FileName,SubTime,LastUpdate,SubUID,DelFlag ");
            strSql.Append(" FROM CunWuGongKai ");
            strSql.Append(" where DelFlag=0 and ID=" + id);
            return DbHelperMySQL.Query(strSql.ToString());
        }


    }
}
