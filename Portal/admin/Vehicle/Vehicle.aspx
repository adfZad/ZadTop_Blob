<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Vehicle.aspx.cs" Inherits="admin_Vehicle_Vehicle" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $('#ddlModel').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlShape').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlColor').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlCustomer').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#sddlMake').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $("#txtPurchaseDate").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy"
            }).on("hide", function (e) {

            });
            $(function () {
                $('[id*=gvCustomers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
                    Vehicle/صناعة</h5>
            </div>
    <div id="dvInput" runat="server" class="card small mb-5" style="margin: 0 auto;width:90%">
     <div class="card-header">
                 <label style="font-size: 14px; font-weight: bold;">
                            Enter Vehicle Details</label>
     </div>
     <div class="card-body">
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 
        <div class="row">
                        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="valtxtCode" runat="server" ControlToValidate="txtVehicle" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtVehicle" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtVehicle" style="color:red">
                                Plate No/الشفرة<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlModel" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlModel" style="color:red">
                                Model<span style="color: red">*</span></label>
                               <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlModel"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlShape" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlShape" style="color:red">
                                Shape<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlShape"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
       </div>
        <div class="row">
                        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtModelYear" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtModelYear" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtModelYear" style="color:red">
                                Model Year/الشفرة<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlColor" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlColor" style="color:red">
                                Color<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlColor"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlCustomer" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlCustomer" style="color:red">
                                Customer<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlCustomer"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
       </div>
        <div class="row">
        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtChassisNo" 
                                ErrorMessage="Chassis No" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtChassisNo" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtChassisNo" style="color:red">
                                Chassis No/الشفرة<span style="color: red">*</span></label>
                          
                            </div>
         </div>
        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEngineNo" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtEngineNo" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtEngineNo" style="color:red">
                                Engine No/الشفرة<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCylinders" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtCylinders" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtCylinders" style="color:blue">
                                Cylinders/الشفرة</label>
                          
                            </div>
                        </div>
       </div>
        <div class="row">
        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPassengers" 
                                ErrorMessage="Chassis No" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtPassengers" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtPassengers" style="color:red">
                                Passengers/الشفرة<span style="color: red">*</span></label>
                          
                            </div>
         </div>
        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPurchaseDate" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtPurchaseDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtPurchaseDate" style="color:blue">
                                Purchase Date/الشفرة</label>
                          
                            </div>
                        </div>
        <div class="col-md-4">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPurchaseCost" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtPurchaseCost" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtPurchaseCost" style="color:blue">
                                Purchase Cost/الشفرة</label>
                          
                            </div>
                        </div>
       </div>
        <div class="row">
                        <div class="col-md-4">
                         <div class="md-form">
                            <asp:TextBox ID="txtDesc" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtDesc" style="color:blue">
                                 Description/وصف</label>
                                
                        </div>
                        </div>
                        <div class="col-md-4">
                         <div class="md-form">
                          <asp:TextBox ID="txtAltDesc" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtAltDesc" style="color:blue">
                                 وصف/Description</label>
                                
                        </div>
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
                        
                        <div class="col-md-12" style="text-align:center;">
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
   
 
      <div id="collapsesearch" class="collapse show">
<%--       <div class="card-header">
                 <label style="font-size: 14px; font-weight: bold;">
                            Search Customer Details</label>
     </div>--%>
       <div class="card-body" >
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
                        <table  id="gvCustomers"class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptVehicle" runat="server" OnItemDataBound="rptVehicle_ItemDataBound"
                                OnItemCommand="rptVehicle_ItemCommand">
                                <HeaderTemplate>
                                    <thead class="thead-blue-gradient">
                                        <tr>
                                            <th>
                                               
                                            </th>
                                            <th>
                                                Code<br/>الشفرة
                                            </th>
                                            <th>
                                             Description<br/>وصف
                                            </th>
                                            <th>
                                             Description<br/>وصف
                                            </th>
                                            <th>
                                             Make/Model<br/>وصف
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
                                            <%# Eval("Description")%>
                                        </td>
                                        <td>
                                            <%# Eval("AltDescription")%>
                                        </td>
                                        <td>
                                            <%# Eval("MakeModelName")%>
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
