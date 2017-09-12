using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
    /// <summary>
    /// cw_approval:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class cw_approval
    {
        public cw_approval()
        { }
        #region Model
        private int _id;
        private string _accountid;
        private DateTime? _accounttime;
        private DateTime? _creattime;
        private string _pibanka;
        private string _pibankama;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 帐套id
        /// </summary>
        public string accountid
        {
            set { _accountid = value; }
            get { return _accountid; }
        }
        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? accounttime
        {
            set { _accounttime = value; }
            get { return _accounttime; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? creattime
        {
            set { _creattime = value; }
            get { return _creattime; }
        }
        /// <summary>
        /// 审批人
        /// </summary>
        public string pibanka
        {
            set { _pibanka = value; }
            get { return _pibanka; }
        }
        /// <summary>
        /// 条形码
        /// </summary>
        public string pibankama
        {
            set { _pibankama = value; }
            get { return _pibankama; }
        }
        #endregion Model

    }
}
