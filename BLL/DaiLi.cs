using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
namespace SanZi.BLL
{
	/// <summary>
	/// 业务逻辑类daili 的摘要说明。
	/// </summary>
	public class DaiLi
	{
		private readonly SanZi.DAL.DaiLi dal=new SanZi.DAL.DaiLi();
		public DaiLi()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DaiLi_ID)
		{
			return dal.Exists(DaiLi_ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SanZi.Model.DaiLi model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.DaiLi model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int DaiLi_ID)
		{
			
			dal.Delete(DaiLi_ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.DaiLi GetModel(int DaiLi_ID)
		{
			
			return dal.GetModel(DaiLi_ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SanZi.Model.DaiLi GetModelByCache(int DaiLi_ID)
		{
			
			string CacheKey = "dailiModel-" + DaiLi_ID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DaiLi_ID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SanZi.Model.DaiLi)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SanZi.Model.DaiLi> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SanZi.Model.DaiLi> DataTableToList(DataTable dt)
		{
			List<SanZi.Model.DaiLi> modelList = new List<SanZi.Model.DaiLi>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SanZi.Model.DaiLi model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SanZi.Model.DaiLi();
					if(dt.Rows[n]["DaiLi_ID"].ToString()!="")
					{
						model.DaiLi_ID=int.Parse(dt.Rows[n]["DaiLi_ID"].ToString());
					}
					model.VillageName=dt.Rows[n]["VillageName"].ToString();
					if(dt.Rows[n]["ApplyDate"].ToString()!="")
					{
						model.ApplyDate=dt.Rows[n]["ApplyDate"].ToString();
					}
					model.ProjectName=dt.Rows[n]["ProjectName"].ToString();
					model.EstimateValue=dt.Rows[n]["EstimateValue"].ToString();
					model.ZhiBu_YuanYin=dt.Rows[n]["ZhiBu_YuanYin"].ToString();
					if(dt.Rows[n]["ZhiBu_Time"].ToString()!="")
					{
						model.ZhiBu_Time=int.Parse(dt.Rows[n]["ZhiBu_Time"].ToString());
					}
					model.ZhiBu_ShiXiang=dt.Rows[n]["ZhiBu_ShiXiang"].ToString();
					if(dt.Rows[n]["LiangWei_Time"].ToString()!="")
					{
						model.LiangWei_Time=int.Parse(dt.Rows[n]["LiangWei_Time"].ToString());
					}
					if(dt.Rows[n]["ShenYi_Tiime"].ToString()!="")
					{
						model.ShenYi_Tiime=int.Parse(dt.Rows[n]["ShenYi_Tiime"].ToString());
					}
					if(dt.Rows[n]["YiAnGongKai_FromDate"].ToString()!="")
					{
						model.YiAnGongKai_FromDate=int.Parse(dt.Rows[n]["YiAnGongKai_FromDate"].ToString());
					}
					if(dt.Rows[n]["YiAnGongKai_EndDate"].ToString()!="")
					{
						model.YiAnGongKai_EndDate=int.Parse(dt.Rows[n]["YiAnGongKai_EndDate"].ToString());
					}
					if(dt.Rows[n]["CunMin_Time"].ToString()!="")
					{
						model.CunMin_Time=int.Parse(dt.Rows[n]["CunMin_Time"].ToString());
					}
					if(dt.Rows[n]["CunMin_TotalNum"].ToString()!="")
					{
						model.CunMin_TotalNum=int.Parse(dt.Rows[n]["CunMin_TotalNum"].ToString());
					}
					if(dt.Rows[n]["CunMin_AttendNum"].ToString()!="")
					{
						model.CunMin_AttendNum=int.Parse(dt.Rows[n]["CunMin_AttendNum"].ToString());
					}
					if(dt.Rows[n]["CunMin_PassNum"].ToString()!="")
					{
						model.CunMin_PassNum=int.Parse(dt.Rows[n]["CunMin_PassNum"].ToString());
					}
					if(dt.Rows[n]["CunMin_PassRate"].ToString()!="")
					{
						model.CunMin_PassRate=decimal.Parse(dt.Rows[n]["CunMin_PassRate"].ToString());
					}
					if(dt.Rows[n]["JieGuoGongKai_FromDate"].ToString()!="")
					{
						model.JieGuoGongKai_FromDate=int.Parse(dt.Rows[n]["JieGuoGongKai_FromDate"].ToString());
					}
					if(dt.Rows[n]["JieGuoGongKai_EndDate"].ToString()!="")
					{
						model.JieGuoGongKai_EndDate=int.Parse(dt.Rows[n]["JieGuoGongKai_EndDate"].ToString());
					}
					model.ZhiBuShuJi=dt.Rows[n]["ZhiBuShuJi"].ToString();
					model.CuZhuren=dt.Rows[n]["CuZhuren"].ToString();
					model.CunWuJianDuZuZhang=dt.Rows[n]["CunWuJianDuZuZhang"].ToString();
					model.LianZhengJianDuYuan=dt.Rows[n]["LianZhengJianDuYuan"].ToString();
					model.XiangZhenYiJian=dt.Rows[n]["XiangZhenYiJian"].ToString();
					if(dt.Rows[n]["SubTime"].ToString()!="")
					{
						model.SubTime=int.Parse(dt.Rows[n]["SubTime"].ToString());
					}
					if(dt.Rows[n]["SubUID"].ToString()!="")
					{
						model.SubUID=int.Parse(dt.Rows[n]["SubUID"].ToString());
					}
					if(dt.Rows[n]["DelFlag"].ToString()!="")
					{
						model.DelFlag=int.Parse(dt.Rows[n]["DelFlag"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

        /// <summary>
        /// 取批办卡列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetDaiLiList(string strWhere)
        {
            return dal.GetDaiLiList(strWhere);
        }

        public void SaveDaiLi(int deptid, int SubUID, string deptName, string applyDate, string background, string projectName, string projectType, string money, string userid, string time1, string time2, string time3, string time4, string time5, string time6, string time7, string time8, string time9, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            dal.SaveDaiLi(deptid, SubUID, deptName, applyDate, background, projectName, projectType, money, userid, time1, time2, time3, time4, time5, time6, time7, time8, time9, value1, value2, value3, value4, value5, value6);
        }

        public void DelDaiLi(int id)
        {
            dal.DelDaiLi(id);
        }

        /// <summary>
        /// 查询代理申请信息
        /// </summary>
        /// <param name="struid"></param>
        /// <returns></returns>
        public DataTable GetDaiLiByPid(int id)
        {
            DataTable dt = dal.GetDaiLiByPid(id).Tables[0];
            return dt;
        }

        /// <summary>
        /// 取代理申请，已同意村民代表信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetShenPiRen(int did)
        {
            DataTable dt = dal.GetShenPiRen(did).Tables[0];
            return dt;
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法
	}
}

