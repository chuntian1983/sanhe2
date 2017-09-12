//'*********************************************************
// '1�� Purpose: �ж������Ƿ�Ϊ������
// ' Inputs:String
// ' Returns:True, False
//'*********************************************************
function IsInteger(str){
	if (str=="") {
		return false;
	}
	if (isNaN(str) || (!(parseFloat(str)>=0)) || parseInt(str)!=parseFloat(str)){
		return false;
	}
	return true;
}
//'*********************************************************
//'*********************************************************
// ' 2��Purpose: �ж������Ƿ�Ϊ����
// ' Inputs:String
// ' Returns:True,False
//'*********************************************************
function IsFloat(str){ 
	if (str==""){
		return false;
	}
	if (isNaN(str) || (!(parseFloat(str)))||(!(parseFloat(str)>0))){
		return false;
	}

	return true;}
//'*********************************************************
//' 3��Purpose: �ж������Ƿ�Ϊ�绰����
// ' Inputs:String
// ' Returns:True,False
//'*********************************************************
function IsTelephone(str){
	var i,strlengh,tempchar;
	str=CStr(str);
	if(str=="") return false;
	strlength=str.length;
	for(i=0;i<strlength;i++){
		tempchar=str.substring(i,i+1);
		if(!(tempchar==0||tempchar==1||tempchar==2||tempchar==3||tempchar==4||tempchar==5||tempchar==6||tempchar==7||tempchar==8||tempchar==9||tempchar=='-')){
			return false;
		}
	}
	return true;
}

//'*********************************************************
//'*********************************************************
// '4�� Purpose: �ж������Ƿ�ΪEmail
// ' Inputs:String
// ' Returns:True,False
//'*********************************************************
function IsEmail(str){
	var bflag=true
	if (str.indexOf("'")!=-1) {
		bflag=false
	}
	if (str.indexOf("@")==-1) {
		bflag=false
	}
	else if(str.charAt(0)=="@"){
		bflag=false
	}
	
	return bflag
}
//'*********************************************************
// '5�� Purpose: �ж������Ƿ���Ϊ����
// ' Inputs:String
// ' Returns:True,False
//'*********************************************************
function CheckChinese(str){
	if(escape(str).indexOf("%u")!=-1){ 
		return true;
	}
	return false;
}

//'*********************************************************
//'*********************************************************
//' 6��Purpose: �ж������Ƿ��пո�
// ' Inputs:String
// ' Returns:True,False
//'*********************************************************
function CheckBlank(str){
	var strlength;
	var k;
	var ch;
	strlength=str.length;
	for(k=0;k<=strlength;k++){
		ch=str.substring(k,k+1);
		if(ch==" "){
		return false;
		}
	}
	return true;
}
//'*********************************************************
//'*********************************************************
//' 7��Purpose: ȥ��Str���߿ո�
// ' Inputs:Str
// 'Returns:ȥ�����߿ո��Str
//'*********************************************************
function Trim(str){
	var i,strlength,t,chartemp,returnstr;
	str=CStr(str);
	strlength=str.length;
	t=str;
	for(i=0;i<strlength;i++){
		chartemp=str.substring(i,i+1);
		if(chartemp==" "){
			t=str.substring(i+1,strlength);
		}
		else{
			break;
		}
	}
	returnstr=t;
	
	strlength=t.length;
	for(i=strlength;i>=0;i--){
		chartemp=t.substring(i,i-1);
		if(chartemp==" "){
			returnstr=t.substring(i-1,0);
		}
		else{
			break;
		}
	}
	return (returnstr);
}
//'*********************************************************
//'*********************************************************
//' 8��Purpose:����ֵ��ת��Ϊ�ַ�����
// ' Inputs:int
// 'Returns:String
//'*********************************************************
function CStr(inp){
	return(""+inp+"");
}

//'*********************************************************
//'*********************************************************
// ' 9��Purpose: ȥ���ַ�: " ",","
// ' Inputs:String
// ' Returns:String
//'*********************************************************
function Repp(str){
	var str1;
	str1=str;
	str1=Replace(str1,",","��",1,0);
	str1=Replace(str1," ","",1,0);
	return str1;
} 

//'*********************************************************
//'*********************************************************
// '10��Purpose: ȥ�����Ϸ��ַ�: ',",<,>
// ' Inputs:String
// ' Returns:String
//'*********************************************************
function Rep(str){
	var str1;
	str1=str;
	str1=Replace(str1,"'","��",1,0);
	str1=Replace(str1,'"',"��",1,0);
	str1=Replace(str1,"<","��",1,0);
	str1=Replace(str1,">","��",1,0);
	return str1;
} 

//'*********************************************************
//'*********************************************************
// '11�� Purpose: ����ַ�
// ' Inputs:Ŀ��String,��������ַ�,�����Ϊ�ַ���,��Сд�Ƿ�����1Ϊ����,�Ƿ����ִ���
// ' Returns:String
//'*********************************************************
function Replace(target,oldTerm,newTerm,caseSens,wordOnly) {
	var wk ;
	var ind = 0; 
	var next = 0; 
	wk=CStr(target); 
	if (!caseSens){
		oldTerm = oldTerm.toLowerCase();
		wk = target.toLowerCase(); 
	}
	while ((ind = wk.indexOf(oldTerm,next)) >= 0) {
	if (wordOnly){
		var before = ind -1;
		var after = ind + oldTerm.length;
		if (!(space(wk.charAt(before)) && space(wk.charAt(after)))){
			next = ind + oldTerm.length;
			continue;} 
}
target = target.substring(0,ind) + newTerm +target.substring(ind+oldTerm.length,target.length);
wk = wk.substring(0,ind) + newTerm + wk.substring(ind+oldTerm.length,wk.length);
next = ind + newTerm.length;
if (next >= wk.length) { 
	break;
		}
	}
	return target;
}
//'*********************************************************

