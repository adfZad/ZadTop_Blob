<%@ Page Title="" Language="C#" MasterPageFile="~/manager/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="manager_Task_List" EnableEventValidation = "false" %>

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
        $(task).ready(function () {

               $(function () {
                $('[id*=gvTask]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                    "responsive": true,
                    "sPaginationType": "full_numbers"
                });
            });
                   
        });
         function loadDetails(id){
        $.colorbox({iframe:true, width:"70%", height:"100%", href:"../Profile/Task.aspx?id="+id});
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnSortColumn" runat="server" />
    <div class="accordion" id="accChoosePending" role="tablist" aria-multiselectable="true">
        <!-- Accordion card -->
        <div class="card card-cascade narrower mb-5">
            <div class="view py-3 gradient-card-header info-color-dark" style="width: 92%;">
                <h5 class="mb-0">
                    Task Details</h5>
            </div>
            
 <div class="card-body">

     <div class="row">
     <div class="col-md-12">
   
                        <table id="gvTask" class="table table-striped table-bordered dt-responsive nowrap">
                            <asp:Repeater ID="rptTask" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                          
                                            <th>
                                            Name<br/>الشفرة
                                            </th>
                                            <th>
                                             Description<br/>وصف
                                            </th>
                                            <th>
                                             AltDescription<br/>وصف
                                            </th>
                                         
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        
                                        <td onclick="loadDetails(<%# Eval("ID")%>)">
                                            <%# Eval("Name")%>
                                        </td>
                                        <td>
                                            <%# Eval("Description")%>
                                        </td>
                                        <td>
                                            <%# Eval("AltDescription")%>
                                        </td>
                                     
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                  
                   </div>
    </div>
     </div>     
           </div>
           </div>  


</asp:Content>
