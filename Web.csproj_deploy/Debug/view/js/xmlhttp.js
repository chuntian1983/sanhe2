function SendHttp(sAspFile,sSend)
{if (navigator.onLine==false) 
    {return "�����ڴ����ѻ�״̬,������������!"} 
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.Open("POST", sAspFile, false);
	try { xmlhttp.Send("<root>"+sSend+"</root>");}
	catch (exception)
	{
	   alert("������æ!")
	}
	try
	{ var str11=xmlhttp.responseText //ϵͳ����: -1072896748��
	}
	catch (exception)
	{if (exception.description=='ϵͳ����: -1072896748��') 
		{	
	 	str11=""
		}
	}
	return str11
}
