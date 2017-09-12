using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
namespace SanZi.BLL
{
	/// <summary>
	/// 业务逻辑类usertitle 的摘要说明。
	/// </summary>
	public class UserTitle
	{
		private readonly SanZi.DAL.UserTitle dal=new SanZi.DAL.UserTitle();
		public UserTitle()
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
		public bool Exists(int TitleID)
		{
			return dal.Exists(TitleID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SanZi.Model.UserTitle model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.UserTitle model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int TitleID)
		{
			
			dal.Delete(TitleID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.UserTitle GetModel(int TitleID)
		{
			
			return dal.GetModel(TitleID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SanZi.Model.UserTitle GetModelByCache(int TitleID)
		{
			
			string CacheKey = "usertitleModel-" + TitleID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TitleID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SanZi.Model.UserTitle)objModel;
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
		public List<SanZi.Model.UserTitle> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SanZi.Model.UserTitle> DataTableToList(DataTable dt)
		{
			List<SanZi.Model.UserTitle> modelList = new List<SanZi.Model.UserTitle>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SanZi.Model.UserTitle model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SanZi.Model.UserTitle();
					if(dt.Rows[n]["TitleID"].ToString()!="")
					{
						model.TitleID=int.Parse(dt.Rows[n]["TitleID"].ToString());
					}
					model.TitleName=dt.Rows[n]["TitleName"].ToString();
					model.TitleDesc=dt.Rows[n]["TitleDesc"].ToString();
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
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法
	}
}

