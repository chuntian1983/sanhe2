using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
    [Serializable]
   public  class mzcp
    {
        public mzcp(){}
    #region Model
        private int _id;
        private string _content;
        private string _title;
        private string _addtime;
        private string _danwei;
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
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
        public string DanWei
        {
            set { _danwei = value; }
            get { return _danwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string  AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }


    #endregion Model
    }
}
