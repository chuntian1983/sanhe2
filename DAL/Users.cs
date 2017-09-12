using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace SanZi.DAL
{
	/// <summary>
	/// 数据访问类users。
	/// </summary>
	public class Users
	{
		public Users()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("UserID", "users"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from users");
			strSql.Append(" where UserID=@UserID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.Int32)};
			parameters[0].Value = UserID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 通过条形码查是否存在该记录
        /// </summary>
        public bool ExistsByBarCode(string BarCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(UserID) from users");
            strSql.Append(" where BarCode='" + BarCode + "' ");

            return DbHelperMySQL.Exists(strSql.ToString());
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>

        public void AddUser(SanZi.Model.Users model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into users(");
            strSql.Append("TitleID,TitleName,DeptID,DeptName,TrueName,BarCode,SubTime,SubUID,TelPhone,Address)");
            strSql.Append(" values (");
            strSql.Append(""+model.TitleID+",");
            strSql.Append("'" + model.TitleName + "',");
            strSql.Append("" + model.DeptID + ",");
            strSql.Append("'" + model.DeptName + "',");
            strSql.Append("'" + model.TrueName + "',");
            strSql.Append("'" + model.BarCode + "',");
            strSql.Append("" + model.SubTime + ",");
            strSql.Append("" + model.SubUID + ",");
            strSql.Append("'" + model.TelPhone + "',");
            strSql.Append("'" + model.Address + "'");
            strSql.Append(")");

            DbHelperMySQL.ExecuteSql(strSql.ToString());
        
		}
        public StringBuilder AddUsers(SanZi.Model.Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into users(");
            strSql.Append("TitleID,TitleName,DeptID,DeptName,TrueName,BarCode,SubTime,SubUID,TelPhone,Address)");
            strSql.Append(" values (");
            strSql.Append("" + model.TitleID + ",");
            strSql.Append("'" + model.TitleName + "',");
            strSql.Append("" + model.DeptID + ",");
            strSql.Append("'" + model.DeptName + "',");
            strSql.Append("'" + model.TrueName + "',");
            strSql.Append("'" + model.BarCode + "',");
            strSql.Append("" + model.SubTime + ",");
            strSql.Append("" + model.SubUID + ",");
            strSql.Append("'" + model.TelPhone + "',");
            strSql.Append("'" + model.Address + "'");
            strSql.Append(")");
            return strSql;
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.Users model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update users set ");
			strSql.Append("TitleID=@TitleID,");
			strSql.Append("TitleName=@TitleName,");
			strSql.Append("DeptID=@DeptID,");
			strSql.Append("DeptName=@DeptName,");
			strSql.Append("TrueName=@TrueName,");
			strSql.Append("BarCode=@BarCode,");
			strSql.Append("SubTime=@SubTime,");
			strSql.Append("SubUID=@SubUID,");
			strSql.Append("DelFlag=@DelFlag");
			strSql.Append(" where UserID=@UserID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.Int32,11),
					new MySqlParameter("@TitleID", MySqlDbType.Int32,11),
					new MySqlParameter("@TitleName", MySqlDbType.VarChar,50),
					new MySqlParameter("@DeptID", MySqlDbType.Int32,11),
					new MySqlParameter("@DeptName", MySqlDbType.VarChar,100),
					new MySqlParameter("@TrueName", MySqlDbType.VarChar,50),
					new MySqlParameter("@BarCode", MySqlDbType.VarChar,100),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,11)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.TitleID;
			parameters[2].Value = model.TitleName;
			parameters[3].Value = model.DeptID;
			parameters[4].Value = model.DeptName;
			parameters[5].Value = model.TrueName;
			parameters[6].Value = model.BarCode;
			parameters[7].Value = model.SubTime;
			parameters[8].Value = model.SubUID;
			parameters[9].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from users ");
			strSql.Append(" where UserID=@UserID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.Int32)};
			parameters[0].Value = UserID;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.Users GetModel(int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,TitleID,TitleName,DeptID,DeptName,TrueName,BarCode,SubTime,SubUID,DelFlag from users ");
			strSql.Append(" where UserID=@UserID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.Int32)};
			parameters[0].Value = UserID;

			SanZi.Model.Users model=new SanZi.Model.Users();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TitleID"].ToString()!="")
				{
					model.TitleID=int.Parse(ds.Tables[0].Rows[0]["TitleID"].ToString());
				}
				model.TitleName=ds.Tables[0].Rows[0]["TitleName"].ToString();
				if(ds.Tables[0].Rows[0]["DeptID"].ToString()!="")
				{
					model.DeptID=int.Parse(ds.Tables[0].Rows[0]["DeptID"].ToString());
				}
				model.DeptName=ds.Tables[0].Rows[0]["DeptName"].ToString();
				model.TrueName=ds.Tables[0].Rows[0]["TrueName"].ToString();
				model.BarCode=ds.Tables[0].Rows[0]["BarCode"].ToString();
				if(ds.Tables[0].Rows[0]["SubTime"].ToString()!="")
				{
					model.SubTime=int.Parse(ds.Tables[0].Rows[0]["SubTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubUID"].ToString()!="")
				{
					model.SubUID=int.Parse(ds.Tables[0].Rows[0]["SubUID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DelFlag"].ToString()!="")
				{
					model.DelFlag=int.Parse(ds.Tables[0].Rows[0]["DelFlag"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 获得权利人数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,TitleID,TitleName,DeptID,DeptName,TrueName,BarCode,SubTime,SubUID,DelFlag,TelPhone,Address ");
			strSql.Append(" FROM users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where (TrueName like '%" + strWhere + "%' or BarCode like '%" + strWhere + "%' or TitleName like '%" + strWhere + "%' or DeptName like '%" + strWhere + "%')");
            }
            strSql.Append(" order by UserID desc ");
			return DbHelperMySQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 取批办卡列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetPiBanKaList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.PID,a.DeptID,a.OutReason,a.OutMoney,a.State,a.SubTime,b.DeptName,a.d,a.c ,a.subid,a.zhaiyao,a.lujing");
            strSql.Append(" FROM PiBanKa a left join Department b on a.DeptID=b.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where a.DelFlag=0 and a.state=1 and (a.OutReason like '%" + strWhere + "%' or b.DeptName like '%" + strWhere + "%')");
            }
            else
            {
                strSql.Append(" where a.DelFlag=0 and a.state=1  ");
            }
            strSql.Append(" order by a.PID desc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

		/// <summary>
        /// 通过条形码取用户信息
		/// </summary>
        public DataSet GetUserInfoByBarcode(string BarCode)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,TitleID,TitleName,DeptID,DeptName,TrueName,BarCode,SubTime,SubUID,DelFlag ");
            strSql.Append(" FROM users ");
            if (BarCode.Trim() != "")
            {
                strSql.Append(" where DelFlag=0 and UserID=" + BarCode);
            }
            else
            {
                strSql.Append(" where DelFlag=0  ");
            }
            strSql.Append(" order by UserID desc ");
            return DbHelperMySQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <returns></returns>
        public DataSet ExportUserData()
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select DeptName as 单位名称,TitleName as 职务,TrueName as 职务,BarCode as 条形码信息 ");
            strSql.Append("select DeptName as Department,TitleName as Title,TrueName as UserName,BarCode ");
            strSql.Append(" FROM users ");
            return DbHelperMySQL.Query(strSql.ToString());
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
			parameters[0].Value = "users";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uid"></param>
        public void DelUser(int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Users WHERE userid=" + uid);
            DbHelperMySQL.Query(strSql.ToString());
        }

        public DataSet GetUserInfoByID(int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,TitleID,TitleName,DeptID,DeptName,TelPhone,TrueName,BarCode,SubTime,SubUID,DelFlag ");
            strSql.Append(" FROM users ");
            strSql.Append(" where DelFlag=0 and UserID=" + uid);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        public void UpdateUser(int deptid, string deptname, int titleid, string titlename, string dh, string truename, int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users  set ");
            strSql.Append("deptid=" + deptid + ",DeptName='" + deptname + "',");
            strSql.Append("TitleID=" + titleid + ",TitleName='" + titlename + "',");
            strSql.Append("TelPhone='" + dh + "',");
            strSql.Append("TrueName='" + truename + "'");
            strSql.Append(" WHERE userid=" + uid);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }
		#endregion  成员方法
	}
}

