using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
namespace SanZi.BLL
{
	/// <summary>
	/// ҵ���߼���pibankabarcode ��ժҪ˵����
	/// </summary>
	public class PiBanKaBarcode
	{
		private readonly SanZi.DAL.PiBankaBarcode dal=new SanZi.DAL.PiBankaBarcode();
		public PiBanKaBarcode()
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
		public void Add(SanZi.Model.PiBanKaBarcode model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(SanZi.Model.PiBanKaBarcode model)
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
		public SanZi.Model.PiBanKaBarcode GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public SanZi.Model.PiBanKaBarcode GetModelByCache(int ID)
		{
			
			string CacheKey = "pibankabarcodeModel-" + ID;
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
			return (SanZi.Model.PiBanKaBarcode)objModel;
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
		public List<SanZi.Model.PiBanKaBarcode> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<SanZi.Model.PiBanKaBarcode> DataTableToList(DataTable dt)
		{
			List<SanZi.Model.PiBanKaBarcode> modelList = new List<SanZi.Model.PiBanKaBarcode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SanZi.Model.PiBanKaBarcode model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SanZi.Model.PiBanKaBarcode();
					if(dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["PID"].ToString()!="")
					{
						model.PID=int.Parse(dt.Rows[n]["PID"].ToString());
					}
					model.BarCode=dt.Rows[n]["BarCode"].ToString();
					if(dt.Rows[n]["SubTime"].ToString()!="")
					{
						model.SubTime=int.Parse(dt.Rows[n]["SubTime"].ToString());
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

