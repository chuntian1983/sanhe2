using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
namespace SanZi.BLL
{
	/// <summary>
	/// 业务逻辑类ceping 的摘要说明。
	/// </summary>
	public class CePing
	{
		private readonly SanZi.DAL.Ceping dal=new SanZi.DAL.Ceping();
		public CePing()
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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SanZi.Model.CePing model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.CePing model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			dal.Delete(ID);
		}
        /// <summary>
        /// 民主测评 删除一条数据
        /// </summary>
        public void DeleteMzcp(int ID)
        {

            dal.DeleteMzcp(ID);
        }
        
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.CePing GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SanZi.Model.CePing GetModelByCache(int ID)
		{
			
			string CacheKey = "cepingModel-" + ID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SanZi.Model.CePing)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// 获得民主测评结果数据列表
        /// </summary>
        public DataSet GetMzcpList(string strWhere)
        {
            return dal.GetMzcpList(strWhere);
        }

        
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SanZi.Model.CePing> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SanZi.Model.CePing> DataTableToList(DataTable dt)
		{
			List<SanZi.Model.CePing> modelList = new List<SanZi.Model.CePing>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SanZi.Model.CePing model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SanZi.Model.CePing();
					if(dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					model.Evaluation=dt.Rows[n]["Evaluation"].ToString();
					if(dt.Rows[n]["SubUID"].ToString()!="")
					{
						model.SubUID=int.Parse(dt.Rows[n]["SubUID"].ToString());
					}
					model.SubIP=dt.Rows[n]["SubIP"].ToString();
					if(dt.Rows[n]["SubTime"].ToString()!="")
					{
						model.SubTime=int.Parse(dt.Rows[n]["SubTime"].ToString());
					}
					if(dt.Rows[n]["OptionChecked"].ToString()!="")
					{
						model.OptionChecked=int.Parse(dt.Rows[n]["OptionChecked"].ToString());
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

        //2010/9/27王亚琪添加
        //添加民主测评
        public void Addcpjg(SanZi.Model.mzcp model)
        {
            dal.Addcpjg(model);
        }
        //添加村预算书
        public void Addcyss(SanZi.Model.cyss model)
        {
            dal.Addcyss(model);
        }
          public void Updatecyss(SanZi.Model.cyss model)
        {
            dal.Updatecyss(model);
        }
        
        /// <summary>
        /// 查看测评结果详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetCePingJieGuoByID(int id)
        {
            DataTable dt = dal.GetCePingJieGuoByID(id).Tables[0];
            return dt;
        }

		#endregion  成员方法
	}
}

