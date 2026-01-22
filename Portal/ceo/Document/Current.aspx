<%@ Page Title="" Language="C#" MasterPageFile="~/ceo/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Current.aspx.cs" Inherits="ceo_Document_Current" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<%@ Register Namespace="ZadHolding.Pager" Assembly="ZadHolding.Pager" TagPrefix="zhp" %>
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

            $(function () {
                $('[id*=gvDocument]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                    "responsive": true,
                    "sPaginationType": "full_numbers"
                });
            });

        });
        function loadDetails(id) {
            $.colorbox({ iframe: true, width: "70%", height: "100%", href: "../../Profile/Document.aspx?id=" + id });
        }
        function loadTaskDetails(id) {
            $.colorbox({ iframe: true, width: "100%", height: "100%", href: "../../Profile/TaskDetails.aspx?id=" + id });
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:HiddenField ID="hdnSortColumn" runat="server" />
    <div class="accordion" id="accChoosePending" role="tablist" aria-multiselectable="true">
        <!-- Accordion card -->
        <div class="card card-cascade narrower mb-5">
            <div class="view py-3 gradient-card-header info-color-dark" style="width: 92%;">
                <h5 class="mb-0">
                    Document Details</h5>
            </div>
            
 <div class="card-body">

     <div class="row">
     <div class="col-md-12">
   
                        <table id="gvDocument" class="table table-striped table-bordered dt-responsive">
                            <asp:Repeater ID="rptDocument" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                          
                                            <th>
                                                Doc-No<br/>الشفرة
                                            </th>
                                            <th>
                                             Doc Type/>وصف
                                            </th>
                                            <th>
                                             Purpose<br/>وصف
                                            </th>
                                            <th>
                                             Status<br/>وصف
                                            </th>
                                            <th>
                                            Created Date<br/>تم التحديث بواسطة
                                            </th>
                                            <th>
                                            Last Updated By<br/>تم التحديث بواسطة
                                            </th>
                                           
                                            
                                            
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        
                                          <td>
                                           <a href="<%# Eval("AltDescription")%>" target="_blank"><%# Eval("DocumentFullName")%></a>
                                 
                                        </td>
                                        <td >
                                            <%# Eval("TypeName")%>
                                        </td>
                                        <td>
                                            <%# Eval("Purpose")%>
                                        </td>
                                        <td onclick="loadTaskDetails(<%# Eval("ID")%>)">
                                        <a style="color:Blue"><%# Eval("DocumentStatusName")%></a>
                                        </td>
                                         <td>
                                         <%# (DateTime.Parse(Eval("CreatedTime").ToString()) == DateTime.MinValue) ? "<b>-</b>" : string.Format("{0:dd/MMM/yyyy}", Eval("CreatedTime"))%>
                                         </td>
                                        <td>
                                            <%# Eval("UpdatedName")%>
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

