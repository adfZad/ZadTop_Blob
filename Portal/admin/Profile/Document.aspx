<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Document.aspx.cs"
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
     <nav>
  <div class="nav nav-tabs brown" id="nav-tab" role="tablist">
  <a class="nav-item nav-link active" id="nav-data-tab" data-toggle="tab" href="#nav-data" role="tab" aria-controls="nav-data" aria-selected="true"><i class="fa fa-magic fa-1x pb-1"
          aria-hidden="false"></i>Data</a>
   <a class="nav-item nav-link " id="nav-flow-tab" data-toggle="tab" href="#nav-flow" role="tab" aria-controls="nav-flow" aria-selected="true"><i class="fa fa-sort-amount-desc fa-1x pb-1"
          aria-hidden="true"></i>Flow</a>
    
   <a class="nav-item nav-link" id="nav-attach-tab" data-toggle="tab" href="#nav-attach" role="tab" aria-controls="nav-attach" aria-selected="false"><i class="fa fa-paperclip fa-1x pb-1"
          aria-hidden="true"></i>Attachments</a>
  </div>
</nav>
<div class="tab-content pt-5" id="nav-tabContent">
<div class="tab-pane fade show active" id="nav-data" role="tabpanel" aria-labelledby="nav-data">        
            <div class="row">
            <table class="table table-striped table-bordered dt-responsive nowrap">
                   <tr>
                   <td>
                    
                   Document No
                   </td>
                   <td >
                   
                          <a href="<%= MainDocPath %>" target="_blank"><%= documentInfo.Name%></a>
                   </td>
                   </tr> 
                    <tr>
                   <td>
                   Status
                   </td>
                   <td>
                    
                    <%= documentInfo.DocumentStatusName%>
                   </td>
                   </tr>  
                   <tr>
                   <td>
                   Type
                   </td>
                   <td>
                    
                          <%= documentInfo.TypeName%>
                   </td>
                   </tr>  
                   </table>   

                    </div>
            
</div>
<div class="tab-pane fade show" id="nav-flow" role="tabpanel" aria-labelledby="nav-flow">
<div class="col-md-12">
        <table id="gvTask" class="table  table-responsive-md btn-table">
                            <asp:Repeater ID="rptTask" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                          
                                            <th>
                                              <i class="fa fa-sort-amount-asc mr-2 blue-text" aria-hidden="true">  Level<br/>
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
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr  onclick="loadDetails(<%# Eval("DocumentId")%>)">
                                        <td>
                                            <%# Eval("LevelName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%# Eval("TaskStatusName")%>
                                        </td>
                                        <td>
                                        <%# (DateTime.Parse(Eval("CreatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : Eval("CreatedTime")%>
                                       </td>
                                       <td>
                                        <%# (DateTime.Parse(Eval("UpdatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : Eval("UpdatedTime")%>
                                       </td>
                                     
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                   </div>
</div>
<div class="tab-pane fade show" id="nav-attach" role="tabpanel" aria-labelledby="nav-attach">
<div class="row">
<table class="table-bordered ">
                   <tr>
                   <td>
                   
                   Main Document 
                   </td>
                   <td>
                  
                    <a href="<%= MainDocPath %>" target="_blank"><%= documentInfo.Name%></a>
                   
                   </td>
                   </tr> 
                   <tr>
                   <td>
                   Attachment -1 
                   </td>
                   <td>
                     <a href="<%= OneDocPath %>" target="_blank"><%= documentInfo.Four%></a>
                   </td>
                   </tr>  
                     <tr>
                    <td>
                   Attachment -2 
                   </td>
                   <td>
                     <a href="<%= TwoDocPath %>" target="_blank"><%= documentInfo.Five%></a>
                   </td>
                   </tr>  
                     <tr>
                   <td>
                   Attachment -3 
                   </td>
                   <td>
                     <a href="<%= ThreeDocPath %>" target="_blank"><%= documentInfo.Six%></a>
                   </td>
                   </tr>  
                   </table>                       
                   </div>
</div>
        </div>
</div>
    </form>
</body>
</html>
