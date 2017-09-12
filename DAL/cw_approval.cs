using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;

namespace SanZi.DAL
{
    /// <summary>
    /// 数据访问类:cw_approval
    /// </summary>
    public partial class cw_approval
    {
        public cw_approval()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from cw_approval");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SanZi.Model.cw_approval model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into cw_approval(");
            strSql.Append("accountid,accounttime,creattime,pibanka,pibankama)");
            strSql.Append(" values (");
            strSql.Append("@accountid,@accounttime,@creattime,@pibanka,@pibankama)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@accountid", MySqlDbType.VarChar,100),
					new MySqlParameter("@accounttime", MySqlDbType.DateTime),
					new MySqlParameter("@creattime", MySqlDbType.DateTime),
					new MySqlParameter("@pibanka", MySqlDbType.VarChar,500),
					new MySqlParameter("@pibankama", MySqlDbType.VarChar,2000)};
            parameters[0].Value = model.accountid;
            parameters[1].Value = model.accounttime;
            parameters[2].Value = model.creattime;
            parameters[3].Value = model.pibanka;
            parameters[4].Value = model.pibankama;
            
            DbHelperMySQL.ExecuteSql1(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SanZi.Model.cw_approval model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update cw_approval set ");
            strSql.Append("accountid=@accountid,");
            strSql.Append("accounttime=@accounttime,");
            strSql.Append("creattime=@creattime,");
            strSql.Append("pibanka=@pibanka,");
            strSql.Append("pibankama=@pibankama");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@accountid", MySqlDbType.VarChar,100),
					new MySqlParameter("@accounttime", MySqlDbType.DateTime),
					new MySqlParameter("@creattime", MySqlDbType.DateTime),
					new MySqlParameter("@pibanka", MySqlDbType.VarChar,500),
					new MySqlParameter("@pibankama", MySqlDbType.VarChar,2000),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = model.accountid;
            parameters[1].Value = model.accounttime;
            parameters[2].Value = model.creattime;
            parameters[3].Value = model.pibanka;
            parameters[4].Value = model.pibankama;
            parameters[5].Value = model.id;

            int rows = DbHelperMySQL.ExecuteSql1(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from cw_approval ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
};
            parameters[0].Value = id;

            int rows = DbHelperMySQL.ExecuteSql1(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from cw_approval ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql1(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SanZi.Model.cw_approval GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,accountid,accounttime,creattime,pibanka,pibankama from cw_approval ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
};
            parameters[0].Value = id;

            SanZi.Model.cw_approval model = new Model.cw_approval();
            DataSet ds = DbHelperMySQL.Query1(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["accountid"] != null && ds.Tables[0].Rows[0]["accountid"].ToString() != "")
                {
                    model.accountid = ds.Tables[0].Rows[0]["accountid"].ToString();
                }
                if (ds.Tables[0].Rows[0]["accounttime"] != null && ds.Tables[0].Rows[0]["accounttime"].ToString() != "")
                {
                    model.accounttime = DateTime.Parse(ds.Tables[0].Rows[0]["accounttime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["creattime"] != null && ds.Tables[0].Rows[0]["creattime"].ToString() != "")
                {
                    model.creattime = DateTime.Parse(ds.Tables[0].Rows[0]["creattime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pibanka"] != null && ds.Tables[0].Rows[0]["pibanka"].ToString() != "")
                {
                    model.pibanka = ds.Tables[0].Rows[0]["pibanka"].ToString();
                }
                if (ds.Tables[0].Rows[0]["pibankama"] != null && ds.Tables[0].Rows[0]["pibankama"].ToString() != "")
                {
                    model.pibankama = ds.Tables[0].Rows[0]["pibankama"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,accountid,accounttime,creattime,pibanka,pibankama ");
            strSql.Append(" FROM cw_approval ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query1(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "cw_approval";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}
