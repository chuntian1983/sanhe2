using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
    [Serializable]
  public  class cyss
    {
        public cyss() { }
        #region Model
        private int _id;
        private string _title;
        private string _xmmc;
        private string _xmsssj;
        private string _xmmx;
        private string _daiwei;
        private string _subtime;
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string xmmc
        {
            set { _xmmc = value; }
            get { return _xmmc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string xmsssj
        {
            set { _xmsssj = value; }
            get { return _xmsssj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string xmmx
        {
            set { _xmmx = value; }
            get { return _xmmx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string danwei
        {
            set { _daiwei = value; }
            get { return _daiwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string subTime
        {
            set { _subtime = value; }
            get { return _subtime; }
        }
        #endregion Model
    }
}
