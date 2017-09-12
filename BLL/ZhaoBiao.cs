using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;


namespace SanZi.BLL
{
  public  class ZhaoBiao
    {
        public readonly SanZi.DAL.ZhaoBiao dal = new SanZi.DAL.ZhaoBiao();
        public ZhaoBiao() { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SanZi.Model.zbgg model)
        {
            dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SanZi.Model.zbgg model)
        {
            dal.Update(model);
        }
        /// <summary>
        ///竞价招投标记录 增加一条数据
        /// </summary>
        public void Addjjztb(SanZi.Model.jjztb model)
        {
            dal.Addjjztb(model);
        }
        /// <summary>
        ///竞价招投标记录 更新一条数据
        /// </summary>
        public void Updatejjztb(SanZi.Model.jjztb model)
        {
            dal.Updatejjztb(model);
        }
        /// <summary>
        ///中标公告 增加一条数据
        /// </summary>
        public void Addzbgg(SanZi.Model.zhongb model)
        {
            dal.Addzbgg(model);
        }
        /// <summary>
        ///中标公告 更新一条数据
        /// </summary>
        public void Updatezbgg(SanZi.Model.zhongb model)
        {
            dal.Updatezbgg(model);
        }
        /// <summary>
        ///参投登记表 增加一条数据
        /// </summary>
        public void Addctdjb(SanZi.Model.ctdjb model)
        {
            
            dal.Addctdjb(model);
        }
     
        /// <summary>
        ///项目 增加一条数据
        /// </summary>
        public void Addxiangmu(SanZi.Model.xiangmu model)
        {
            dal.Addxiangmu(model);
        }
       
        /// <summary>
        ///项目 更新一条数据
        /// </summary>
        public void UpdateXM(SanZi.Model.xiangmu model)
        {
            dal.UpdateXM(model);
        }
        /// <summary>
        ///项目 查看项目
        /// </summary>
        public DataSet selectxiangmu(string strWhere)
        {
            return dal.selectxiangmu(strWhere);
        }
        /// <summary>
        /// 查看参投登记表
        /// </summary>
        public DataSet selectctdj(string strWhere)
        {
            return dal.selectctdj(strWhere);
        }

        /// <summary>
        /// 村预算书 删除一条数据
        /// </summary>
        public void DeleteCyss(int ID)
        {

            dal.DeleteCyss(ID);
        }

        /// <summary>
        /// 获得村预算书数据列表
        /// </summary>
        public DataSet GetCyssList(string strWhere)
        {
            return dal.GetCyssList(strWhere);
        }

        /// <summary>
        /// 竞价招投标 删除一条数据
        /// </summary>
        public void DeleteJjztb(int ID)
        {

            dal.DeleteJjztb(ID);
        }

        /// <summary>
        /// 获得竞价招投标 数据列表
        /// </summary>
        public DataSet GetJjztbList(string strWhere)
        {
            return dal.GetJjztbList(strWhere);
        }
        /// <summary>
        /// 中标公告 删除一条数据
        /// </summary>
        public void DeleteZhongb(int ID)
        {

            dal.DeleteZhongb(ID);
        }

        /// <summary>
        /// 参投登记表 删除一条数据
        /// </summary>
        public void DeleteCtdjb(int ID)
        {

            dal.DeleteCtdjb(ID);
        }

        /// <summary>
        /// 招标公告 删除一条数据
        /// </summary>
        public void DeleteZbgg(int ID)
        {

            dal.DeleteZbgg(ID);
        }
        /// <summary>
        /// 获得竞价招投标 数据列表
        /// </summary>
        public DataSet GetZhongbList(string strWhere)
        {
            return dal.GetZhongbList(strWhere);
        }

        /// <summary>
        /// 获得参投登记表 数据列表
        /// </summary>
        public DataSet GetCtdjbList(string strWhere)
        {
            return dal.GetCtdjbList(strWhere);
        }

        /// <summary>
        /// 获得招标公告 数据列表
        /// </summary>
        public DataSet GetZbggList(string strWhere)
        {
            return dal.GetZbggList(strWhere);
        }

        /// <summary>
        /// 获得中标公告 数据列表
        /// </summary>
        public DataSet GetZbgonggList(string strWhere)
        {
            return dal.GetZbgonggList(strWhere);
        }
        /// <summary>
        /// 获得项目 数据列表
        /// </summary>
        public DataSet GetXiangmuList(string strWhere)
        {
            return dal.GetXiangmuList(strWhere);
        }

        /// <summary>
        /// 查看中标公告详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetZhongBiaoGongGaoByID(int id)
        {
            DataTable dt = dal.GetZhongBiaoGongGaoByID(id).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查看竞价招投标记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetZhaoTouBiaoByID(int id)
        {
            DataTable dt = dal.GetZhaoTouBiaoByID(id).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查看预算书详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetYuSuanShuByID(int id)
        {
            DataTable dt = dal.GetYuSuanShuByID(id).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查看招标公告详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetZhaoBiaoGongGaoByID(int id)
        {
            DataTable dt = dal.GetZhaoBiaoGongGaoByID(id).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获得项目列表
        /// </summary>
        public DataSet getProjectList(string strWhere)
        {
            return dal.getProjectList(strWhere);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="pid"></param>
        public void DelProject(int id)
        {
            dal.DelProject(id);
        }

        /// <summary>
        /// 取项目信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataTable getProjectByID(int id)
        {
            return dal.getProjectByID(id).Tables[0];
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        public void UpdateProject(int pid, string projectName)
        {
            dal.UpdateProject(pid, projectName);
        }

    }
}
