function getLodop(oOBJECT,oEMBED){
    var strHtml1="<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='/Images/print/install_lodop.exe'>执行安装</a>,安装后请刷新页面或重新进入。</font>";
    var strHtml2="<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='/Images/print/install_lodop.exe'>执行升级</a>,升级后请重新进入。</font>";
    var strHtml3="<br><br><font color='#FF00FF'>(注：如曾安装过Lodop旧版附件npActiveXPLugin,请在【工具】->【附加组件】->【扩展】中先卸载它)</font>";
    var LODOP=oEMBED;		
	try{		     
	     if (navigator.appVersion.indexOf("MSIE")>=0) LODOP=oOBJECT;

	     if ((LODOP==null)||(typeof(LODOP.VERSION)=="undefined")) {
		 if (navigator.userAgent.indexOf('Firefox')>=0)
  	         document.documentElement.innerHTML=strHtml3+document.documentElement.innerHTML;
		 if (navigator.appVersion.indexOf("MSIE")>=0) document.write(strHtml1); else
		 document.documentElement.innerHTML=strHtml1+document.documentElement.innerHTML;
	     } else if (LODOP.VERSION<"6.0.1.0") {
		 if (navigator.appVersion.indexOf("MSIE")>=0) document.write(strHtml2); else
		 document.documentElement.innerHTML=strHtml2+document.documentElement.innerHTML; 
	     }
	     //*****如下空白位置适合调用统一功能:*********	     


	     //*******************************************
	     return LODOP; 
	}catch(err){
	     document.documentElement.innerHTML="Error:"+strHtml1+document.documentElement.innerHTML;
	     return LODOP; 
	}
}

var paramname=decodeURIComponent("%E5%B1%B1%E4%B8%9C%E5%86%9C%E5%8F%8B%E8%BD%AF%E4%BB%B6%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8");
document.write("<object id='LODOP' classid='clsid:2105C259-1E0C-4534-8141-A753534CB4CA' width=0 height=0>");
document.write("<embed id='LODOP_EM' type='application/x-print-lodop' width=0 height=0 pluginspage='/Images/print/install_lodop.exe'></embed>");
document.write("<param name='CompanyName' value='"+paramname+"' /><param name='License' value='449637775718688748719056235623' />");
document.write("</object>");

var LODOP=getLodop(document.getElementById('LODOP'),document.getElementById('LODOP_EM'));  

function PrintOneURL0(printUrl, intOrient){
    if(!printUrl)
    {
        if(document.all("PrintUrl"))
        {
            printUrl=document.getElementById("PrintUrl").value;
        }
        else
        {
            alert("缺少打印地址！");
            return false;
        }
    }
    if(intOrient) { LODOP.SET_PRINT_PAGESIZE(intOrient, 0, 0, "A4"); }
	LODOP.ADD_PRINT_URL(30,35,746,539,printUrl);
	LODOP.SET_PRINT_STYLEA(1,"HOrient",3);
	LODOP.SET_PRINT_STYLEA(1,"VOrient",3);
	LODOP.PREVIEW();
	return false;
}
function PrintOneURL1(printUrl, intOrient){
    if(!printUrl)
    {
        if(document.all("PrintUrl"))
        {
            printUrl=document.getElementById("PrintUrl").value;
        }
        else
        {
            alert("缺少打印地址！");
            return false;
        }
    }
    if(intOrient) { LODOP.SET_PRINT_PAGESIZE(intOrient, 0, 0, "A4"); }
	LODOP.ADD_PRINT_URL(30,35,746,539,printUrl);
	LODOP.SET_PRINT_STYLEA(1,"HOrient",3);
	LODOP.SET_PRINT_STYLEA(1,"VOrient",3);
	LODOP.PRINT();
	return false;
}
