//***************************************
//	功能:常用验证函数库 
//	版本:1.0
//	编写:孙军
//***************************************

//**********************************************************************************
//	日期函数
//	编写:陈耀泉
//********************

// 函数名称: IsDatetime("2004-2-1 15:59") 必须用-号分隔 
// 功    能: 日期检测函数
// 说    明: 检测包括时分的日期。
function IsDatetime(Mydate,num)
{
	if(num=="1")
	{
		var regExp=/(\d{4})\s*-\s*(\d{1,2})\s*-\s*(\d{1,2})\s+(\d{1,2})\s*:\s*(\d{1,2})/i;
		if(isDate(Mydate.replace(regExp,"$1-$2-$3")))
		{
			var getHour=Mydate.replace(regExp,"$4")
			var getMinute=Mydate.replace(regExp,"$5")
			if(parseInt(getHour)>23) return false;
			if(parseInt(getMinute)>59) return false;
			return true;
		}
		else
		return false;
	}
	else
	{
		var regExp=/(\d{4})\s*-\s*(\d{1,2})\s*-\s*(\d{1,2})\s+(\d{1,2})\s*:\s*(\d{1,2})\s*:\s*(\d{1,2})/i;
		if(isDate(Mydate.replace(regExp,"$1-$2-$3")))
		{
			var getHour=Mydate.replace(regExp,"$4")
			var getMinute=Mydate.replace(regExp,"$5")
			var getSecond=Mydate.replace(regExp,"$6")
			if(parseInt(getHour)>23) return false;
			if(parseInt(getMinute)>59) return false;
			if(parseInt(getSecond)>59) return false;		
			return true;
		}
		else
		return false;	
	}
}

// 函数名称: ToDateObject("2004-2-1 15:59")
// 功    能: 将日期字符串转换为日期对象
// 说    明: 
function ToDateObject(Mydate)
{
	if(isDate(Mydate))
	{
		var regExp=/(\d{4})\s*-\s*(\d{1,2})\s*-\s*(\d{1,2})/i;
		var getYears=Mydate.replace(regExp,"$1")
		var getMonths=Mydate.replace(regExp,"$2")
		var getDates=Mydate.replace(regExp,"$3")
		var date=new Date(getYears,getMonths,getDates)

	}
	else if(IsDatetime(Mydate))
	{
		var regExp=/(\d{4})\s*-\s*(\d{1,2})\s*-\s*(\d{1,2})\s+(\d{1,2})\s*:\s*(\d{1,2})\s*:\s*(\d{1,2})/i;
		var getYears=Mydate.replace(regExp,"$1")
		var getMonths=Mydate.replace(regExp,"$2")
		var getDates=Mydate.replace(regExp,"$3")
		var getHour=Mydate.replace(regExp,"$4")
		var getMinute=Mydate.replace(regExp,"$5")
		var getSecond=Mydate.replace(regExp,"$6")
		var date=new Date(getYears,getMonths,getDates,getHour,getMinute,getSecond)	
	}
	else if(IsDatetime(Mydate),"1")
	{
		var regExp=/(\d{4})\s*-\s*(\d{1,2})\s*-\s*(\d{1,2})\s+(\d{1,2})\s*:\s*(\d{1,2})/i;
		var getYears=Mydate.replace(regExp,"$1")
		var getMonths=Mydate.replace(regExp,"$2")
		var getDates=Mydate.replace(regExp,"$3")
		var getHour=Mydate.replace(regExp,"$4")
		var getMinute=Mydate.replace(regExp,"$5")
		var date=new Date(getYears,getMonths,getDates,getHour,getMinute)
	}
	else
	{
		return "NaN";
	}
	date.setMonth(date.getMonth()-1)
	return date
}

