using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类usertitle 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class UserTitle
	{
		public UserTitle()
		{}
		#region Model
		private int _titleid;
		private string _titlename;
		private string _titledesc;
		private int? _subtime;
		private int? _subuid;
		private int? _delflag;
		/// <summary>
		/// auto_increment
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
		public string TitleDesc
		{
			set{ _titledesc=value;}
			get{return _titledesc;}
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
		public int? SubUID
		{
			set{ _subuid=value;}
			get{return _subuid;}
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

