$(document).ready(function () {
    $("input").blur(function () {
        var userid = $("#txtuserid").attr('value');
        var password = $("#txtpassword").attr('value');
        if(userid=='')
        {
            $('div.formerrormsg').text("Please enter your student ID");
            
        }
        else if(password=='')
        {
            $('div.formerrormsg').text("Please enter your password");
        }
        else
        {
            $('div.formerrormsg').text("");
        }
        
    });
});
function ValidateUser() {
    var userid = $("#txtuserid").attr('value');
    var password = $("#txtpassword").attr('value');
    var url = "/Home/ValidateUser/";
    $('img.loading').show();
    $.ajax({
        url: url,
        data: { userId: userid, password: password },
        cache: false,
        type: "POST",
        success: function (data) {

            if (data == "1") {
                window.location.href = '/Home/Index';
            } else {
                $('div.formerrormsg').text("You Student ID or password is incorrect");
            }
            $("#txtuserid").attr({ 'value': '' });
            $("#txtpassword").attr({ 'value': '' });
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
    $('img.loading').hide();
}