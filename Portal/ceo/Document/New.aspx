<%@ Page Title="" Language="C#" MasterPageFile="~/ceo/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="ceo_Document_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

           $('#ddlFlow').selectize({
                selectOnTab:true
            });
            $('#ddlType').selectize({
                selectOnTab: true
            });
            $('#ddlCurrency').selectize({
                selectOnTab: true
            });
        });

//        function askConfirm() {
//            if (confirm("Please confirm.")) {
//                return true;
//            }
//            else {
//                return false;
//            }
//        }

//          function validateFileUpload(source, args) {
//            var ret = false;
//            var fuImage = document.getElementById(source.controltovalidate);
//            if (fuImage != null) {

//                if (source.controltovalidate.indexOf("Image") != -1)
//                    ret = isValidImage(fuImage.value, true);
//                else
//                    ret = isValidImage(fuImage.value, false);
//            }

//            args.IsValid = ret;
//        }

//        var validExt = new Array();

//        function isValidImage(imagepath, isImage) {

//            if (isImage)
//                validExt = ['.jpg', '.jpeg', '.png'];
//            else
//                validExt = ['.pdf'];

//            if (imagepath == null || imagepath.length == 0)
//                return false;

//            var index = imagepath.lastIndexOf('.');
//            if (index == -1)
//                return false;

//            var ext = imagepath.substring(index).toLowerCase();

//            //itreate on each valid extensions
//            for (i = 0; i < validExt.length; i++) {

//                if (ext == validExt[i])
//                    return true;
//            }

//            return false;
//        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="curId" runat="server" />
<div class="card card-cascade narrower mb-5">
        <div class="view py-3 gradient-card-header info-color-dark" style="width: 20%;">
                <h5 class="mb-0">
                    Document Entry</h5>
            </div>
<nav>
  <div class="nav nav-tabs brown" id="nav-tab" role="tablist">
   <a class="nav-item nav-link btn active" id="nav-flow-tab" data-toggle="tab" href="#nav-flow" role="tab" aria-controls="nav-flow" aria-selected="true"><i class="fa fa-sort-amount-desc fa-2x pb-2"
          aria-hidden="false"></i>Details</a>
   <a class="nav-item nav-link btn" id="nav-attach-tab" data-toggle="tab" href="#nav-attach" role="tab" aria-controls="nav-attach" aria-selected="false"><i class="fa fa-paperclip fa-2x pb-2"
          aria-hidden="true"></i>Attachments</a>
  </div>
</nav>


<div class="tab-content pt-5" id="nav-tabContent">
<div class="tab-pane fade show active" id="nav-flow" role="tabpanel" aria-labelledby="nav-flow">
<div class="row">
            <div class="col-md-3">
                            <div class="md-form">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                <label for="txtName">
                                    Reference No<span style="color: red">*</span></label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="Reference No" Text="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
         <div class="col-md-3">

                                <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlType">
                                    Document Type <span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="error" ControlToValidate="ddlType"
                                    ErrorMessage="Document Type." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                        </div>
      
         <div class="col-md-3">
                            <div class="md-form">
                                <asp:TextBox ID="txtPurpose" runat="server" CssClass="form-control"></asp:TextBox>
                                <label for="txtPurpose">
                                    Party Name<span style="color: red">*</span></label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPurpose"
                                    ErrorMessage="Party Name" Text="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
         <div class="col-md-3">
                            <div class=" md-form">
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <label for="txtDesc">
                                   Payment / Document For</label>
                            </div>
                        </div>

 </div>  
<div class="row">
<div class="col-md-2">

                                <asp:DropDownList ID="ddlCurrency" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="QAR" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="USD" Value="2"></asp:ListItem>
                                             <asp:ListItem Text="EUR" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="AED" Value="4"></asp:ListItem>												
                                                   <asp:ListItem Text="NA" Value="5"></asp:ListItem>
				                   <asp:ListItem Text="CAD" Value="6"></asp:ListItem>
				                   <asp:ListItem Text="OMR" Value="7"></asp:ListItem>
				                   <asp:ListItem Text="CHF" Value="8"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlCurrency">
                                    Currency<span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="error" ControlToValidate="ddlCurrency"
                                    ErrorMessage="Currency." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>

