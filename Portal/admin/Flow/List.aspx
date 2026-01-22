<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="admin_Flow_List" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/selectize.default.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/selectize.bootstrap2.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/selectize.js" type="text/javascript"></script>
    <link href="../../Styles/datepickercss/bootstrap-datepicker3.standalone.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../Scripts/datepickerScripts/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../../Scripts/datepickerScripts/locales/bootstrap-datepicker.en-GB.min.js"
        type="text/javascript"></script>
    <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
       <link href="../../Styles/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#txtFrom").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy",
            }).on("hide", function (e) {
          
            });
            $("#txtTo").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy",
            }).on("hide", function (e) {
               
            });

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

            $('#ddlFlowTemplate').selectize({
                sortField: 'text',
                 onChange:function(){
                    if($("#ddlFlowTemplate").val()>0){
                    }
               
                    else {
                        $("#ddlFlowTemplate").val("0");
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


             $('#ddlUserName').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlUserName").val()>0){
                    }
               
                    else {
                        $("#ddlUserName").val("0");
                    }
                 } 
            });

             $('#ddlDivision').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlDivision").val()>0){
                    }
               
                    else {
                        $("#ddlDivision").val("0");
                    }
                 } 
            });
        });
       

   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnSortColumn" runat="server" />
    <div class="accordion" id="accChoosePending" role="tablist" aria-multiselectable="true">
        <!-- Accordion card -->
        <div class="card card-cascade narrower mb-5">
              <div class="view py-3 gradient-card-header info-color-dark" style="width: 92%;">
                <h5 class="mb-0">
                    Flow Details List</h5>
            </div>
            <!-- Card header -->
            <div class="card-header" role="tab" id="headingPending">
                <a data-toggle="collapse" data-parent="accChoosePending" title="Test details" href="#collapsePending"
                    aria-expanded="true" aria-controls="collapsePending">
                    <h5 class="mb-0">
                        <label id="lblPendingDetails" style="font-size: 14px; font-weight: bold;">
                            Flow Details List</label><i class="fa fa-angle-down rotate-icon"></i>
                    </h5>
                </a>
            </div>
            <div id="collapsePending" class="collapse show" role="tabpanel" aria-labelledby="headingPending"
                data-parent="accChoosePending">
                <div class="card-body">
                    <div class="row">
                            <div class="col-md-2">
                            <div class="md-form">
                             <asp:DropDownList ID="ddlFlow" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                     <label for="ddlFlow">Flow</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="md-form">
                            <asp:DropDownList ID="ddlFlowTemplate" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                     <label for="ddlFlowTemplate">Flow Template</label>
                            </div>
                        </div>
                      

                        <div class="col-md-2">
                            <div class="md-form">
                            <asp:DropDownList ID="ddlLevel" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                     <label for="ddlLevel">Level</label>
                            </div>
                        </div>

                         <div class="col-md-2">
                            <div class="md-form">
                            <asp:DropDownList ID="ddlUserName" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                     <label for="ddlUserName">User Name</label>
                            </div>
                        </div>
                                              
                    </div>
                    
                    <div class="row">
                     <div class="col-md-2">
                            <div class="md-form">
                             <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-blue px-3 waves-effect waves-light"
                        Text="Search" OnClick="btnSearch_Click" Height="36px" Width="106px" />
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-striped">
                            <asp:Repeater ID="rptUser" runat="server" OnItemDataBound="rptUser_ItemDataBound"
                                OnItemCommand="rptUser_ItemCommand">
                                <HeaderTemplate>
                                 <div class="rptr_header" style="width: 2000px;">
                                    <thead class="fa-leaf mr-2 blue-text">
                                        <tr>
                                        <div class="w40">
                                            <th style="color: Black" Font-Bold="true">
                                                Serial No
                                            </th>
                                            </div>
                                             <div class="w150">
                                            <th>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkSort_Click" 
                                            CommandArgument="[Id]">Name</asp:LinkButton>
                                            </th>
                                             </div>
                                             <div class="w150">
                                            <th>
                                            <asp:LinkButton ID="lnkCallStarted" runat="server" OnClick="lnkSort_Click" 
                                            CommandArgument="[CreatedDate]"> Flow</asp:LinkButton>
                                            </th>
                                             </div>
                                             <div class="w100">
                                            <th>
                                            <asp:LinkButton ID="lnkCompletionDate" runat="server" OnClick="lnkSort_Click" 
                                             CommandArgument="[CompletionDate]">Flow Template</asp:LinkButton>
                                            </th>
                                             </div>
                                            <div class="w200">
                                            <th>
                                            <asp:LinkButton ID="lnkSortCallerName" runat="server" OnClick="lnkSort_Click" 
                                            CommandArgument="[CallerName]">User Name</asp:LinkButton>
                                            </th>
                                             </div>
                                            <div class="w200">
                                            <th>
                                            <asp:LinkButton ID="lnkDivision" runat="server" OnClick="lnkSort_Click" 
                                             CommandArgument="[Division]">Level</asp:LinkButton>
                                            </th>
                                             </div>
                                            <div class="w200">
                                            <th>
                                            <asp:LinkButton ID="lnkOpenedBy" runat="server" OnClick="lnkSort_Click" 
                                             CommandArgument="[OpenedByName]">Description</asp:LinkButton>
                                            </th>
                                             </div>
                                            <div class="w100">
                                            <th>
                                            <asp:LinkButton ID="lnkProblemType" runat="server" OnClick="lnkSort_Click" 
                                             CommandArgument="[ProblemTypeName]">  AltDescription</asp:LinkButton>
                                            </th>
                                             </div>
                                             </tr>
                                    </thead>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <div class="rptr_item_template" style="width: 2000px;">
                                    <tr onclick="loadDetails(<%# Eval("Id")%>)">
                                        <div class="w40">
                                        <td>
                                            <asp:Label ID="lblRowNo" runat="server" />
                                        </td>
                                        </div>
                                         <div class="w150">
                                        <td>
                                        <%# Eval("Name")%>
                                        </td>
                                        </div>
                                        <div class="w150">
                                        <td>
                                        <%# Eval("FlowName")%>
                                        </td>
                                        </div>
                                         <div class="w100">
                                        <td>
                                        <%# Eval("FlowTemplate")%>
                                        </td>
                                        </div>
                                         <div class="wL200">
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        </div>
                                        <div class="wL200">
                                         <td>
                                         <%# Eval("LevelName")%>
                                        </td>
                                         </div>
                                         <div class="wL200">
                                        <td>
                                            <%# Eval("Description")%>
                                        </td>
                                        </div>
                                         <div class="wL200">
                                         <td>
                                            <%# Eval("AltDescription")%>
                                        </td>
                                        </div>
                                        
                                     
                                        
                                         
                                    </tr>
                                    </div>
                                </ItemTemplate>
                                 <AlternatingItemTemplate>
                                   <div class="rptr_alt_item_template" style="width: 2000px;">
                              <tr onclick="loadDetails(<%# Eval("Id")%>)">
                                        <div class="w40">
                                        <td>
                                            <asp:Label ID="lblRowNo" runat="server" />
                                        </td>
                                        </div>
                                         <div class="w150">
                                        <td>
                                        <%# Eval("Name")%>
                                        </td>
                                        </div>
                                        <div class="w150">
                                        <td>
                                        <%# Eval("FlowName")%>
                                        </td>
                                        </div>
                                         <div class="w100">
                                        <td>
                                        <%# Eval("FlowTemplate")%>
                                        </td>
                                        </div>
                                         <div class="wL200">
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        </div>
                                        <div class="wL200">
                                         <td>
                                         <%# Eval("LevelName")%>
                                        </td>
                                         </div>
                                         <div class="wL200">
                                        <td>
                                            <%# Eval("Description")%>
                                        </td>
                                        </div>
                                         <div class="wL200">
                                         <td>
                                            <%# Eval("AltDescription")%>
                                        </td>
                                        </div>
                                        
                                     
                                        
                                         
                                    </tr>

                                   </div>
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
                </div>
            </div>
        </div>
        <!-- Accordion card -->
    </div>
</asp:Content>