// 函数名称: ToDateString(new Date(),1)
// 功    能: 将日期对象串转换为日期字符串
// 说    明: num参数可选，为1时返回2004-2-2 14:15格式日期,默认返回2004-2-2格式日期
function ToDateString(Mydate,num)
{
	if(!(Mydate.constructor==Date)) return false;
	var getYears=Mydate.getYear()
	var getMonths=parseInt(Mydate.getMonth())+1
	var getDates=Mydate.getDate()
	var getHour=Mydate.getHours()
	var getMinute=Mydate.getMinutes()
	var getSecond=Mydate.getSeconds()
	M=((getMonths.toString().length==1)?"0"+getMonths:getMonths)
	D=((getDates.toString().length==1)?"0"+getDates:getDates)
	H=((getHour.toString().length==1)?"0"+getHour:getHour)
	m=((getMinute.toString().length==1)?"0"+getMinute:getMinute)
	s=((getSecond.toString().length==1)?"0"+getSecond:getSecond)
	if(num=="0")
	{
		return getYears+"-"+M+"-"+D
	}
	else if(num=="1")
	{
		return getYears+"-"+M+"-"+D+" "+H+":"+m
	}
	else
	{
		return getYears+"-"+M+"-"+D+" "+H+":"+m+":"+s	
	}
}

// 函数名称: Datediff("y","2004-1-1","2004-2-1")
// 描    述: 返回两个日期之间的时间间隔
// 语    法: 用法仿照vbscript中的datediff,datepart参数包括y(年),m(月),d(日),h(小时),n(分钟)
function Datediff(datepart,startDate,endDate)
{
	if(datepart=="") return;
	var sDate=null
	var eDate=null
	if(!(startDate.constructor==Date))
	{
		if((!isDate(startDate))&&(!(IsDatetime(startDate)||IsDatetime(startDate,"1")))) return;
		sDate=ToDateObject(startDate)
	}
	else
		sDate=startDate
	if(!(endDate.constructor==Date))
	{
		if((!isDate(endDate))&&(!(IsDatetime(startDate)||IsDatetime(startDate,"1")))) return;
		eDate=ToDateObject(endDate)
	}
	else
		eDate=endDate
	var getsYear=parseInt(sDate.getYear())
	var getsMonth=parseInt(sDate.getMonth())
	
	var geteYear=parseInt(eDate.getYear())
	var geteMonth=parseInt(eDate.getMonth())
	var count=0	
	switch(datepart)
	{
		case "y":
			count=geteYear-getsYear
			break
		case "m":
			count=(geteYear-getsYear)*12+geteMonth-getsMonth
			break
		case "d":
			count=(eDate-sDate)/86400000
			break
		case "h":
			count=(eDate-sDate)/3600000
			break
		case "n":
			count=(eDate-sDate)/60000
	}
	return Math.floor(count)
}


// 函数名称: DateAdd("y",1,"2004-2-1")
// 描    述: 返回已添加指定时间间隔的日期
// 语    法: 用法仿照vbscript中的DateAdd,datepart参数包括y(年),m(月),d(日),h(小时),n(分钟)
//           参数num值为0、1或不填,等于0时返回"2004-2-1"格式日期,等于1时返回"2004-2-1 14:15" 格式日期,不填返回"2004-2-1 14:15:59"格式日期
function DateAdd(datepart,number,date,num)
{
	if(datepart=="") return false;
	if(Math.isInt(number)) return false;
	var mydate=null
	if(!(date.constructor==Date))
	{
		if((!isDate(date))&&((!IsDatetime(date))||(!IsDatetime(date,"1")))) return false;
		mydate=ToDateObject(date)
	}
	else
		mydate=date
	var getYears=parseInt(mydate.getYear())
	var getMonths=parseInt(mydate.getMonth())
	var getDates=parseInt(mydate.getDate())
	var getHour=parseInt(mydate.getHours())
	var getMinute=parseInt(mydate.getMinutes())
	var i=parseInt(number)
	var myDate=null
	switch(datepart)
	{
		case "y":
			mydate.setFullYear(getYears+i)
			break
		case "m":
			mydate.setMonth(getMonths+i)
			break
		case "d":
			mydate.setDate(getDates+i)
			break
		case "h":
			mydate.setHours(getHour+i)
			break
		case "n":
			mydate.setMinutes(getMinute+i)
	}
	return ToDateString(mydate,num);
}
//
//*********************************************************************************************

