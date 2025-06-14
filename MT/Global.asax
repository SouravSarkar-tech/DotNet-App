﻿<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        log4net.Config.XmlConfigurator.Configure();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        //Accenture.MWT.ExceptionUtil.ExceptionHandling exceptionUtil = new Accenture.MWT.ExceptionUtil.ExceptionHandling();

        //Exception objErr = Server.GetLastError().GetBaseException();
        //exceptionUtil.LogException(Request.Url.ToString(), objErr.Message.ToString(), objErr.StackTrace.ToString());

        //Server.ClearError();
        ////Response.Redirect("../../webpages/error.aspx");
        //Response.Redirect("../login.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
