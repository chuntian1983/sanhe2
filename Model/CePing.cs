using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类ceping 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class CePing
	{
		public CePing()
		{}
		#region Model
		private int _id;
        private int _deptid;
        private string _deptname;
		private string _evaluation;
		private int? _subuid;
		private string _subip;
		private int? _subtime;
		private int? _optionchecked;
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
		public string Evaluation
		{
			set{ _evaluation=value;}
			get{return _evaluation;}
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
		public int? SubTime
		{
			set{ _subtime=value;}
			get{return _subtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OptionChecked
		{
			set{ _optionchecked=value;}
			get{return _optionchecked;}
		}
		#endregion Model

	}
}

