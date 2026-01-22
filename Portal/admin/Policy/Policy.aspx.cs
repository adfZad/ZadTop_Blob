using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Business;
using ZadHolding.PortalBase;
using System.Collections.Specialized;

public partial class admin_Policy_Policy : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        btnSave.Attributes.Add("onclick", "return askConfirm();");
        
        if (!IsPostBack)
        {
            BindData();
           
            
        }
    }

    protected void rptPolicy_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptPolicy_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Policy.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;
          
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Policy.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long id = 0;
        long value = 0;
        decimal dvalue = 0;
        DateTime date = DateTime.MinValue;

        PolicyInfo policyInfo       = new PolicyInfo();
        policyInfo.Amendment_No     = 0;

        date = DateTime.ParseExact(txtIssueDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.IssueDate = date;

        date = DateTime.ParseExact(txtPaymentDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.PaymentDate = date;

        date = DateTime.ParseExact(txtDateFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.DateFrom = date;

        date = DateTime.ParseExact(txtDateTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.DateTo = date;

        policyInfo.RegFlag = chkRegistration.Checked;

        decimal.TryParse(txtInsuredValue.Text.Trim(), out dvalue);
        policyInfo.InsuredValue = dvalue;

        decimal.TryParse(txtPremium.Text.Trim(), out dvalue);
        policyInfo.Premium = dvalue;

        decimal.TryParse(txtDeductibles.Text.Trim(), out dvalue);
        policyInfo.Deductibles = dvalue;

        long.TryParse(ddlCoverage.SelectedItem.Value, out value);
        policyInfo.CoverageId = value;
      


        long.TryParse(ddlVehicle.SelectedItem.Value, out value);
        policyInfo.VehicleId = value;


        policyInfo.CompanyId = 1;

        policyInfo.Active            = chkActive.Checked;
        policyInfo.CreatedId         = PortalUser.Current.UserId;
        policyInfo.CreatedTime       = DateTime.Now;
        policyInfo.UpdatedId         = PortalUser.Current.UserId;
        policyInfo.UpdatedTime       = DateTime.Now;

        byte result = PolicyManager.Insert(policyInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Policy.aspx?pg={0}", 1));
        }
        else if (result == 2)
        {
            lblMessage.Text = "Duplicate values not allowed.";
        }
        else
        {
            lblMessage.Text = "Error occured. Try again.";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long id = 0;

        long value = 0;
        decimal dvalue = 0;
        DateTime date = DateTime.MinValue;

        long.TryParse(hdnId.Value, out id);
        PolicyInfo policyInfo = new PolicyInfo();
        policyInfo.Id = id;

        long.TryParse(hdnAmentNo.Value, out id);
        id = id + 1;

        policyInfo.Amendment_No = id;

        date = DateTime.ParseExact(txtIssueDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.IssueDate = date;

        date = DateTime.ParseExact(txtPaymentDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.PaymentDate = date;

        date = DateTime.ParseExact(txtDateFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.DateFrom = date;

        date = DateTime.ParseExact(txtDateTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        policyInfo.DateTo = date;

        policyInfo.RegFlag = chkRegistration.Checked;

        decimal.TryParse(txtInsuredValue.Text.Trim(), out dvalue);
        policyInfo.InsuredValue = dvalue;

        decimal.TryParse(txtPremium.Text.Trim(), out dvalue);
        policyInfo.Premium = dvalue;

        decimal.TryParse(txtDeductibles.Text.Trim(), out dvalue);
        policyInfo.Deductibles = dvalue;

        long.TryParse(ddlCoverage.SelectedItem.Value, out value);
        policyInfo.CoverageId = value;
      


        long.TryParse(ddlVehicle.SelectedItem.Value, out value);
        policyInfo.VehicleId = value;


        policyInfo.CompanyId = 1;
        policyInfo.Active = chkActive.Checked;
        policyInfo.UpdatedId = PortalUser.Current.UserId;
        policyInfo.UpdatedTime = DateTime.Now;

      
        byte result = PolicyManager.Update(policyInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Policy.aspx?pg={0}", 1));
        }
        else if (result == 2)
        {
            lblMessage.Text = "Duplicate values not allowed.";
        }
        else
        {
            lblMessage.Text = "Error occured. Try again.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Policy.aspx?pg={0}", 1));
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
     
    private void BindData()
    {


        PolicyInfoCollection policyInfoCollection = PolicyManager.GetAll();
        rptPolicy.DataSource = policyInfoCollection;
        rptPolicy.DataBind();

      

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    hdnAmentNo.Value = string.Empty;
                    txtIssueDate.Text = string.Empty;
                    txtPaymentDate.Text = string.Empty;
                    txtDateFrom.Text = string.Empty;
                    txtDateTo.Text = string.Empty;
                    txtInsuredValue.Text = string.Empty;
                    txtPremium.Text = string.Empty;
                    txtDeductibles.Text = string.Empty; 
                    
                    chkRegistration.Checked = true;
                    chkActive.Checked = true;
                    BindCoverage();
                    BindVehicle();
                   
                    dvInput.Style.Remove("display");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);
                    BindCoverage();
                    BindVehicle();
                    PolicyInfo policyInfo = PolicyManager.Get(id);
                    if (policyInfo != null)
                    {
                        hdnId.Value = policyInfo.Id.ToString();
                        hdnAmentNo.Value = policyInfo.Amendment_No.ToString();
                        txtIssueDate.Text = string.Format("{0:dd/MM/yyyy}", policyInfo.IssueDate);
                        txtPaymentDate.Text = string.Format("{0:dd/MM/yyyy}", policyInfo.PaymentDate);
                        txtDateFrom.Text = string.Format("{0:dd/MM/yyyy}", policyInfo.DateFrom);
                        txtDateTo.Text = string.Format("{0:dd/MM/yyyy}", policyInfo.DateTo);
                        txtInsuredValue.Text = string.Format("{0:0,0.00}", policyInfo.InsuredValue);
                        txtPremium.Text = string.Format("{0:0,0.00}", policyInfo.Premium);
                        txtDeductibles.Text = string.Format("{0:0,0.00}", policyInfo.Deductibles); 
                       
                        chkActive.Checked = policyInfo.Active;
                        chkRegistration.Checked = policyInfo.Active;

                        ddlCoverage.SelectedValue = policyInfo.CoverageId.ToString();
                        ddlVehicle.SelectedValue = policyInfo.VehicleId.ToString();
                    }

                    dvInput.Style.Remove("display");
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;

                    break;
                default:
                    dvInput.Style.Add("display", "none");
                   
                    break;
            }
        }
        else
        {
            dvInput.Style.Add("display", "none");
          
         
        }
    }

    private void BindCoverage()
    {
        CoverageInfoCollection coverageInfoCollection = CoverageManager.GetAll();
        ddlCoverage.DataSource = coverageInfoCollection;
        ddlCoverage.DataTextField = "Name";
        ddlCoverage.DataValueField = "Id";
        ddlCoverage.DataBind();
        ddlCoverage.Items.Insert(0, new ListItem(""));
        ddlCoverage.SelectedIndex = 0;

              
    }
    private void BindVehicle()
    {
        VehicleInfoCollection vehicleInfoCollection = VehicleManager.GetAll();
        ddlVehicle.DataSource = vehicleInfoCollection;
        ddlVehicle.DataTextField = "Name";
        ddlVehicle.DataValueField = "Id";
        ddlVehicle.DataBind();

        ddlVehicle.Items.Insert(0, new ListItem(""));
        ddlVehicle.SelectedIndex = 0;

    }
  
}