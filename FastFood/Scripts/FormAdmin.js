$(document).ready(function () {
    $("input").blur(function () {
        var userid = $("#txtuserid").attr('value');
        var password = $("#txtpassword").attr('value');
        if (userid == '') {
            $('div.formerrormsg').text("Please enter your username");

        }
        else if (password == '') {
            $('div.formerrormsg').hide();
            //$('div.formerrormsg').text("Please enter your password");
        }
        else {
            $('div.formerrormsg').text("");
        }

    });
});
function ValidateUser() {
    var userid = $("#txtuserid").attr('value');
    var password = $("#txtpassword").attr('value');
    var url = "/Admin/ValidateAdmin/";
    $('div.formerrormsg').text("Please wait");
    $('a.#btn').text("Please wait");
    $.ajax({
        url: url,
        data: { userId: userid, password: password },
        cache: false,
        type: "POST",
        success: function (data) {

            if (data == "1") {
                window.location.href = '/Admin/Index';
            } else {
                $('div.formerrormsg').text("Your username or password is incorrect");
            }
            $("#txtuserid").attr({ 'value': '' });
            $("#txtpassword").attr({ 'value': '' });
        },
        error: function (reponse) {
            console.log(response);
        }
    });
    $("#btnlogin").val('Login');
}