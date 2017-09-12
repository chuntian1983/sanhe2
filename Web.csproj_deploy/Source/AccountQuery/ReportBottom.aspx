<table id="tabReportBottom" cellpadding="0" cellspacing="0" style="width: 750px;
    font-size: 10pt; height: 80px; margin-top: 8px">
    <tr>
        <td id="SignName0" style="width: 35%">
            <%=DefConfigs.GetReportSignName(0)%></td>
        <td id="SignName1" style="width: 35%">
            <%=DefConfigs.GetReportSignName(1)%></td>
        <td id="SignName5" style="width: 30%">
            <%=DefConfigs.GetReportSignName(5)%></td>
    </tr>
    <tr>
        <td id="SignName2">
            <%=DefConfigs.GetReportSignName(2)%></td>
        <td id="SignName3">
            <%=DefConfigs.GetReportSignName(3)%></td>
        <td id="SignName4">
            <%=DefConfigs.GetReportSignName(4)%></td>
    </tr>
</table>
<script type="text/javascript">
    var isSignName = 0;
    CheckSignName("SignName0");
    CheckSignName("SignName1");
    CheckSignName("SignName2");
    CheckSignName("SignName3");
    CheckSignName("SignName4");
    function CheckSignName(id) {
        var signName = document.getElementsByName(id);
        for (var i = 0; i < signName.length; i++) {
            var hm = signName[i].innerHTML.replace(/&nbsp;/g, "");
            if (hm == "：" || hm == "") {
                isSignName++;
                signName[i].innerHTML = "&nbsp;";
            }
            else {
                if (hm.indexOf("：") == -1 && hm.indexOf(":") == -1) {
                    signName[i].innerHTML += "：";
                }
            }
        }
    }
    var gWidth = 750;
    if (document.all("GridView1")) {
        gWidth = document.getElementById("GridView1").offsetWidth;
    }
    if (document.all("GridViewWidth")) {
        gWidth = document.getElementById("GridViewWidth").value;
    }
    var rbottom = document.getElementsByName("tabReportBottom");
    if (isSignName < 5 || document.getElementById("SignName5").innerHTML.length > 0) {
        for (var i = 0; i < rbottom.length; i++) {
            rbottom[i].style.width = gWidth;
        }
    }
    else {
        for (var i = 0; i < rbottom.length; i++) {
            rbottom[i].style.display = "none";
        }
    }
</script>
