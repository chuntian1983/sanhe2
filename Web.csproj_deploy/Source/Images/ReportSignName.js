var isSignName=0;
CheckSignName("SignName0");
CheckSignName("SignName1");
CheckSignName("SignName2");
CheckSignName("SignName3");
CheckSignName("SignName4");
function CheckSignName(id)
{
    var signName=document.getElementsByName(id);
    for(var i=0;i<signName.length;i++)
    {
        if(signName[i].innerHTML.replace(/&nbsp;/g,"")=="£º")
        {
            isSignName++;
            signName[i].innerHTML="&nbsp;";
        }
    }
}
var rbottom=document.getElementsByName("tabReportBottom");
if(isSignName<5||document.getElementById("SignName5").innerHTML.length>0)
{
    for(var i=0;i<rbottom.length;i++)
    {
       rbottom[i].style.width=gWidth;
    }
}
else
{
    for(var i=0;i<rbottom.length;i++)
    {
       rbottom[i].style.display="none";
    }
}