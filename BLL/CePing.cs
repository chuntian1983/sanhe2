using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
namespace SanZi.BLL
{
	/// <summary>
	/// ҵ���߼���ceping ��ժҪ˵����
	/// </summary>
	public class CePing
	{
		private readonly SanZi.DAL.Ceping dal=new SanZi.DAL.Ceping();
		public CePing()
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
		public void Add(SanZi.Model.CePing model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(SanZi.Model.CePing model)
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
        /// �������� ɾ��һ������
        /// </summary>
        public void DeleteMzcp(int ID)
        {

            dal.DeleteMzcp(ID);
        }
        
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SanZi.Model.CePing GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// �������������������б�
        /// </summary>
        public DataSet GetMzcpList(string strWhere)
        {
            return dal.GetMzcpList(strWhere);
        }

        
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SanZi.Model.CePing> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
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

        //2010/9/27���������
        //�����������
        public void Addcpjg(SanZi.Model.mzcp model)
        {
            dal.Addcpjg(model);
        }
        //��Ӵ�Ԥ����
        public void Addcyss(SanZi.Model.cyss model)
        {
            dal.Addcyss(model);
        }
          public void Updatecyss(SanZi.Model.cyss model)
        {
            dal.Updatecyss(model);
        }
        
        /// <summary>
        /// �鿴���������ϸ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetCePingJieGuoByID(int id)
        {
            DataTable dt = dal.GetCePingJieGuoByID(id).Tables[0];
            return dt;
        }

		#endregion  ��Ա����
	}
}

