using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类pibankabarcode 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PiBanKaBarcode
	{
		public PiBanKaBarcode()
		{}
		#region Model
		private int _id;
		private int? _pid;
		private string _barcode;
		private int? _subtime;
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
		public int? PID
		{
			set{ _pid=value;}
			get{return _pid;}
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