// 函数名称: isDate("2001-12-11") 必须用-号分隔 
// 功    能: 日期检测函数
// 说    明: 只能是检测日期,不包括分秒,如2001-12-12 12:10测是不合法的


function isDate(oDate)
{
 var rgExp = /^\s*(\d{4})-(\d{1,2})-(\d{1,2})$/;
 var arr = rgExp.exec(oDate);
 if(arr==null) return false; 
 if(arr[0].replace(/\s/gi,"")!=oDate.replace(/\s/gi,"")) return false;
 if(arr[2]=="08") arr[2]="8";
 if(arr[2]=="09") arr[2]="9";
 if(arr[3]=="08") arr[3]="8";
 if(arr[3]=="09") arr[3]="9";
 
 if(parseInt(arr[1])>2099||parseInt(arr[1])<1900) return false;
 if(parseInt(arr[2])>12||parseInt(arr[2])<1) return false;
 if(parseInt(arr[2])==2)
   { if(parseInt(arr[1])%4!=0) 
        { if(parseInt(arr[3])>28||parseInt(arr[3])<1) return false;}
    else{if(parseInt(arr[3])>29||parseInt(arr[3])<1) return false;}
   }
 else{ if(parseInt(arr[2])==4||parseInt(arr[2])==6||parseInt(arr[2])==9||parseInt(arr[2])==11)
      { if(parseInt(arr[3])>30||parseInt(arr[3])<1) return false; } 
    else {if(parseInt(arr[3])>31||parseInt(arr[3])<1) return false;}}
  return true;
}

//功能:验证是否是整数
//名称:isInt(num,[num1],[num2])
//说明:num1与num2均是可选的
//     假如选择了 num1 而没有选择 num2 则表示要验证的数必须是具有num1位
//     假如选择了 num2 则表示要验证的数是最少num1位,而最多不能超过num2位
  
//     对于只选择num2,而不选择num1,将返回假
//     对于num2<num1 也将返回假 

function Math.isInt(num)
{
 if(Math.isInt.arguments[1]==null)
    {if(Math.isInt.arguments[2]==null) var rgExp = /^\s*[1-9]\d*$/; else return false;}
 else 
   {
     if(Math.isInt.arguments[2]==null) var rgExp =  "/^\s*[1-9]\\d{"+Math.isInt.arguments[1]+"}$/";
     else
       {
	 if(Math.isInt.arguments[2]<Math.isInt.arguments[1]) return false;
         else {var rgExp =  "/^\s*[1-9]\d{"+Math.isInt.arguments[1]+","+Math.isInt.arguments[2]+"}$/";}
       } 
   }
 try{ var arr = rgExp.exec(num);
 if(arr==null) return false;
 if(arr[0].replace(/\s/gi,"")!=num.replace(/\s/gi,"")) return false;
 }
 catch(e){return false;}
 return true;
}

//     功能：格式化日期
// 运行环境：IE6.0

