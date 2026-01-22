<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="CreateFlowDetail.aspx.cs" Inherits="admin_Flow_CreateFlowDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/selectize.default.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/selectize.bootstrap2.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/selectize.js" type="text/javascript"></script>
    <link href="../../Styles/datepickercss/bootstrap-datepicker3.standalone.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../Scripts/datepickerScripts/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../../Scripts/datepickerScripts/locales/bootstrap-datepicker.en-GB.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#txtDOB").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy",
//                endDate: "16/12/2019",
//                startDate: "01/01/2019"
            }).on("hide", function (e) {
          
            });
            $("#txtToDate").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy",
//                endDate: "16/12/2019",
//                startDate: "01/01/2019"
            }).on("hide", function (e) {
               
            });

             $("#txtIssueDate").datepicker({
                autoclose: true,
                clearBtn: false,
                format: "dd/mm/yyyy",
//                endDate: "16/12/2019",
//                startDate: "01/01/2019"
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
                sortField: 'text'
            });

            $('#ddlUserName').selectize({
                sortField: 'text'
            });

             $('#ddlLevel').selectize({
                sortField: 'text'
            });

            $('#ddlDesignation').selectize({
                sortField: 'text'
            });

            $('#ddlAssignedTo').selectize({
                sortField: 'text'
            });

            $('#ddlProblemType').selectize({
                sortField: 'text'
            });

             $('#ddlSeverity').selectize({
                sortField: 'text'
            });

             $('#ddlStatus').selectize({
                sortField: 'text'
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
    <div class="accordion" id="accChoosePending" role="tablist" aria-multiselectable="true">
        <!-- Accordion card -->
        <div class="card card-cascade narrower mb-5">
            <div class="view py-3 gradient-card-header info-color-dark" style="width: 92%;">
                <h5 class="mb-0">
                    Create Flow Details</h5>
            </div>
            <!-- Card header -->
            <div class="card-header" role="tab" id="headingPending">
                <a data-toggle="collapse" data-parent="accChoosePending" title="Test details" href="#collapsePending"
                    aria-expanded="true"  aria-controls="collapsePending">
                    <h5 class="mb-0">
                        <label id="lblPendingDetails" style="font-size: 14px; font-weight: bold;">
                            Enter Flow Details</label><i class="fa fa-angle-down rotate-icon"></i>
                    </h5>
                </a>
            </div>
            <div id="collapsePending" class="collapse show" role="tabpanel" aria-labelledby="headingPending"
                data-parent="accChoosePending">
                <div class="card-body">
                   <div class="row">                   
                        <div class="col-md-3">
                            <div class="md-form">
                            <asp:TextBox ID="txtMake" runat="server" CssClass="form-control" ></asp:TextBox>
                            <label for="txtMake"> 
                                   Name<span style="color: red">*</span></label> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="error" Text="*"
                        runat="server" ControlToValidate="txtMake" ErrorMessage="Name."></asp:RequiredFieldValidator> 

                           

                                 
                                                       
                            </div>
                        </div>
                        <div class="col-md-3">
                        <div class="md-form">
                                <asp:DropDownList ID="ddlFlow" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlFlow">
                                    Flow 
                                </label>   
                                 <asp:CompareValidator ID="CompareValidator5" runat="server" CssClass="error" ControlToValidate="ddlFlow"
                                    ErrorMessage="Flow" Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                     <br/>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Text="*"
                        runat="server" ControlToValidate="ddlFlow" ErrorMessage="Flow."></asp:RequiredFieldValidator>                                     
                            </div>
                             

                        </div>
                        <div class="col-md-3">
                              <div class="md-form">
                                <asp:DropDownList ID="ddlFlowTemplate" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlFlowTemplate">
                                    Flow Template
                                </label> 
                                 <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="error" ControlToValidate="ddlFlowTemplate"
                                    ErrorMessage="FlowTemplate" Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                     <br/>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Text="*"
                        runat="server" ControlToValidate="ddlFlowTemplate" ErrorMessage="Flow Template."></asp:RequiredFieldValidator>   
                               
                            </div>
                        </div>
                        <div class="col-md-3">
                              <div class="md-form">
                                 <asp:DropDownList ID="ddlUserName" runat="server"
                                    ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlUserName">
                                    User Name
                                </label> 
                                <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="error" ControlToValidate="ddlUserName"
                                    ErrorMessage="UserName" Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                     <br/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="error" Text="*"
                        runat="server" ControlToValidate="ddlUserName" ErrorMessage="User Name."></asp:RequiredFieldValidator>     
                                
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="md-form">
                                <asp:DropDownList ID="ddlLevel" runat="server" 
                                    ClientIDMode="Static"   style="text-transform:capitalize; list-style-type:disc" >
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlLevel">
                                    Level<span style="color: red">*</span></label>
                                      <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="error" ControlToValidate="ddlLevel"
                                    ErrorMessage="Level" Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                     <br/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="error" Text="*"
                        runat="server" ControlToValidate="ddlLevel" ErrorMessage="Level."></asp:RequiredFieldValidator>     
                                   
                        </div>
                        </div>

                         <div class="col-md-3">
                              <div class="md-form">
                                <div class="form_input_div">
                                 <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                            <label for="txtDesc"> 
                                   Description<span style="color: red">*</span></label> 
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="error" Text="*"
                        runat="server" ControlToValidate="txtDesc" ErrorMessage="Description."></asp:RequiredFieldValidator> 
                </div>
                            </div>
                         

                        </div>

                        

                        <div class="col-md-3">
                            <div class="md-form">
                               <asp:TextBox ID="txtAltDesc" runat="server" CssClass="form-control"></asp:TextBox>
                            <label for="txtAltDesc"> 
                                   وصف/Description<span style="color: red">*</span></label>   
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="error" Text="*"
                        runat="server" ControlToValidate="txtAltDesc" ErrorMessage="AltDescription."></asp:RequiredFieldValidator>                                
                            </div>
                        </div>



                        
                          

                     
                     <div class="col-md-3">
                            <asp:Button ID="btnSave" runat="server" Text="Save" 
                                CssClass="btn btn-blue px-3 waves-effect waves-light" 
                                onclick="btnSave_Click" />
                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CausesValidation = "false"
                                CssClass="btn btn-red px-3 waves-effect waves-light" 
                                onclick="btnCancel_Click" />
                           
                        </div>
                        
                    </div>
                 
                
                <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMessage" Style="color: #EC2310;" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
            </div>
        </div>
        <!-- Accordion card -->
    </div>
    
</asp:Content>

