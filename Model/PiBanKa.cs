using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类pibanka 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PiBanKa
	{
		public PiBanKa()
		{}
		#region Model
		private int _pid;
		private int? _deptid;
		private decimal? _reason;
		private decimal? _outmoney;
		private int? _subuid;
		private int? _subtime;
		private int? _delflag;
        private string _subid;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DeptID
		{
			set{ _deptid=value;}
			get{return _deptid;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string subid
        {
            set { _subid = value; }
            get { return _subid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OutMoney
		{
			set{ _outmoney=value;}
			get{return _outmoney;}
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
		public int? SubTime
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
		#endregion Model

	}
}

