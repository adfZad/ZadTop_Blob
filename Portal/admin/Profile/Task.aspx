<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Task.aspx.cs"
    Inherits="admin_Profile_Document" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/material.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Font-Awesome/css/font-awesome.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Scripts/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="../../Scripts/modernizr-2.8.3.js" type="text/javascript"></script>
    <script src="../../Scripts/popper.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../../Scripts/material.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            background: none !important;
        }
    </style>
</head>
<body class="fixed-sn light-blue-skin">
    <form id="form1" runat="server">
    <div class="card">
        <div class="card-header">
        Task Detils
        </div>
        <div class="card-body">
            <div class="row">
                     <table class="table table-striped table-bordered dt-responsive nowrap">
                   <tr>
                   <td>
                   
                   Allocated Time
                   </td>
                   <td>
                     <%= (DateTime.Parse(taskInfo.CreatedTime.ToString()) == DateTime.MinValue) ? "<b>-</b>" : taskInfo.CreatedTime.ToString()%>
                   </td>
                   </tr> 
                   <tr>
                   <td>
                   Completed Time
                   </td>
                   <td>
                    <%= (DateTime.Parse(taskInfo.UpdatedTime.ToString()) == DateTime.MinValue) ? "<b>-</b>" : taskInfo.UpdatedTime.ToString()%>
                   
                   </td>
                   </tr>  
                     <tr>
                    <td>
                   User Name
                   </td>
                   <td>
                     <%= taskInfo.UserName%>
                   </td>
                   </tr>  
                     <tr>
                   <td>
                    Status 
                   </td>
                   <td>
                      <%= taskInfo.TaskStatusName%>
                   </td>
                   </tr>  
                    <tr>
                   <td>
                    Level 
                   </td>
                   <td>
                      <%= taskInfo.LevelName%>
                   </td>
                   </tr>  
                    <tr>
                   <td>
                    Remarks 
                   </td>
                   <td>
                      <%= taskInfo.AltDescription%>
                   </td>
                   </tr>  
                   </table>   
                    </div>
            
        </div>
    </div>
    </form>
</body>
</html>
