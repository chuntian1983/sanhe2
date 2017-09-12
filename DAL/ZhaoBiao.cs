using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using Maticsoft.DBUtility;//请先添加引用

namespace SanZi.DAL
{
    public class ZhaoBiao
    {
        public ZhaoBiao() { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SanZi.Model.zbgg model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into zbgg(");
            sb1.Append("cwh,zbgc,nrqk,bmtj,yz,startTime,finishTime,bmdd,lxfs,subTime,str");
            sb2.Append(" values (");
            sb2.Append("'" + model.Cwh + "'");
            sb2.Append(",'" + model.Zbgc + "'");
            sb2.Append(",'" + model.Nrqk + "'");
            sb2.Append(",'" + model.Bmtj + "'");
            sb2.Append(",'" + model.Yz + "'");
            sb2.Append(",'" + model.StartTime + "'");
            sb2.Append(",'" + model.FinishTime + "'");
            sb2.Append(",'" + model.Bmdd + "'");
            sb2.Append(",'" + model.Lxfs + "'");
            sb2.Append(",'" + model.SubTime + "'");
            sb2.Append(",'" + model.str + "'");
            sb1.Append(") ");
            sb2.Append(");");
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SanZi.Model.zbgg model)
        {
            StringBuilder sb1 = new StringBuilder();

            sb1.Append("update zbgg set ");
            sb1.Append("cwh = '" + model.Cwh + "',");
            sb1.Append("zbgc = '" + model.Zbgc + "',");
            sb1.Append("nrqk = '" + model.Nrqk + "',");
            sb1.Append("bmtj = '" + model.Bmtj + "',");
            sb1.Append("yz = '" + model.Yz + "',");
            sb1.Append("startTime = '" + model.StartTime + "',");
            sb1.Append("finishTime = '" + model.FinishTime + "',");
            sb1.Append("bmdd = '" + model.Bmdd + "',");
            sb1.Append("lxfs = '" + model.Lxfs + "',");
            sb1.Append("str = '" + model.str + "',");
            sb1.Append("subTime = '" + model.SubTime + "' ");
            sb1.Append(" where id = '" + model.ID + "'");
            string strsql = sb1.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Addjjztb(SanZi.Model.jjztb model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into jjztb(");
            sb1.Append("xmmc,subTime,adress,cyry,zcr,cbr,jlr,zynr");
            sb2.Append(" values (");
            sb2.Append("'" + model.Xmmc + "'");
            sb2.Append(",'" + model.SubTime + "'");
            sb2.Append(",'" + model.Adress + "'");
            sb2.Append(",'" + model.Cyry + "'");
            sb2.Append(",'" + model.Zcr + "'");
            sb2.Append(",'" + model.Cbr + "'");
            sb2.Append(",'" + model.Jlr + "'");
            sb2.Append(",'" + model.Zynr + "'");
            sb1.Append(") ");
            sb2.Append(");");
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Updatejjztb(SanZi.Model.jjztb model)
        {
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("update jjztb set ");
            sb1.Append("Xmmc = '" + model.Xmmc + "',");
            sb1.Append("SubTime = '" + model.SubTime + "',");
            sb1.Append("Adress = '" + model.Adress + "',");
            sb1.Append("bmtj = '" + model.Cyry + "',");
            sb1.Append("Zcr = '" + model.Zcr + "',");
            sb1.Append("Cbr = '" + model.Cbr + "',");
            sb1.Append("Jlr = '" + model.Jlr + "',");
            sb1.Append("Zynr = '" + model.Zynr + "'");
            sb1.Append(" where id = '" + model.ID + "'");
            string strsql = sb1.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Addzbgg(SanZi.Model.zhongb model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into zhongb(");
            sb1.Append("ncmc,startTime,finishTime,zbmc,dwa,dws,zbdw,ztbdw,subTime");
            sb2.Append(" values (");
            sb2.Append("'" + model.Ncmc + "'");
            sb2.Append(",'" + model.StartTime + "'");
            sb2.Append(",'" + model.FinishTime + "'");
            sb2.Append(",'" + model.Zbmc + "'");
            sb2.Append(",'" + model.Dwa + "'");
            sb2.Append("," + model.Dws + "");
            sb2.Append(",'" + model.Zbdw + "'");
            sb2.Append(",'" + model.Ztbdw + "'");
            sb2.Append(",'" + model.SubTime + "'");
            if (model.Dwb != "")
            {
                sb1.Append(",dwb");
                sb2.Append(",'" + model.Dwb + "'");
            }
            if (model.Dwc != "")
            {
                sb1.Append(",dwc");
                sb2.Append(",'" + model.Dwc + "'");
            }
            sb1.Append(") ");
            sb2.Append(");");
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Updatezbgg(SanZi.Model.zhongb model)
        {
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("update zhongb set ");
            sb1.Append("ncmc = '" + model.Ncmc + "',");
            sb1.Append("startTime = '" + model.StartTime + "',");
            sb1.Append("finishTime = '" + model.FinishTime + "',");
            sb1.Append("zbmc = '" + model.Zbmc + "',");
            sb1.Append("dwa = '" + model.Dwa + "',");
            sb1.Append("dws = '" + model.Dws + "',");
            sb1.Append("zbdw = '" + model.Zbdw + "',");
            sb1.Append("ztbdw = '" + model.Ztbdw + "',");
            if (model.Dwb != "")
            {
                sb1.Append("dwb ='" + model.Dwb + "',");
            }
            if (model.Dwc != "")
            {
                sb1.Append("dwc ='" + model.Dwc + "',");
            }
            sb1.Append("subTime = '" + model.SubTime + "'");
            sb1.Append(" where id = '" + model.ID + "'");
            string strsql = sb1.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Addctdjb(SanZi.Model.ctdjb model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into ctdjb(");
            sb1.Append("xmID,xmmc,subTime,xh,tbr,zizhi,fzr,lxdh");
            sb2.Append(" values (");
            sb2.Append("" + model.XmID + "");
            sb2.Append(",'" + model.Xmmc + "'");
            sb2.Append(",'" + model.SubTime + "'");
            sb2.Append("," + model.Xh + "");
            sb2.Append(",'" + model.Tbr + "'");
            sb2.Append(",'" + model.Zizhi + "'");
            sb2.Append(",'" + model.Fzr + "'");
            sb2.Append(",'" + model.Lxdh + "'");
            sb1.Append(") ");
            sb2.Append(");");
            
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }

        /// <summary>
        ///项目 增加一条数据
        /// </summary>
        public void Addxiangmu(SanZi.Model.xiangmu model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into xiangmu(");
            sb1.Append("xmmc,dailiID");
            sb1.Append(") ");
            sb2.Append(" values (");
            sb2.Append("'" + model.Xmmc + "','" + model.DaiLiId + "'");
            sb2.Append(");");
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }

        /// <summary>
        ///项目 查询所有项目
        /// </summary>
        public DataSet selectxiangmu(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,xmmc ");
            strSql.Append(" FROM xiangmu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        /// <summary>
        ///项目 更新所有项目
        /// </summary>
        public void UpdateXM(SanZi.Model.xiangmu model)
        {
            StringBuilder sb1 = new StringBuilder();

            sb1.Append("update xiangmu set ");
            sb1.Append(" xmmc = '" + model.Xmmc + "' ");
            sb1.Append(" where dailiid = ");
            sb1.Append("'" + model.DaiLiId + "'");
            string strsql = sb1.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }
        /// <summary>
        /// 查询参投登记
        /// </summary>
        public DataSet selectctdj(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ctdjb ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除村预算书 一条数据
        /// </summary>
        public void DeleteCyss(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from cyss ");
            strSql.Append(" where ID=" + ID);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 获得村预算书 数据列表
        /// </summary>
        public DataSet GetCyssList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,xmmc,xmsssj,xmmx,danwei,SubTime ");
            strSql.Append(" FROM cyss ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY ID DESC ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除竞价招投标 一条数据
        /// </summary>
        public void DeleteJjztb(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jjztb ");
            strSql.Append(" where ID=" + ID);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 获得竞价招投标 数据列表
        /// </summary>
        public DataSet GetJjztbList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,xmmc,subTime,adress,cyry,zcr,cbr,jlr,zynr ");
            strSql.Append(" FROM jjztb ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY ID desc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得招标公告 数据列表
        /// </summary>
        public DataSet GetZbggList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,cwh,zbgc,nrqk,bmtj,yz,startTime,finishTime,bmdd,lxfs,SubTime ");
            strSql.Append(" FROM zbgg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得中标公告 数据列表
        /// </summary>
        public DataSet GetZbgonggList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ncmc,startTime,finishTime,zbmc,dwa,dwb,dwc,dws,zbdw,ztbdw,subTime	");
            strSql.Append(" FROM zhongb ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 删除中标公告 一条数据
        /// </summary>
        public void DeleteZhongb(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from zhongb ");
            strSql.Append(" where ID=" + ID);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除招标公告 一条数据
        /// </summary>
        public void DeleteZbgg(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from zbgg ");
            strSql.Append(" where ID=" + ID);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 删除参投登记表 一条数据
        /// </summary>
        public void DeleteCtdjb(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ctdjb ");
            strSql.Append(" where ID=" + ID);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 获得中标公告 数据列表
        /// </summary>
        public DataSet GetZhongbList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM zhongb ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得参投登记表 数据列表
        /// </summary>
        public DataSet GetCtdjbList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ctdjb ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得项目 数据列表
        /// </summary>
        public DataSet GetXiangmuList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM xiangmu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查看中标公告详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetZhongBiaoGongGaoByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ncmc,startTime,finishTime,zbmc,dwa,dwb,dwc,dws,zbdw,ztbdw,SubTime ");
            strSql.Append(" FROM zhongb ");
            strSql.Append(" where id= " + id);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查看中标公告详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetZhaoTouBiaoByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,xmmc,subTime,adress,cyry,zcr,cbr,jlr,zynr ");
            strSql.Append(" FROM jjztb ");
            strSql.Append(" where id= " + id);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查看预算书详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetYuSuanShuByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,xmmc,xmsssj,xmmx,danwei,SubTime ");
            strSql.Append(" FROM cyss ");
            strSql.Append(" where id= " + id);
            return DbHelperMySQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 查看招标公告详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetZhaoBiaoGongGaoByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,cwh,zbgc,nrqk,bmtj,yz,startTime,finishTime,bmdd,lxfs,SubTime,str ");
            strSql.Append(" FROM zbgg ");
            strSql.Append(" where id= " + id);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得项目列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet getProjectList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,xmmc,DelFlag ");
            strSql.Append(" FROM xiangmu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where DelFlag=0 and xmmc like '%" + strWhere + "%' ");
            }
            else
            {
                strSql.Append(" where DelFlag=0  ");
            }
            strSql.Append(" order by ID desc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uid"></param>
        public void DelProject(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update xiangmu  set delflag=1");
            strSql.Append(" WHERE id=" + id);
            DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 取项目信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataSet getProjectByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,xmmc,DelFlag ");
            strSql.Append(" FROM xiangmu ");
            strSql.Append(" where DelFlag=0 and id=" + id);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新项目信息
        /// </summary>
        public void UpdateProject(int pid, string projectName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update xiangmu  set ");
            strSql.Append("xmmc='" + projectName + "' ");
            strSql.Append(" WHERE id=" + pid);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

    }
}
