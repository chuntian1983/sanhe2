using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace SanZi.DAL
{
	/// <summary>
	/// 数据访问类usertitle。
	/// </summary>
	public class UserTitle
	{
		public UserTitle()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("TitleID", "usertitle"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TitleID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from usertitle");
			strSql.Append(" where TitleID=@TitleID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TitleID", MySqlDbType.Int32)};
			parameters[0].Value = TitleID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SanZi.Model.UserTitle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into usertitle(");
			strSql.Append("TitleName,TitleDesc,SubTime,SubUID,DelFlag)");
			strSql.Append(" values (");
			strSql.Append("@TitleName,@TitleDesc,@SubTime,@SubUID,@DelFlag)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TitleName", MySqlDbType.VarChar,50),
					new MySqlParameter("@TitleDesc", MySqlDbType.VarChar,100),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,5)};
			parameters[0].Value = model.TitleName;
			parameters[1].Value = model.TitleDesc;
			parameters[2].Value = model.SubTime;
			parameters[3].Value = model.SubUID;
			parameters[4].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.UserTitle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update usertitle set ");
			strSql.Append("TitleName=@TitleName,");
			strSql.Append("TitleDesc=@TitleDesc,");
			strSql.Append("SubTime=@SubTime,");
			strSql.Append("SubUID=@SubUID,");
			strSql.Append("DelFlag=@DelFlag");
			strSql.Append(" where TitleID=@TitleID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TitleID", MySqlDbType.Int32,11),
					new MySqlParameter("@TitleName", MySqlDbType.VarChar,50),
					new MySqlParameter("@TitleDesc", MySqlDbType.VarChar,100),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,5)};
			parameters[0].Value = model.TitleID;
			parameters[1].Value = model.TitleName;
			parameters[2].Value = model.TitleDesc;
			parameters[3].Value = model.SubTime;
			parameters[4].Value = model.SubUID;
			parameters[5].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int TitleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from usertitle ");
			strSql.Append(" where TitleID=@TitleID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TitleID", MySqlDbType.Int32)};
			parameters[0].Value = TitleID;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.UserTitle GetModel(int TitleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TitleID,TitleName,TitleDesc,SubTime,SubUID,DelFlag from usertitle ");
			strSql.Append(" where TitleID=@TitleID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TitleID", MySqlDbType.Int32)};
			parameters[0].Value = TitleID;

			SanZi.Model.UserTitle model=new SanZi.Model.UserTitle();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TitleID"].ToString()!="")
				{
					model.TitleID=int.Parse(ds.Tables[0].Rows[0]["TitleID"].ToString());
				}
				model.TitleName=ds.Tables[0].Rows[0]["TitleName"].ToString();
				model.TitleDesc=ds.Tables[0].Rows[0]["TitleDesc"].ToString();
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TitleID,TitleName,TitleDesc,SubTime,SubUID,DelFlag ");
			strSql.Append(" FROM usertitle ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where delflag=0 and " + strWhere);
            }
            else
            {
                strSql.Append(" where delflag=0  " );
            }
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
			parameters[0].Value = "usertitle";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}

