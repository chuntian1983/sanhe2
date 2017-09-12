using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace SanZi.DAL
{
	/// <summary>
	/// 数据访问类daili。
	/// </summary>
	public class DaiLi
	{
		public DaiLi()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("DaiLi_ID", "daili"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DaiLi_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from daili");
			strSql.Append(" where DaiLi_ID=@DaiLi_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@DaiLi_ID", MySqlDbType.Int32)};
			parameters[0].Value = DaiLi_ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SanZi.Model.DaiLi model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into daili(");
			strSql.Append("VillageName,ApplyDate,ProjectName,EstimateValue,ZhiBu_YuanYin,ZhiBu_Time,ZhiBu_ShiXiang,LiangWei_Time,ShenYi_Tiime,YiAnGongKai_FromDate,YiAnGongKai_EndDate,CunMin_Time,CunMin_TotalNum,CunMin_AttendNum,CunMin_PassNum,CunMin_PassRate,JieGuoGongKai_FromDate,JieGuoGongKai_EndDate,ZhiBuShuJi,CuZhuren,CunWuJianDuZuZhang,LianZhengJianDuYuan,XiangZhenYiJian,SubTime,SubUID,DelFlag)");
			strSql.Append(" values (");
			strSql.Append("@VillageName,@ApplyDate,@ProjectName,@EstimateValue,@ZhiBu_YuanYin,@ZhiBu_Time,@ZhiBu_ShiXiang,@LiangWei_Time,@ShenYi_Tiime,@YiAnGongKai_FromDate,@YiAnGongKai_EndDate,@CunMin_Time,@CunMin_TotalNum,@CunMin_AttendNum,@CunMin_PassNum,@CunMin_PassRate,@JieGuoGongKai_FromDate,@JieGuoGongKai_EndDate,@ZhiBuShuJi,@CuZhuren,@CunWuJianDuZuZhang,@LianZhengJianDuYuan,@XiangZhenYiJian,@SubTime,@SubUID,@DelFlag)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@VillageName", MySqlDbType.VarChar,50),
					new MySqlParameter("@ApplyDate", MySqlDbType.Int32,11),
					new MySqlParameter("@ProjectName", MySqlDbType.VarChar,100),
					new MySqlParameter("@EstimateValue", MySqlDbType.VarChar,50),
					new MySqlParameter("@ZhiBu_YuanYin", MySqlDbType.VarChar,100),
					new MySqlParameter("@ZhiBu_Time", MySqlDbType.Int32,11),
					new MySqlParameter("@ZhiBu_ShiXiang", MySqlDbType.VarChar,100),
					new MySqlParameter("@LiangWei_Time", MySqlDbType.Int32,11),
					new MySqlParameter("@ShenYi_Tiime", MySqlDbType.Int32,11),
					new MySqlParameter("@YiAnGongKai_FromDate", MySqlDbType.Int32,11),
					new MySqlParameter("@YiAnGongKai_EndDate", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_Time", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_TotalNum", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_AttendNum", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_PassNum", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_PassRate", MySqlDbType.Float),
					new MySqlParameter("@JieGuoGongKai_FromDate", MySqlDbType.Int32,11),
					new MySqlParameter("@JieGuoGongKai_EndDate", MySqlDbType.Int32,11),
					new MySqlParameter("@ZhiBuShuJi", MySqlDbType.VarChar,50),
					new MySqlParameter("@CuZhuren", MySqlDbType.VarChar,50),
					new MySqlParameter("@CunWuJianDuZuZhang", MySqlDbType.VarChar,50),
					new MySqlParameter("@LianZhengJianDuYuan", MySqlDbType.VarChar,50),
					new MySqlParameter("@XiangZhenYiJian", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,4)};
			parameters[0].Value = model.VillageName;
			parameters[1].Value = model.ApplyDate;
			parameters[2].Value = model.ProjectName;
			parameters[3].Value = model.EstimateValue;
			parameters[4].Value = model.ZhiBu_YuanYin;
			parameters[5].Value = model.ZhiBu_Time;
			parameters[6].Value = model.ZhiBu_ShiXiang;
			parameters[7].Value = model.LiangWei_Time;
			parameters[8].Value = model.ShenYi_Tiime;
			parameters[9].Value = model.YiAnGongKai_FromDate;
			parameters[10].Value = model.YiAnGongKai_EndDate;
			parameters[11].Value = model.CunMin_Time;
			parameters[12].Value = model.CunMin_TotalNum;
			parameters[13].Value = model.CunMin_AttendNum;
			parameters[14].Value = model.CunMin_PassNum;
			parameters[15].Value = model.CunMin_PassRate;
			parameters[16].Value = model.JieGuoGongKai_FromDate;
			parameters[17].Value = model.JieGuoGongKai_EndDate;
			parameters[18].Value = model.ZhiBuShuJi;
			parameters[19].Value = model.CuZhuren;
			parameters[20].Value = model.CunWuJianDuZuZhang;
			parameters[21].Value = model.LianZhengJianDuYuan;
			parameters[22].Value = model.XiangZhenYiJian;
			parameters[23].Value = model.SubTime;
			parameters[24].Value = model.SubUID;
			parameters[25].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.DaiLi model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update daili set ");
			strSql.Append("VillageName=@VillageName,");
			strSql.Append("ApplyDate=@ApplyDate,");
			strSql.Append("ProjectName=@ProjectName,");
			strSql.Append("EstimateValue=@EstimateValue,");
			strSql.Append("ZhiBu_YuanYin=@ZhiBu_YuanYin,");
			strSql.Append("ZhiBu_Time=@ZhiBu_Time,");
			strSql.Append("ZhiBu_ShiXiang=@ZhiBu_ShiXiang,");
			strSql.Append("LiangWei_Time=@LiangWei_Time,");
			strSql.Append("ShenYi_Tiime=@ShenYi_Tiime,");
			strSql.Append("YiAnGongKai_FromDate=@YiAnGongKai_FromDate,");
			strSql.Append("YiAnGongKai_EndDate=@YiAnGongKai_EndDate,");
			strSql.Append("CunMin_Time=@CunMin_Time,");
			strSql.Append("CunMin_TotalNum=@CunMin_TotalNum,");
			strSql.Append("CunMin_AttendNum=@CunMin_AttendNum,");
			strSql.Append("CunMin_PassNum=@CunMin_PassNum,");
			strSql.Append("CunMin_PassRate=@CunMin_PassRate,");
			strSql.Append("JieGuoGongKai_FromDate=@JieGuoGongKai_FromDate,");
			strSql.Append("JieGuoGongKai_EndDate=@JieGuoGongKai_EndDate,");
			strSql.Append("ZhiBuShuJi=@ZhiBuShuJi,");
			strSql.Append("CuZhuren=@CuZhuren,");
			strSql.Append("CunWuJianDuZuZhang=@CunWuJianDuZuZhang,");
			strSql.Append("LianZhengJianDuYuan=@LianZhengJianDuYuan,");
			strSql.Append("XiangZhenYiJian=@XiangZhenYiJian,");
			strSql.Append("SubTime=@SubTime,");
			strSql.Append("SubUID=@SubUID,");
            strSql.Append("time1=@time1,");
            strSql.Append("time2=@time2,");
            strSql.Append("time3=@time3,");
            strSql.Append("time4=@time4,");
            strSql.Append("time5=@time5,");
            strSql.Append("time6=@time6,");
            strSql.Append("time7=@time7,");
            strSql.Append("time8=@time8,");
            strSql.Append("time9=@time9,");
            strSql.Append("value1=@value1,");
            strSql.Append("value2=@value2,");
            strSql.Append("value3=@value3,");
            strSql.Append("value4=@value4,");
            strSql.Append("value5=@value5,");
            strSql.Append("value6=@value6,");
			strSql.Append("DelFlag=@DelFlag");
			strSql.Append(" where DaiLi_ID=@DaiLi_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@DaiLi_ID", MySqlDbType.Int32,11),
					new MySqlParameter("@VillageName", MySqlDbType.VarChar,50),
					new MySqlParameter("@ApplyDate", MySqlDbType.Int32,11),
					new MySqlParameter("@ProjectName", MySqlDbType.VarChar,100),
					new MySqlParameter("@EstimateValue", MySqlDbType.VarChar,50),
					new MySqlParameter("@ZhiBu_YuanYin", MySqlDbType.VarChar,100),
					new MySqlParameter("@ZhiBu_Time", MySqlDbType.Int32,11),
					new MySqlParameter("@ZhiBu_ShiXiang", MySqlDbType.VarChar,100),
					new MySqlParameter("@LiangWei_Time", MySqlDbType.Int32,11),
					new MySqlParameter("@ShenYi_Tiime", MySqlDbType.Int32,11),
					new MySqlParameter("@YiAnGongKai_FromDate", MySqlDbType.Int32,11),
					new MySqlParameter("@YiAnGongKai_EndDate", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_Time", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_TotalNum", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_AttendNum", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_PassNum", MySqlDbType.Int32,11),
					new MySqlParameter("@CunMin_PassRate", MySqlDbType.Float),
					new MySqlParameter("@JieGuoGongKai_FromDate", MySqlDbType.Int32,11),
					new MySqlParameter("@JieGuoGongKai_EndDate", MySqlDbType.Int32,11),
					new MySqlParameter("@ZhiBuShuJi", MySqlDbType.VarChar,50),
					new MySqlParameter("@CuZhuren", MySqlDbType.VarChar,50),
					new MySqlParameter("@CunWuJianDuZuZhang", MySqlDbType.VarChar,50),
					new MySqlParameter("@LianZhengJianDuYuan", MySqlDbType.VarChar,50),
					new MySqlParameter("@XiangZhenYiJian", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
                    new MySqlParameter("@time1", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time2", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time3", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time4", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time5", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time6", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time7", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time8", MySqlDbType.VarChar,50),
                    new MySqlParameter("@time9", MySqlDbType.VarChar,50),
                    new MySqlParameter("@value1", MySqlDbType.VarChar,50),
                    new MySqlParameter("@value2", MySqlDbType.VarChar,50),
                    new MySqlParameter("@value3", MySqlDbType.VarChar,50),
                    new MySqlParameter("@value4", MySqlDbType.VarChar,50),
                    new MySqlParameter("@value5", MySqlDbType.VarChar,50),
                    new MySqlParameter("@value6", MySqlDbType.VarChar,100),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,4)};
			parameters[0].Value = model.DaiLi_ID;
			parameters[1].Value = model.VillageName;
			parameters[2].Value = model.ApplyDate;
			parameters[3].Value = model.ProjectName;
			parameters[4].Value = model.EstimateValue;
			parameters[5].Value = model.ZhiBu_YuanYin;
			parameters[6].Value = model.ZhiBu_Time;
			parameters[7].Value = model.ZhiBu_ShiXiang;
			parameters[8].Value = model.LiangWei_Time;
			parameters[9].Value = model.ShenYi_Tiime;
			parameters[10].Value = model.YiAnGongKai_FromDate;
			parameters[11].Value = model.YiAnGongKai_EndDate;
			parameters[12].Value = model.CunMin_Time;
			parameters[13].Value = model.CunMin_TotalNum;
			parameters[14].Value = model.CunMin_AttendNum;
			parameters[15].Value = model.CunMin_PassNum;
			parameters[16].Value = model.CunMin_PassRate;
			parameters[17].Value = model.JieGuoGongKai_FromDate;
			parameters[18].Value = model.JieGuoGongKai_EndDate;
			parameters[19].Value = model.ZhiBuShuJi;
			parameters[20].Value = model.CuZhuren;
			parameters[21].Value = model.CunWuJianDuZuZhang;
			parameters[22].Value = model.LianZhengJianDuYuan;
			parameters[23].Value = model.XiangZhenYiJian;
			parameters[24].Value = model.SubTime;
            parameters[25].Value = model.SubUID;
            parameters[26].Value = model.time1;
            parameters[27].Value = model.time2;
            parameters[28].Value = model.time3;
            parameters[29].Value = model.time4;
            parameters[30].Value = model.time5;
            parameters[31].Value = model.time6;
            parameters[32].Value = model.time7;
            parameters[33].Value = model.time8;
            parameters[34].Value = model.time9;
            parameters[35].Value = model.value1;
            parameters[36].Value = model.value2;
            parameters[37].Value = model.value3;
            parameters[38].Value = model.value4;
            parameters[39].Value = model.value5;
            parameters[40].Value = model.value6;
			parameters[41].Value = model.DelFlag;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int DaiLi_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from daili ");
			strSql.Append(" where DaiLi_ID=@DaiLi_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@DaiLi_ID", MySqlDbType.Int32)};
			parameters[0].Value = DaiLi_ID;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.DaiLi GetModel(int DaiLi_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DaiLi_ID,VillageName,ApplyDate,ProjectName,EstimateValue,ZhiBu_YuanYin,ZhiBu_Time,ZhiBu_ShiXiang,LiangWei_Time,ShenYi_Tiime,YiAnGongKai_FromDate,YiAnGongKai_EndDate,CunMin_Time,CunMin_TotalNum,CunMin_AttendNum,CunMin_PassNum,CunMin_PassRate,JieGuoGongKai_FromDate,JieGuoGongKai_EndDate,ZhiBuShuJi,CuZhuren,CunWuJianDuZuZhang,LianZhengJianDuYuan,XiangZhenYiJian,SubTime,SubUID,DelFlag from daili ");
			strSql.Append(" where DaiLi_ID=@DaiLi_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@DaiLi_ID", MySqlDbType.Int32)};
			parameters[0].Value = DaiLi_ID;

			SanZi.Model.DaiLi model=new SanZi.Model.DaiLi();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DaiLi_ID"].ToString()!="")
				{
					model.DaiLi_ID=int.Parse(ds.Tables[0].Rows[0]["DaiLi_ID"].ToString());
				}
				model.VillageName=ds.Tables[0].Rows[0]["VillageName"].ToString();
				if(ds.Tables[0].Rows[0]["ApplyDate"].ToString()!="")
				{
					model.ApplyDate=ds.Tables[0].Rows[0]["ApplyDate"].ToString();
				}
				model.ProjectName=ds.Tables[0].Rows[0]["ProjectName"].ToString();
				model.EstimateValue=ds.Tables[0].Rows[0]["EstimateValue"].ToString();
				model.ZhiBu_YuanYin=ds.Tables[0].Rows[0]["ZhiBu_YuanYin"].ToString();
				if(ds.Tables[0].Rows[0]["ZhiBu_Time"].ToString()!="")
				{
					model.ZhiBu_Time=int.Parse(ds.Tables[0].Rows[0]["ZhiBu_Time"].ToString());
				}
				model.ZhiBu_ShiXiang=ds.Tables[0].Rows[0]["ZhiBu_ShiXiang"].ToString();
				if(ds.Tables[0].Rows[0]["LiangWei_Time"].ToString()!="")
				{
					model.LiangWei_Time=int.Parse(ds.Tables[0].Rows[0]["LiangWei_Time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShenYi_Tiime"].ToString()!="")
				{
					model.ShenYi_Tiime=int.Parse(ds.Tables[0].Rows[0]["ShenYi_Tiime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["YiAnGongKai_FromDate"].ToString()!="")
				{
					model.YiAnGongKai_FromDate=int.Parse(ds.Tables[0].Rows[0]["YiAnGongKai_FromDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["YiAnGongKai_EndDate"].ToString()!="")
				{
					model.YiAnGongKai_EndDate=int.Parse(ds.Tables[0].Rows[0]["YiAnGongKai_EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CunMin_Time"].ToString()!="")
				{
					model.CunMin_Time=int.Parse(ds.Tables[0].Rows[0]["CunMin_Time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CunMin_TotalNum"].ToString()!="")
				{
					model.CunMin_TotalNum=int.Parse(ds.Tables[0].Rows[0]["CunMin_TotalNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CunMin_AttendNum"].ToString()!="")
				{
					model.CunMin_AttendNum=int.Parse(ds.Tables[0].Rows[0]["CunMin_AttendNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CunMin_PassNum"].ToString()!="")
				{
					model.CunMin_PassNum=int.Parse(ds.Tables[0].Rows[0]["CunMin_PassNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CunMin_PassRate"].ToString()!="")
				{
					model.CunMin_PassRate=decimal.Parse(ds.Tables[0].Rows[0]["CunMin_PassRate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JieGuoGongKai_FromDate"].ToString()!="")
				{
					model.JieGuoGongKai_FromDate=int.Parse(ds.Tables[0].Rows[0]["JieGuoGongKai_FromDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JieGuoGongKai_EndDate"].ToString()!="")
				{
					model.JieGuoGongKai_EndDate=int.Parse(ds.Tables[0].Rows[0]["JieGuoGongKai_EndDate"].ToString());
				}
				model.ZhiBuShuJi=ds.Tables[0].Rows[0]["ZhiBuShuJi"].ToString();
				model.CuZhuren=ds.Tables[0].Rows[0]["CuZhuren"].ToString();
				model.CunWuJianDuZuZhang=ds.Tables[0].Rows[0]["CunWuJianDuZuZhang"].ToString();
				model.LianZhengJianDuYuan=ds.Tables[0].Rows[0]["LianZhengJianDuYuan"].ToString();
				model.XiangZhenYiJian=ds.Tables[0].Rows[0]["XiangZhenYiJian"].ToString();
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
			strSql.Append("select DaiLi_ID,VillageName,ApplyDate,ProjectName,EstimateValue,ZhiBu_YuanYin,ZhiBu_Time,ZhiBu_ShiXiang,LiangWei_Time,ShenYi_Tiime,YiAnGongKai_FromDate,YiAnGongKai_EndDate,CunMin_Time,CunMin_TotalNum,CunMin_AttendNum,CunMin_PassNum,CunMin_PassRate,JieGuoGongKai_FromDate,JieGuoGongKai_EndDate,ZhiBuShuJi,CuZhuren,CunWuJianDuZuZhang,LianZhengJianDuYuan,XiangZhenYiJian,SubTime,SubUID,DelFlag ");
			strSql.Append(" FROM daili ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			parameters[0].Value = "daili";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        /// <summary>
        /// 取批办卡列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetDaiLiList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.DaiLi_ID,a.DeptID,a.VillageName,a.ApplyDate,a.ProjectName,a.ProjectType,a.EstimateValue,a.ZhiBu_YuanYin,b.DeptName ");
            strSql.Append(" FROM DaiLi a left join Department b on a.DeptID=b.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where a.DelFlag=0 and (a.VillageName like '%" + strWhere + "%' or a.ProjectName like '%" + strWhere + "%')");
            }
            else
            {
                strSql.Append(" where a.DelFlag=0 ");
            }
            strSql.Append(" order by a.DaiLi_ID desc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 保存批办卡信息
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="applyDate"></param>
        /// <param name="background"></param>
        /// <param name="projectName"></param>
        /// <param name="money"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public void SaveDaiLi(int deptid, int SubUID, string deptname, string applyDate, string background, string projectName, string projectType, string money, string userid, string time1, string time2, string time3, string time4, string time5, string time6, string time7, string time8, string time9, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            StringBuilder strSql = new StringBuilder();
            //System.DateTime time = System.DateTime.Now;
            //int intTime = (int)LTP.Common.TimeParser.ConvertDateTimeInt(time);//

            strSql.Append(" insert into daili(DeptID,VillageName,ApplyDate,ProjectName,ZhiBu_YuanYin,EstimateValue,ProjectType,SubUID,time1,time2,time3,time4,time5,time6,time7,time8,time9,value1,value2,value3,value4,value5,value6) ");
            strSql.Append(" values(");
            strSql.Append(deptid + ",'" + deptname + "','" + applyDate + "',");
            strSql.Append("'" + projectName + "','" + background + "',");
            strSql.Append("'" + money + "','" + projectType + "'");
            strSql.Append(",'" + SubUID + "','" + time1 + "','" + time2 + "','" + time3 + "','" + time4 + "','" + time5 + "','" + time6 + "','" + time7 + "','" + time8 + "','" + time9 + "' ,'" + value1 + "','" + value2 + "','" + value3 + "','" + value4 + "','" + value5 + "','" + value6 + "') ");
            DbHelperMySQL.ExecuteSql(strSql.ToString());

            int maxPID = DbHelperMySQL.GetMaxID("DaiLi_ID", "DaiLi") - 1;
            if (userid != "")
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append(" insert into dailibarcode(DaiLi_ID,UID,BarCode,SubTime)");
                strSql2.Append(" select " + maxPID + ",UserID,BarCode,'" + System.DateTime.Now.ToString() + "'");
                strSql2.Append(" from Users where userid in(" + userid + ")");
                DbHelperMySQL.ExecuteSql(strSql2.ToString());
            }
        }

        /// <summary>
        /// 删除代理
        /// </summary>
        /// <param name="uid"></param>
        public void DelDaiLi(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DaiLi  set delflag=1");
            strSql.Append(" WHERE DaiLi_ID=" + id);
            DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询代理申请信息
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataSet GetDaiLiByPid(int pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.DaiLi_ID,a.DeptID,a.VillageName,a.ApplyDate,a.ProjectName,a.ProjectType,a.EstimateValue,a.ZhiBu_YuanYin,b.DeptName,a.time1,a.time2,a.time3,a.time4,a.time5,a.time6,a.time7,a.time8,a.time9,a.value1,a.value2,a.value3,a.value4,a.value5,a.value6 ");
            strSql.Append(" FROM DaiLi a LEFT JOIN Department b  on a.DeptID=b.id");
            strSql.Append(" WHERE a.delflag=0 and a.DaiLi_ID=" + pid);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 取代理申请，已同意村民代表信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataSet GetShenPiRen(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.uid,b.TitleName,b.TitleID,b.DeptID,b.DeptName,b.TrueName,b.Barcode");
            strSql.Append(" FROM dailibarcode a LEFT JOIN users b  on a.UID=b.Userid");
            strSql.Append(" WHERE a.delflag=0 and a.DaiLi_ID="+id);
            return DbHelperMySQL.Query(strSql.ToString());
        }

		#endregion  成员方法
	}
}

