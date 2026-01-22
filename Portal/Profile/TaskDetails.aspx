<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="TaskDetails.aspx.cs"
    Inherits="admin_Profile_Document" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../Styles/pager.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/material.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Font-Awesome/css/font-awesome.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../Scripts/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="../Scripts/modernizr-2.8.3.js" type="text/javascript"></script>
    <script src="../Scripts/popper.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../Scripts/material.js" type="text/javascript"></script>
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
        Task Details (<%= DocumentInfo.DocumentFullName%>)
        </div>
        <div class="card-body">
            <div class="row">
                     <table id="gvTask" class="table  table-responsive-md btn-table">
                            <asp:Repeater ID="rptTask" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                          
                                            <th>
                                              <i class="fa fa-sort-amount-asc mr-2 blue-text" aria-hidden="true">Level<br/>
                                            </th>
                                            <th>
                                             <i class="fa fa-user mr-2 blue-text" aria-hidden="false"></i>User<br/>
                                            </th>
                                            <th>
                                             <i class="fa fa-edit mr-2 blue-text" aria-hidden="false"></i>Status<br/>
                                            </th>
                                                 
                                             <th>
                                             <i class="fa fa-clock-o mr-2 blue-text" aria-hidden="false"></i>Allocated Time<br/>
                                            </th> 
                                             <th>
                                             <i class="fa fa-clock-o mr-2 blue-text" aria-hidden="false"></i>Completed Time<br/>
                                            </th> 
                                            <th>
                                             <i class="fa fa-edit mr-2 blue-text" aria-hidden="false"></i>Comments<br/>
                                            </th> 
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr >
                                        <td>
                                            <%# Eval("LevelName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%# Eval("ApprovalStatusName")%>
                                        </td>
                                        <td nowrap="nowrap">
                                        <%# (DateTime.Parse(Eval("CreatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : Eval("CreatedTime")%>
                                       </td>
                                       <td nowrap="nowrap">
                                        <%# (DateTime.Parse(Eval("UpdatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : Eval("UpdatedTime")%>
                                       </td>
                                       <td>
                                            <%# Eval("AltDescription")%>
                                        </td>
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
            
        </div>
    </div>
    </form>
</body>
</html>
