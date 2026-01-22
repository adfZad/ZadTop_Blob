<%@ Page Title="" Language="C#" MasterPageFile="~/management/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="SignedList.aspx.cs" Inherits="management_Document_SignedList" EnableEventValidation = "false" %>

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

            $('#ddlProperty').selectize({
                sortField: 'text',
                selectOnTab: true
            });

             $('#ddlPropertyName').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlCategory').selectize({
                sortField: 'text',
                selectOnTab: true
            });

             $('#ddlCategoryName').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlType').selectize({
                sortField: 'text',
                selectOnTab: true
            });

             $('#ddlTypeName').selectize({
                sortField: 'text',
                selectOnTab: true
            });

             $('#ddlUnit').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlUnitName').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlDesignation').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlDivisionName').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlRoleName').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlNationalityName').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlProblemType').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlDesignationName').selectize({
                sortField: 'text',
                selectOnTab: true
            });

            $('#ddlAssignedTo').selectize({
                sortField: 'text',
                selectOnTab: true
            });

             $('#ddlResolution').selectize({
                sortField: 'text',
                selectOnTab: true
            });
             $('#ddlSeverity').selectize({
                sortField: 'text',
                selectOnTab: true
            });
              $('#ddlStatus').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlStatusName').selectize({
                sortField: 'text',
                selectOnTab: true
            });
            $('#ddlProblemSubCategory').selectize({
                sortField: 'text',
                selectOnTab: true
            });


            
            

                    


                
            });
        

       
         
    </script>

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
         function loadDetails(id){
        $.colorbox({iframe:true, width:"70%", height:"100%", href:"../../Profile/Document.aspx?id="+id});
        }
        function loadTaskDetails(id) {
            $.colorbox({ iframe: true, width: "100%", height: "100%", href: "../../Profile/TaskDetails.aspx?id=" + id });
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnSortColumn" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" />
    
    <div class="row"  style="width: 120%;">

    <div class= "card-header" style="width: 120%;margin-left:-80px;">
    <h5>Document Signed Details</h5> 
    </div>                              
    <div class="card-body" style="width: 120%;margin-left:-80px;">
     <div class="row">
     <div class="col-md-4">
     <div class="md-form">
     <asp:DropDownList ID="ddlDivisionName" runat="server" ClientIDMode="Static">
     <asp:ListItem Text="" Value="0"></asp:ListItem>
     </asp:DropDownList>
     <label for="ddlDivisionName">
     Division</label>
     </div>
     </div>   
     <div class="col-md-4">
     <div class="md-form">
     <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-blue px-3 waves-effect waves-light"
     Text="Search" OnClick="btnSearch_Click" Height="36px" Width="106px" CausesValidation="false"/>
     </div>
     </div> 
     </div>  
     </div>                           
   
     <div class="card-body"  style="width: 120%;margin-left:-80px;">
  
     <table id="gvDocument"class="table table-striped table-bordered ">
                             <asp:Repeater ID="rptDocument" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                         <tr>
                                          
                                            <th>
                                             Division
                                            </th>   
                                            <th>
                                             Approved Ref-No
                                            </th>                                         
                                            <th>
                                             Ref-No
                                            </th>
                                            <th>
                                             Status
                                            </th>
                                            <th>
                                            Type
                                            </th>
                                            <th>
                                            Party Name
                                            </th>
                                            <th>
                                            Payment / Documents For
                                            </th>                                            
                                            <th>
                                            Currency
                                            </th>
											<th>
                                            Amount
                                            </th>
                                            <th>
                                            Exchange Rate
                                            </th>                                            
                                            <th>
                                            Amount in QAR
                                            </th>                                           
                                            <th>
                                            Created Date
                                            </th>
                                            <th>
                                            Last Updated By
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
                                        <a href="<%# Eval("AltDescription")%>" target="_blank"><%# Eval("Date")%></a>
                                        </td>                                        
                                        <td>
                                        <a href="<%# Eval("AltDescription")%>" target="_blank"><%# Eval("DocumentFullName")%></a>
                                        </td>
                                        <td>
                                        <%# Eval("StatusName")%>
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
     
       
    <div class="row">
                        <div class="col-md-12">
                            <zhp:ZadPager ID="pager" runat="server" OnCommand="pager_Command" GenerateGoToSection="true"
                                GenerateHiddenHyperlinks="true" Visible="false" />
                        </div>
                    </div>    

</div> 




 </div>


</asp:Content>