//'*********************************************************
//'*********************************************************
// '12�� Purpose: �ж��ַ������Ƿ�������źͷֺźͶ���
// ' Inputs:str
// ' Returns:True,Flase
//'*********************************************************
function CheckSpecialChar(strchar) {
	var intfind=strchar.indexOf('"');
	if (intfind>-1) {
		return false;
	}

	intfind=strchar.indexOf("'");
	if (intfind>-1) {
		return false;
	}

	intfind=strchar.indexOf(';');
	if (intfind>-1) {
		return false;
	}
	
	intfind=strchar.indexOf(',');
	if (intfind>-1) {
		return false;
	}

	return true;
}  

//'*********************************************************

//'*********************************************************
//'*********************************************************
// '13�� Purpose: ��������
// ' Inputs:strNumber
// ' Returns:strNumber
//'*********************************************************
function Round(strNumber) {
	if (strNumber=="") {
		return strNumber;
	}
	var intfind=strNumber.indexOf(".");
	strNumber=strNumber*100;
	if (intfind>-1) {
		strNumber=Math.round(strNumber);
	}
	return strNumber/100;
}


//'*********************************************************

//'*********************************************************

//'*********************************************************
//'*********************************************************
// '14�� Purpose: �Ƚ����ڵĴ�С��ϵ
// 'Inputs:datstr,datstr1
// ' Returns:True,Flase
//'*********************************************************
function CompareDate(qsDat,zzDat) {
	qsDat=wf_DateToChar(qsDat,"s");
	zzDat=wf_DateToChar(zzDat,"s");
	if (CStr(qsDat)>CStr(zzDat)){
		return false;		
	}
	return true;
}

//'*********************************************************

//'*********************************************************

//'*********************************************************
//'*********************************************************
// '15�� XML���ݴ���
// 'Inputs��sAspFile Ϊ���õ�ASP�ļ�����������ѯ������
// 		    sSendΪSEND��XML�ַ���
// 'Returns����������HTTP����Ӧ���

//'*********************************************************

function SendHttp(sAspFile,sSend)
{
    if (navigator.onLine==false) 
    {
		return "�����ڴ����ѻ�״̬,������������!"
		
    } 
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
 
     xmlhttp.Open("POST", sAspFile, false);
  
	try
	{
	    xmlhttp.Send("<root>"+sSend+"</root>");
	   
	}
	
	catch (exception)
	{
		//alert("������æ!")
	}
   // alert(xmlhttp.responseText) 
	
	try
	{
		var str11=xmlhttp.responseText //ϵͳ����: -1072896748��
	}
	catch (exception)
	{
		if (exception.description=='ϵͳ����: -1072896748��') 
		{	
			str11=""
			//alert("aa")
		}
			
	}
	//if (str11.indexOf("-2147483638")!=-1) str11=""
	//alert(str11)
	return str11
	
}
//'*********************************************************
// '16�� ȫѡ
// 'Inputs��objD Ŀ��
//		    objS Դ
// 'Returns��

//'*********************************************************
function selSub(objD,objS){
	if (objS.checked){
		for(i=0;i<objD.all.length;i++){

			if("INPUT"==objD.all(i).tagName){
				objD.all(i).checked="true";
			}
		}
	}
	else{
		for(i=0;i<objD.all.length;i++){
			if("INPUT"==objD.all(i).tagName){
				objD.all(i).checked="";
			}
		}
	}
}
//'*********************************************************
// '17�� ҳ����Ϣ����
// 'Inputs��strFind  Ҫ���ҵ�����
//          objRange ���ҷ�Χ
// 'Returns��
//'*********************************************************
var ncall_message=0;
function findInPage(strFind,objRange){
  var txt, i, found;

  if (strFind=="")
    return false;
    txt =objRange.createTextRange();
    for (i = 0; i <= ncall_message && (found = txt.findText(strFind)) != false; i++){
		txt.moveStart("character", 1);
		txt.moveEnd("textedit");
    }

    if (found){
      txt.moveStart("character", -1);
      txt.findText(strFind);
      txt.select();
      txt.scrollIntoView();
      ncall_message++;
    }else{
		if (ncall_message > 0){
			ncall_message = 0;
			findInPage(strFind,objRange);
		}else
        alert("û���ҵ�!");
    }
	return false;
}
//'*********************************************************
// '18�� IE�汾
// 'Returns��
//'*********************************************************
function ieVer()
 { 
   var ua=window.navigator.userAgent;
   var msieIndex=ua.indexOf("MSIE")
   return ua.substring(msieIndex+5,msieIndex+6);
}


    Array.prototype.clear=function(){ 
        this.length=0; 
    } 
    Array.prototype.insertAt=function(index,obj){ 
        this.splice(index,0,obj); 
    } 
    Array.prototype.removeAt=function(index){ 
        this.splice(index,1); 
    } 
    Array.prototype.remove=function(obj){ 
        var index=this.indexOf(obj); 
        if (index>=0){ 
        this.removeAt(index); 
        } 
    } 