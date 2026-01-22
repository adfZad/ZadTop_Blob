<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="AdminTools_Test" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
    <div class="content_page" style="width: 586px;">
         <asp:Label ID="lblDelMessage" style="color: #EC2310;" runat="server"></asp:Label>
        <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="false" CssClass="add_button" OnClick="btnAdd_Click" />
        <asp:Repeater ID="rptTest" runat="server" OnItemDataBound="rptTest_ItemDataBound"
            OnItemCommand="rptTest_ItemCommand">
            <HeaderTemplate>
                <div class="rptr_header" style="width: 586px;">
                    <div class="w40">
                        #</div>
                    <div class="w200">
                        Test</div>
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
                <div class="rptr_item_template">
                    <div class="w40">
                        <asp:Label ID="lblRowNo" runat="server" /></div>
                    <div class="w200">
                        <%# Eval("Name")%></div>
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
                <div class="rptr_alt_item_template">
                    <div class="w40">
                        <asp:Label ID="lblRowNo" runat="server" /></div>
                    <div class="w200">
                        <%# Eval("Name")%></div>
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
        <div class="rptr_footer" style="width: 584px;">
            <zhp:ZadPager ID="pager" runat="server" OnCommand="pager_Command" GenerateGoToSection="true"
                GenerateHiddenHyperlinks="true" />
        </div>
        <div class="cb">
        </div>
        <div id="dvInput" runat="server" class="input_content">
            <asp:HiddenField ID="hdnId" runat="server" />
            <div class="form_input_div">
                <label class="form_label">
                    Test</label>
                <asp:TextBox ID="txtTest" runat="server" CssClass="form_input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTest"
                    ErrorMessage="Test" Text="*"></asp:RequiredFieldValidator>
            </div>
            <div class="form_input_div">
                <label class="form_label">
                    Description</label>
                <asp:TextBox ID="txtDesc" runat="server" CssClass="form_input"></asp:TextBox></div>
            <div class="form_input_div_button">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
            </div>
            <div class="form_input_div_button">
                <asp:Label ID="lblMessage" style="color: #EC2310;" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>


