using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using ExcelDataReader;
using ZadHolding.Been;

public class ExcelManager
{
    public static DataTable ReadExcelData(FileUpload File1, string strSheetName, FileType fileType)
    {
        string strExtensionName = "";
        string strFileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);
        DataTable dtt = null;

        try
        {
            if (!string.IsNullOrEmpty(strFileName))
            {
                //get the extension name, check if it's a spreadsheet
                strExtensionName = strFileName.Substring(strFileName.IndexOf(".") + 1);
                if (strExtensionName.Equals("xls", StringComparison.OrdinalIgnoreCase) || strExtensionName.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    /*Import data*/
                    if (File1.PostedFile != null && File1.HasFile)
                    {
                        //upload the file to server
                        // Use temp path for Azure compatibility (avoiding local storage permission issues)
                        string tempPath = Path.GetTempPath();
                        FileInfo file = new FileInfo(File1.PostedFile.FileName);
                        string strServerFileName = System.Guid.NewGuid().ToString() + "-" + file.Name;
                        string strFullPath = Path.Combine(tempPath, strServerFileName);
                        
                        // Ensure directory exists
                        string dirPath = Path.GetDirectoryName(strFullPath);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }

                        File1.PostedFile.SaveAs(strFullPath);

                        // Read using ExcelDataReader
                        using (var stream = File.Open(strFullPath, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });

                                bool blExists = false;
                                if (result.Tables.Count > 0)
                                {
                                    if (result.Tables.Contains(strSheetName))
                                    {
                                        dtt = result.Tables[strSheetName];
                                        blExists = true;
                                    }
                                    else
                                    {
                                        // Try finding by name ignoring case or similar logic if needed, 
                                        // but original code was exact match with $ appended.
                                        // ExcelDataReader usually returns exact sheet names.
                                        // Fallback: check if any table matches
                                        foreach (DataTable table in result.Tables)
                                        {
                                            if (table.TableName.Equals(strSheetName, StringComparison.OrdinalIgnoreCase))
                                            {
                                                dtt = table;
                                                blExists = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (blExists && dtt != null)
                                {
                                    // Filter/Sort logic from original code:
                                    // Original: Select * from [Sheet$] order by ...
                                    // We have the DataTable in memory. We can Sort it using DataView.
                                    DataView dv = dtt.DefaultView;
                                    
                                    if (fileType == FileType.MarketPrice)
                                    {
                                         // Check if columns exist before sorting to avoid crash
                                         if (dtt.Columns.Contains("CompanyCode") && dtt.Columns.Contains("CreatedDate"))
                                            dv.Sort = "CompanyCode, CreatedDate";
                                    }
                                    else if (fileType == FileType.ExchangeRate) 
                                    {
                                        if (dtt.Columns.Contains("CurrencyCode") && dtt.Columns.Contains("CreatedDate"))
                                            dv.Sort = "CurrencyCode, CreatedDate";
                                    }
                                    
                                    dtt = dv.ToTable();

                                    dtt.Columns.Add("Id", typeof(int));
                                    dtt.Columns.Add("IsUpdated", typeof(bool));

                                    for (int i = 0; i < dtt.Rows.Count; i++)
                                    {
                                        dtt.Rows[i]["Id"] = i + 1;
                                        dtt.Rows[i]["IsUpdated"] = true;
                                    }
                                    DataColumn[] keyColumn = new DataColumn[1];
                                    keyColumn[0] = dtt.Columns["Id"];
                                    dtt.PrimaryKey = keyColumn;

                                    dtt.Columns.Add("Status", typeof(string));
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }

        return dtt;
    }
}
