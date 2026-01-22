<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="admin_Revise_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {

            $('#ddlType').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlFlow').selectize({
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
    <asp:HiddenField ID="curId" runat="server" />
<div class="card card-cascade narrower mb-5">
        <div class="view py-3 gradient-card-header info-color-dark" style="width: 20%;">
                <h5 class="mb-0">
                    Document Entry</h5>
            </div>

<div class="tab-content pt-5" id="nav-tabContent">
<div class="tab-pane fade show active" id="nav-flow" role="tabpanel" aria-labelledby="nav-flow">
<div class="row">
        <div class="col-md-4">
        <div class=" md-form">
                                <asp:DropDownList ID="ddlFlow" runat="server"  AutoPostBack="true"
                                    ClientIDMode="Static" OnSelectedIndexChanged="ddlFlow_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlFlow">
                                    Flow Template <span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="error" ControlToValidate="ddlFlow"
                                    ErrorMessage="Flow." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                            </div>
        </div>
        <div class="col-md-6">
        <table id="gvTemplate" class="table  table-responsive-md btn-table">
                            <asp:Repeater ID="rptTemplate" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                          
                                            <th>
                                              <i class="fa fa-sort-amount-asc mr-2 blue-text" aria-hidden="true">  Level<br/>
                                            </th>
                                            <th>
                                             <i class="fa fa-user mr-2 blue-text" aria-hidden="false"></i>User<br/>
                                            </th>
                                            <th>
                                             <i class="fa fa-user mr-2 blue-text" aria-hidden="false"></i>User<br/>
                                            </th>                                           
                                            
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("LevelName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                         <td>
                                            <%# Eval("Active")%>
                                        </td>
                                         
                                        
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                   </div>
        <div class="col-md-4">
        <asp:Button ID="btnSave" runat="server" Text="Create" OnClick="btnSave_Click" CssClass="btn btn-dark-green px-3 waves-effect waves-light" />
        </div>
 </div> 
     
 
</div>
<div class="tab-pane fade show" id="nav-attach" role="tabpanel" aria-labelledby="nav-attach">
<div class="col-md-12" style="text-align:center;">
                           
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-red px-3 waves-effect waves-light" />
                        </div>
</div>
</div>
</div>  
</asp:Content>
