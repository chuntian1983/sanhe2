using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.AccountManage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string str = string.Empty;
        private string id = string.Empty;
        //public string info = "";
        
        public int count = 0;
        string[] subjectlist=new string[25];
        protected void Page_Load(object sender, EventArgs e)
        {
            

                
                str = Request.QueryString["info"];
                id = Request.QueryString["id"];
                string[] strs = { "!--!" };
                string[] strlist = str.Split(strs, StringSplitOptions.RemoveEmptyEntries);
                string[] strsplit = { "!__!" };
                count = strlist.Length;
                if (!IsPostBack)
                {
                    string zzkm = string.Empty;
                    string mxkm = string.Empty;
                for (int i = 0; i < strlist.Length; i++)
                {
                    HiddenField hid = new HiddenField();
                    string[] strnew = strlist[i].Split(strsplit, StringSplitOptions.None);
                    subjectlist[i] = strnew[1];
                    switch (i)
                    {
                        case 0:
                            this.TextBox1.Text = strnew[0];

                            //this.Label1.Text = CommClass.GetDataTable("select subjectname from cw_subject where subjectno='" + strnew[1] + "'").Rows[0]["subjectname"].ToString();
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label1.Text = zzkm;
                            this.Label11.Text = mxkm;
                            Hidden1.Value = strnew[1];
                            break;
                        case 1:
                            this.TextBox2.Text = strnew[0];
                            //this.Label2.Text = CommClass.GetDataTable("select subjectname from cw_subject where subjectno='" + strnew[1] + "'").Rows[0]["subjectname"].ToString();
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label2.Text = zzkm;
                            this.Label22.Text = mxkm;
                            Hidden2.Value = strnew[1];
                            break;
                        case 2:
                            this.TextBox3.Text = strnew[0];
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label3.Text = zzkm;
                            this.Label32.Text = mxkm;
                            Hidden3.Value = strnew[1];
                            break;
                        case 3:
                            this.TextBox4.Text = strnew[0];
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label4.Text = zzkm;
                            this.Label42.Text = mxkm;
                            Hidden4.Value = strnew[1];
                            break;
                        case 4:
                            this.TextBox5.Text = strnew[0];
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label5.Text = zzkm;
                            this.Label52.Text = mxkm;
                            Hidden5.Value = strnew[1];
                            break;
                        case 5:
                            this.TextBox6.Text = strnew[0];
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label6.Text = zzkm;
                            this.Label62.Text = mxkm;
                            Hidden6.Value = strnew[1];
                            break;
                        case 6:
                            this.TextBox7.Text = strnew[0];
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label7.Text = zzkm;
                            this.Label72.Text = mxkm;
                            Hidden7.Value = strnew[1];
                            break;
                        case 7:
                            this.TextBox8.Text = strnew[0];
                            Getkmmx(strnew[1],ref zzkm,ref mxkm);
                            this.Label8.Text = zzkm;
                            this.Label82.Text = mxkm;
                            Hidden8.Value = strnew[1];
                            break;
                        

                    }



                }
            }

        }
        public string Savepingzhen()
        {
            string str = "";
            string zb = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(zb))
            {
                string[] strlists = zb.Split('|');

                int vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Voucher'").Rows[0]["lastid"].ToString()) + 1;

                string strsql = "insert into cw_voucher(id,voucherno,voucherdate,isauditing,isrecord,director,dobill,addonscount,delflag)values(" + vocid + ",'" + strlists[0] + "','" + strlists[1] + "','0','0','" + strlists[2] + "','" + strlists[3] + "','0','0')";
                CommClass.ExecuteSQL(strsql);
                string sqlstr = "update cw_recordid set lastid=" + vocid + " where  tablename='CW_Voucher'";
                DataTable dt = CommClass.GetDataTable(sqlstr);
                str = vocid.ToString();
            }
            return str;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string money = "0";
            int vocid = 0;
            string strsql, sqlup;
            string id = Savepingzhen();
            string info = "";
            if (!string.IsNullOrEmpty(id))
            {
                for (int i = 0; i < count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (string.IsNullOrEmpty(this.TextBox12.Text.Trim()))
                            {
                                money = "-" + this.TextBox13.Text;
                            }
                            else { money ="+"+ this.TextBox12.Text; }
                            info += this.TextBox1.Text + "!--!" + this.Hidden1.Value + "!--!" + money + "!__!";
                            break;
                        case 1:
                            if (string.IsNullOrEmpty(this.TextBox22.Text.Trim()))
                            {
                                money = "-" + this.TextBox23.Text;
                            }
                            else { money = "+" + this.TextBox22.Text; }
                            info += this.TextBox2.Text + "!--!" + this.Hidden2.Value + "!--!" + money + "!__!";
                            break;
                        case 2:
                            if (string.IsNullOrEmpty(this.TextBox32.Text.Trim()))
                            {
                                money = "-" + this.TextBox33.Text;
                            }
                            else { money = "+" + this.TextBox32.Text; }
                            info += this.TextBox3.Text + "!--!" + this.Hidden3.Value + "!--!" + money + "!__!";
                            break;
                        case 3:
                            if (string.IsNullOrEmpty(this.TextBox42.Text.Trim()))
                            {
                                money = "-" + this.TextBox43.Text;
                            }
                            else { money = "+" + this.TextBox42.Text; }
                            info += this.TextBox4.Text + "!--!" + this.Hidden4.Value + "!--!" + money + "!__!";
                            break;
                        case 4:
                            if (string.IsNullOrEmpty(this.TextBox52.Text.Trim()))
                            {
                                money = "-" + this.TextBox53.Text;
                            }
                            else { money = "+" + this.TextBox52.Text; }
                            info += this.TextBox5.Text + "!--!" + this.Hidden5.Value + "!--!" + money + "!__!";
                            break;
                        case 5:
                            if (string.IsNullOrEmpty(this.TextBox62.Text.Trim()))
                            {
                                money = "-" + this.TextBox63.Text;
                            }
                            else { money = "+" + this.TextBox62.Text; }
                            info += this.TextBox6.Text + "!--!" + this.Hidden6.Value + "!--!" + money + "!__!";
                            break;
                        case 6:
                            if (string.IsNullOrEmpty(this.TextBox72.Text.Trim()))
                            {
                                money = "-" + this.TextBox73.Text;
                            }
                            else { money = "+" + this.TextBox72.Text; }
                            info += this.TextBox7.Text + "!--!" + this.Hidden7.Value + "!--!" + money + "!__!";
                            break;
                        case 7:
                            if (string.IsNullOrEmpty(this.TextBox82.Text.Trim()))
                            {
                                money = "-" + this.TextBox83.Text;
                            }
                            else { money = "+" + this.TextBox82.Text; }
                            info += this.TextBox8.Text + "!--!" + this.Hidden8.Value + "!--!" + money + "!__!";
                            break;
                    }
                }
            }
            #region 保存
            //if (!string.IsNullOrEmpty(id))
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        switch (i)
            //        {
            //            case 0:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox12.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox13.Text;
            //                }
            //                else { money = this.TextBox12.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox1.Text + "','" + Hidden1.Value + "','" + this.Label1.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;
            //            case 1:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox22.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox23.Text;
            //                }
            //                else { money = this.TextBox22.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox3.Text + "','" + Hidden2.Value + "','" + this.Label2.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;
            //            case 2:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox32.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox33.Text;
            //                }
            //                else { money = "-" + this.TextBox32.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox3.Text + "','" + Hidden3.Value + "','" + this.Label3.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;
            //            case 3:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox42.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox43.Text;
            //                }
            //                else { money = this.TextBox42.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox4.Text + "','" + Hidden4.Value + "','" + this.Label4.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;
            //            case 4:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox52.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox53.Text;
            //                }
            //                else { money = this.TextBox52.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox5.Text + "','" + Hidden5.Value + "','" + this.Label5.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;
            //            case 5:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox62.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox63.Text;
            //                }
            //                else { money = this.TextBox62.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox6.Text + "','" + Hidden6.Value + "','" + this.Label6.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;
            //            case 6:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox72.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox73.Text;
            //                }
            //                else { money = this.TextBox72.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox7.Text + "','" + Hidden7.Value + "','" + this.Label7.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;
            //            case 7:
            //                vocid = int.Parse(CommClass.GetDataTable("select * from cw_recordid where tablename='CW_Entry'").Rows[0]["lastid"].ToString()) + 1;

            //                if (string.IsNullOrEmpty(this.TextBox82.Text.Trim()))
            //                {
            //                    money = "-" + this.TextBox83.Text;
            //                }
            //                else { money = this.TextBox82.Text; }
            //                strsql = "insert into cw_entry(id,voucherid,vsummary,subjectno,subjectname,summoney) values('" + vocid + "','" + id + "','" + this.TextBox8.Text + "','" + Hidden8.Value + "','" + this.Label8.Text + "','" + money + "')";
            //                CommClass.ExecuteSQL(strsql);
            //                sqlup = "update cw_recordid set lastid='" + vocid + "' where tablename='CW_Entry'";
            //                CommClass.ExecuteSQL(sqlup);
            //                break;


            //        }
            //    }

            //}
           #endregion
            LTP.Common.MessageBox.ResponseScript(this, "window.returnValue = "+info+";window.close();");
        }
        protected void Getkmmx(string kmno, ref string zzkm, ref string mxkm)
        {
            
            DataTable dt = new DataTable();
            dt = CommClass.GetDataTable("select * from cw_subject where subjectno='" + kmno + "'");
            if (dt.Rows.Count>0)
            {
                
                if (dt.Rows[0]["parentno"].ToString() != "000")
                {
                    mxkm = dt.Rows[0]["subjectname"].ToString();
                    DataTable dtnew = CommClass.GetDataTable("select * from cw_subject where subjectno='" + dt.Rows[0]["parentno"].ToString() + "'");
                    if (dtnew.Rows.Count > 0)
                    {
                        
                        if (dtnew.Rows[0]["parentno"].ToString() != "000")
                        {
                            mxkm = dtnew.Rows[0]["subjectname"].ToString() + "/" + mxkm;
                            DataTable dt3 = CommClass.GetDataTable("select * from cw_subject where subjectno='" + dtnew.Rows[0]["parentno"].ToString() + "'");
                            if (dt3.Rows.Count>0)
                            {
                                
                                if (dt3.Rows[0]["parentno"].ToString() == "000")
                                {
                                    zzkm = dt3.Rows[0]["subjectname"].ToString();
                                }
                                else
                                {
                                    mxkm = dt3.Rows[0]["subjectname"].ToString() + "/" + mxkm;
                                }
                            }
                        }
                        else
                        {
                            zzkm = dtnew.Rows[0]["subjectname"].ToString();
                        }
                    }
                    
                }
                else 
                {
                    zzkm = dt.Rows[0]["subjectname"].ToString();
                }
            }
        }
    }
}