</div>
<div class="col-md-2">
<div class="md-form">
<asp:TextBox ID="txtOtherAmount" runat="server" CssClass="form-control"></asp:TextBox>
<label for="txtOtherAmount">
Amount<span style="color: red">*</span></label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtOtherAmount"
ErrorMessage="Non QAR Amount" Text="*"></asp:RequiredFieldValidator>
</div>
</div> 
<div class="col-md-2">
<div class="md-form">
<asp:TextBox ID="txtExchangeRate" runat="server" CssClass="form-control"></asp:TextBox>
<label for="txtExchangeRate">
Exchange Rate<span style="color: red">*</span></label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtExchangeRate"
ErrorMessage="Exchange Rate" Text="*"></asp:RequiredFieldValidator>
</div>
</div>
<div class="col-md-2">
<div class="md-form">
<asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
<label for="txtAmount">
Amount (QAR)<span style="color: red">*</span></label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAmount"
ErrorMessage="Amount (QAR)" Text="*"></asp:RequiredFieldValidator>
</div>
</div>
                    
<div class="col-md-4">

<asp:DropDownList ID="ddlFlow" runat="server"  AutoPostBack="true"
                                    ClientIDMode="Static" OnSelectedIndexChanged="ddlFlow_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlFlow">
                                    Flow Template <span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="error" ControlToValidate="ddlFlow"
                                    ErrorMessage="Flow." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>

        </div>
<div class="col-md-6">
        <table id="gvTemplate" class="table  table-responsive-md btn-table">
                            <asp:Repeater ID="rptTemplate" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                          
                                            <th>
                                              <i class="fa fa-sort-amount-asc mr-2 blue-text" aria-hidden="true">  Level<br/>
                                            </th>
                                            <th>
                                             <i class="fa fa-user mr-2 blue-text" aria-hidden="false"></i>User<br/>
                                            </th>
                                                                                       
                                            
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("LevelName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        
                                         
                                        
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                   </div>

 </div>     
 
</div>
<div class="tab-pane fade show" id="nav-attach" role="tabpanel" aria-labelledby="nav-attach">
<asp:Panel ID="pnlCreate" runat="server">                      
  <label>Main Document</label>

      <asp:FileUpload ID="fupRegistration" runat="server"/>
                                              
        <asp:CustomValidator ID="cvRegistrationImage" runat="server" ErrorMessage="Registration : Invalid file format."
            CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupRegistration"
            OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
        <asp:CustomValidator ID="cvRegistrationImageSize" runat="server" ControlToValidate="fupRegistration"
            CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
            OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fupRegistration"
                                    ErrorMessage="Main  Document" Text="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtOne" runat="server" Text="Annexure 1"></asp:TextBox>
                <asp:FileUpload ID="fupOne" runat="server"/>
                                              
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Registration : Invalid file format."
                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupOne"
                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="fupOne"
                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                             
                                <asp:TextBox ID="txtTwo" runat="server" Text="Annexure 2"></asp:TextBox>
                               <asp:FileUpload ID="fupTwo" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupTwo"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="fupTwo"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                          
                               <asp:TextBox ID="txtThree" runat="server" Text="Annexure 3"></asp:TextBox>
                               <asp:FileUpload ID="fupThree" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator5" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupThree"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator6" runat="server" ControlToValidate="fupThree"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                                
                               <asp:TextBox ID="txtFour" runat="server" Text="Annexure 4"></asp:TextBox>
                               <asp:FileUpload ID="fupFour" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator7" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupFour"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator8" runat="server" ControlToValidate="fupFour"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                                <asp:TextBox ID="txtFive" runat="server" Text="Annexure 5"></asp:TextBox>
                               <asp:FileUpload ID="fupFive" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator9" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupFive"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator10" runat="server" ControlToValidate="fupFive"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                                  <asp:TextBox ID="txtSix" runat="server" Text="Annexure 6"></asp:TextBox>
                               <asp:FileUpload ID="fupSix" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator11" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupSix"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator12" runat="server" ControlToValidate="fupSix"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
               
     </asp:Panel>
<div class="col-md-12" style="text-align:center;">
                            <asp:Button ID="btnSave" runat="server" Text="Create" OnClick="btnSave_Click" CssClass="btn btn-dark-green px-3 waves-effect waves-light" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-red px-3 waves-effect waves-light" />
                        </div>
</div>


</div>


  </div>  
</asp:Content>
