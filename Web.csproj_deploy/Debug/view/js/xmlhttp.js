function SendHttp(sAspFile,sSend)
{if (navigator.onLine==false) 
    {return "您现在处于脱机状态,请联机后再试!"} 
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.Open("POST", sAspFile, false);
	try { xmlhttp.Send("<root>"+sSend+"</root>");}
	catch (exception)
	{
	   alert("服务器忙!")
	}
	try
	{ var str11=xmlhttp.responseText //系统错误: -1072896748。
	}
	catch (exception)
	{if (exception.description=='系统错误: -1072896748。') 
		{	
	 	str11=""
		}
	}
	return str11
}
