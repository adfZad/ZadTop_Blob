<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Template.aspx.cs" Inherits="admin_Flow_Template" EnableEventValidation = "false" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
  <script src="https://cdn.datatables.net/1.10.24/js/dataTables.bootstrap4.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.7/js/dataTables.responsive.min.js"type="text/javascript"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.7/js/responsive.bootstrap4.min.js"type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.bootstrap4.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.print.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.colVis.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.2/css/bootstrap.css"type="text/javascript"></script>
    <script src="https://cdn.datatables.net/1.10.24/css/dataTables.bootstrap4.min.css" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.7/css/responsive.bootstrap4.min.css" type="text/javascript"></script>
      <script type="text/javascript" language="javascript">

        $(document).ready(function () {
         $('#ddlFlow').selectize({
                sortField: 'text',
                 onChange:function(){
                    if($("#ddlFlow").val()>0){
                    }
               
                    else {
                        $("#ddlFlow").val("0");
                    }
                 } 
            });

               $('#ddlUser').selectize({
                sortField: 'text',
                 onChange:function(){
                    if($("#ddlUser").val()>0){
                    }
               
                    else {
                        $("#ddlUser").val("0");
                    }
                 }
            });

            $('#ddlApprove').selectize({
                sortField: 'text',
                onChange: function () {
                    if ($("#ddlApprove").val() > 0) {
                    }

                    else {
                        $("#ddlApprove").val("0");
                    }
                }
            });

               $('#ddlLevel').selectize({
                sortField: 'text',
                 onChange:function(){
                    if($("#ddlLevel").val()>0){
                    }
               
                    else {
                        $("#ddlLevel").val("0");
                    }
                 } 
            });
            $(document).ready(function() {
            var table = $('#gvTemplate').DataTable( {
            lengthChange: true,         
            
            


            buttons: [
            {
                extend: 'excel',
                title: 'Flow Template'
 
            },    
                    
          ],
        
    } );
 
    table.buttons().container()
        .appendTo( '#gvTemplate_wrapper .col-md-6:eq(0)' );
         } );

          

            $(function () {
                $('[id*=#gvTemplate]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                    "responsive": true,
                    "sPaginationType": "full_numbers",

                    


                });
            });
        });

        function askConfirm() {
            if (confirm("Please confirm.")) {
                return true;
            }
            else {
                return false;
            }
        }
         
    


         

   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="card card-cascade narrower mb-5">
    <div class="view py-3 gradient-card-header info-color-dark" style="width: 30%;">
                <h5 class="mb-0">
                    Flow Template</h5>
            </div>
    <div id="dvInput" runat="server" class="card small mb-5" style="margin: 0 auto;width:20rem">
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 
                        
                        <div class="col-md-12">
                            <asp:Label ID="lblMessage" Style="color: #EC2310;" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:DropDownList ID="ddlFlow" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlFlow">
                                    Flow
                                </label>
                               
                          
                            </div>
                        </div>

                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:DropDownList ID="ddlUser" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlUser">
                                    UserName
                                </label>
                              
                          
                            </div>
                        </div>

                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:DropDownList ID="ddlLevel" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlLevel">
                                    Level
                                </label>
                               
                          
                            </div>
                        </div>

                        <div class="col-md-12">
                         <div class="md-form">
                          <asp:RequiredFieldValidator ID="valtxtEnDescription" runat="server" ControlToValidate="txtDesc" 
                                ErrorMessage="English Description" ForeColor="red">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtDesc" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtDesc">
                                 Description/وصف<span style="color: red">*</span></label>
                                
                        </div>
                        </div>

                        <div class="col-md-12">
                         <div class="md-form">
                           <asp:RequiredFieldValidator ID="valArDescription" runat="server" ControlToValidate="txtAltDesc" 
                                ErrorMessage="Arabic Description" ForeColor="red">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtAltDesc" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtAltDesc">
                                 وصف/Description<span style="color: red">*</span></label>
                                
                        </div>
                        </div>

                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:DropDownList ID="ddlApprove" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                                <label for="ddlApprove">
                                    Approval Type
                                </label>
                               
                          
                            </div>
                        </div>
                        <div class="col-md-12" style="text-align:center;">
                          <div class="switch round blue-white-switch">
                                    <label>
                                        Signature
                                        <input id="chkActive" name="chkActive" type="checkbox" checked="checked" runat="server"
                                            clientidmode="Static" />
                                        <span class="lever"></span>
                                    </label>
                              </div> 
                        </div>
                        <div class="col-md-12" style="text-align:center;">
                            <asp:Button ID="btnSave" runat="server" Text="Save/حفظ" OnClick="btnSave_Click" CssClass="btn btn-blue px-3 waves-effect waves-light "/>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update/تحديث" OnClick="btnUpdate_Click" CssClass="btn btn-blue px-3 waves-effect waves-light"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel/إلغاء" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-red px-3 waves-effect waves-light"/>
                            
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblDelMessage" Style="color: #EC2310;" runat="server"></asp:Label>
                        </div>
                        

                   </div>
    <div class="card-body">
     <div class="row">
    <div class="col-md-12 text-right">
       
                           <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                Text="" CausesValidation="false" CssClass="btn btn-green px-3 waves-effect waves-light">
                                                <i class="fa fa-plus "></i>
                                            </asp:LinkButton>
    </div>        
    </div>
     <div class="row">
     <div class="col-md-12">
   
                        <table id="gvTemplate" class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptTemplate" runat="server" OnItemDataBound="rptTemplate_ItemDataBound"
                                OnItemCommand="rptTemplate_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>
                                            
                                            </th>
                                            <th>
                                                
                                            </th>
                                            <th>
                                                Division
                                            </th>
                                             <th>
                                                Flow Name<br/>الشفرة
                                            </th>
                                             <th>
                                                User Name<br/>الشفرة
                                            </th>
                                             <th>
                                                Level Name<br/>الشفرة
                                            </th>
                                             <th>
                                                Approve Type<br/>الشفرة
                                            </th>
                                            <th>
                                             UpdatedBy<br/>تم التحديث بواسطة
                                            </th>
                                           
                                           
                                            
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                         <td>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false">
                                                <i class="fa fa-remove"></i>
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false">
                                                <i class="fa fa-pencil-square-o fa-sm"></i>
                                            </asp:LinkButton>
                                        </td>
                                         <td>
                                            <%# Eval("DivisionName")%>
                                        </td>
                                         <td>
                                            <%# Eval("FlowName")%>
                                        </td>
                                         <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                         <td>
                                            <%# Eval("LevelName")%>
                                        </td>
                                          <td>
                                            <%# Eval("ApproveName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UpdatedName")%>
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

