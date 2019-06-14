/**
 * Created by caojia on 20190522.
 */
var httpUrl = "http://localhost:59998/api/";

$(function () {
    var settings = {
        url: httpUrl + "Spxx/QuerySpxxListByPage",
        queryParams: function (params) {
            return {
                //params.limit页面大小，params.offset页码
                rows: params.limit,
                page: params.offset / params.limit + 1,
            };
        },
        /*json data start*/
        responseHandler: function (res) {
            var d = eval("(" + res + ")");
            console.log(d.data);
            return {
                total: d.data.total,
                rows: d.data.rows
            };
        },
        pageSize: 10,
        pageList: [2, 5, 10, 20, 50],
        /*json data end*/
        columns: [{
            checkbox: true
        }, {
            field: 'SpId',
            visible: false
        }, {
            field: 'SpName',
            title: '商品名称'
        }, {
            field: 'SpPJJE',
            title: '票据金额'
        }, {
            field: 'SpCPRQC',
            title: '出票人'
        },
        {
            field: 'SpQPJ',
            title: '起拍价'
        },
        {
            field: 'SpCPRQ',
            title: '出票日期'
        }, {
            field: 'SpHPDQR',
            title: '汇票到期日'
        }, {
            field: 'SpCDRQC',
            title: '承兑人'
        }
        ]
    }
    $MB.initTable('spxxTable', settings);
});