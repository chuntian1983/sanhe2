using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;
using System.Text;
namespace SanZi.BLL
{
    /// <summary>
    /// 业务逻辑类pibanka 的摘要说明。
    /// </summary>
    public class PiBanKa
    {
        private readonly SanZi.DAL.PiBanKa dal = new SanZi.DAL.PiBanKa();
        public PiBanKa()
        { }


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
        public bool Exists(int PID)
        {
            return dal.Exists(PID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SanZi.Model.PiBanKa model)
        {
            dal.Add(model);
        }
        public void Add1(SanZi.Model.PiBanKa model)
        {
            dal.Add1(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SanZi.Model.PiBanKa model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int PID)
        {

            dal.Delete(PID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SanZi.Model.PiBanKa GetModel(int PID)
        {

            return dal.GetModel(PID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public SanZi.Model.PiBanKa GetModelByCache(int PID)
        {

            string CacheKey = "pibankaModel-" + PID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(PID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SanZi.Model.PiBanKa)objModel;
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
        public List<SanZi.Model.PiBanKa> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SanZi.Model.PiBanKa> DataTableToList(DataTable dt)
        {
            List<SanZi.Model.PiBanKa> modelList = new List<SanZi.Model.PiBanKa>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SanZi.Model.PiBanKa model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SanZi.Model.PiBanKa();
                    if (dt.Rows[n]["PID"].ToString() != "")
                    {
                        model.PID = int.Parse(dt.Rows[n]["PID"].ToString());
                    }
                    if (dt.Rows[n]["DeptID"].ToString() != "")
                    {
                        model.DeptID = int.Parse(dt.Rows[n]["DeptID"].ToString());
                    }
                    if (dt.Rows[n]["Reason"].ToString() != "")
                    {
                        model.Reason = decimal.Parse(dt.Rows[n]["Reason"].ToString());
                    }
                    if (dt.Rows[n]["OutMoney"].ToString() != "")
                    {
                        model.OutMoney = decimal.Parse(dt.Rows[n]["OutMoney"].ToString());
                    }
                    if (dt.Rows[n]["SubUID"].ToString() != "")
                    {
                        model.SubUID = int.Parse(dt.Rows[n]["SubUID"].ToString());
                    }
                    if (dt.Rows[n]["SubTime"].ToString() != "")
                    {
                        model.SubTime = int.Parse(dt.Rows[n]["SubTime"].ToString());
                    }
                    if (dt.Rows[n]["DelFlag"].ToString() != "")
                    {
                        model.DelFlag = int.Parse(dt.Rows[n]["DelFlag"].ToString());
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

        #region 根据支出金额查询审批条件

        public DataTable getConditionByDept(int deptid)
        {
            DataTable dt = dal.GetCondition(deptid).Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据支出金额查询审批条件
        /// </summary>
        /// <param name="money"></param>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public string getCondition(decimal money, int deptid)
        {
            string wh = string.Format("where Step1<={0} and Step2>{0}", money.ToString());
            DataTable titles = dal.GetConditionBy(wh);
            if (titles.Rows.Count > 0)
            {
                StringBuilder sbReturn = new StringBuilder();
                sbReturn.Append("<table cellSpacing=1 cellPadding=4 bgColor=#999999 border=0 width='455px' style='table-layout:fixed'>");
                sbReturn.Append("<tr bgcolor='#edf2f7'><td>需要");
                foreach (DataRow drow in titles.Rows)
                {
                    sbReturn.AppendFormat("{0}等{1}人员，", drow["UserTitles"].ToString(), drow["BiliShow"].ToString());
                }
                sbReturn.Append("审核通过。</td></tr></table>");
                return sbReturn.ToString();
            }
            else
            {
                return "<span style='color:red'>没有审核人员！</span>";
            }
        }
        #endregion

        /// <summary>
        /// 保存批办卡信息
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="reason"></param>
        /// <param name="money"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public string AddPiBanKa(int deptid, string reason, float money, string barcode)
        {
            string strResult = string.Empty;//返回信息
            strResult = "ok|成功保存";
            strResult = "error|错误原因";
            return "";
        }

        /// <summary>
        /// 根据单位查询审批条件
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="stepA"></param>
        /// <param name="stepB"></param>
        /// <param name="conID"></param>
        public void getConditionByDept(int deptid, ref string stepA, ref string stepB, ref string conID)
        {
            DataTable dt = dal.GetCondition(deptid).Tables[0];
            if (dt.Rows.Count == 1)
            {
                stepA = dt.Rows[0]["step1"].ToString();
                stepB = dt.Rows[0]["step2"].ToString();
                conID = dt.Rows[0]["id"].ToString();
            }
            else
            {
                stepA = dt.Rows[1]["step1"].ToString();
                stepB = dt.Rows[1]["step2"].ToString();
                conID = dt.Rows[1]["id"].ToString();
            }
        }

        /// <summary>
        /// 保存单位支出金额设置
        /// </summary>
        /// <param name="stepa"></param>
        /// <param name="stepB"></param>
        /// <param name="deptID"></param>
        /// <param name="conID"></param>
        public void SetCondition(float stepA, float stepB, int deptID)
        {
            dal.UpdateCondition(stepA, stepB, deptID);
        }

        /// <summary>
        /// 保存批办卡信息
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="deptname"></param>
        /// <param name="reason"></param>
        /// <param name="outMoney"></param>
        /// <param name="struid"></param>
        /// <returns>批办卡ID</returns>
        public int SavePiBanKa(int deptid, int SubUID, string deptname, string reason, decimal outMoney, string struid, string d, string c, string lujing, string zhaiyao, string time1, string time2, string time3, string time4, string time5, string time6, string value1, string value2, string value3)
        {
            return dal.SavePiBanKa(deptid, SubUID, deptname, reason, outMoney, struid, d, c,lujing,zhaiyao,time1,time2,time3,time4,time5,time6,value1,value2,value3);
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="struid"></param>
        /// <returns></returns>
        public DataTable GetUserInfoByID(string struid)
        {
            DataTable dt = dal.GetUserInfoByID(struid).Tables[0];
            return dt;
        }

        /// <summary>
        /// 取村民代表总数
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public int GetCunMinDaiBiaoNum(int deptid)
        {
            DataTable dt = dal.GetCunMinDaiBiaoNum(deptid).Tables[0];
            return dt.Rows.Count;
        }

        public DataTable GetPiBanKaByPid(int pid)
        {
            DataTable dt = dal.GetPiBanKaByPid(pid).Tables[0];
            return dt;
        }

        /// <summary>
        /// 取批办卡审批人
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetShenPiRen(int pid)
        {
            DataTable dt = dal.GetShenPiRen(pid).Tables[0];
            return dt;
        }

        /// <summary>
        /// 删除批办卡
        /// </summary>
        /// <param name="pid"></param>
        public void DelPiBanKa(int pid)
        {
            dal.DelPiBanka(pid);
        }

    }
}

