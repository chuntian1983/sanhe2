using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
    [Serializable]
   public  class xiangmu
    {
        public xiangmu() { }
        #region Model
        private int _id;
        private string _xmmc;
        private string _dailiID;
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        public string DaiLiId
        {
            set { _dailiID = value; }
            get { return _dailiID; }
        }
        #endregion Model
    }
}
