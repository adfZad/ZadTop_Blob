<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Tools_Customer" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
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
    <div class="accordion" id="accChoosePending" role="tablist" aria-multiselectable="true">
        <!-- Accordion card -->
        <div class="card card-cascade narrower mb-5">
          <%--  <div class="view py-3 gradient-card-header info-color-dark" style="width: 20%;">
                <h5 class="mb-0">
                    Customer</h5>
            </div>--%>
            <!-- Card header -->
            <div class="card-header" role="tab" id="headingPending">
                <a data-toggle="collapse" data-parent="accChoosePending" title="Test details" href="#collapsePending"
                    aria-expanded="true" aria-controls="collapsePending">
                    <h5 class="mb-0">
                        <label id="lblPendingDetails" style="font-size: 14px; font-weight: bold;">
                            Customer Details</label><i class="fa fa-angle-down rotate-icon"></i>
                    </h5>
                </a>
            </div>
            <div id="collapsePending" class="collapse show" role="tabpanel" aria-labelledby="headingPending"
                data-parent="accChoosePending">
                <div class="card-body">
                    <asp:Label ID="lblDelMessage" Style="color: #EC2310;" runat="server"></asp:Label>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="false" CssClass="btn btn-blue px-3 waves-effect waves-light"
                        OnClick="btnAdd_Click" />
                    <div class="table-responsive">
                        <table class="table">
                            <asp:Repeater ID="rptCustomer" runat="server" OnItemDataBound="rptCustomer_ItemDataBound"
                                OnItemCommand="rptCustomer_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>
                                                #
                                            </th>
                                            <th>
                                                Customer
                                            </th>
                                            <th>
                                                Description
                                            </th>
                                            <th>
                                                Edit
                                            </th>
                                            <th>
                                                Delete
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
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false" CssClass="btn btn-green px-3 waves-effect waves-light">
                                                 <i class="fa fa-edit"></i>
                                               <%-- <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" alt="Edit" ToolTip="Edit"
                                                    Style="padding: 0px; border: none;" />--%>
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false" CssClass="btn btn-red px-3 waves-effect waves-light">
                                                 <i class="fa fa-remove"></i>
                                               <%-- <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.jpg" alt="Delete"
                                                    ToolTip="Delete" />--%>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
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
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false" CssClass="btn btn-green px-3 waves-effect waves-light">
                                                 <i class="fa fa-edit"></i>
                                               <%-- <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" alt="Edit" ToolTip="Edit" />--%>
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id")%>'
                                                Text="" CausesValidation="false" CssClass="btn btn-red px-3 waves-effect waves-light">
                                                 <i class="fa fa-remove"></i>
                                               <%-- <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.jpg" alt="Delete"
                                                    ToolTip="Delete" Style="padding: 0px; border: none;" />--%>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <zhp:ZadPager ID="pager" runat="server" OnCommand="pager_Command" GenerateGoToSection="true"
                                GenerateHiddenHyperlinks="true" />
                        </div>
                    </div>
                    <div id="dvInput" runat="server" class="row">
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtCustomer">
                                Customer<span style="color: red">*</span></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomer"
                                ErrorMessage="Customer" Text="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtDesc">
                                Description<span style="color: red">*</span></label>
                                </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-blue px-3 waves-effect waves-light"/>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-blue px-3 waves-effect waves-light"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-red px-3 waves-effect waves-light"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMessage" Style="color: #EC2310;" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Accordion card -->
    </div>
</asp:Content>
