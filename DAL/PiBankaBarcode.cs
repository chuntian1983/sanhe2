using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//�����������
namespace SanZi.DAL
{
	/// <summary>
	/// ���ݷ�����pibankabarcode��
	/// </summary>
	public class PiBankaBarcode
	{
		public PiBankaBarcode()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("ID", "pibankabarcode"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from pibankabarcode");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(SanZi.Model.PiBanKaBarcode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into pibankabarcode(");
			strSql.Append("PID,BarCode,SubTime,DelFlag)");
			strSql.Append(" values (");
			strSql.Append("@PID,@BarCode,@SubTime,@DelFlag)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@PID", MySqlDbType.Int32,11),
					new MySqlParameter("@BarCode", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.BarCode;
			parameters[2].Value = model.SubTime;
			parameters[3].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(SanZi.Model.PiBanKaBarcode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update pibankabarcode set ");
			strSql.Append("PID=@PID,");
			strSql.Append("BarCode=@BarCode,");
			strSql.Append("SubTime=@SubTime,");
			strSql.Append("DelFlag=@DelFlag");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11),
					new MySqlParameter("@PID", MySqlDbType.Int32,11),
					new MySqlParameter("@BarCode", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.PID;
			parameters[2].Value = model.BarCode;
			parameters[3].Value = model.SubTime;
			parameters[4].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from pibankabarcode ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)};
			parameters[0].Value = ID;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SanZi.Model.PiBanKaBarcode GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,BarCode,SubTime,DelFlag from pibankabarcode ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)};
			parameters[0].Value = ID;

			SanZi.Model.PiBanKaBarcode model=new SanZi.Model.PiBanKaBarcode();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PID"].ToString()!="")
				{
					model.PID=int.Parse(ds.Tables[0].Rows[0]["PID"].ToString());
				}
				model.BarCode=ds.Tables[0].Rows[0]["BarCode"].ToString();
				if(ds.Tables[0].Rows[0]["SubTime"].ToString()!="")
				{
					model.SubTime=int.Parse(ds.Tables[0].Rows[0]["SubTime"].ToString());
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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,BarCode,SubTime,DelFlag ");
			strSql.Append(" FROM pibankabarcode ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// ��ҳ��ȡ�����б�
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
			parameters[0].Value = "pibankabarcode";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  ��Ա����
	}
}

