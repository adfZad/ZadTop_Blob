<%@ Page Title="" Language="C#" MasterPageFile="~/viewer/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Type.aspx.cs" Inherits="admin_Document_Type" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $(function () {
                $('[id*=gvType]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                    "responsive": true,
                    "sPaginationType": "full_numbers"
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
                    Type</h5>
            </div>
    <div id="dvInput" runat="server" class="card small mb-5" style="margin: 0 auto;width:20rem">
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 
                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="valtxtCode" runat="server" ControlToValidate="txtType" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtType" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtType">
                                Name/الشفرة<span style="color: red">*</span></label>
                          
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
                        <div class="col-md-12" style="text-align:center;">
                          <div class="switch round blue-white-switch">
                                    <label>
                                        Active/نشيط
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
                        <div class="col-md-12">
                            <asp:Label ID="lblMessage" Style="color: #EC2310;" runat="server" Text=""></asp:Label>
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
   
                        <table id="gvType" class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptType" runat="server" OnItemDataBound="rptType_ItemDataBound"
                                OnItemCommand="rptType_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>
                                                
                                            </th>
                                            <th>
                                                Name<br/>الشفرة
                                            </th>
                                            <th>
                                             Description<br/>وصف
                                            </th>
                                            <th>
                                             AltDescription<br/>وصف
                                            </th>
                                            <th>
                                             UpdatedBy<br/>تم التحديث بواسطة
                                            </th>
                                            <th>
                                                Active<br/>نشيط
                                            </th>
                                           
                                            
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false">
                                                <i class="fa fa-pencil-square-o fa-sm"></i>
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("Name")%>
                                        </td>
                                        <td>
                                            <%# Eval("Description")%>
                                        </td>
                                        <td>
                                            <%# Eval("AltDescription")%>
                                        </td>
                                        <td>
                                            <%# Eval("UpdatedName")%>
                                        </td>
                                        <td>
                                            <%# Eval("Active")%>
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

