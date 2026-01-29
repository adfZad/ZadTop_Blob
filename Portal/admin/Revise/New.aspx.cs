using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Been.Collection;
using ZadHolding.Business;
using ZadHolding.PortalBase;
using System.Collections.Specialized;
using ZadHolding.Utilities;
using System.Drawing.Imaging;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Data;
using System.IO;


public partial class admin_Revise_New : System.Web.UI.Page
{
    public static TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            BindFlow();
        }
    }

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
             if (ddlFlow.SelectedIndex > 0)
        {
            long flowId = 0;
            long.TryParse(ddlFlow.SelectedItem.Value, out flowId);
            DocumentInfo documentInfo = DocumentManager.Get(flowId);
            textwriter(documentInfo);
         }
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }

    private void BindFlow()
    {
        long totalRows = 0;
        DocumentSearchParams documentSearchParams = new DocumentSearchParams();
        documentSearchParams.CurrentPage = 1;
        documentSearchParams.PageSize = 5000;
        documentSearchParams.SortColumn = "";
        documentSearchParams.SortOrder = "";
        documentSearchParams.DocumentStatusId = 1;

        //documentSearchParams.DivisionId = PortalUser.Current.DivisionId;
        //documentSearchParams.DepartmentId = PortalUser.Current.DepartmentId;
        DocumentInfoCollection documentInfoCollection = DocumentManager.Search(documentSearchParams, out totalRows);
        //DocumentInfoCollection documentInfoCollection = DocumentManager.GetAll();
        ddlFlow.DataSource = documentInfoCollection;
        ddlFlow.DataTextField = "Name";
        ddlFlow.DataValueField = "Id";
        ddlFlow.DataBind();


        ddlFlow.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
        ddlFlow.SelectedIndex = 0;
    }
    
    protected void ddlFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFlow.SelectedIndex > 0)
        {
            long flowId = 0;
            long.TryParse(ddlFlow.SelectedItem.Value, out flowId);
            TaskInfoCollection taskInfoCollection  = TaskManager.GetTaskByDocument(flowId);
            rptTemplate.DataSource = taskInfoCollection;
            rptTemplate.DataBind();
        }
    }
    protected string DocPathMethod(string fileName)
    {
        string path = "about:blank";

        if (!string.IsNullOrEmpty(fileName) && FileManager.FileExists(FileType.Passport, fileName))
        {
             path = FileManager.GetSourceDirectory(FileType.Passport, fileName);
        }
      
        return path;

    }

    protected string LocalDocPathMethod(string fileName)
    {
        string path = "#";
        string localPath = "";

        if (!string.IsNullOrEmpty(fileName))
        {
            path = string.Format("../../{0}/{1}/{2}/{3}/{4}",
                WebConfigKeys.UploadImageRootDirectory,
                FileType.Passport,
                fileName.Substring(0, 1),
                fileName.Substring(0, 2),
                fileName);

            localPath = Server.MapPath(path);

            if (!File.Exists(localPath))
            {
                path = "#";
            }
            else
            {
                path = localPath;
            }
        }

        return path;

    }

    public void textwriter(DocumentInfo documentInfo)
    {
        string fileNameExisting = LocalDocPathMethod(documentInfo.Primary);
        string fileNameNew = LocalDocPathMethod(documentInfo.AltDescription);
        string approvalString = "";
        long totalRows = 0;
        TaskSearchParams taskSearchParams = new TaskSearchParams();
        taskSearchParams.CurrentPage = 1;
        taskSearchParams.PageSize = 100;
        taskSearchParams.SortColumn = "";
        taskSearchParams.SortOrder = "";
        taskSearchParams.DocumentId = documentInfo.Id;
        taskSearchParams.TaskStatusId = 0 ;
        TaskInfoCollection finishedTaskInfoCollection = TaskManager.Search(taskSearchParams, out totalRows);
        long rowcount =0;
        TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
        templateInfoCollection = TemplateManager.GetTemplateByDocumentId(documentInfo.Id);
        foreach (TemplateInfo templateInfo in templateInfoCollection)
        {
            if (templateInfo.Active == true)    
            rowcount++;
        }
        rowcount = rowcount + 1;
        System.Drawing.Point p = new System.Drawing.Point(5, 580);
        AddTextToPdf(fileNameExisting, fileNameNew, approvalString, p, finishedTaskInfoCollection, documentInfo, rowcount);

    }

    public void AddTextToPdf(string inputPdfPath, string outputPdfPath, string textToAdd, System.Drawing.Point point, TaskInfoCollection taskInfoCollection,DocumentInfo documentInfo,long totalRows)
    {
        if (inputPdfPath.Contains("#") || !File.Exists(inputPdfPath))
        {
             return;
        }

        string pathin = inputPdfPath;
        string pathout = outputPdfPath;
        int i = Convert.ToInt32(totalRows);
        using (PdfReader reader = new PdfReader(pathin))
        using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create)))
        {

            reader.SelectPages("1");
            var pageSize = reader.GetPageSize(1);
            string path = "../../Fonts/AAutoSignature-1GD9j.ttf";
            string lpath = getLocalPath(path);

            PdfContentByte pbover = stamper.GetOverContent
                (1);
            Font sigFont = new Font(BaseFont.CreateFont(lpath, BaseFont.CP1257, BaseFont.EMBEDDED), 6);
            Font regFont = new Font(BaseFont.CreateFont("../../Fonts/TIMES.TTF", BaseFont.MACROMAN, BaseFont.EMBEDDED), 6);

            PdfPTable table = new PdfPTable(i);
            int x = point.X;
            int y = point.Y;
            x = (int)(pageSize.Width);
            y = (int)(pageSize.Height - 18);

            //foreach (TaskInfo taskInfo in taskInfoCollection)
            //{
            //    if (taskInfo.LevelId == 1)
            //    {
            //        PdfPCell cell = new PdfPCell(new Phrase("Created By", regFont));
            //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //        cell.Border = Rectangle.NO_BORDER;
            //        table.AddCell(cell);
            //    }
            //    else
            //    {
            //        if (taskInfo.ApprovalStatusId == 2)
            //        {
            //            PdfPCell cell = new PdfPCell(new Phrase("Rejected By", regFont));
            //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //            cell.Border = Rectangle.NO_BORDER;
            //            table.AddCell(cell);
            //        }
            //        if (taskInfo.ApprovalStatusId == 4)
            //        {
            //            PdfPCell cell = new PdfPCell(new Phrase("Returned By", regFont));
            //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //            cell.Border = Rectangle.NO_BORDER;
            //            table.AddCell(cell);
            //        }
            //        if (taskInfo.ApprovalStatusId == 1)
            //        {
            //            if (taskInfo.LevelId == 1)
            //            {

            //                PdfPCell cell = new PdfPCell(new Phrase("", regFont));
            //                cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //                cell.Border = Rectangle.NO_BORDER;
            //                table.AddCell(cell);
            //            }
            //            string s = "";
            //            TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
            //            templateInfoCollection = TemplateManager.GetTemplateByDocumentId(documentInfo.Id);
            //            foreach (TemplateInfo templateInfo in templateInfoCollection)
            //            {
            //                if (templateInfo.LevelId == taskInfo.LevelId)
            //                {
            //                    if (templateInfo.Active == true)
            //                    {
            //                        s = taskInfo.ApproveName.ToString();
            //                        PdfPCell cell = new PdfPCell(new Phrase(s, regFont));
            //                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //                        cell.Border = Rectangle.NO_BORDER;
            //                        table.AddCell(cell);
            //                    }
            //                    else
            //                    {
            //                        s = taskInfo.UserAltDescription;

            //                        x = x - 30;
            //                        ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(s, sigFont), x, y, 0);
            //                        //PdfPCell cell = new PdfPCell(new Phrase("", sigFont));
            //                        //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //                        //cell.Border = Rectangle.NO_BORDER;
            //                        //table.AddCell(cell);

            //                    }
            //                }
            //            }

            //        }
            //    }

            //}

            foreach (TaskInfo taskInfo in taskInfoCollection)
            {
                if (taskInfo.ApprovalStatusId == 1)
                {
                    string s = "";
                    TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
                    templateInfoCollection = TemplateManager.GetTemplateByDocumentId(documentInfo.Id);
                    foreach (TemplateInfo templateInfo in templateInfoCollection)
                    {
                        if (templateInfo.LevelId == taskInfo.LevelId)
                        {
                            if (templateInfo.Active == false)
                            {
                                s = taskInfo.UserAltDescription;
                                x = x - 30;
                                ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(s, sigFont), x, y, 0);
                            }
                            else
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDescription, sigFont));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                table.AddCell(cell);
                            }
                        }

                    }
                }

            }


            foreach (TaskInfo taskInfo in taskInfoCollection)
            {
                if (taskInfo.LevelId == 1)
                {

                    PdfPCell cell = new PdfPCell(new Phrase("", regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                else
                {
                    TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
                    templateInfoCollection = TemplateManager.GetTemplateByDocumentId(documentInfo.Id);
                    foreach (TemplateInfo templateInfo in templateInfoCollection)
                    {
                        if (templateInfo.LevelId == taskInfo.LevelId)
                        {
                            if (templateInfo.Active == true)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDescription, sigFont));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                table.AddCell(cell);
                            }
                            //else
                            //{
                            //    PdfPCell cell = new PdfPCell(new Phrase("", sigFont));
                            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            //    cell.Border = Rectangle.NO_BORDER;
                            //    table.AddCell(cell);
                            //}
                        }
                    }

                }
            }

            foreach (TaskInfo taskInfo in taskInfoCollection)
            {

                if (taskInfo.LevelId == 1)
                {

                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserFullName, regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                else
                {
                    TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
                    templateInfoCollection = TemplateManager.GetTemplateByDocumentId(documentInfo.Id);
                    foreach (TemplateInfo templateInfo in templateInfoCollection)
                    {
                        if (templateInfo.LevelId == taskInfo.LevelId)
                        {
                            if (templateInfo.Active == true)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserFullName, regFont));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                table.AddCell(cell);
                            }
                            //else
                            //{
                            //    PdfPCell cell = new PdfPCell(new Phrase("", regFont));
                            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            //    cell.Border = Rectangle.NO_BORDER;
                            //    table.AddCell(cell);
                            //}
                        }
                    }
                }

            }


            foreach (TaskInfo taskInfo in taskInfoCollection)
            {
                if (taskInfo.LevelId == 1)
                {

                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDesignationName, regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                    table.AddCell(cell);

                }
                else
                {
                    TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
                    templateInfoCollection = TemplateManager.GetTemplateByDocumentId(documentInfo.Id);
                    foreach (TemplateInfo templateInfo in templateInfoCollection)
                    {
                        if (templateInfo.LevelId == taskInfo.LevelId)
                        {
                            if (templateInfo.Active == true)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDesignationName, regFont));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                table.AddCell(cell);
                            }
                            //else
                            //{
                            //    PdfPCell cell = new PdfPCell(new Phrase("", regFont));
                            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            //    cell.Border = Rectangle.NO_BORDER;
                            //    table.AddCell(cell);
                            //}
                        }
                    }

                }
            }

            foreach (TaskInfo taskInfo in taskInfoCollection)
            {

                if (taskInfo.LevelId == 1)
                {

                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UpdatedTime.ToShortDateString(), regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                else
                {
                    TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
                    templateInfoCollection = TemplateManager.GetTemplateByDocumentId(documentInfo.Id);
                    foreach (TemplateInfo templateInfo in templateInfoCollection)
                    {
                        if (templateInfo.LevelId == taskInfo.LevelId)
                        {
                            if (templateInfo.Active == true)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UpdatedTime.ToShortDateString(), regFont));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                table.AddCell(cell);
                            }
                            //else
                            //{
                            //    PdfPCell cell = new PdfPCell(new Phrase("", regFont));
                            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            //    cell.Border = Rectangle.NO_BORDER;
                            //    table.AddCell(cell);
                            //}
                        }
                    }
                }

            }


            table.TotalWidth = 765f;

            table.WriteSelectedRows(0, -1, 15, 60, pbover);
            stamper.FormFlattening = true;
            Response.Redirect("../Document/List.aspx");
        }


    }

    public string getLocalPath(string path)
    {
        return Server.MapPath(path);
    }

  
}