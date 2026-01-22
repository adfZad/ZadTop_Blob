<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Policy.aspx.cs" Inherits="admin_Policy_Policy" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $('#ddlCoverage').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlVehicle').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $("#txtIssueDate").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy"
            }).on("hide", function (e) {

            });
            $("#txtPaymentDate").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy"
            }).on("hide", function (e) {

            });
            $("#txtDateFrom").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy"
            }).on("hide", function (e) {

            });
            $("#txtDateTo").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy"
            }).on("hide", function (e) {

            });
            $(function () {
                $('[id*=gvPolicys]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
                    Policy/صناعة</h5>
            </div>
    <div id="dvInput" runat="server" class="card small" style="margin: 0 auto;width:90%">
     <div class="card-header">
                 <label style="font-size: 14px; font-weight: bold;">
                            Enter Policy Details</label>
     </div>
     <div class="card-body">
                         <asp:HiddenField ID="hdnId" runat="server" />
                          <asp:HiddenField ID="hdnAmentNo" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 
       <div class="row">  
                        
                        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtDateFrom" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtDateFrom" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtDateFrom" style="color:blue">
                                Date From/الشفرة</label>
                          
                            </div>
                        </div>
                        <div class="col-md-4">
                         <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDateTo" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtDateTo" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtDateTo" style="color:blue">
                                Date To/الشفرة</label>
                          
                            </div>

                        </div>

                        
        </div>               
       <div class="row">              
                        
                  
                        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIssueDate" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtIssueDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtIssueDate" style="color:blue">
                                Issue Date/الشفرة</label>
                          
                            </div>
                        </div>
                        <div class="col-md-4">
                         <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPaymentDate" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtPaymentDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtPaymentDate" style="color:blue">
                                Payment Date/الشفرة</label>
                          
                            </div>

                        </div>
                      
   
       </div>   
       <div class="row">              
                       
                        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInsuredValue" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtInsuredValue" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtInsuredValue" style="color:blue">
                                Insured Value/الشفرة</label>
                          
                            </div>
                        </div>
                         <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPremium" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtPremium" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtPremium" style="color:blue">
                                Premium Value/الشفرة</label>
                          
                            </div>
                        </div>
                          <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDeductibles" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtDeductibles" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtDeductibles" style="color:blue">
                                Deductibles/الشفرة</label>
                          
                            </div>
                        </div>
    </div>  
       <div class="row">              
                       
                            <div class="col-md-4">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlCoverage" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlCoverage" style="color:red">
                                Coverage Type<span style="color: red">*</span></label>
                             <%-- <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlCoverage"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlVehicle" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlVehicle" style="color:red">
                                Vehicle No<span style="color: red">*</span></label>
                             <%-- <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlVehicle"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>--%>
                            </div>
                        </div>

    </div>    
       <div class="row">              
                  <div class="col-md-4">
                          <div class="switch round blue-white-switch">
                                    <label>
                                        Registration/نشيط
                                        <input id="chkRegistration" name="chkActive" type="checkbox" checked="checked" runat="server"
                                            clientidmode="Static" />
                                        <span class="lever"></span>
                                    </label>
                              </div> 
                        </div>     
                  <div class="col-md-4">
                          <div class="switch round blue-white-switch">
                                    <label>
                                        Active/نشيط
                                        <input id="chkActive" name="chkActive" type="checkbox" checked="checked" runat="server"
                                            clientidmode="Static" />
                                        <span class="lever"></span>
                                    </label>
                              </div> 
                        </div>
         

    </div>             
      <div class="row">  
       <div class="col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="Save/حفظ" OnClick="btnSave_Click" CssClass="btn btn-blue px-3 waves-effect waves-light "/>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update/تحديث" OnClick="btnUpdate_Click" CssClass="btn btn-blue px-3 waves-effect waves-light"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel/إلغاء" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-red px-3 waves-effect waves-light"/>
                            
                        </div>
    </div>  
       <div class="col-md-12">
                            <asp:Label ID="lblDelMessage" Style="color: #EC2310;" runat="server"></asp:Label>
                        </div>
       <div class="col-md-12">
                            <asp:Label ID="lblMessage" Style="color: #EC2310;" runat="server" Text=""></asp:Label>
                        </div>
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
    <table id="gvPolicys"class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptPolicy" runat="server" OnItemDataBound="rptPolicy_ItemDataBound"
                                OnItemCommand="rptPolicy_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>
                                               
                                            </th>
                                            <th>
                                                Code<br/>الشفرة
                                            </th>
                                             <th>
                                                Name<br/>الشفرة
                                            </th>
                                            <th>
                                             Category<br/>وصف
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
                                                Text="" CausesValidation="false" >
                                                 <i class="fa fa-pencil-square-o fa-lg"></i>
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("Name")%>
                                        </td>
                                         <td>
                                            <%# Eval("Name")%>
                                        </td>
                                        
                                        <td>
                                            <%# Eval("MakeName")%>
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
