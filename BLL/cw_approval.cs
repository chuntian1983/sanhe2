using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SanZi.DAL;
using LTP.Common;
using System.Data;

namespace SanZi.BLL
{
    /// <summary>
    /// cw_approval
    /// </summary>
    public partial class cw_approval
    {
        private readonly SanZi.DAL.cw_approval dal = new DAL.cw_approval();
        public cw_approval()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SanZi.Model.cw_approval model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SanZi.Model.cw_approval model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SanZi.Model.cw_approval GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public SanZi.Model.cw_approval GetModelByCache(int id)
        {

            string CacheKey = "cw_approvalModel-" + id;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(id);
                    if (objModel != null)
                    {
                        int ModelCache =LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SanZi.Model.cw_approval)objModel;
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
        public List<SanZi.Model.cw_approval> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SanZi.Model.cw_approval> DataTableToList(DataTable dt)
        {
            List<SanZi.Model.cw_approval> modelList = new List<SanZi.Model.cw_approval>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SanZi.Model.cw_approval model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SanZi.Model.cw_approval();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["accountid"] != null && dt.Rows[n]["accountid"].ToString() != "")
                    {
                        model.accountid = dt.Rows[n]["accountid"].ToString();
                    }
                    if (dt.Rows[n]["accounttime"] != null && dt.Rows[n]["accounttime"].ToString() != "")
                    {
                        model.accounttime = DateTime.Parse(dt.Rows[n]["accounttime"].ToString());
                    }
                    if (dt.Rows[n]["creattime"] != null && dt.Rows[n]["creattime"].ToString() != "")
                    {
                        model.creattime = DateTime.Parse(dt.Rows[n]["creattime"].ToString());
                    }
                    if (dt.Rows[n]["pibanka"] != null && dt.Rows[n]["pibanka"].ToString() != "")
                    {
                        model.pibanka = dt.Rows[n]["pibanka"].ToString();
                    }
                    if (dt.Rows[n]["pibankama"] != null && dt.Rows[n]["pibankama"].ToString() != "")
                    {
                        model.pibankama = dt.Rows[n]["pibankama"].ToString();
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
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}
