using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
namespace SanZi.BLL
{
	/// <summary>
	/// ҵ���߼���project ��ժҪ˵����
	/// </summary>
	public class Project
	{
		private readonly SanZi.DAL.Project dal=new SanZi.DAL.Project();
		public Project()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(SanZi.Model.Project model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(SanZi.Model.Project model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int ID)
		{
			
			dal.Delete(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SanZi.Model.Project GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public SanZi.Model.Project GetModelByCache(int ID)
		{
			
			string CacheKey = "projectModel-" + ID;
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
			return (SanZi.Model.Project)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SanZi.Model.Project> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SanZi.Model.Project> DataTableToList(DataTable dt)
		{
			List<SanZi.Model.Project> modelList = new List<SanZi.Model.Project>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SanZi.Model.Project model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SanZi.Model.Project();
					if(dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					model.ProjectName=dt.Rows[n]["ProjectName"].ToString();
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
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  ��Ա����
	}
}

