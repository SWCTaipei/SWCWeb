! function(a) {
    "function" == typeof define && define.amd ? define(["jquery", "moment"], a) : "object" == typeof exports ? module.exports = a(require("jquery"), require("moment")) : a(jQuery, moment)
}(function(a, b) {
    ! function() {
        ! function() {
            "use strict";
            var a = (b.defineLocale || b.lang).call(b, "zh-tw", {
                months: "1 月_2 月_3 月_4 月_5 月_6 月_7 月_8 月_9 月_10 月_11 月_12 月".split("_"),
                monthsShort: "1 月_2 月_3 月_4 月_5 月_6 月_7 月_8 月_9 月_10 月_11 月_12 月".split("_"),
                weekdays: "星期日_星期一_星期二_星期三_星期四_星期五_星期六".split("_"),
                weekdaysShort: "週日_週一_週二_週三_週四_週五_週六".split("_"),
                weekdaysMin: "日_一_二_三_四_五_六".split("_"),
                longDateFormat: {
                    LT: "Ah:mm",
                    LTS: "Ah:m:s",
                    L: "YYYY 年 MMM D 日 ",
                    LL: "YYYY 年 MMM D 日 ",
                    LLL: "YYYY 年 MMM D 日 Ah:mm ",
                    LLLL: "YYYY 年 MMM D 日 dddd Ah:mm ",
                    l: "YYYY年 MMM D 日 ",
                    ll: "YYYY 年 MMM D 日 ",
                    lll: "YYYY 年 MMM D 日 Ah:mm ",
                    llll: "YYYY 年 MMM D 日 dddd Ah:mm "
                },
                meridiemParse: "",
                meridiemHour: function(a, b) {
                    return ""
                },
                meridiem: function(a, b, c) {
                    return ""
                },
                calendar: {
                    sameDay: "[今天]LT",
                    nextDay: "[明天]LT",
                    nextWeek: "[下]ddddLT",
                    lastDay: "[昨天]LT",
                    lastWeek: "[上]ddddLT",
                    sameElse: "L"
                },
                ordinalParse: /\d{1,2}(日|月|週)/,
                ordinal: function(a, b) {
                    switch (b) {
                        case "d":
                        case "D":
                        case "DDD":
                            return a + "日";
                        case "M":
                            return a + "月";
                        case "w":
                        case "W":
                            return a + "週";
                        default:
                            return a
                    }
                },
                relativeTime: {
                    future: "%s內",
                    past: "%s前",
                    s: "幾秒",
                    m: "一分鐘",
                    mm: "%d分鐘",
                    h: "一小時",
                    hh: "%d小時",
                    d: "一天",
                    dd: "%d天",
                    M: "一個月",
                    MM: "%d個月",
                    y: "一年",
                    yy: "%d年"
                }
            });
            return a
        }(), a.fullCalendar.datepickerLang("zh-tw", "zh-TW", {
            closeText: "關閉",
            prevText: "&#x3C;上月",
            nextText: "下月&#x3E;",
            currentText: "今天",
            monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            dayNames: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
            dayNamesShort: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
            weekHeader: "周",
            dateFormat: "yy/mm/dd",
            firstDay: 1,
            isRTL: !1,
            showMonthAfterYear: !0,
            yearSuffix: " 年"
        }), a.fullCalendar.lang("zh-tw", {
            buttonText: {
                month: "月",
                week: "週",
                day: "天",
                list: "待辦事項"
            },
            allDayText: "全天",
            eventLimitText: "更多",
			allDaySlot:false
        })
    }(), (b.locale || b.lang).call(b, "en"), a.fullCalendar.lang("en"), a.datepicker && a.datepicker.setDefaults(a.datepicker.regional[""])
});
















