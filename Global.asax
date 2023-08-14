<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 在應用程式啟動時執行的程式碼
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在應用程式關閉時執行的程式碼

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在發生未處理的錯誤時執行的程式碼

        Exception lastError = Server.GetLastError();
        if (lastError != null)
        {
            //异常信息
            string strExceptionMessage = string.Empty;

            //对HTTP 404做额外处理，其他错误全部当成500服务器错误
            HttpException httpError = lastError as HttpException;
            if (httpError != null)
            {
                //获取错误代码
                int httpCode = httpError.GetHttpCode();
                strExceptionMessage = httpError.Message;
                if (httpCode == 400 || httpCode == 404)
                {
                    Response.StatusCode = 404;
                    //跳转到指定的静态404信息页面，根据需求自己更改URL
                    Response.WriteFile("~/errPage/404.htm");
                    Server.ClearError();
                    return;
                }
            }

            strExceptionMessage = lastError.Message;

            /*-----------------------------------------------------
             * 此处代码可根据需求进行日志记录，或者处理其他业务流程
             * ---------------------------------------------------*/

            /*
             * 跳转到指定的http 500错误信息页面
             * 跳转到静态页面一定要用Response.WriteFile方法                 
             */
            Response.StatusCode = 500;
            Response.WriteFile("~/errPage/500.htm");

            //一定要调用Server.ClearError()否则会触发错误详情页（就是黄页）
            Server.ClearError();       
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新的工作階段啟動時執行的程式碼

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在工作階段結束時執行的程式碼
        // 注意: 只有在  Web.config 檔案中將 sessionstate 模式設定為 InProc 時，
        // 才會引起 Session_End 事件。如果將 session 模式設定為 StateServer 
        // 或 SQLServer，則不會引起該事件。

    }
       
</script>
