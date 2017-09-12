using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
using System.Text;
namespace SanZi.BLL
{
	/// <summary>
	/// 业务逻辑类users 的摘要说明。
	/// </summary>
	public class Users
	{
		private readonly SanZi.DAL.Users dal=new SanZi.DAL.Users();
		public Users()
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
		public bool Exists(int UserID)
		{
			return dal.Exists(UserID);
		}

        /// <summary>
        /// 通过条形码查是否存在该记录
        /// </summary>
        public bool ExistsByBarCode(string BarCode)
        {
            return dal.ExistsByBarCode(BarCode);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void AddUser(SanZi.Model.Users model)
		{
            dal.AddUser(model);
		}
        public string AddUsers(SanZi.Model.Users model)
        {
           return  dal.AddUsers(model).ToString();
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SanZi.Model.Users model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int UserID)
		{
			
			dal.Delete(UserID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SanZi.Model.Users GetModel(int UserID)
		{
			
			return dal.GetModel(UserID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public SanZi.Model.Users GetModelByCache(int UserID)
		{
			
			string CacheKey = "usersModel-" + UserID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SanZi.Model.Users)objModel;
		}

		/// <summary>
		/// 获得权利人列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 取批办卡列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetPiBanKaList(string strWhere)
        {
            return dal.GetPiBanKaList(strWhere);
        }

		/// <summary>
		/// 通过条形码取用户信息
		/// </summary>
        public DataSet GetUserInfoByBarcode(string Barcode)
		{
            return dal.GetUserInfoByBarcode(Barcode);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SanZi.Model.Users> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SanZi.Model.Users> DataTableToList(DataTable dt)
		{
			List<SanZi.Model.Users> modelList = new List<SanZi.Model.Users>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SanZi.Model.Users model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SanZi.Model.Users();
					if(dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
					}
					if(dt.Rows[n]["TitleID"].ToString()!="")
					{
						model.TitleID=int.Parse(dt.Rows[n]["TitleID"].ToString());
					}
					model.TitleName=dt.Rows[n]["TitleName"].ToString();
					if(dt.Rows[n]["DeptID"].ToString()!="")
					{
						model.DeptID=int.Parse(dt.Rows[n]["DeptID"].ToString());
					}
					model.DeptName=dt.Rows[n]["DeptName"].ToString();
					model.TrueName=dt.Rows[n]["TrueName"].ToString();
					model.BarCode=dt.Rows[n]["BarCode"].ToString();
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
        /// 导出用户数据
        /// </summary>
        /// <returns></returns>
        public DataSet ExportUserData()
        {
            return dal.ExportUserData();
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="pid"></param>
        public void DelUser(int uid)
        {
            dal.DelUser(uid);
        }

        /// <summary>
        /// 取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataTable getUserInfoByID(int uid)
        {
            return dal.GetUserInfoByID(uid).Tables[0];
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        public void UpdateUser(int deptid, string deptname, int titleid, string titlename, string dh, string truename, int uid)
        {
            dal.UpdateUser(deptid, deptname, titleid, titlename, dh, truename, uid);
        }
		#endregion  成员方法

    }
}

