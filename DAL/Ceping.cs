using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace SanZi.DAL
{
    /// <summary>
    /// 数据访问类ceping。
    /// </summary>
    public class Ceping
    {
        public Ceping()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("ID", "ceping");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ceping");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }
      


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SanZi.Model.CePing model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into ceping(");
            sb1.Append("DeptID,DeptName,OptionChecked");
            sb2.Append(" values (");
            sb2.Append("" + model.DeptID + ",");
            sb2.Append("'" + model.DeptName + "',");
            sb2.Append("" + model.OptionChecked + "");
            if (model.Evaluation.ToString() != "")
            {
                sb1.Append(",Evaluation");
                sb2.Append("," + model.Evaluation + "");
            }
            if (model.SubUID.ToString() != "")
            {
                sb1.Append(",SubUID");
                sb2.Append("," + model.SubUID + "");
            }
            if (model.SubIP.ToString() != "")
            {
                sb1.Append(",SubIP");
                sb2.Append(",'" + model.SubIP + "'");
            }
            if (model.SubTime.ToString() != "")
            {
                sb1.Append(",SubTime");
                sb2.Append("," + model.SubTime + "");
            }
            sb1.Append(") ");
            sb2.Append(");");

            //strSql.Append("@Evaluation,@SubUID,@SubIP,@SubTime,@OptionChecked)");
            //MySqlParameter[] parameters = {
            //        new MySqlParameter("@Evaluation", MySqlDbType.Text),
            //        new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
            //        new MySqlParameter("@SubIP", MySqlDbType.VarChar,50),
            //        new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
            //        new MySqlParameter("@OptionChecked", MySqlDbType.Int32,4)};
            //parameters[0].Value = model.Evaluation;
            //parameters[1].Value = model.SubUID;
            //parameters[2].Value = model.SubIP;
            //parameters[3].Value = model.SubTime;
            //parameters[4].Value = model.OptionChecked;
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql1(strsql);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SanZi.Model.CePing model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ceping set ");
            strSql.Append("Evaluation=@Evaluation,");
            strSql.Append("SubUID=@SubUID,");
            strSql.Append("SubIP=@SubIP,");
            strSql.Append("SubTime=@SubTime,");
            strSql.Append("OptionChecked=@OptionChecked");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11),
					new MySqlParameter("@Evaluation", MySqlDbType.Text),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@SubIP", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@OptionChecked", MySqlDbType.Int32,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Evaluation;
            parameters[2].Value = model.SubUID;
            parameters[3].Value = model.SubIP;
            parameters[4].Value = model.SubTime;
            parameters[5].Value = model.OptionChecked;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ceping ");
            strSql.Append(" where ID=" + ID);
            DbHelperMySQL.ExecuteSql1(strSql.ToString());
        }

        /// <summary>
        /// 删除民主测评 一条数据
        /// </summary>
        public void DeleteMzcp(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from mzcp ");
            strSql.Append(" where ID=" + ID);
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SanZi.Model.CePing GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Evaluation,SubUID,SubIP,SubTime,OptionChecked from ceping ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)};
            parameters[0].Value = ID;

            SanZi.Model.CePing model = new SanZi.Model.CePing();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Evaluation = ds.Tables[0].Rows[0]["Evaluation"].ToString();
                if (ds.Tables[0].Rows[0]["SubUID"].ToString() != "")
                {
                    model.SubUID = int.Parse(ds.Tables[0].Rows[0]["SubUID"].ToString());
                }
                model.SubIP = ds.Tables[0].Rows[0]["SubIP"].ToString();
                if (ds.Tables[0].Rows[0]["SubTime"].ToString() != "")
                {
                    model.SubTime = int.Parse(ds.Tables[0].Rows[0]["SubTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OptionChecked"].ToString() != "")
                {
                    model.OptionChecked = int.Parse(ds.Tables[0].Rows[0]["OptionChecked"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DeptID,DeptName,Evaluation,SubUID,SubIP,SubTime,OptionChecked ");
            strSql.Append(" FROM ceping ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by id desc ");
            return DbHelperMySQL.Query1(strSql.ToString());
        }

        /// <summary>
        /// 获得民主测评结果 数据列表
        /// </summary>
        public DataSet GetMzcpList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,Content,DanWei,AddTime ");
            strSql.Append(" FROM mzcp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }



        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "ceping";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        //2010/9/27王亚琪添加 添加民主测评结果
        public void Addcpjg(SanZi.Model.mzcp model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into mzcp(");
            sb1.Append("Title");
            sb2.Append(" values (");
            sb2.Append("'" + model.Title + "'");
            if (model.Content.ToString() != "")
            {
                sb1.Append(",Content");
                sb2.Append(",'" + model.Content + "'");
            }
            if (model.DanWei.ToString() != "")
            {
                sb1.Append(",DanWei");
                sb2.Append(",'" + model.DanWei + "'");
            }
            if (model.AddTime.ToString() != "")
            {
                sb1.Append(",AddTime");
                sb2.Append(",'" + model.AddTime + "'");
            }
            sb1.Append(") ");
            sb2.Append(");");
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }

        //添加村预算书
        public void Addcyss(SanZi.Model.cyss model)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb1.Append("insert into cyss(");
            sb1.Append("Title");
            sb2.Append(" values (");
            sb2.Append("'" + model.Title + "'");
            if (model.xmmc.ToString() != "")
            {
                sb1.Append(",xmmc");
                sb2.Append(",'" + model.xmmc + "'");
            }
            if (model.xmsssj.ToString() != "")
            {
                sb1.Append(",xmsssj");
                sb2.Append(",'" + model.xmsssj + "'");
            }
            if (model.xmmx.ToString() != "")
            {
                sb1.Append(",xmmx");
                sb2.Append(",'" + model.xmmx + "'");
            }
            if (model.danwei.ToString() != "")
            {
                sb1.Append(",danwei");
                sb2.Append(",'" + model.danwei + "'");
            }
            if (model.subTime.ToString() != "")
            {
                sb1.Append(",subTime");
                sb2.Append(",'" + model.subTime + "'");
            }
            sb1.Append(") ");
            sb2.Append(");");
            string strsql = sb1.ToString() + sb2.ToString();
            DbHelperMySQL.ExecuteSql(strsql);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Updatecyss(SanZi.Model.cyss model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update cyss set ");
            strSql.Append("title=@title,");
            strSql.Append("xmmc=@xmmc,");
            strSql.Append("xmsssj=@xmsssj,");
            strSql.Append("xmmx=@xmmx,");
            strSql.Append("subTime=@subTime,");
            strSql.Append("danwei=@danwei");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11),
					new MySqlParameter("@title", MySqlDbType.Text),
					new MySqlParameter("@xmmc", MySqlDbType.Text),
					new MySqlParameter("@xmsssj", MySqlDbType.Text),
					new MySqlParameter("@xmmx", MySqlDbType.Text),
                    new MySqlParameter("@danwei", MySqlDbType.Text),
					new MySqlParameter("@subTime", MySqlDbType.String)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.xmmc;
            parameters[3].Value = model.xmsssj;
            parameters[4].Value = model.xmmx;
            parameters[5].Value = model.danwei;
            parameters[6].Value = model.subTime;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 查看测评结果详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetCePingJieGuoByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,Content,DanWei,AddTime ");
            strSql.Append(" FROM mzcp ");
            strSql.Append(" where id= " + id);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}

