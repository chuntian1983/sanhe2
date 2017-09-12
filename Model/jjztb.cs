using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanZi.Model
{
  public  class jjztb
    {
      public jjztb() { }
      #region Model
      private int _id;
      private string _xmmc;
      private string _subtime;
      private string _adress;
      private string _cyry;
      private string _zcr;
      private string _cbr;
      private string _jlr;
      private string _zynr;
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
      public string SubTime
      {
          set { _subtime = value; }
          get { return _subtime; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string Adress
      {
          set { _adress = value; }
          get { return _adress; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string Cyry
      {
          set { _cyry = value; }
          get { return _cyry; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string Zcr
      {
          set { _zcr = value; }
          get { return _zcr; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string Cbr
      {
          set { _cbr = value; }
          get { return _cbr; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string Jlr
      {
          set { _jlr = value; }
          get { return _jlr; }
      }
      /// <summary>
      /// 
      /// </summary>
      public string Zynr
      {
          set { _zynr = value; }
          get { return _zynr; }
      }
     
      #endregion Model
    }
}
