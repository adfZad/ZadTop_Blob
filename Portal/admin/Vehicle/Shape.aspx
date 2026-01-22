<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Shape.aspx.cs" Inherits="admin_Vehicle_Shape" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
        
            $('#ddlType').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#sddlType').selectize({
                sortField: 'text',
                selectOnTab: true
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
                    Shape/صناعة</h5>
            </div>
    <div id="dvInput" runat="server" class="card small mb-5" style="margin: 0 auto;width:20rem">
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 

                         <div class="col-md-12">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlType">
                                Type<span style="color: red">*</span></label>
                              <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlType"
                                ErrorMessage="*" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="valtxtCode" runat="server" ControlToValidate="txtShape" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtShape" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtShape">
                                Code/الشفرة<span style="color: red">*</span></label>
                          
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
    <div class="card">
                <a data-toggle="collapse" data-parent="accChoosePending" href="#collapsesearch">
                <label style="margin-left:98%">
                            </label><i class="fa fa-angle-down rotate-icon"></i>
                 
                </a>
 
      <div id="collapsesearch" class="collapse show">
       <div class="card-body" >
         
               
                    <div class="row">
                    <div class="col-md-2">
                            <div class="md-form">
                                <asp:DropDownList ID="sddlType" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlType">
                                  Type <span style="color: red">*</span></label>
                              
                            </div>
                        </div>
                          <div class="col-md-2">
                            <div class="md-form">
                            <asp:TextBox ID="txtName" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtName">Code</label>
                            </div>
                    </div>
                          <div class="col-md-2">
                            <div class="md-form">
                            <asp:TextBox ID="txtSDescription" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtSDescription">Description(EN)</label>
                            </div>
                    </div>
                          <div class="col-md-2">
                            <div class="md-form">
                            <asp:TextBox ID="txtSAltDescription" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtSAltDescription">Description(AR)</label>
                            </div>
                    </div>
                          <div class="col-md-2">
                            <div class="md-form">
                           <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                                Text="" CausesValidation="false" CssClass="btn btn-blue px-3 waves-effect waves-light">
                                                <i class="fa fa-search"></i>
                                            </asp:LinkButton>
                          <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                Text="" CausesValidation="false" CssClass="btn btn-green px-3 waves-effect waves-light">
                                                <i class="fa fa-plus "></i>
                                            </asp:LinkButton>
                            </div>
                        </div>
                    </div>
     </div>
     </div>
 
<%--    </div>
    <div class="card card-cascade narrower">--%>

                   <div class="table-responsive">
                        <table class="table table-striped ">
                            <asp:Repeater ID="rptShape" runat="server" OnItemDataBound="rptShape_ItemDataBound"
                                OnItemCommand="rptShape_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>
                                                #
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
                                             Type code<br/>وصف
                                            </th>
                                            <th>
                                             UpdatedBy<br/>تم التحديث بواسطة
                                            </th>
                                            <th>
                                                Active<br/>نشيط
                                            </th>
                                            <th>
                                                Edit<br/>تعديل
                                            </th>
                                            
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRowNo" runat="server" />
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
                                            <%# Eval("TypeName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UpdatedName")%>
                                        </td>
                                         <td>
                                            <%# Eval("Active")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false" CssClass="btn btn-red btn-tn px-2 waves-effect waves-light">
                                                 <i class="fa fa-pencil-square-o fa-lg"></i>
                                            </asp:LinkButton>
                                        </td> 
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <zhp:ZadPager ID="pager" runat="server" OnCommand="pager_Command" GenerateGoToSection="true"
                                GenerateHiddenHyperlinks="true" />
                        </div>
                    </div>


         
            </div>
  </div>
</asp:Content>
