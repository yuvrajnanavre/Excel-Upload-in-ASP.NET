using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace SwitchData
{
    public class GLSpliter
    {
        public DataTable SwitchData(String FileName, string Extension)
        {
            int LineNo = 0;
            DataTable dtSwitchData = new DataTable();
            try
            {
                //Creating stucture of MRGL temp table for bulk insert                 
                // dtMRGL.Columns.Add("MetadataID");
                dtSwitchData.Columns.Add("TerminalID");
                dtSwitchData.Columns.Add("TransactionNumber");
                dtSwitchData.Columns.Add("Amount");
                dtSwitchData.Columns.Add("TransactionDate");
                dtSwitchData.Columns.Add("ResponseCode");
                dtSwitchData.Columns.Add("Channel");
                dtSwitchData.Columns.Add("CreatedBy");
                dtSwitchData.Columns.Add("CreatedOn");
                dtSwitchData.Columns.Add("ModifiedBy");
                dtSwitchData.Columns.Add("ModifiedOn");
                // Declaring column variable for capturing data from excel data table.

                string TerminalID = string.Empty;
                string TransactionNumber = string.Empty;
                decimal Amount = 0;
                DateTime TransactionDate = new DateTime();
                string ResponseCode = string.Empty;
                string Channel = string.Empty;
                string CreatedBy = string.Empty;
                DateTime CreatedOn = new DateTime();
                string ModifiedBy = string.Empty;
                DateTime ModifiedOn = new DateTime();


                // Excel connection string for getting data from Excel file for .xls and .xlsx files.
                String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + FileName + ";Extended Properties=Excel 8.0;";
                OleDbConnection objConn;
                // string extension = Path.GetExtension(FileName);
                switch (Extension.ToLower())
                {

                    case ".xls": //Excel 97-03
                        //connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\";";
                        // connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text'";
                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1\'";
                        break;
                    case ".xlsx": //Excel 07 or higher
                        //connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GLFilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\";";
                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text'";
                        break;
                }

                objConn = new OleDbConnection(connString);
                objConn.Open();

                DataTable dtExcle = new DataTable();
                dtExcle = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String[] excelSheets = new String[dtExcle.Rows.Count];
                int j = 0;
                foreach (DataRow row in dtExcle.Rows)
                {
                    int InsertCount = 0;
                    int TotalCount = 0;
                    excelSheets[j] = row["TABLE_NAME"].ToString();
                    string Getdatafromsheet1 = "SELECT * FROM [" + excelSheets[0].ToString().Replace("'", "") + "]";

                    OleDbCommand cmd = new OleDbCommand(Getdatafromsheet1, objConn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dtSheet = new DataTable();
                    da.Fill(dtSheet);
                    objConn.Close();
                    TotalCount = dtSheet.Rows.Count;

                    if (dtSheet.Rows.Count >= 1)
                    {
                        for (int k = 1; k < dtSheet.Rows.Count; k++)
                        {
                            LineNo++;
                            try
                            {

                                // Reading values from dataTable and assigning to variables 

                                //TransactionDate = DateTime.ParseExact(dtSheet.Rows[k][0].ToString().Trim(), new[] { "MM/dd/yy", "MM-dd-yy" }, null, DateTimeStyles.None);
                                //Remark = Convert.ToString(dtSheet.Rows[k][1]).Trim();
                                TerminalID = Convert.ToString(dtSheet.Rows[k][0]).Trim();
                                TransactionNumber = Convert.ToString(dtSheet.Rows[k][1]).Trim();
                                Amount = Convert.ToDecimal(dtSheet.Rows[k][2]);
                                TransactionDate = DateTime.ParseExact(dtSheet.Rows[k][3].ToString().Trim(), new[] { "dd/mm/yyyy", "yyyy/mm/dd", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy hh:mm:ss ttt", "d/M/yyyy hh:mm:ss ttt", "d/MM/yyyy hh:mm:ss ttt", "dd/M/yyyy hh:mm:ss ttt", "MM-dd-yy" }, null, DateTimeStyles.None);
                                ResponseCode = Convert.ToString(dtSheet.Rows[k][4]).Trim();
                                Channel = Convert.ToString(dtSheet.Rows[k][5]).Trim();
                                CreatedBy = "";
                                CreatedOn = System.DateTime.Now;
                                ModifiedBy = "";
                                ModifiedOn = System.DateTime.Now;

                                // adding variable values into temp databale 
                                dtSwitchData.Rows.Add(
                                   TerminalID,
                                   TransactionNumber,
                                   Amount,
                                   TransactionDate.ToString("MM-dd-yyyy HH:mm:ss"),
                                   ResponseCode,
                                   Channel,
                                   CreatedBy,
                                   CreatedOn.ToString("MM-dd-yyyy HH:mm:ss"),
                                   ModifiedBy,
                                   ModifiedOn.ToString("MM-dd-yyyy HH:mm:ss")
                                     );

                            }
                            catch (Exception ex)
                            {
                                // FileUploadStatus.Text = "Error: " + ex.Message;     
                            }
                            j++;

                        }

                    }
                    if (dtSwitchData.Rows.Count > 0)
                    {
                        InsertCount = dtSwitchData.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);    
            }
            return dtSwitchData;
        }
    }
}