function formatDate(oDate)
{
  //alert(oDate);
  var rgExp = /^(\d{4})-(\d{1,2})-(\d{1,2})(\s+)(\d{1,2}):(\d{1,2}):(\d{1,2})$/
  if(rgExp.exec(oDate)!=null)  
    return  oDate.replace(rgExp,function($0,$1,$2,$3,$4,$5,$6,$7)
             { 
             var a1 = $1; 
             var a2 = $2; 
             var a3 = $3; 
             var a4 = $4; 
             var a5 = $5; 
             var a6 = $6; 
             var a7 = $7;
             if(a2.length<2) a2 = "0"+a2;
             if(a3.length<2) a3 = "0"+a3;
             if(a5.length<2) a5 = "0"+a5;
             if(a6.length<2) a6 = "0"+a6;
             if(a7.length<2) a7 = "0"+a7;
             if(formatDate.arguments[1]==null) return (a1+"-"+a2+"-"+a3+a4+a5+":"+a6+":"+a7);
             else return (a1+"-"+a2+"-"+a3);
             });
  else
    {
    var rgExp = /^(\d{4})-(\d{1,2})-(\d{1,2})\s*$/
    return  oDate.replace(rgExp,function($0,$1,$2,$3)
             { 
             var a1 = $1; 
             var a2 = $2; 
             var a3 = $3; 
             if(a2.length<2) a2 = "0"+a2;
             if(a3.length<2) a3 = "0"+a3;
             return (a1+"-"+a2+"-"+a3);
             });
    }   
}

function formatDateToChar(oDate)
{
  //alert(oDate);
  var rgExp = /^(\d{4})-(\d{1,2})-(\d{1,2})(\s+)(\d{1,2}):(\d{1,2}):(\d{1,2})$/
  if(rgExp.exec(oDate)!=null)  
    return  oDate.replace(rgExp,function($0,$1,$2,$3,$4,$5,$6,$7)
             { 
             var a1 = $1; 
             var a2 = $2; 
             var a3 = $3; 
             var a4 = $4; 
             var a5 = $5; 
             var a6 = $6; 
             var a7 = $7;
             if(arguments[1]==null) return (a1+"年"+a2+"月"+a3+"日"+a4+a5+"时"+a6+"分"+a7+"秒");
             else return (a1+"年"+a2+"月"+a3+"日");
             });
  else
    {
    var rgExp = /^(\d{4})-(\d{1,2})-(\d{1,2})\s*$/
    return  oDate.replace(rgExp,function($0,$1,$2,$3)
             { 
             var a1 = $1; 
             var a2 = $2; 
             var a3 = $3; 
             return (a1+"年"+a2+"月"+a3+"日");
             });
    }   
}

// 计算阶乘 摘自jscript范例

function factorial(aNumber)  {
aNumber = Math.floor(aNumber);  // 如果这个数不是一个整数，则向下舍入。
if (aNumber < 0)  {  // 如果这个数小于 0，拒绝接收。
    return -1;
    }
      if (aNumber == 0)  {  // 如果为 0，则其阶乘为 1。
      return 1;
      }
        else return (aNumber * factorial(aNumber - 1));  // 否则，递归直至完成。
}


//************************************************************
//
//            String 类
//
//************************************************************

//字符串操作

function String.Convert(v,dv)
{
	if(typeof(v)=="string")return v;
	if(typeof(dv)=="undefined")dv="";
	else dv=String.Convert(dv);	
	if(typeof(v)=="undefined")return dv;
	if(v===null)return dv;
	try{
		v=v+""
		if(v==="undefined")return dv;
		return String.Convert(v,dv);
	}catch(x){}
	return "[unconvertable]";
}

function String.prototype.Trim() 
{
	return this.replace(/^\s*/g,"").replace(/\s*$/g,"");
} function String.Trim(str){return String.Convert(str).Trim();}

function String.prototype.TrimLeft()
{
	return this.replace(/^\s*/g,"");
}function String.TrimLeft(str){return String.Convert(str).TrimLeft();}

function String.prototype.TrimRight()
{
	return this.replace(/\s*$/g,"");
}function String.TrimRight(str){return String.Convert(str).TrimRight();}

function String.prototype.Left(count)
{
	return this.substr(0,count);
}function String.Left(str,count){return String.Convert(str).Left(count);}

function String.prototype.Right(count)
{
	return this.substr(this.length-count,count);
}function String.Right(str,count){return String.Convert(str).Right(count);}

