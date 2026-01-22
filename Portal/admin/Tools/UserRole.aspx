<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserRole.aspx.cs" Inherits="AdminTools_UserRole" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="content_page" style="width: 98%; overflow-x: scroll; overflow-y: hidden;
        border: solid 1px #A49161;">
        <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="false" CssClass="add_button"
            OnClick="btnAdd_Click" />
        <asp:Repeater ID="rptUserRole" runat="server" OnItemDataBound="rptUserRole_ItemDataBound"
            OnItemCommand="rptUserRole_ItemCommand">
            <HeaderTemplate>
                <div class="rptr_header" style="width: 988px;">
                    <div class="w40">
                        #</div>
                    <div class="w200">
                        Role Name</div>
                    <div class="w200">
                        Description</div>
                    <div class="w70">
                        Edit</div>
                    <div class="w70">
                        Delete</div>
                </div>
                <div class="cb">
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="rptr_item_template" style="width: 988px;">
                    <div class="w40">
                        <asp:Label ID="lblRowNo" runat="server" /></div>
                    <div class="w200">
                        <%# Eval("RoleName")%></div>
                    <div class="w200">
                        <%# Eval("Description")%></div>
                    <div class="w70">
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id")%>'
                            Text="" CausesValidation="false">
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" alt="Edit" ToolTip="Edit"
                                Style="padding: 0px; border: none;" />
                        </asp:LinkButton>
                    </div>
                    <div class="w70">
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id")%>'
                            Text="" CausesValidation="false">
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.jpg" alt="Delete"
                                ToolTip="Delete" Style="padding: 0px; border: none;" />
                        </asp:LinkButton></div>
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div class="rptr_alt_item_template" style="width: 988px;">
                    <div class="w40">
                        <asp:Label ID="lblRowNo" runat="server" /></div>
                    <div class="w200">
                        <%# Eval("RoleName")%></div>
                    <div class="w200">
                        <%# Eval("Description")%></div>
                    <div class="w70">
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id")%>'
                            Text="" CausesValidation="false">
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" alt="Edit" ToolTip="Edit"
                                Style="padding: 0px; border: none;" />
                        </asp:LinkButton>
                    </div>
                    <div class="w70">
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id")%>'
                            Text="" CausesValidation="false">
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.jpg" alt="Delete"
                                ToolTip="Delete" Style="padding: 0px; border: none;" />
                        </asp:LinkButton></div>
                </div>
            </AlternatingItemTemplate>
        </asp:Repeater>
        <div class="rptr_footer" style="width: 986px;">
            <zhp:ZadPager ID="pager" runat="server" OnCommand="pager_Command" GenerateGoToSection="true"
                GenerateHiddenHyperlinks="true" />
        </div>
        <div class="cb">
        </div>
    </div>
    <div class="content_page" style="width: 98%;">
        <div id="dvInput" runat="server" class="input_content" style="width: 100%;">
            <asp:HiddenField ID="hdnId" runat="server" />
            <div class="form_input_div">
                <label class="form_label">
                    RoleName</label>
                <asp:TextBox ID="txtRoleName" runat="server" CssClass="form_input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoleName"
                    ErrorMessage="Department" Text="*"></asp:RequiredFieldValidator>
            </div>
            <div class="form_input_div">
                <label class="form_label">
                    Description</label>
                <asp:TextBox ID="txtDesc" runat="server" CssClass="form_input"></asp:TextBox></div>
            <div class="form_input_div_button">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                    OnClick="btnCancel_Click" />
            </div>
            <div class="form_input_div_button">
                <asp:Label ID="lblMessage" Style="color: #EC2310;" runat="server" Text=""></asp:Label>
            </div>
        </div>
    
    </div>
</asp:Content>
