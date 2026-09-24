<%@ Application Language="C#" %>
<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        // Application start code. Nothing required for this project.
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Log unexpected errors to a file (App_Data/errors.log).
        try
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                string path = Server.MapPath("~/App_Data/errors.log");
                System.IO.File.AppendAllText(path,
                    string.Format("[{0}] {1}\r\n{2}\r\n",
                        System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ex.Message, ex.StackTrace));
            }
        }
        catch { /* never let logging crash the app */ }
    }
</script>