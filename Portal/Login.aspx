<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="" charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Payment Portal</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/material.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Font-Awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="Scripts/modernizr-2.8.3.js" type="text/javascript"></script>
    <script src="Scripts/popper.js" type="text/javascript"></script>
    <script src="Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/material.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("label[for='txtUserName']").addClass("active");
            $("label[for='materialFormPasswordEx']").addClass("active");

        });
    </script>
</head>
<body class="fixed-sn light-blue-skin">
    <form id="frmLogin" runat="server">
   
            <div class="row container-fluid" style="padding-top:20px;">
                <div class="col-md-3 mb-5"></div>
                <div class="col-md-6 mb-5">

                    <div id="dvLogin" class="card mx-xl-5">
                        <div class="card-body">

                            <!--Header-->
                            <div class="form-header brown rounded">
                                <h2>Payment Portal</h2>
                            </div>

                            <!-- Material input email -->
                            <div class="md-form font-weight-light">
                                <i class="fa fa-envelope prefix grey-text"></i>
                                 <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                <label for="txtUserName" class="active">User Name</label>
                                <div class="userNameError" style="padding-left:50px;"></div>
                            </div>

                            <!-- Material input password -->
                            <div class="md-form font-weight-light">
                                <i class="fa fa-lock prefix grey-text"></i>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <label for="materialFormPasswordEx" class="active">Password</label>
                                <div class="pwdError" style="padding-left:50px;"></div>
                            </div>

                            <div class="text-center mt-4">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" cssclass="btn btn-brown waves-effect waves-light"/>
                               
                                <input type="button" id="btnReset" name="btnReset" value="Reset" class="btn btn-brown waves-effect waves-light" />
                            </div>
                        </div>

                        <!--Footer-->

                    </div>

                </div>
                <div class="col-md-3 mb-5"></div>
            </div>
     </form>
</body>

</html>
