using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
    [Serializable]
   public class zhongb
    {
        public zhongb() { }
        #region Model
        private int _id;
        private string _ncmc;
        private string _starttime;
        private string _finishtime;
        private string _zbmc;
        private string _dwa;
        private string _dwb;
        private string _dwc;
        private string _dws;
        private string _zbdw;
        private string _ztbdw;
        private string _subtime;
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Ncmc
        {
            set { _ncmc = value; }
            get { return _ncmc; }
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
        public string Zbmc
        {
            set { _zbmc = value; }
            get { return _zbmc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dwa
        {
            set { _dwa = value; }
            get { return _dwa; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dwb
        {
            set { _dwb = value; }
            get { return _dwb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dwc
        {
            set { _dwc = value; }
            get { return _dwc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dws
        {
            set { _dws = value; }
            get { return _dws; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Zbdw
        {
            set { _zbdw = value; }
            get { return _zbdw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ztbdw
        {
            set { _ztbdw = value; }
            get { return _ztbdw; }
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
