<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Password.aspx.cs" Inherits="Profile_Password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="../../Styles/pager.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/material.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Font-Awesome/css/font-awesome.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Scripts/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="../../Scripts/modernizr-2.8.3.js" type="text/javascript"></script>
    <script src="../../Scripts/popper.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../../Scripts/material.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            background: none !important;
        }
    </style>
</head>
<body class="fixed-sn light-blue-skin">
    <form id="form1" runat="server">
<div class="card small mb-5" style="margin: 0 auto;width:25rem">                        
                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="BulletList"/> 
  <div class="card-body">                

                         <div class="row">
                      
                        <div class="col-md-12">
                        <div class="md-form">
                                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <label for="txtOldPassword" class="active">Old Password</label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOldPassword" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                          </div>
                        </div>
                        </div>

                         <div class="row">
                      
                        <div class="col-md-12">
                        <div class="md-form">
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <label for="txtNewPassword" class="active">New Password</label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNewPassword" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                          </div>
                        </div>
                        </div>

                       
                        <div class="row">
                      
                        <div class="col-md-12">
                        <div class="md-form">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <label for="txtConfirmPassword" class="active">Confirm Password</label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirmPassword" 
                                ErrorMessage="Code" ForeColor="red">*</asp:RequiredFieldValidator> 
                          </div>
                        </div>
                        </div>


<div class="row">
                       <div class="col-md-12">
                            <asp:Label ID="lblDelMessage" Style="color: #EC2310;" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12" style="text-align:center;">
                           
                            <asp:Button ID="btnSave1" runat="server" Text="Chnage Password"  CssClass="btn btn-blue px-3 waves-effect waves-light"   onclick="btnSave_Click" />
                            
                        </div>
                      
</div>
</div>
</div>
    </form>
</body>
</html>
