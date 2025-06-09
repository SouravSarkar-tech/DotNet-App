function IsNumber() {
    if (!(event.keyCode == 13) && !(event.keyCode == 32) && !(event.keyCode == 35) && !(event.keyCode == 46) && !(event.keyCode >= 48 && event.keyCode <= 57))
        return false;
}


function IsOnlyNumber() {
    if (event.keyCode > 31 && (event.keyCode < 48 || event.keyCode > 57))
    //if (!(event.keyCode == 13) && !(event.keyCode == 32) && !(event.keyCode == 35) && !(event.keyCode == 46) && !(event.keyCode >= 48 && event.keyCode <= 57))
        return false;
}

function OpenRequestHistory(mid) {
    window.open("../Common/WorkFlow.aspx?MHId=" + mid , "_blank", "location=0, menubar=0, toolbar=0, resizable=1");
}

function OpenRequestHistory(mid,Mpid) {
    window.open("../Common/WorkFlow.aspx?MHId=" + mid + "&MPId=" + Mpid, "_blank", "location=0, menubar=0, toolbar=0, resizable=1");
}

