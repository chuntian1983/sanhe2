using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类tousu 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tousu
	{
		public Tousu()
		{}
		#region Model
		private int _id;
        private int _deptid;
        private string _deptname;
		private string _content;
		private int? _subuid;
		private string _subip;
		private string _subtime;
		private int? _delflag;
        private int? _tflag;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SubUID
		{
			set{ _subuid=value;}
			get{return _subuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SubIP
		{
			set{ _subip=value;}
			get{return _subip;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string SubTime
		{
			set{ _subtime=value;}
			get{return _subtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DelFlag
		{
			set{ _delflag=value;}
			get{return _delflag;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? TFlag
        {
            set { _tflag = value; }
            get { return _tflag; }
        }
		#endregion Model

	}
}

