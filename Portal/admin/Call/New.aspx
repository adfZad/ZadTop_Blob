<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="admin_Call_New"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <script type="text/javascript">
     $(document).ready(function () {

         $('#ddlType').selectize({
             sortField: 'text',
             selectOnTab: true
         });
         $('#ddlUser').selectize({
             sortField: 'text',
             selectOnTab: true
         });
         $('#ddlDivision').selectize({
             sortField: 'text',
             selectOnTab: true
         });
         $('#ddlAssign').selectize({
             sortField: 'text',
             selectOnTab: true
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

     function validateFileUpload(source, args) {
         var ret = false;
         var fuImage = document.getElementById(source.controltovalidate);
         if (fuImage != null) {

             //             if (source.controltovalidate.indexOf("Image") != -1)
             //                 ret = isValidImage(fuImage.value, true);
             //             else
             //                 ret = isValidImage(fuImage.value, false);
             ret = isValidImage(fuImage.value, true);
         }
         else {
             ret = isValidImage(fuImage.value, false);
         }
         args.IsValid = ret;
     }

     var validExt = new Array();

     function isValidImage(imagepath, isImage) {

//         if (isImage)
//             validExt = ['.jpg', '.jpeg', '.png'];
//         else
//             validExt = ['.pdf'];
         validExt = ['.jpg', '.jpeg', '.png', '.pdf'];
         if (imagepath == null || imagepath.length == 0)
             return false;

         var index = imagepath.lastIndexOf('.');
         if (index == -1)
             return false;

         var ext = imagepath.substring(index).toLowerCase();

         //itreate on each valid extensions
         for (i = 0; i < validExt.length; i++) {

             if (ext == validExt[i])
                 return true;
         }

         return false;
     }
     function txtDesc_onclick() {

     }

 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
        <div class=" md-form">
                                <asp:DropDownList ID="ddlUser" runat="server"  AutoPostBack="true"
                                    ClientIDMode="Static" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlUser">
                                   Caller <span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="error" ControlToValidate="ddlUser"
                                    ErrorMessage="User." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                            </div>
        </div>
         <div class="col-md-3">
                            <div class=" md-form">
                                <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlType">
                                    Problem Type <span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="error" ControlToValidate="ddlType"
                                    ErrorMessage="Type." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                            </div>
                        </div>
         <div class="col-md-3">
                          <div class=" md-form">
                                <asp:DropDownList ID="ddlDivision" runat="server" ClientIDMode="Static" >
                                   <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlDivision">
                                   Division <span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="error" ControlToValidate="ddlDivision"
                                    ErrorMessage="ddlDivision." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                            </div>
                        </div>
         <div class="col-md-3">
                          <div class=" md-form">
                                <asp:DropDownList ID="ddlAssign" runat="server" ClientIDMode="Static" >
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="ddlAssign">
                                   Assign To <span style="color: red">*</span></label>
                                 <asp:CompareValidator ID="CompareValidator4" runat="server" CssClass="error" ControlToValidate="ddlAssign"
                                    ErrorMessage="ddlAssign." Text="*" Type="Integer" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                            </div>
                        </div>
 </div>  
<div class="row">
         <div class="col-md-12">
               <div class="md-form">
              <i class="fa fa-pencil prefix"></i>
              <textarea id="txtDesc" class="md-textarea" rows="3" cols="6" runat="server" ClientIDMode="Static" onclick="return txtDesc_onclick()"></textarea>
              <label for="txtDesc">Remarks<span style="color: red">*</span></label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtDesc"
                                    ErrorMessage="txtDesc" Text="*"></asp:RequiredFieldValidator>
    </div>
    </div>
     
 </div>     
</div>
<div class="tab-pane fade show" id="nav-attach" role="tabpanel" aria-labelledby="nav-attach">
<asp:Panel ID="pnlCreate" runat="server">                      
  <asp:TextBox ID="txtMain" runat="server" Text="Document Name"></asp:TextBox>
         <asp:FileUpload ID="fupImage" runat="server" CssClass="form_input_file" />
                    <asp:CustomValidator ID="CustomValidator13" runat="server" ErrorMessage="Photo: Invalid file format."
                        CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupImage"
                        OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator14" runat="server" ControlToValidate="fupImage"
                        CssClass="error" ErrorMessage="Passport Image: Maximum file size exceed." OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
     <%-- <asp:FileUpload ID="fupRegistration" runat="server"/>--%>
                                              
     <%--   <asp:CustomValidator ID="cvRegistrationImage" runat="server" ErrorMessage="Registration : Invalid file format."
            CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupRegistration"
            OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
        <asp:CustomValidator ID="cvRegistrationImageSize" runat="server" ControlToValidate="fupRegistration"
            CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
            OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>--%>
  
<%--   <label class="form_label">
                        Photo</label>--%>
             


                <asp:TextBox ID="txtOne" runat="server" Text="Document Name"></asp:TextBox>
                
                                   <asp:FileUpload ID="fupOne" runat="server" CssClass="form_input_file" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Photo: Invalid file format."
                        CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupOne"
                        OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="fupOne"
                        CssClass="error" ErrorMessage="Passport Image: Maximum file size exceed." OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                             
                                <asp:TextBox ID="txtTwo" runat="server" Text="Document Name"></asp:TextBox>
                               <asp:FileUpload ID="fupTwo" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupTwo"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="fupTwo"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                          
                               <asp:TextBox ID="txtThree" runat="server" Text="Document Name"></asp:TextBox>
                               <asp:FileUpload ID="fupThree" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator5" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupThree"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator6" runat="server" ControlToValidate="fupThree"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                                
                               <asp:TextBox ID="txtFour" runat="server" Text="Document Name"></asp:TextBox>
                               <asp:FileUpload ID="fupFour" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator7" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupFour"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator8" runat="server" ControlToValidate="fupFour"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                                <asp:TextBox ID="txtFive" runat="server" Text="Document Name"></asp:TextBox>
                               <asp:FileUpload ID="fupFive" runat="server"/>
                                              
                                                <asp:CustomValidator ID="CustomValidator9" runat="server" ErrorMessage="Registration : Invalid file format."
                                                    CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupFive"
                                                    OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="CustomValidator10" runat="server" ControlToValidate="fupFive"
                                                    CssClass="error" ErrorMessage="Registration Image: Maximum file size exceed."
                                                    OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                                  <asp:TextBox ID="txtSix" runat="server" Text="Document Name"></asp:TextBox>
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

