<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="admin_Tools_User" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    
    
    
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $('#ddlDivision').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlDepartment').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlRole').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlDesignation').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $(function () {
                $('[id*=gvUser]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="card card-cascade narrower mb-5">
    <div class="view py-3 gradient-card-header info-color-dark" style="width: 30%;">
                <h5 class="mb-0">
                    User/صناعة</h5>
            </div>
    <div id="dvInput" runat="server" class="card small mb-5" style="margin: 0 auto;width:40rem">
                        <asp:HiddenField ID="hdnId" runat="server" />

<div class="card-body">                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 
<div class="row">
                        <div class="col-md-6">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtFirstName">
                               First Name<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
                        <div class="col-md-6">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtLastName">
                               Last Name<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
</div>
<div class="row">
                        <div class="col-md-6">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlDivision" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlDivision">
                                Division<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlDivision"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlRole" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlRole">
                                Role<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlRole"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
</div>
<div class="row">
                        <div class="col-md-6">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="valtxtCode" runat="server" ControlToValidate="txtUserName" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtUserName" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtUserName">
                               User Name<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
                        <div class="col-md-6">
                        <div class="md-form">
                        <asp:RequiredFieldValidator ID="valtxtPassword" runat="server" ControlToValidate="txtPassword" 
                                ErrorMessage="Password" ForeColor="red">*</asp:RequiredFieldValidator> 
                                <asp:TextBox ID="txtPassword" runat="server" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
                                <label for="txtPassword">Password</label>
  
                        
                          </div>
                        </div>
</div>
<div class="row">
                        <div class="col-md-6">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlDesignation" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlDesignation">
                                Designation<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlDesignation"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDescription"  
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                            <label for="txtDescription" class="active">
                               Signature<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
</div>
<div class="row">
                       
                        <div class="col-md-6">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAltDescription" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtAltDescription" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtAltDescription">
                               Intital<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlDepartment" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlDepartment">
                                Department<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlDepartment"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
</div>
<div class="row">
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
</div>
                   </div>
    <div class="card">
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
        <table id="gvUser" class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptUser" runat="server" OnItemDataBound="rptUser_ItemDataBound"
                                OnItemCommand="rptUser_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>
                                                
                                            </th>
                                            <th>
                                                Name<br/>الشفرة
                                            </th>
                                            <th>
                                             Division<br/>وصف
                                            </th>
                                            <th>
                                             Role<br/>وصف
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
                                                 <i class="fa fa-pencil-square-o fa-lg"></i>
                                            </asp:LinkButton>
                                        </td> 
                                        <td>
                                            <%# Eval("Name")%>
                                        </td>
                                        <td>
                                            <%# Eval("DivisionName")%>
                                        </td>
                                        <td>
                                            <%# Eval("RoleName")%>
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
