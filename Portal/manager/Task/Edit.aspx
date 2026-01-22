<%@ Page Title="" Language="C#" MasterPageFile="~/manager/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="manager_Task_Edit" %>


<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <link href="../../Styles/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#FF').popover('show')
        });

        function askConfirm() {
            if (confirm("Click OK to Confirm the Operation")) {
                return true;
            }
            else {
                return false;
            }
        }

        function loadDetails(id) {
            $.colorbox({ iframe: true, width: "100%", height: "400px", href: "../Profile/Task.aspx?id=" + id });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:HiddenField ID="curId" runat="server" />
        <div class="card card-cascade narrower mb-5" >
              
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
                   <asp:HiddenField ID="hdnId" runat="server" />
                 <asp:HiddenField ID="hdnDocumentId" runat="server" /> 
<div class="tab-pane fade show active" id="nav-data" role="tabpanel" aria-labelledby="nav-data">
   <div class="row"> 
        <div class="col-md-8">
        <iframe id="myframe" width="100%" height="100%" runat="server"></iframe>
        </div>
        <div class="col-md-4">

                   <table class="table table-bordered dt-responsive nowrap">
                   <tr>
                   <td><span style="font-weight:bold">
                   Reference No </span>
                   </td>
                   <td>
                    <a href="<%= ApproveDocPathString %>" target="_search" style="color:Blue;text-decoration:underline"><asp:Label ID="lblDocumentName" runat="server"></asp:Label></a>
                   </td>
                   </tr> 
                   <tr>
                   <td><span style="font-weight:bold">
                   Document Type </span>
                   </td>
                   <td >
                    <asp:Label ID="lblDocumentTypeName" runat="server" ></asp:Label> 
                   </td>
                   </tr>  
                    <tr>
                   <td ><span style="font-weight:bold">Party Name</span>
                   </td>
                    <td>
                    <asp:Label ID="lblDocumentPurpose" runat="server" ></asp:Label> 
                   </td>
                   </tr>  
                     <tr>
                   <td><span style="font-weight:bold">
                   Payment / Document For </span>
                   </td>
                    <td>
                    <asp:Label ID="lblDocumentRemarks" runat="server" ></asp:Label> 
                   </td>
                   </tr>  
                   <tr>
                   <td ><span style="font-weight:bold">
                  Remarks(if any)</span>               
                   </td>
                     <td>
                   <asp:TextBox ID="txtRemarks" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </td>
                   </tr>  
                   </table>   
                  <asp:LinkButton  ID="btnSave" runat="server" ToolTip="Click To Approve the Document"  OnClick="btnSave_Click" class="btn btn-rounded btn-dark-green">
                                                 <i class="fa fa-thumbs-up"></i></asp:LinkButton>
                              <asp:LinkButton ID="btnReturn" runat="server" ToolTip="Click To Return the Document" OnClick="btnReturn_Click" class="btn btn-rounded btn-yellow">
                                                 <i class="fa fa-undo"></i></asp:LinkButton>
                               <asp:LinkButton ID="btnCancel" runat="server" ToolTip="Click To Decline the Document" OnClick="btnCancel_Click" class="btn btn-rounded btn-red">
                                                 <i class="fa fa-remove"></i></asp:LinkButton>
    </div>
    </div>
 
</div> 
<div class="tab-pane fade show" id="nav-flow" role="tabpanel" aria-labelledby="nav-flow">

        <div class="col-md-12">
        <table id="gvTask" class="table table-striped table-bordered dt-responsive">
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
                                             <i class="fa fa-clock-o mr-2 blue-text" aria-hidden="false"></i>Completed Time<br/>
                                            </th> 
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr  onclick="loadDetails(<%# Eval("Id")%>)">
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
                                        <%# (DateTime.Parse(Eval("UpdatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : string.Format("{0:dd/MMM/yyyy}",Eval("UpdatedTime"))%>
                                       </td>
                                     
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                   </div>

</div>
<div class="tab-pane fade show" id="nav-attach" role="tabpanel" aria-labelledby="nav-attach">
<table class="table table-striped table-bordered dt-responsive nowrap">

                   <tr>
                
                   <td>
                     <a href="<%= OneDocPathString %>" target="_blank" style="color:Blue;text-decoration:underline"><%= OneDocNameString%></a>
                   </td>
                   </tr>  
                   <tr>
                 
                   <td>
                     <a href="<%= TwoDocPathString %>" target="_blank" style="color:Blue;text-decoration:underline"><%= TwoDocNameString%></a>
                   </td>
                   </tr>  
                   <tr>
                  
                   <td>
                     <a href="<%= ThreeDocPathString %>" target="_blank" style="color:Blue;text-decoration:underline"><%= ThreeDocNameString %></a>
                   </td>
                   </tr>  
                   <tr>
                  
                   <td>
                     <a href="<%= FourDocPathString %>" target="_blank" style="color:Blue;text-decoration:underline"><%= FourDocNameString %></a>
                   </td>
                   </tr>  
                   <tr>
                  
                   <td>
                     <a href="<%= FiveDocPathString %>" target="_blank" style="color:Blue;text-decoration:underline"><%= FiveDocNameString %></a>
                   </td>
                   </tr>  
                   <tr>
                  
                   <td>
                     <a href="<%= SixDocPathString %>" target="_blank" style="color:Blue;text-decoration:underline"><%= SixDocNameString %></a>
                   </td>
                   </tr>  
                   </table>   
</div>
</div>
</div>
</asp:Content>

