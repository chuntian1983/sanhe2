using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace SanZi.DAL
{
	/// <summary>
	/// 数据访问类tousu。
	/// </summary>
	public class Tousu
	{
		public Tousu()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("ID", "tousu"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tousu");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SanZi.Model.Tousu model)
		{
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into tousu(");
            sb1.Append("DeptID,DeptName,Content,TFlag");
            sb2.Append(" values (");
            sb2.Append("" + model.DeptID + ",");
            sb2.Append("'" + model.DeptName + "',");
            sb2.Append("'" + model.Content + "',");
            sb2.Append("'" + model.TFlag + "'");

            if (model.SubUID.ToString() != "")
            {
                sb1.Append(",SubUID");
                sb2.Append("," + model.SubUID + "");
            }
            if (model.SubIP.ToString() != "")
            {
                sb1.Append(",SubIP");
                sb2.Append(",'" + model.SubIP + "'");
            }
            if (model.SubTime.ToString() != "")
            {
                sb1.Append(",SubTime");
                sb2.Append(",'" + model.SubTime + "'");
            }
            sb1.Append(") ");
            sb2.Append(");");
            string strSql = sb1.ToString() + sb2.ToString();
			DbHelperMySQL.ExecuteSql1(strSql);

			
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.Tousu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tousu set ");
			strSql.Append("Content=@Content,");
			strSql.Append("SubUID=@SubUID,");
			strSql.Append("SubIP=@SubIP,");
			strSql.Append("SubTime=@SubTime,");
			strSql.Append("DelFlag=@DelFlag");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11),
					new MySqlParameter("@Content", MySqlDbType.Text),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@SubIP", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.Content;
			parameters[2].Value = model.SubUID;
			parameters[3].Value = model.SubIP;
			parameters[4].Value = model.SubTime;
			parameters[5].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tousu set DelFlag=1");
			strSql.Append(" where ID="+ID);
			DbHelperMySQL.ExecuteSql1(strSql.ToString());
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.Tousu GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            //strSql.Append("select ID,DeptID,DeptName,Content,SubUID,SubIP,SubTime,DelFlag from tousu ");
            //strSql.Append(" where ID=@ID ");
            //MySqlParameter[] parameters = {
            //        new MySqlParameter("@ID", MySqlDbType.Int32)};
            //parameters[0].Value = ID;

			SanZi.Model.Tousu model=new SanZi.Model.Tousu();
            
			//DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
            DataSet ds = DbHelperMySQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				if(ds.Tables[0].Rows[0]["SubUID"].ToString()!="")
				{
					model.SubUID=int.Parse(ds.Tables[0].Rows[0]["SubUID"].ToString());
				}
				model.SubIP=ds.Tables[0].Rows[0]["SubIP"].ToString();
				if(ds.Tables[0].Rows[0]["SubTime"].ToString()!="")
				{
					model.SubTime=ds.Tables[0].Rows[0]["SubTime"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DelFlag"].ToString()!="")
				{
					model.DelFlag=int.Parse(ds.Tables[0].Rows[0]["DelFlag"].ToString());
				}

                model.DeptID = int.Parse(ds.Tables[0].Rows[0]["DeptID"].ToString());
                model.DeptName = ds.Tables[0].Rows[0]["DeptName"].ToString();
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
			strSql.Append("select ID,DeptID,DeptName,Content,SubUID,SubIP,SubTime,DelFlag ");
			strSql.Append(" FROM tousu ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query1(strSql.ToString());
		}

        public DataSet GetTouSuByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DeptID,DeptName,Content,SubUID,SubIP,SubTime,DelFlag ");
            strSql.Append(" FROM tousu ");
            strSql.Append(" where id= "+id);
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
			parameters[0].Value = "tousu";
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

