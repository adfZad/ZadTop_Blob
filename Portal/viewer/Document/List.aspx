<%@ Page Title="" Language="C#" MasterPageFile="~/viewer/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="admin_Document_List" EnableEventValidation = "false" %>

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

    <script src="https://cdn.datatables.net/1.10.24/js/dataTables.bootstrap4.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.7/js/dataTables.responsive.min.js"type="text/javascript"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.7/js/responsive.bootstrap4.min.js"type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.bootstrap4.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.print.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.colVis.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.2/css/bootstrap.css"type="text/javascript"></script>
    <script src="https://cdn.datatables.net/1.10.24/css/dataTables.bootstrap4.min.css" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.7/css/responsive.bootstrap4.min.css" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

          
           
          
            
          
            $(document).ready(function() {
            var table = $('#gvDocument').DataTable( {
            lengthChange: true,
            
            
            


            buttons: [
            {
                extend: 'excel',
                title: 'Call Log Details'
 
            },    
                   
          ],
        
    } );
 
    table.buttons().container()
        .appendTo( '#gvDocument_wrapper .col-md-6:eq(0)' );
         } );

          

            $(function () {
                $('[id*=#gvDocument]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                    "responsive": true,
                    "sPaginationType": "full_numbers",

                    


                });
            });
        });

         

        
         
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(function () {
                $('[id*=#gvDocument]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
                                             Division<br/>الشفرة
                                            </th>                                            
                                            <th>
                                             Ref-No<br/>الشفرة
                                            </th>
                                             <th>
                                             Status<br/>الحالة
                                            </th>
                                            <th>
                                            Type/>يكتب
                                            </th>
                                            <th>
                                            Party Name<br/>اسم الحزب
                                            </th>
                                            <th>
                                            Payment / Documents For/الدفع / المستندات
                                            </th>                                            
                                            <th>
                                            Currency/عملة
                                            </th>
											<th>
                                            Amount/مقدار
                                            </th>
                                            <th>
                                            Exchange Rate/سعر الصرف
                                            </th>                                            
                                            <th>
                                            Amount in QAR/المبلغ بالريال القطري
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
                                        <%# Eval("DivisionName")%>
                                        </td>                                        
                                        <td>
                                        <a href="<%# Eval("AltDescription")%>" target="_blank"><%# Eval("DocumentFullName")%></a>
                                        </td>
                                        <td onclick="loadTaskDetails(<%# Eval("ID")%>)">
                                        <a style="color:Blue"><%# Eval("DocumentStatusName")%></a>
                                        </td>
                                        <td>
                                        <%# Eval("TypeName")%>
                                        </td>
                                        <td>
                                        <%# Eval("Purpose")%>
                                        </td>
                                        <td>
                                        <%# Eval("Description")%>
                                        </td>                                        
                                        <td>
                                        <%# string.Format("{0:0,0.00;(0,0.00);0}", Eval("Currency"))%>    
                                        </td>
										<td>
										<%# string.Format("{0:0,0.00;(0,0.00);0}", Eval("OtherAmount"))%>                                            
                                        </td>
                                        <td>
                                        <%# Eval("Rate")%>
                                        </td>                                         
                                        <td> 
                                        <%# string.Format("{0:0,0.00;(0,0.00);0}", Eval("Amount"))%> 										
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
