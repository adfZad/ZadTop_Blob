<%@ Page Title="" Language="C#" MasterPageFile="~/ceo/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="ceo_Tools_DashBoard" %>
 
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
    <asp:HiddenField ID="hdnId1" runat="server" />
<div class="row" id="divTask" runat="server">
<div class="col-md-12 ">
<div class= "card-header">
                                            
                                               <h5>Pending Tasks</h5> 
                                                
                                            </div>
<div class="card-body">
  
     <table id="gvTask"class="table table-striped table-bordered ">
                            <asp:Repeater ID="rpttask" runat="server" >
                                <HeaderTemplate>
                                    <thead>
                                        <tr class="red-text">
                                            <th>
                                            Division
                                            </th>                                            
                                             <th>
                                            Reference No
                                            </th>
                                            <th>
                                            Type
                                            </th>
											 <th>
                                            Party Name
                                            </th>
                                            <th>
                                            Payment / Document For
                                            </th>											
                                             <th>
                                            Currency
                                            </th>
											<th>
                                            Amount
                                            </th>
                                            <th>
                                            Exchange Rate
                                            </th>
                                            <th>
                                            Amount In QAR
                                            </th>                             
                                            <th>
                                            Allocated Time
                                            </th>
                                           
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="black-text">
                                     <td>
                                
                                    <div class="switch round blue-white-switch">
                                    <asp:HiddenField ID="hdnDocumentId" runat="server" Value= '<%# Eval("DocumentId")%>'/>
                                    <asp:HiddenField ID="hdnIds" runat="server" Value= '<%# Eval("Id")%>'/>
                                    
                                    
                                      <asp:CheckBox ID="chkActive" Text = '<%# Eval("DivisionName")%>' runat="server"></asp:CheckBox></div> 
                                   <%-- <label>
                                     
                                    <input id="chkActive" name="chkActive" type="checkbox" checked="checked" runat="server"
                                            clientidmode="Static" />
                                     <span class="lever"></span>
                                    </label>--%>
                              </div> 
                                        </td>                                   
                                     <td>
                                    <asp:HyperLink ID="hypEmployeeName" runat="server" style="color:Blue;text-decoration:underline" Font-Bold="true"  NavigateUrl='<%# string.Format("../Task/Edit.aspx?id={0}",Eval("Id")) %>'>
                                    <%# Eval("DocumentName")%></asp:HyperLink> 
                                    </td>
                                     <td>
                                    <%# Eval("DocumentTypeName")%>
                                    </td>
									 <td>
                                    <%# Eval("DocumentPurpose")%>
                                    </td>
                                    <td>
                                    <%# Eval("DocumentDescription")%>
                                    </td>									
                                    <td>
                                    <%# Eval("Currency")%>
                                    </td>
									<td align="right">
                                    <%# string.Format("{0:0,0.00;(0,0.00);0}", Eval("OtherAmount"))%>
                                    </td>
                                    <td align="right">
									 <%# Eval("Rate")%>                                                                      
                                    </td>                                  
                                    <td align="right">
                                    <%# string.Format("{0:0,0.00;(0,0.00);0}", Eval("DocumentAmount"))%>
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
    <div class="row" id ="divRemarks" runat="server">      
  <div class="col-md-12">  
  <span style="font-weight:bold; padding-left:20px;">Remarks(if any)</span>               
  <asp:TextBox ID="txtRemarks" runat="server" ClientIDMode="Static"></asp:TextBox>
  </div>
  </div>
     <div class="row">
     <div class="col-md-4">
     <div class="md-form">
     </div>
     </div>
     <div class="col-md-4">
     <div class="md-form">
     <asp:LinkButton  ID="btnSave" runat="server" ToolTip="Click To Approve the Document"  OnClick="btnSave_Click" class="btn btn-rounded btn-dark-green">
                                                 <i class="fa fa-thumbs-up"></i></asp:LinkButton>
     <asp:LinkButton ID="btnReturn" runat="server" ToolTip="Click To Return the Document"  OnClick="btnReturn_Click" class="btn btn-rounded btn-yellow">
                                                 <i class="fa fa-undo"></i></asp:LinkButton>
     
          
     </div>
     </div> 
     <div class="col-md-4">
     <div class="md-form">
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

    
    
       
    <div class="row">
                        <div class="col-md-12">
                            <zhp:ZadPager ID="pager" runat="server" OnCommand="pager_Command" GenerateGoToSection="true"
                                GenerateHiddenHyperlinks="true" Visible="false" />
                        </div>
                    </div>   
</asp:Content>
