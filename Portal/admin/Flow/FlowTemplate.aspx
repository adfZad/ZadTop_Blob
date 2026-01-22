<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="FlowTemplate.aspx.cs" Inherits="admin_Flow_FlowTemplate" EnableEventValidation = "false" %>

<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $(function () {
                $('[id*=gvMake]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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

            $('#ddlStatus').selectize({
                sortField: 'text',
                onChange:function(){
                    if($("#ddlStatus").val()>0){
                    }
               
                    else {
                        $("#ddlStatus").val("0");
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

               $('#ddlUser').selectize({
                sortField: 'text',
                 onChange:function(){
                    if($("#ddlUser").val()>0){
                    }
               
                    else {
                        $("#ddlUser").val("0");
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

            $('#ddlCommunicationMode').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlCommunicationMode").val()>0){
                    }
               
                    else {
                        $("#ddlCommunicationMode").val("0");
                    }
                 } 
            });


             $('#ddlColorMode').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlColorMode").val()>0){
                    }
               
                    else {
                        $("#ddlColorMode").val("0");
                    }
                 } 
            });

              $('#ddlSize').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlSize").val()>0){
                    }
               
                    else {
                        $("#ddlSize").val("0");
                    }
                 } 
            });

              $('#ddlPlan').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlPlan").val()>0){
                    }
               
                    else {
                        $("#ddlPlan").val("0");
                    }
                 } 
            });

             $('#ddlPrinterMode').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlPrinterMode").val()>0){
                    }
               
                    else {
                        $("#ddlPrinterMode").val("0");
                    }
                 } 
            });

            
             $('#ddlCategory').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlCategory").val()>0){
                    }
               
                    else {
                        $("#ddlCategory").val("0");
                    }
                 } 
            });

              $('#ddlColor').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlColor").val()>0){
                    }
               
                    else {
                        $("#ddlColor").val("0");
                    }
                 } 
            });

            
              $('#ddlMultiMedia').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlMultiMedia").val()>0){
                    }
               
                    else {
                        $("#ddlMultiMedia").val("0");
                    }
                 } 
            });


             
             
              $('#ddlCamera').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlCamera").val()>0){
                    }
               
                    else {
                        $("#ddlCamera").val("0");
                    }
                 } 
            });

             $('#ddlWifi').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlWifi").val()>0){
                    }
               
                    else {
                        $("#ddlWifi").val("0");
                    }
                 } 
            });


             $('#ddlGeneration').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlGeneration").val()>0){
                    }
               
                    else {
                        $("#ddlGeneration").val("0");
                    }
                 } 
            });


             $('#ddlBluetooth').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlBluetooth").val()>0){
                    }
               
                    else {
                        $("#ddlBluetooth").val("0");
                    }
                 } 
            });

             $('#ddlOpticalDrive').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlOpticalDrive").val()>0){
                    }
               
                    else {
                        $("#ddlOpticalDrive").val("0");
                    }
                 } 
            });

             $('#ddlFloppy').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlFloppy").val()>0){
                    }
               
                    else {
                        $("#ddlFloppy").val("0");
                    }
                 } 
            });

             $('#ddlMonitor').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlMonitor").val()>0){
                    }
               
                    else {
                        $("#ddlMonitor").val("0");
                    }
                 } 
            });

             $('#ddlKeyboard').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlKeyboard").val()>0){
                    }
               
                    else {
                        $("#ddlKeyboard").val("0");
                    }
                 } 
            });


             $('#ddlMouse').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlMouse").val()>0){
                    }
               
                    else {
                        $("#ddlMouse").val("0");
                    }
                 } 
            });

             $('#ddlKeyboard').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlKeyboard").val()>0){
                    }
               
                    else {
                        $("#ddlKeyboard").val("0");
                    }
                 } 
            });

             $('#ddlRaidController').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlRaidController").val()>0){
                    }
               
                    else {
                        $("#ddlRaidController").val("0");
                    }
                 } 
            });

              $('#ddlAssetType').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlAssetType").val()>0){
                    }
               
                    else {
                        $("#ddlAssetType").val("0");
                    }
                 } 
            });

              $("#txtPurchaseDate").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy",
//                endDate: "16/12/2019",
//                startDate: "01/01/2019"
            }).on("hide", function (e) {
               
            });
             $('#ddlautoFeeder').selectize({
                sortField: 'text',
                  onChange:function(){
                    if($("#ddlautoFeeder").val()>0){
                    }
               
                    else {
                        $("#ddlautoFeeder").val("0");
                    }
                 } 
            });
        });


         

   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="card card-cascade narrower mb-5">
    <div class="view py-3 gradient-card-header info-color-dark" style="width: 30%;">
                <h5 class="mb-0">
                    Flow Template</h5>
            </div>
    <div id="dvInput" runat="server" class="card small mb-5" style="margin: 0 auto;width:20rem">
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 
                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:RequiredFieldValidator ID="valtxtCode" runat="server" ControlToValidate="txtMake" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                            <asp:TextBox ID="txtMake" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <label for="txtMake">
                                Name/الشفرة<span style="color: red">*</span></label>
                          
                            </div>
                        </div>
                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:DropDownList ID="ddlFlow" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlFlow">
                                    Flow
                                </label>
                               
                          
                            </div>
                        </div>

                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:DropDownList ID="ddlUser" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlUser">
                                    UserName
                                </label>
                              
                          
                            </div>
                        </div>

                        <div class="col-md-12">
                        <div class="md-form">
                          <asp:DropDownList ID="ddlLevel" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlLevel">
                                    Level
                                </label>
                               
                          
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
   
                        <table id="gvMake" class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptMake" runat="server" OnItemDataBound="rptMake_ItemDataBound"
                                OnItemCommand="rptMake_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>
                                                
                                            </th>
                                            <th>
                                                Name<br/>الشفرة
                                            </th>
                                             <th>
                                                Flow Name<br/>الشفرة
                                            </th>
                                             <th>
                                                User Name<br/>الشفرة
                                            </th>
                                             <th>
                                                Level Name<br/>الشفرة
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
                                            <%# Eval("FlowName")%>
                                        </td>
                                         <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                         <td>
                                            <%# Eval("LevelName")%>
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

