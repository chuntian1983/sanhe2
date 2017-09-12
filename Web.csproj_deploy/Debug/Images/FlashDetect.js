var flashinstalled = 0;
var flashversion = 0;
var MSDetect = "false";

if (navigator.plugins && navigator.plugins.length)
{
	x = navigator.plugins["Shockwave Flash"];
	if (x)
	{
		flashinstalled = 2;
		if (x.description)
		{
			y = x.description;
			flashversion = y.charAt(y.indexOf('.')-1);
		}
	}
	else
		flashinstalled = 1;
	if (navigator.plugins["Shockwave Flash 2.0"])
	{
		flashinstalled = 2;
		flashversion = 2;
	}
}
else if (navigator.mimeTypes && navigator.mimeTypes.length)
{
	x = navigator.mimeTypes['application/x-shockwave-flash'];
	if (x && x.enabledPlugin)
		flashinstalled = 2;
	else
		flashinstalled = 1;
}
else
	MSDetect = "true";

if (MSDetect == "true") {
	var doc = '<scr' + 'ipt language="VBScript"\> \n';
	doc += 'on error resume next \n';
	doc += '	For i = 3 to 10\n';
	doc += '		If Not(IsObject(CreateObject("ShockwaveFlash.ShockwaveFlash." & i))) Then\n';
	doc += '\n';
	doc += '		Else\n';
	doc += '			flashinstalled = 2\n';
	doc += '			flashversion = i\n';
	doc += '		End If\n';
	doc += '	Next\n';
	doc += '\n';
	doc += 'If flashinstalled = 0 Then\n';
	doc += '	flashinstalled = 1\n';
	doc += 'End If\n';
	doc += '</scr' + 'ipt\> \n';
	document.write(doc);
}

function overwriteWithFlash(strSRC, strName, intWidth, intHeight, intFlashVersion){
	if(flashinstalled == 0)
		return false;
	if(flashversion < intFlashVersion)
		return false;
	var strFlash = '<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" '; 	
	strFlash += 'codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" '; 	
	strFlash += 'width="'+intWidth+'" height="'+intHeight+'" id="oSwf'+strName+'" align="middle">'; 	
	strFlash += '<param name="movie" value="'+strSRC+'" />'; 	
	strFlash += '<param name="quality" value="high" />'; 	
	strFlash += '<param name="bgcolor" value="#ffffff" />'; 	
	strFlash += '<param name="wmode" value="opaque" />'; 	
	strFlash += '<embed src="'+strSRC+'" '; 	
	strFlash += 'quality="high" bgcolor="#ffffff" '; 	
	strFlash += 'width="'+intWidth+'" height="'+intHeight+'" '; 	
	strFlash += 'name="oSwf'+strName+'" align="middle" type="application/x-shockwave-flash" wmode="opaque" pluginspage="http://www.macromedia.com/go/getflashplayer" /></object>'; 	
	document.getElementById(strName).innerHTML = strFlash;
}
