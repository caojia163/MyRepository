var httpUrl = "http://localhost:59998/api/";
$(function () {
    $("#cklogin").click(function () {
        if ($("#cklogin").attr("checked")) {
            $("#cklogin").attr("checked", false);
        } else {
            $("#cklogin").attr("checked", true);
        }
    });

    getCookie();

    $("#btnLogin").click(function () {
        login();
    });
})

//登录提交
function login() {
    var UserName;
    var PassWord;

    if ($("#username").val() == "") {
        $MB.n_warning("用户名不能为空");
        return;
    }
    if ($("#password").val() == "") {
        $MB.n_warning("密码不能为空");
        return;
    }

    saveInfo();

    //传入参数
    var obj = { UserName: $("#username").val(), PassWord: $("#password").val() };
    console.log(JSON.stringify(obj));
    $.ajax({
        url: httpUrl + 'Login/UserLogin',
        type: 'POST',
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            var res = eval("(" + data + ")");
            if (res.status == 1) {
                location.href = "../../../../View/index.html";
            } else {
                $MB.n_warning(res.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    })
}

// 保存Cookie
function saveInfo() {
    try {
        // 保存按键是否选中
        var isSave = document.getElementById('cklogin').checked;
        if (isSave) {
            var username = $("#username").val();
            var password = $("#password").val();
            if (username != "" && password != "") {
                SetCookie(username, password);
            }
        } else {
            SetCookie("", "");
        }

    } catch (e) {}
}

// 保存Cookie
function SetCookie(username, password) {
    var d = new Date();
    d.setDate(d.getDate() + 7); //7天后失效
    document.cookie = ("username=" + username + "%%" + password + ";expires=" + d.toGMTString());
}

// 获取登陆的用户名和密码
function getCookie() {
    debugger;
    var nmpsd;
    var nm;
    var psd;
    var cookieString = new String(document.cookie);
    var cookieHeader = "username=";
    var beginPosition = cookieString.indexOf(cookieHeader);
    cookieString = cookieString.substring(beginPosition);
    var ends = cookieString.indexOf(";");
    if (ends != -1) {
        cookieString = cookieString.substring(0, ends);
    }
    if (beginPosition > -1) {
        nmpsd = cookieString.substring(cookieHeader.length);
        if (nmpsd != "") {
            beginPosition = nmpsd.indexOf("%%");
            nm = nmpsd.substring(0, beginPosition);
            psd = nmpsd.substring(beginPosition + 2);
            $("#username").val(nm)
            $("#password").val(psd)

            if (nm != "" && psd != "") {
                document.getElementById('cklogin').checked = true;
            }
        }
    }
}