function String.prototype.RemoveBlank()
{
	return this.replace(/\s*　*/g,"");
}function String.RemoveBlank(str){return String.Convert(str).RemoveBlank();}

function Repeat(num,strRepeat)
{
 var str = ""
 for(uf_iBegin=0;uf_iBegin<num;uf_iBegin++)
  {str+=strRepeat;}
 return str;
}
function IsEmpty(sStr){
	var oRE = /^[　\s]*$/;
	var lRE = oRE.test(sStr);
	return lRE;
}

//* 检测传入值是否为正整数

function IsInteger(nNum){
	if (IsEmpty(nNum)) return false;
    if(nNum=="0") return false;
	var oRE = /^[\d]+$/gi;
	var lRE = oRE.test(nNum);
	return lRE;
}
function IsFloat(nNum){
	if (IsEmpty(nNum)) return false;
	var oRE = /^\d+\.\d+$/gi;
	var lRE = oRE.test(nNum);
	return lRE;
}

//* 按指定长度截取字符串

function CutStr(sStr,nLen){
	if (IsEmpty(sStr) || !IsInteger(nLen)) return false;
	var nStrLen	= sStr.length;
	sStr = ReEnHtml(sStr);
	var nCountLen	= 0;
	var nCharLen	= 0;
	var sCutStr	= "";
	for (var nTempCount = 0; nTempCount < nStrLen; nTempCount++){
		nCharLen = Math.abs(sStr.charCodeAt(nTempCount));
		nCountLen = (nCharLen > 255) ? nCountLen += 2 : nCountLen += 1;
		if (nCountLen > nLen){
			sCutStr = sStr.substring(0,nTempCount) + "...";
			break;
		}else{
			sCutStr = sStr;
		}
	}
	return sCutStr.replace(/\n/,"，");
}

//* 从 URL 字符串中提取变量的值

function GetQueryValue(sQuery,sPan){
	if (IsEmpty(sQuery) || IsEmpty(sPan)) return false;
	if (sQuery.indexOf("?") == 0) sQuery = sQuery.substr(1);
	if (sQuery.indexOf("&") >= 0){
		var aQuery = sQuery.split("&");
		var sTempQuery;
		for (var nTempCount = 0; nTempCount < aQuery.length; nTempCount++){
			sTempQuery = aQuery[nTempCount];
			if (sTempQuery.indexOf("=") >= 0){
				if (sTempQuery.substring(0,sTempQuery.indexOf("=")) == sPan){
					return sTempQuery.substr(sTempQuery.indexOf("=") + 1);
				}
			}else return false;
		}
		return false;
	}else{
		if (sQuery.indexOf("=") >= 0){
			if (sQuery.substring(0,sQuery.indexOf("=")) == sPan){
				return sQuery.substr(sQuery.indexOf("=") + 1);
			}else return false;
		}else return false;
	}
}

//* 检测字符串占用字节数函数

function StrLen(sStr){
	var lCn = ("金和人".length = 3);
	var nStrLen	= sStr.length;
	if (lCn){
		var nTempLen	= nStrLen;
		var nCharLen	= 0;
		for (var nTempCount = 0; nTempCount < nStrLen; nTempCount++){
			nCharLen = Math.abs(sStr.charCodeAt(nTempCount));
			if (nCharLen < 0) nCharLen += 65536;
			if (nCharLen > 255) nTempLen ++;
		}
		return nTempLen;
	}else return nStrLen;
}

//* 搜索结果突出显示关键字函数

function ReplaceQuery(sStr,sKey){
	if (IsEmpty(sStr) || IsEmpty(sKey)) return "转换搜索结果时出现错误。";
	sStr = ReEnHtml(sStr);
	var oRE		= new RegExp("(" + sKey + ")","gi");
	return sStr.replace(oRE,"<span style=\"color:red\">$1</span>");
}

