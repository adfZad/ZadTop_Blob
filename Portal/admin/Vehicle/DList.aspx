<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="DList.aspx.cs" Inherits="admin_Vehicle_DList" EnableEventValidation = "false" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/material.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Font-Awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/DataTable/datatables.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/DataTable/dataTables.bootstrap4.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/DataTable/responsive.bootstrap4.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/selectize.default.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/selectize.bootstrap2.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/datepickercss/bootstrap-datepicker3.standalone.min.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="../../Scripts/modernizr-2.8.3.js" type="text/javascript"></script>
     <script src="../../Scripts/popper.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../../Scripts/material.js" type="text/javascript"></script>
    <script src="../../Scripts/DataTable/datatables.js" type="text/javascript"></script>
    <script src="../../Scripts/DataTable/dataTables.bootstrap4.js" type="text/javascript"></script>
    <script src="../../Scripts/DataTable/dataTables.responsive.js" type="text/javascript"></script>
    <script src="../../Scripts/DataTable/responsive.bootstrap4.js" type="text/javascript"></script>
    <script src="../../Scripts/selectize.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/datepickerScripts/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../../Scripts/datepickerScripts/locales/bootstrap-datepicker.en-GB.min.js" type="text/javascript"></script>

    

    
   
   
   

   



 <%--   <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.9/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/responsive/1.0.7/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/responsive/1.0.7/js/dataTables.responsive.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            $('[id*=gvCustomers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="card card-cascade narrower mb-5">
        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered dt-responsive nowrap"
            Width="100%">
            <Columns>
    <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
      <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
      <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
     <asp:BoundField DataField="Name" HeaderText = "Name"/>
    <asp:BoundField DataField="Description" HeaderText = "Description"/>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
