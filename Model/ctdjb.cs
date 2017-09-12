using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
    [Serializable]
   public class ctdjb
    {
        public ctdjb() { }
        #region Model
        private int _id;
        private int _xmid;
        private string _xmmc;
        private string _subtime;
        private string _xh;
        private string _tbr;
        private string _zizhi;
        private string _fzr;
        private string _lxdh;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int XmID
        {
            set { _xmid = value; }
            get { return _xmid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Xmmc
        {
            set { _xmmc = value; }
            get { return _xmmc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubTime
        {
            set { _subtime = value; }
            get { return _subtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Xh
        {
            set { _xh = value; }
            get { return _xh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tbr
        {
            set { _tbr = value; }
            get { return _tbr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Zizhi
        {
            set { _zizhi = value; }
            get { return _zizhi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzr
        {
            set { _fzr = value; }
            get { return _fzr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lxdh
        {
            set { _lxdh = value; }
            get { return _lxdh; }
        }
        #endregion Model
    }
}
