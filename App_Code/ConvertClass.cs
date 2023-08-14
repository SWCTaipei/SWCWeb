using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// ConvertClass 的摘要描述
/// </summary>
public class ConvertClass
{
    public ConvertClass()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public string ADDate(string iDate)
    {
        iDate = iDate.Replace(".","/");
        CultureInfo culture = new CultureInfo("zh-TW");
        culture.DateTimeFormat.Calendar = new TaiwanCalendar();
        return DateTime.Parse(iDate, culture).ToString("yyyy-MM-dd");
    }
}