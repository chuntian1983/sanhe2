using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace SanZi.DAL
{
    /// <summary>
    /// 数据访问类pibanka。
    /// </summary>
    public class PiBanKa
    {
        public PiBanKa()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("PID", "pibanka");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from pibanka");
            strSql.Append(" where PID=@PID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PID", MySqlDbType.Int32)};
            parameters[0].Value = PID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SanZi.Model.PiBanKa model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pibanka(");
            strSql.Append("DeptID,Reason,OutMoney,SubUID,SubTime,DelFlag)");
            strSql.Append(" values (");
            strSql.Append("@DeptID,@Reason,@OutMoney,@SubUID,@SubTime,@DelFlag)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DeptID", MySqlDbType.Int32,11),
					new MySqlParameter("@Reason", MySqlDbType.Float),
					new MySqlParameter("@OutMoney", MySqlDbType.Float),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,4)};
            parameters[0].Value = model.DeptID;
            parameters[1].Value = model.Reason;
            parameters[2].Value = model.OutMoney;
            parameters[3].Value = model.SubUID;
            parameters[4].Value = model.SubTime;
            parameters[5].Value = model.DelFlag;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }
        public void Add1(SanZi.Model.PiBanKa model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pibanka set ");
            strSql.Append(" subid=@subid");
            strSql.Append(" where PID=@PID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PID", MySqlDbType.Int32,11),
					new MySqlParameter("@subid", MySqlDbType.VarChar,50)
					};
            parameters[0].Value = model.PID;
            parameters[1].Value = model.subid;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SanZi.Model.PiBanKa model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pibanka set ");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("Reason=@Reason,");
            strSql.Append("OutMoney=@OutMoney,");
            strSql.Append("SubUID=@SubUID,");
            strSql.Append("SubTime=@SubTime,");
            strSql.Append("DelFlag=@DelFlag");
            strSql.Append(" where PID=@PID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PID", MySqlDbType.Int32,11),
					new MySqlParameter("@DeptID", MySqlDbType.Int32,11),
					new MySqlParameter("@Reason", MySqlDbType.Float),
					new MySqlParameter("@OutMoney", MySqlDbType.Float),
					new MySqlParameter("@SubUID", MySqlDbType.Int32,11),
					new MySqlParameter("@SubTime", MySqlDbType.Int32,11),
					new MySqlParameter("@DelFlag", MySqlDbType.Int32,4)};
            parameters[0].Value = model.PID;
            parameters[1].Value = model.DeptID;
            parameters[2].Value = model.Reason;
            parameters[3].Value = model.OutMoney;
            parameters[4].Value = model.SubUID;
            parameters[5].Value = model.SubTime;
            parameters[6].Value = model.DelFlag;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int PID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from pibanka ");
            strSql.Append(" where PID=@PID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PID", MySqlDbType.Int32)};
            parameters[0].Value = PID;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SanZi.Model.PiBanKa GetModel(int PID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PID,DeptID,Reason,OutMoney,SubUID,SubTime,DelFlag from pibanka ");
            strSql.Append(" where PID=@PID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PID", MySqlDbType.Int32)};
            parameters[0].Value = PID;

            SanZi.Model.PiBanKa model = new SanZi.Model.PiBanKa();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PID"].ToString() != "")
                {
                    model.PID = int.Parse(ds.Tables[0].Rows[0]["PID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeptID"].ToString() != "")
                {
                    model.DeptID = int.Parse(ds.Tables[0].Rows[0]["DeptID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Reason"].ToString() != "")
                {
                    model.Reason = decimal.Parse(ds.Tables[0].Rows[0]["Reason"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OutMoney"].ToString() != "")
                {
                    model.OutMoney = decimal.Parse(ds.Tables[0].Rows[0]["OutMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubUID"].ToString() != "")
                {
                    model.SubUID = int.Parse(ds.Tables[0].Rows[0]["SubUID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubTime"].ToString() != "")
                {
                    model.SubTime = int.Parse(ds.Tables[0].Rows[0]["SubTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DelFlag"].ToString() != "")
                {
                    model.DelFlag = int.Parse(ds.Tables[0].Rows[0]["DelFlag"].ToString());
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
            strSql.Append("select PID,DeptID,Reason,OutMoney,SubUID,SubTime,DelFlag ");
            strSql.Append(" FROM pibanka ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询部门支出金额设置
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataSet GetCondition(int deptid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            //strSql.Append("select id,step1,step2,deptid ");
            strSql.Append(" FROM Contidion ");
            //strSql.Append(" where delflag=0 and deptid=" + deptid + "");
            strSql.Append(" order by id desc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询部门支出金额设置
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataTable GetConditionBy(string wh)
        {
            string sql = string.Concat("select * from contidion ", wh, " order by Step1,Step2,ID");
            return DbHelperMySQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 修改村支出金额设置条件
        /// </summary>
        /// <param name="stepA"></param>
        /// <param name="stepB"></param>
        /// <param name="deptID"></param>
        /// <param name="conID"></param>
        public void UpdateCondition(float stepA, float stepB, int deptID)
        {
            StringBuilder strSql = new StringBuilder();
            System.DateTime time = System.DateTime.Now;

            if (this.CheckCondition(deptID))
            {
                strSql.Append("update Contidion");
                strSql.Append(" set step1=" + stepA + ",step2=" + stepB + ",subtime=" + (int)LTP.Common.TimeParser.ConvertDateTimeInt(time));
                strSql.Append(" where id=" + deptID + " or deptid=" + deptID);
            }
            else
            {
                strSql.Append(" insert into Contidion(step1,step2,deptid,subtime) ");
                strSql.Append(" values( " + stepA + "," + stepB + "," + deptID + ",");
                strSql.Append((int)LTP.Common.TimeParser.ConvertDateTimeInt(time) + ")");
            }

            DbHelperMySQL.ExecuteSql(strSql.ToString());

        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool CheckCondition(int deptID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from Contidion where id=" + deptID.ToString());
            return DbHelperMySQL.Exists1(strSql.ToString());
        }

        /// <summary>
        /// 批办卡信息保存
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="deptname"></param>
        /// <param name="reason"></param>
        /// <param name="outmoney"></param>
        /// <param name="struid"></param>
        public int SavePiBanKa(int deptid, int SubUID, string deptname, string reason, decimal outmoney, string struid, string d, string c, string lujing, string zhaiyao, string time1, string time2, string time3, string time4, string time5, string time6, string value1, string value2, string value3)
        {
            StringBuilder strSql = new StringBuilder();
            //System.DateTime time = System.DateTime.Now;
            //int intTime = (int)LTP.Common.TimeParser.ConvertDateTimeInt(time);//

            strSql.Append(" insert into pibanka(DeptID,OutReason,OutMoney,State,SubTime,SubUID,d,c,lujing,zhaiyao,time1,time2,time3,time4,time5,time6,value1,value2,value3) ");
            strSql.Append(" values(");
            strSql.Append(deptid + ",");
            strSql.Append("'" + reason + "'," + outmoney + ",");
            strSql.Append("1,'" + System.DateTime.Now.ToString() + "','" + SubUID + "','" + d + "','" + c + "','" + lujing + "','" + zhaiyao + "','" + time1 + "','" + time2 + "','" + time3 + "','" + time4 + "','" + time5 + "','" + time6 + "','" + value1 + "','" + value2 + "','" + value3 + "'");
            strSql.Append(" ) ");
            DbHelperMySQL.ExecuteSql(strSql.ToString());

            int maxPID = DbHelperMySQL.GetMaxID("PID", "PiBanKa") - 1;
            if (!string.IsNullOrEmpty(struid))
            {
                StringBuilder strSql2 = new StringBuilder();

                strSql2.Append(" insert into pibankabarcode(PID,UID,BarCode,SubTime)");
                strSql2.Append(" select " + maxPID + ",UserID,BarCode,'" + System.DateTime.Now.ToString() + "'");



                strSql2.Append(" from Users where userid in(" + struid + ")");

                DbHelperMySQL.ExecuteSql(strSql2.ToString());
            }

            return maxPID;
        }

        public DataSet GetUserInfoByID(string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,TitleID,TitleName,DeptID,DeptName,TrueName,BarCode ");
            strSql.Append(" FROM users ");
            strSql.Append(" WHERE delflag=0 and UserID in(" + uid + ")");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 取村民代表信息
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataSet GetCunMinDaiBiaoNum(int deptid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,TitleID,TitleName,DeptID,DeptName,TrueName,BarCode ");
            strSql.Append(" FROM users ");
            strSql.Append(" WHERE delflag=0 and deptid=" + deptid + " and TitleName='村民代表'");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查看批办卡信息
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataSet GetPiBanKaByPid(int pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.PID,a.DeptID,a.OutReason,a.OutMoney,a.State,a.SubTime,a.d,a.c,a.zhaiyao,a.lujing,b.DeptName,a.time1,a.time2,a.time3,a.time4,a.time5,a.time6,a.value1,a.value2,a.value3 ");
            strSql.Append(" FROM PiBanKa a LEFT JOIN Department b  on a.DeptID=b.id");
            strSql.Append(" WHERE a.delflag=0 and a.PID=" + pid);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 取批办卡审批人
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataSet GetShenPiRen(int pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct b.UserID,b.TitleName,b.DeptName,b.TrueName ");
            strSql.Append(" FROM pibankabarcode a LEFT JOIN users b  on a.UID=b.Userid");
            strSql.Append(" WHERE b.delflag=0 and a.PID=" + pid);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除批办卡
        /// </summary>
        /// <param name="pid"></param>
        public void DelPiBanka(int pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PiBanKa  set delflag=1");
            strSql.Append(" WHERE pid=" + pid);
            DbHelperMySQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}

