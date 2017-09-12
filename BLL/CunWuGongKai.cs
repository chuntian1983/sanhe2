using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using SanZi.Model;

namespace SanZi.BLL
{
    public class CunWuGongKai
    {
        private readonly SanZi.DAL.CunWuGongKai dal = new SanZi.DAL.CunWuGongKai();
        
        public CunWuGongKai()
        {
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(string title, int deptid, string deptname, string filename, int uid)
        {
            dal.Add(title,deptid,deptname,filename,uid);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(int id,string title,int deptid,string deptname,string filename)
        {
            dal.Update(id,title, deptid, deptname, filename);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 获得村务公开列表
        /// </summary>
        public DataSet GetList(string strWhere,string subid)
        {
            return dal.GetList(strWhere,subid);
        }

        /// <summary>
        /// 取村务公开信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataTable getInfoByID(int id)
        {
            return dal.getInfoByID(id).Tables[0];
        }


    }
}