//******************************************************
//
//             math 类
// 
//******************************************************

//所有参数的最大值，可以传递数组 与max方法类似

function Math.MaxOf()
{
	var arr=arguments;
	var res=-Infinity;
	for(var i=0;i<arr.length;i++)
	{
		var item=arr[i];
		if(item instanceof Array)
		{
			for(var j=0;j<item.length;j++)
			{
				var v=parseFloat(item[j]);
				if(v>res)res=v;
			}
		}
		else
		{
			var v=parseFloat(item);
			if(v>res)res=v;
		}
	}
	return res;	
}
//所有参数的最小值，可以传递数组

function Math.add(n1,n2){
var r1,r2,m;
try{r1=n1.toString().split(".")[1].length}catch(e){r1=0}
try{r2=n2.toString().split(".")[1].length}catch(e){r2=0}
m=Math.pow(10,Math.max(r1,r2))
return (n1*m+n2*m)/m
}

function Math.MinOf()
{
	var arr=arguments;
	var res=Infinity;
	for(var i=0;i<arr.length;i++)
	{
		var item=arr[i];
		if(item instanceof Array)
		{
			for(var j=0;j<item.length;j++)
			{
				var v=parseFloat(item[j]);
				if(v<res)res=v;
			}
		}
		else
		{
			var v=parseFloat(item);
			if(v<res)res=v;
		}
	}
	return res;	
}




//*******************************************
//
//	Number 类
//
//*******************************************

Number.prototype.fix = function(num)
  {
    with(Math)
       {
      var oNum = round(this.valueOf()*pow(10,num))/pow(10,num);
      if(!/\./.test(oNum))
        {
         var oDec = ""
         for(begin=0;begin<num;begin++){oDec+="0";}
         oNum = oNum.toString()+"."+oDec;
        }
       return oNum;
       }
   return num;
  }


//*******************************************
//
//	Date 类
//
//*******************************************
function IsDateCompare(beginDate,endDate)
{
  if((!isDate(beginDate))||(!isDate(endDate))) return false;
  var beginArrayDate = /(\d{4})-(\d{1,2})-(\d{1,2})/.exec(beginDate);
  var endArrayDate = /(\d{4})-(\d{1,2})-(\d{1,2})/.exec(endDate);
  if(parseInt(endArrayDate[1],10)>parseInt(beginArrayDate[1],10)) return true;
  if(parseInt(endArrayDate[1],10)==parseInt(beginArrayDate[1],10))
     {
        if(parseInt(endArrayDate[2],10)>parseInt(beginArrayDate[2],10)) return true;
        else
          {
           
              if(parseInt(endArrayDate[2],10)==parseInt(beginArrayDate[2],10)) 
                 {
                  if(parseInt(endArrayDate[3],10)>=parseInt(beginArrayDate[3],10)) return true;
                 }
          }
     }  
  return false;
}  
//*******************************************
//
//	WINDOWS 类
//
//*******************************************
function mode640(FileName,args)
{
  var str = showModalDialog(FileName,args,'dialogWidth:640px; dialogHeight:480px;status:no;help:no'); 

  return str;
}
function mode480(FileName,args)
{
  var str = showModalDialog(FileName,args,'dialogWidth:480px; dialogHeight:360px;status:no;help:no'); 
  return str;
}
function mode320(FileName,args)
{
  var str=showModalDialog(FileName,args,'dialogWidth:360px; dialogHeight:240px;status:no;help:no'); 
  return str;
}

//测试是否含有英文半角标点符号
function BiaoDian(str)
{
var regExp=/\~|\`|\!|\@|\#|\$|\%|\^|\&|\*|\(|\-|\)|\+|\=|\_|\{|\}|\[|\]|\\|\||\:|\;|\"|\'|\<|\>|\,|\.|\?|\//g;
if(regExp.test(str))
{
	return true;
}
else
{
	return false;
}
}