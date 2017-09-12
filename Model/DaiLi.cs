using System;
namespace SanZi.Model
{
	/// <summary>
	/// 实体类daili 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class DaiLi
	{
		public DaiLi()
		{}
		#region Model
		private int _daili_id;
		private string _villagename;
        private string _applydate;
		private string _projectname;
        private string _projecttype;
		private string _estimatevalue;
		private string _zhibu_yuanyin;
		private int? _zhibu_time;
		private string _zhibu_shixiang;
		private int? _liangwei_time;
		private int? _shenyi_tiime;
		private int? _yiangongkai_fromdate;
		private int? _yiangongkai_enddate;
		private int? _cunmin_time;
		private int? _cunmin_totalnum;
		private int? _cunmin_attendnum;
		private int? _cunmin_passnum;
		private decimal? _cunmin_passrate;
		private int? _jieguogongkai_fromdate;
		private int? _jieguogongkai_enddate;
		private string _zhibushuji;
		private string _cuzhuren;
		private string _cunwujianduzuzhang;
		private string _lianzhengjianduyuan;
		private string _xiangzhenyijian;
		private int? _subtime;
		private int? _subuid;
		private int? _delflag;
        private string _time1;
        private string _time2;
        private string _time3;
        private string _time4;
        private string _time5;
        private string _time6;
        private string _time7;
        private string _time8;
        private string _time9;
        private string _value1;
        private string _value2;
        private string _value3;
        private string _value4;
        private string _value5;
        private string _value6;

		/// <summary>
		/// auto_increment
		/// </summary>
		public int DaiLi_ID
		{
			set{ _daili_id=value;}
			get{return _daili_id;}
		}
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time1
        {
            set { _time1 = value; }
            get { return _time1; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time2
        {
            set { _time2 = value; }
            get { return _time2; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time3
        {
            set { _time3 = value; }
            get { return _time3; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time4
        {
            set { _time4 = value; }
            get { return _time4; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time5
        {
            set { _time5 = value; }
            get { return _time5; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time6
        {
            set { _time6 = value; }
            get { return _time6; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time7
        {
            set { _time7 = value; }
            get { return _time7; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time8
        {
            set { _time8 = value; }
            get { return _time8; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string time9
        {
            set { _time9 = value; }
            get { return _time9; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string value1
        {
            set { _value1 = value; }
            get { return _value1; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string value2
        {
            set { _value2 = value; }
            get { return _value2; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string value3
        {
            set { _value3 = value; }
            get { return _value3; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string value4
        {
            set { _value4 = value; }
            get { return _value4; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string value5
        {
            set { _value5 = value; }
            get { return _value5; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public string value6
        {
            set { _value6 = value; }
            get { return _value6; }
        }
       
		/// <summary>
		/// 
		/// </summary>
		public string VillageName
		{
			set{ _villagename=value;}
			get{return _villagename;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string ProjectType
        {
            set { _projecttype = value; }
            get { return _projecttype; }
        }
		/// <summary>
		/// 
		/// </summary>
        public string ApplyDate
		{
			set{ _applydate=value;}
			get{return _applydate;}
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
		public string EstimateValue
		{
			set{ _estimatevalue=value;}
			get{return _estimatevalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiBu_YuanYin
		{
			set{ _zhibu_yuanyin=value;}
			get{return _zhibu_yuanyin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ZhiBu_Time
		{
			set{ _zhibu_time=value;}
			get{return _zhibu_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiBu_ShiXiang
		{
			set{ _zhibu_shixiang=value;}
			get{return _zhibu_shixiang;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LiangWei_Time
		{
			set{ _liangwei_time=value;}
			get{return _liangwei_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ShenYi_Tiime
		{
			set{ _shenyi_tiime=value;}
			get{return _shenyi_tiime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? YiAnGongKai_FromDate
		{
			set{ _yiangongkai_fromdate=value;}
			get{return _yiangongkai_fromdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? YiAnGongKai_EndDate
		{
			set{ _yiangongkai_enddate=value;}
			get{return _yiangongkai_enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CunMin_Time
		{
			set{ _cunmin_time=value;}
			get{return _cunmin_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CunMin_TotalNum
		{
			set{ _cunmin_totalnum=value;}
			get{return _cunmin_totalnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CunMin_AttendNum
		{
			set{ _cunmin_attendnum=value;}
			get{return _cunmin_attendnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CunMin_PassNum
		{
			set{ _cunmin_passnum=value;}
			get{return _cunmin_passnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CunMin_PassRate
		{
			set{ _cunmin_passrate=value;}
			get{return _cunmin_passrate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? JieGuoGongKai_FromDate
		{
			set{ _jieguogongkai_fromdate=value;}
			get{return _jieguogongkai_fromdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? JieGuoGongKai_EndDate
		{
			set{ _jieguogongkai_enddate=value;}
			get{return _jieguogongkai_enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiBuShuJi
		{
			set{ _zhibushuji=value;}
			get{return _zhibushuji;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CuZhuren
		{
			set{ _cuzhuren=value;}
			get{return _cuzhuren;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CunWuJianDuZuZhang
		{
			set{ _cunwujianduzuzhang=value;}
			get{return _cunwujianduzuzhang;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LianZhengJianDuYuan
		{
			set{ _lianzhengjianduyuan=value;}
			get{return _lianzhengjianduyuan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XiangZhenYiJian
		{
			set{ _xiangzhenyijian=value;}
			get{return _xiangzhenyijian;}
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

