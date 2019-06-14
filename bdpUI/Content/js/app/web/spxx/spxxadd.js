/**
* Created by caojia on 2018-04-28.
*/
var validator;
var $userAddForm = $("#f-Spxx");
var httpUrl = "http://localhost:59998/api/";

$(function () {
    Date.prototype.format = function (fmt) {
        var o = {
            "M+": this.getMonth() + 1,                 //月份
            "d+": this.getDate(),                    //日
            "h+": this.getHours(),                   //小时
            "m+": this.getMinutes(),                 //分
            "s+": this.getSeconds(),                 //秒
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度
            "S": this.getMilliseconds()             //毫秒
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    }
    $("#begintime").datetimepicker({
        format: 'yyyy-mm-dd hh:ii:ss',
        language: 'zh-CN'
    });
    $("#endtime").datetimepicker({
        format: 'yyyy-mm-dd hh:ii:ss',//显示格式
        language: 'zh-CN'
    });
    var endDate = new Date();
    $("#endtime").val(endDate.format("yyyy-MM-dd") + " 23:59:59");

    var startDate = new Date();
    startDate.setDate(startDate.getDate() - 30);
    $("#begintime").val(startDate.format("yyyy-MM-dd") + " 00:00:00");

    //日期2
    $("#begintime1").datetimepicker({
        format: 'yyyy-mm-dd hh:ii:ss',
        language: 'zh-CN'
    });
    $("#endtime1").datetimepicker({
        format: 'yyyy-mm-dd hh:ii:ss',//显示格式
        language: 'zh-CN'
    });
    var endDate = new Date();
    $("#endtime1").val(endDate.format("yyyy-MM-dd") + " 23:59:59");

    var startDate = new Date();
    startDate.setDate(startDate.getDate() - 30);
    $("#begintime1").val(startDate.format("yyyy-MM-dd") + " 00:00:00");

    loadPmfs();
    loadCprCn();
    loadCdrCn();
    loadPjzt();
    loadFkfs();

    validateRule(); //输入验证

    $("#btnAdd").click(function () {

        var beginDate = $("#begintime1").val();
        var endDate = $("#endtime1").val();
        beginDate = new Date(beginDate);
        endDate = new Date(endDate);
        if (beginDate > endDate) {
            alert("拍卖结束时间必须大于开始时间");
            return false;
        }

        var validator = $userAddForm.validate();
        var flag = validator.form();
        if (flag) {
            //上传图片
            uploadImage();
            //提交数据
            $.ajax({
                type: "POST",
                url: httpUrl + "Spxx/AddSpxx",
                data: $userAddForm.serialize(),
                dataType: "json",
                success: function (data) {
                    var res = eval("(" + data + ")");
                    alert(res.msg);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            })
        }
    });
});

function validateRule() {
    var icon = "<i style='color:red'> ";

    $.validator.addMethod("spPJJE", function (value, element) {
        var reg = /^-?\d*\.?\d*$/;
        return this.optional(element) || reg.test(value);
    }, icon + "输入的票据金额只包含数字或小数");

    $.validator.addMethod("spJJFD", function (value, element) {
        var reg = /^-?\d*$/;
        return this.optional(element) || reg.test(value);
    }, icon + "输入的加价幅度只能是数字");

    $.validator.addMethod("spHPH", function (value, element) {
        var reg = /^-?\d*$/;
        return this.optional(element) || reg.test(value);
    }, icon + "输入的汇票号只能是数字");

    $.validator.addMethod("spBZJYQ", function (value, element) {
        var reg = /^-?\d*$/;
        return this.optional(element) || reg.test(value);
    }, icon + "输入的保证金要求只只包含数字或小数");

    icon = icon + "</i>";
    //$.validator.addMethod("isPhone", function (value, element) {
    //    var reg = /^[1][3,4,5,7,8][0-9]{9}$/;
    //    return this.optional(element) || reg.test(value);
    //}, icon + "请输入正确的手机号");

    validator = $userAddForm.validate({
        rules: {
            spPMFS: {
                required: true
            },
            spCPRCN: {
                required: true
            },
            spCDRCN: {
                required: true
            },
            spPJZT: {
                required: true
            },
            spFKFS: {
                required: true
            },
            spPJJE: {
                required: true,
                spPJJE: true
            },
            spJJFD: {
                required: true,
                spJJFD: true
            },
            spHPH: {
                required: true,
                minlength: 6,
                spHPH: true
            },
            spBZJYQ: {
                required: true,
                spBZJYQ: true
            }
        },
        messages: {
            spPMFS: {
                required: icon + "请选择拍卖方式"
            },
            spCPRCN: {
                required: icon + "请选择出票人承诺"
            },
            spCDRCN: {
                required: icon + "请选择承兑人承诺"
            },
            spPJZT: {
                required: icon + "请选择票据状态"
            },
            spFKFS: {
                required: icon + "请选择付款方式"
            },
            spPJJE: {
                required: icon + "请输入票据金额",
            },
            spJJFD: {
                required: icon + "请输入加价幅度"
            },
            spHPH: {
                required: icon + "请输入汇票号",
                minlength: icon + "最少6个字符"
            },
            spBZJYQ: {
                required: icon + "清输入保证金要求"
            }
        }
    });
}

function uploadImage() {
    var formData = new FormData();
    formData.append('file', $('#fileSchp')[0].files[0]);  //添加图片信息的参数
    //formData.append('sizeid', 123);  //添加其他参数

    $.ajax({
        url: httpUrl + 'UploadFile/UploadImages',
        type: 'POST',
        cache: false, //上传文件不需要缓存
        data: formData,
        processData: false, // 告诉jQuery不要去处理发送的数据
        contentType: false, // 告诉jQuery不要去设置Content-Type请求头
        success: function (data) {
            var res = eval("(" + data + ")");
            if (res.status == 1) {
                $("#hidSchp").val(res.data);
                console.log('上传成功！');
            } else {
                console.log(res.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    })
}


//获取拍卖方式下拉框加载
function loadPmfs() {
    $.get(httpUrl + "Spxx/GetDropDownList", { key: "PM" }, function (data) {
        var res = eval("(" + data + ")");
        console.log(res);
        $("#spPMFS").find("option").remove();
        $("#spPMFS").append($("<option value=''>-请选择-</option>"));
        for (var i = 0; i < res.data.length; i++) {
            var id = res.data[i].DictCode;
            var name = res.data[i].DictName;
            var opt = "<option value='" + id + "'>" + name + "</option>";
            $("#spPMFS").append(opt);
        }
    });
}

//获取出票人承诺下拉框加载
function loadCprCn() {
    $.get(httpUrl + "Spxx/GetDropDownList", { key: "CN" }, function (data) {
        var res = eval("(" + data + ")");
        console.log(res);
        $("#spCPRCN").find("option").remove();
        $("#spCPRCN").append($("<option value=''>-请选择-</option>"));
        for (var i = 0; i < res.data.length; i++) {
            var id = res.data[i].DictCode;
            var name = res.data[i].DictName;
            var opt = "<option value='" + id + "'>" + name + "</option>";
            $("#spCPRCN").append(opt);
        }
    });
}

//获取承兑人承诺下拉框加载
function loadCdrCn() {
    $.get(httpUrl + "Spxx/GetDropDownList", { key: "CN" }, function (data) {
        var res = eval("(" + data + ")");
        console.log(res);
        $("#spCDRCN").find("option").remove();
        $("#spCDRCN").append($("<option value=''>-请选择-</option>"));
        for (var i = 0; i < res.data.length; i++) {
            var id = res.data[i].DictCode;
            var name = res.data[i].DictName;
            var opt = "<option value='" + id + "'>" + name + "</option>";
            $("#spCDRCN").append(opt);
        }
    });
}

//获取票据状态下拉框加载
function loadPjzt() {
    $.get(httpUrl + "Spxx/GetDropDownList", { key: "PJ" }, function (data) {
        var res = eval("(" + data + ")");
        console.log(res);
        $("#spPJZT").find("option").remove();
        $("#spPJZT").append($("<option value=''>-请选择-</option>"));
        for (var i = 0; i < res.data.length; i++) {
            var id = res.data[i].DictCode;
            var name = res.data[i].DictName;
            var opt = "<option value='" + id + "'>" + name + "</option>";
            $("#spPJZT").append(opt);
        }
    });
}

//获取付款方式下拉框加载
function loadFkfs() {
    $.get(httpUrl + "Spxx/GetDropDownList", { key: "FK" }, function (data) {
        var res = eval("(" + data + ")");
        console.log(res);
        $("#spFKFS").find("option").remove();
        $("#spFKFS").append($("<option value=''>-请选择-</option>"));
        for (var i = 0; i < res.data.length; i++) {
            var id = res.data[i].DictCode;
            var name = res.data[i].DictName;
            var opt = "<option value='" + id + "'>" + name + "</option>";
            $("#spFKFS").append(opt);
        }
    });
}