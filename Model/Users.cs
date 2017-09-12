using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类users 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Users
	{
		public Users()
		{
        }

		#region Model
		private int _userid;
		private int _titleid;
		private string _titlename;
		private int _deptid;
		private string _deptname;
		private string _truename;
		private string _barcode;
		private int _subtime;
		private int _subuid;
		private int _delflag;
        private string _telphone;
        private string _address;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TitleID
		{
			set{ _titleid=value;}
			get{return _titleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TitleName
		{
			set{ _titlename=value;}
			get{return _titlename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DeptID
		{
			set{ _deptid=value;}
			get{return _deptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeptName
		{
			set{ _deptname=value;}
			get{return _deptname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TrueName
		{
			set{ _truename=value;}
			get{return _truename;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string BarCode
		{
			set{ _barcode=value;}
			get{return _barcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SubTime
		{
			set{ _subtime=value;}
			get{return _subtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SubUID
		{
			set{ _subuid=value;}
			get{return _subuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DelFlag
		{
			set{ _delflag=value;}
			get{return _delflag;}
		}
		#endregion Model

	}
}

