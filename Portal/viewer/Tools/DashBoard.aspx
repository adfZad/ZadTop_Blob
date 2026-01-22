<%@ Page Title="" Language="C#" MasterPageFile="~/viewer/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="admin_Tools_DashBoard" %>
 
<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

       <script type="text/javascript">
        $(document).ready(function () {
            $('#gvTask').DataTable({
             "paging": false
           
            });
            $('#gvReturn').DataTable({
            
            });
//            $(function () {
//                $('[id*=gvTask]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
//                    "responsive": true,
//                    "sPaginationType": "full_numbers"
//                });
//            });
//            $(function () {
//                $('[id*=gvReturn]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
//                    "responsive": true,
//                    "sPaginationType": "full_numbers"
//                });
//            });

        });

        function loadDetails(id) {
            $.colorbox({ iframe: true, width: "70%", height: "100%", href: "../../Profile/Document.aspx?id=" + id });
        }

        function editDetails(id) {
            $.colorbox({ iframe: true, width: "100%", height: "100%", href: "../../Task/Edit.aspx?id=" + id });
        }
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnSortColumn" runat="server" />
<div class="row text-center" id="divTask" runat="server">
<div class="col-md-12 ">
  <div class="card badge">
  <div class= "card-header blue-gradient">
                                            
                                               <h5>Pending Tasks</h5> 
                                                
                                            </div>
  <div class="card-body badge">
  
     <table id="gvTask"class="table table-striped table-bordered dt-responsive nowrap text-left">
                            <asp:Repeater ID="rpttask" runat="server" >
                                <HeaderTemplate>
                                    <thead>
                                        <tr class="red-text">
                                            <th>
                                                Division
                                            </th>
                                            <th>
                                                Type
                                            </th>
                                            <th>
                                                Document No
                                            </th>
                                            <th>
                                                purpose
                                            </th>
                                            <th>
                                                Amount
                                            </th>
                                             <th>
                                                Remarks
                                            </th>
                                            <th>
                                                Allocated Time
                                            </th>
                                           
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="black-text">
                                    <td >
                                    <%# Eval("DivisionName")%>
                                    </td>
                                     <td>
                                    <%# Eval("DocumentTypeName")%>
                                    </td>
                                        <td>
                                        <asp:HyperLink ID="hypEmployeeName" runat="server" style="color:Blue;text-decoration:underline" Font-Bold="true"  NavigateUrl='<%# string.Format("../Task/Edit.aspx?id={0}",Eval("Id")) %>'>
                                         <%# Eval("DocumentName")%></asp:HyperLink> 
                                            
                                        </td>
                                         <td>
                                    <%# Eval("DocumentPurpose")%>
                                    </td>
                                     <td>
                                    <%# Eval("DocumentAmount")%>
                                    </td>
                                     <td>
                                    <%# Eval("DocumentDescription")%>
                                    </td>
                                       <td>
                                        <%# (DateTime.Parse(Eval("CreatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : Eval("CreatedTime")%>
                                       </td>
                                     </tr>
                                </ItemTemplate>
                               
                            </asp:Repeater>
                        </table>   
  
  </div>
  </div>
  </div>
  </div> 
<div class="row">

</div>                
 <div class="row" id="divReturn" runat="server">
 <div class="col-md-12">
  <div  class="card">
  <div class= "card-header brown">
                                            
                                                <h5 class="white-text">Return Tasks</h5>
                                                
                                            </div>
  <div class="card-body">
 
     <table id="gvReturn"class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptreturn" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr class="blue-text">
                                            <th>
                                                Document No
                                            </th>
                                            <th>
                                                Allocated Time
                                            </th>
                                           
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="text-left">
                                        <td>
                                        <asp:HyperLink ID="hypEmployeeName" runat="server" style="color:Blue;text-decoration:underline" Font-Bold="true"  NavigateUrl='<%# string.Format("../Document/Edit.aspx?id={0}",Eval("DocumentId")) %>'>
                                         <%# Eval("DocumentName")%></asp:HyperLink> 
                                           
                                        </td>
                                       <td>
                                        <%# (DateTime.Parse(Eval("CreatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : Eval("CreatedTime")%>
                                       </td>
                                     </tr>
                                </ItemTemplate>
                               
                            </asp:Repeater>
                        </table>   
   </div>
  </div>
  </div>
  </div>
</asp:Content>
