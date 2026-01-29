<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="ZadHolding.PortalBase" %>
<%@ Import Namespace="Microsoft.Practices.EnterpriseLibrary.Data" %>
<%@ Import Namespace="Microsoft.Practices.EnterpriseLibrary.Common.Configuration" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
         // Force TLS 1.2 for Azure Storage
         System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

         // Set the provider factory for Enterprise Library
        DatabaseProviderFactory factory = new DatabaseProviderFactory();
        DatabaseFactory.SetDatabaseProviderFactory(factory);

        // Other startup logic (e.g., route registration, logging setup)

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

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

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        PortalUser.Current.Authorize();
    }
       
</script>
