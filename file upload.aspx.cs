using AppBlock;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class file_upload : System.Web.UI.Page
    {
        string sqlcon = ConfigurationManager.ConnectionStrings["formcreation"].ToString();
        string sql = "";


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                // checks whether the file is uploaded
                if (UploadFileAsCSV.HasFile)
                {
                    string path;
                    string connection = string.Empty;
                    string filename = Path.GetFileName(UploadFileAsCSV.FileName);
                    string fileExtension = Path.GetExtension(filename.ToLower());
                    if (fileExtension != ".xls" && fileExtension != ".xlsx" && fileExtension != ".csv")
                    {
                        string script = "alert('Invalid file format. Please upload an Excel file'); window.location.href = 'file upload.aspx';";
                        ClientScript.RegisterStartupScript(GetType(), "alert", script, true);
                        return;

                    }
                    else
                    {
                        path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            //if folder not exists it will create an directory
                            Directory.CreateDirectory(path);
                        }
                        else
                        {
                            filename = Path.GetFileName(UploadFileAsCSV.FileName);
                            //string newfile = filename.Replace(filename, "UpdatedEmandatefile.xls");
                            UploadFileAsCSV.SaveAs(Server.MapPath("~/Uploads/") + filename);
                            //alert for upload sucess
                            label.Text = "File uploaded successfully";
                            label.ForeColor = System.Drawing.Color.Green;
                            label.Visible = true;
                            //Excel Connection
                            //  ExcelConnection(path);
                            // con.Open();
                            // reads excel data
                            path = Server.MapPath("~/Uploads/" + filename);
                            //DataTable dt = ReadExcelRecords(path, connection);
                            string script;
                            if (fileExtension == ".csv")
                            {
                                DataTable dt = ReadCSVRecords(path);
                                insertDataIntoDatabase(dt);
                                script = "alert('csv Record entered into database Successfully!!!');";
                                Response.Redirect("/RepeaterTable.aspx");

                            }

                            else
                            {
                                DataTable dt = ReadEXCELRecords(path, connection);
                                insertDataIntoDatabase(dt);
                                script = "alert('Excel Record entered into database Successfully!!!');";
                                Response.Redirect("/RepeaterTable.aspx");
                            }


                            ClientScript.RegisterStartupScript(GetType(), "alert", script, true);

                        }
                    }



                }
            }
            catch (Exception ex)
            {
                label.Text = "File uploaded Failed";
                label.ForeColor = System.Drawing.Color.Red;
                label.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Error uploading file: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        protected DataTable ReadCSVRecords(string path)
        {
            try
            {
                //creating an datatable
                DataTable dt = new DataTable();
                // creating column for CSV RECORDS
                dt.Columns.Add("user_id", typeof(int));
                dt.Columns.Add("firstname", typeof(string));
                dt.Columns.Add("lastname", typeof(string));
                dt.Columns.Add("date", typeof(string));
                dt.Columns.Add("address", typeof(string));
                dt.Columns.Add("phonenumber", typeof(long));
                // get file name
                string filename = Path.GetFileName(UploadFileAsCSV.FileName);
                // get file path
                path = Server.MapPath("~/Uploads/" + filename);
                // read text form csv file
                string ReadCSV = File.ReadAllText(path);
                // spliting row after newline
                foreach (string csvRow in ReadCSV.Split('\n')) // csvRow - single row of csv file, ReadCSV.Split('\n')-splits the string into an array of rows based on newline characters.
                {
                    // cheacking the row notEqual to empty or null
                    if (!string.IsNullOrEmpty(csvRow))
                    {
                        //adding each row
                        dt.Rows.Add();

                        // Initialize an variable for the column index
                        int count = 0;
                        // iterate each value in current row
                        foreach (string currentRow in csvRow.Split(','))
                        {

                            dt.Rows[dt.Rows.Count - 1][count] = currentRow;//[dt.Rows.Count - 1]= row index [count]=column index
                            count++;
                        }
                    }
                }
                return dt;




            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Error uploading file: " + ex.Message.Replace("'", "\\'") + "');", true);
                return null;
            }

        }

        protected DataTable ReadEXCELRecords(string path, string connection)
        {

            try
            {
                string filename = Path.GetFileName(UploadFileAsCSV.FileName);
                string fileExtension = Path.GetExtension(filename.ToLower());
                //oledb connection
                if (fileExtension == ".xls")
                {
                    connection = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={path};Extended Properties='Excel 8.0;HDR=YES'";
                }
                else if (fileExtension == ".xlsx")
                {
                    connection = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties='Excel 12.0 Xml;HDR=YES '";
                }

                OleDbConnection con = new OleDbConnection(connection);
                con.Open();
                //creating an new data table
                DataTable dt = new DataTable();
                //creating new sheet and storing table
                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                if (sheets == null || sheets.Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('No sheets found in the Excel file.');", true);
                    return null; // Return null if no sheets are found
                }

                OleDbCommand cmd = new OleDbCommand($"select * from [{sheetName}]", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Error uploading file: " + ex.Message.Replace("'", "\\'") + "');", true);
                return null;
            }
        }


        protected void insertDataIntoDatabase(DataTable dt)
        {
            SqlConnection sc = new SqlConnection(sqlcon);
            sc.Open();

            //Iteration using foreachloop
            foreach (DataRow row in dt.Rows)
            {
                SqlCommand sqlCMD = new SqlCommand("INSERT INTO form (firstname,lastname,date,address,phonenumber) VALUES(@firstname,@lastname,@date,@address,@phonenumber)", sc);

                //add parameters to the sql command
                sqlCMD.Parameters.AddWithValue("@firstname", row["firstname"]);
                sqlCMD.Parameters.AddWithValue("@lastname", row["lastname"]);
                sqlCMD.Parameters.AddWithValue("@date", Convert.ToDateTime(row["date"]));
                sqlCMD.Parameters.AddWithValue("@address", row["address"]);
                sqlCMD.Parameters.AddWithValue("@phonenumber", row["phonenumber"]);


                sqlCMD.ExecuteNonQuery();
            }
            // Read data form DataTable and Insert data into the database(bulk upload method)

            //SqlBulkCopy bulkCopy = new SqlBulkCopy(sc);

            //// set the destination table name
            //bulkCopy.DestinationTableName = "form";

            ////map the sql dataTable colums to the sqlserver table columns
            //bulkCopy.ColumnMappings.Add("firstname", "firstname");
            //bulkCopy.ColumnMappings.Add("lastname", "lastname");
            //bulkCopy.ColumnMappings.Add("date", "date");
            //bulkCopy.ColumnMappings.Add("address", "address");

            ////write from the data table to the database
            //bulkCopy.WriteToServer(dt);


        }

        protected void viewbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/RepeaterTable.aspx");
        }
    }

    }
