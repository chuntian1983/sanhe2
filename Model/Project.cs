using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类project 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Project
	{
		public Project()
		{}
		#region Model
		private int _id;
		private string _projectname;
		private int? _subtime;
		private int? _subuid;
		private int? _delflag;
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
		public string ProjectName
		{
			set{ _projectname=value;}
			get{return _projectname;}
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

