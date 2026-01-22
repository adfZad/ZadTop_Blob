<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="admin_Flow_Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/selectize.default.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/selectize.bootstrap2.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/selectize.js" type="text/javascript"></script>
    <link href="../../Styles/datepickercss/bootstrap-datepicker3.standalone.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../Scripts/datepickerScripts/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../../Scripts/datepickerScripts/locales/bootstrap-datepicker.en-GB.min.js"
        type="text/javascript"></script>

          <script type="text/javascript" language="javascript">
              $(document).ready(function () {

                  var currentDate = new Date();
                  var hr = currentDate.getHours();
                  var mm = currentDate.getMinutes();

                  var config = {
                      timeFormat: 'hh:mm',
                      hour: hr,
                      minute: mm,
                      yearRange: "-100:c",
                      addSliderAccess: true,
                      dateFormat: 'dd/M/yy',
                      showHour: false,
                      changeMonth: true,
                      changeYear: true,
                      showMinute: false,
                      showTime: false,
                      showTimepicker: false
                  };

                  $('#MainContent_txtDateOfJoining').datetimepicker(config);
                  $('#MainContent_txtDOB').datetimepicker(config);
                  $('#MainContent_txtPassportExpiry').datetimepicker(config);
                  $('#MainContent_txtRPExpiry').datetimepicker(config);
                  $('#MainContent_txtVisaIssueDate').datetimepicker(config);

              });

              function validateFileUpload(source, args) {
                  var ret = false;
                  var fuImage = document.getElementById(source.controltovalidate);
                  if (fuImage != null) {

                      if (source.controltovalidate.indexOf("Image") != -1)
                          ret = isValidImage(fuImage.value, true);
                      else
                          ret = isValidImage(fuImage.value, false);
                  }

                  args.IsValid = ret;
              }

              var validExt = new Array();

              function isValidImage(imagepath, isImage) {

                  if (isImage)
                      validExt = ['.jpg', '.jpeg', '.png'];
                  else
                      validExt = ['.pdf'];

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

    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="curId" runat="server" />
    <div class="accordion" id="accChoosePending" role="tablist" aria-multiselectable="true">
        <!-- Accordion card -->
        <div class="card card-cascade narrower mb-5">
            <div class="view py-3 gradient-card-header info-color-dark" style="width: 92%;">
                <h5 class="mb-0">
                    Upload Photo</h5>
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
                <asp:Panel ID="pnlCreate" runat="server">
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
                       <div class="form_input_div">
                    <label class="form_label">
                        Passport</label>
                    <asp:FileUpload ID="fupPassport" runat="server" CssClass="form_input_file" />
                    <asp:CustomValidator ID="cvPassportImage" runat="server" ErrorMessage="Passport : Invalid file format."
                        CssClass="error" ClientValidationFunction="validateFileUpload" ControlToValidate="fupPassport"
                        OnServerValidate="cvImage_ServerValidate">*</asp:CustomValidator>
                    <asp:CustomValidator ID="cvPassportImageSize" runat="server" ControlToValidate="fupPassport"
                        CssClass="error" ErrorMessage="Passport Image: Maximum file size exceed." OnServerValidate="cvFileSize_ServerValidate">*</asp:CustomValidator>
                </div>
                            </div>
                             

                        </div>
                       



                        
                          

                     
                     <div class="col-md-3">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                CssClass="btn btn-blue px-3 waves-effect waves-light"/>
                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CausesValidation = "false" OnClick="btnCancel_Click"
                                CssClass="btn btn-red px-3 waves-effect waves-light"/>
                           
                        </div>
                        
                    </div>
                 
                
                <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMessage" Style="color: #EC2310;" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    </asp:Panel>
            </div>
            
        </div>
        <!-- Accordion card -->
    </div>
    
</asp:Content>


