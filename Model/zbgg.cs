using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
    [Serializable]
   public  class zbgg
    {
        public zbgg() { }
        #region Model
        private int _id;
        private string _cwh;
        private string _zbgc;
        private string _nrqk;
        private string _bmtj;
        private string _yz;
        private string _starttime;
        private string _finishtime;
        private string _bmdd;
        private string _lxfs;
        private string _subtime;
        private string _str;
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Cwh
        {
            set { _cwh = value; }
            get { return _cwh; }
        }
        public string str
        {
            set { _str = value; }
            get { return _str; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Zbgc
        {
            set { _zbgc = value; }
            get { return _zbgc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Nrqk
        {
            set { _nrqk = value; }
            get { return _nrqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Bmtj
        {
            set { _bmtj = value; }
            get { return _bmtj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Yz
        {
            set { _yz = value; }
            get { return _yz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FinishTime
        {
            set { _finishtime = value; }
            get { return _finishtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Bmdd
        {
            set { _bmdd = value; }
            get { return _bmdd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lxfs
        {
            set { _lxfs = value; }
            get { return _lxfs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubTime
        {
            set { _subtime = value; }
            get { return _subtime; }
        }
        #endregion Model
    }
}
