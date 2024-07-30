using AppBlock;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace WebApplication1
{
    public partial class formcreation : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["formcreation"].ToString();
        string sqlconnection = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            sqlconnection = "insert into form(firstname,lastname,address,date) values('" + firstname.Value + "','" + lastname.Value + "','" + address.Value + "',getDate())";
            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlconnection);
            firstname.Value = "";
            lastname.Value = "";
            address.Value = "";
            NoRecords_text.Visible = false;
            filldata();
            btnExportToExcel.Visible = true;
            btnExportToPdf.Visible = true;
            cleardata.Visible = true;

        }

        protected void view_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(con);
            DataTable dtview = new DataTable();
            sqlconnection = "select * from form";
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlconnection, sc);
            da.Fill(dtview);
            sc.Close();

            gridview1.DataSource = dtview;
            gridview1.DataBind();
            int rowCount = dtview.Rows.Count;
            if (rowCount>0)
            {
                cleardata.Visible = true;
                NoRecords_text.Visible = false;
                btnExportToExcel.Visible = true;
                btnExportToPdf.Visible = true;
            }
            else
            {
                cleardata.Visible = false;
                NoRecords_text.Visible = true;
                btnExportToExcel.Visible = false;
                btnExportToPdf.Visible = false;
            }

        }
        public void filldata()
        {
            SqlConnection sc = new SqlConnection(con);
            DataTable dtview = new DataTable();
            sqlconnection = "select * from form";
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlconnection, sc);
            da.Fill(dtview);
            sc.Close();

            gridview1.DataSource = dtview;
            gridview1.DataBind();

        }

        protected void cleardata_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(con);
            DataSet ds = new DataSet();
            sqlconnection = "truncate table form";
            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlconnection);
            //filldata();
            cleardata.Visible = false;
            btnExportToExcel.Visible = false;
            btnExportToPdf.Visible = false;
            NoRecords_text.Visible = true;
            gridview1.Visible = false; 


        }

        protected void reset_Click(object sender, EventArgs e)
        {
            firstname.Value = "";
            lastname.Value = "";
            address.Value = "";
        }

        protected void gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            Label label_name =(Label) gridview1.Rows[e.RowIndex].FindControl("col1") as Label;
            SqlConnection sc = new SqlConnection(con);
            DataTable dtview = new DataTable();
            sqlconnection = "delete from form where firstname='" + label_name.Text + "'";
            SqlHelper.ExecuteNonQuery(sc, CommandType.Text, sqlconnection);
            filldata();
            int rowCount = dtview.Rows.Count;
            if (rowCount == 0)
            {
                NoRecords_text.Attributes.Add("style", "display:block");
                cleardata.Visible = false;
                btnExportToExcel.Visible = false;
                btnExportToPdf.Visible = false;
            }
        }

        protected void gridview1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridview1.EditIndex = e.NewEditIndex;
            {
                filldata();
            }
            
           

        }

        protected void gridview1_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
            TextBox user = (TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol10") as TextBox); 
            Label label_name = (Label)gridview1.Rows[e.RowIndex].FindControl("user_id") as Label;
            Label update = (Label)(gridview1.Rows[e.RowIndex].FindControl("user_id") as Label);
            TextBox fname =(TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol1") as TextBox);
            TextBox lname = (TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol2") as TextBox);
            TextBox address = (TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol3") as TextBox);
            Label date = (Label)(gridview1.Rows[e.RowIndex].FindControl("date") as Label);
            SqlConnection sc = new SqlConnection(con);
            int ii = Convert.ToInt32(user.Text);
            sqlconnection = "update  form set firstname='" + fname.Text + "',lastname='" + lname.Text + "',address='" + address.Text + "' where user_id='" + ii + "' ";
            sc.Open();
            SqlHelper.ExecuteNonQuery(sc, CommandType.Text, sqlconnection);
            sc.Close();
            gridview1.EditIndex = -1;
            filldata();
            



        }

        protected void gridview1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridview1.EditIndex = -1;
            filldata();
        }
        protected void btnExportToPdf_Click(object sender, EventArgs e)
        {
            //creating an sql connection object
            SqlConnection sc = new SqlConnection(con);
            //sql command
            sqlconnection = "select* from form";
            //interact with database
            SqlDataAdapter da = new SqlDataAdapter(sqlconnection, sc);
            //can hold multiple tables but fill one table
            DataSet ds = new DataSet();
            da.Fill(ds);
           // We extract the first table
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //preparing the  response
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=form.pdf");
            Response.ContentType = "application/pdf";
            //Creating HTML for the PDF
            StringWriter sw = new StringWriter();
            sw.Write("<table border=1>");
            sw.Write("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sw.Write("<th>" + column.ColumnName + "</th>");
            }
            sw.Write("</tr>");
            foreach (DataRow row in dt.Rows)
            {
                sw.Write("<tr>");

                foreach (var values in row.ItemArray)
                {
                    sw.Write("<td>" + values.ToString() + "</td>");
                }
                sw.Write("</tr>");
            }
            sw.Write("</table>");
            //string reader reads from string 
            StringReader sr = new StringReader(sw.ToString());
            Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker hw = new HTMLWorker(pdfdoc);
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
            pdfdoc.Open();
            hw.Parse(sr);
            pdfdoc.Close();
            Response.Write(pdfdoc);
            Response.End();
        }
        //protected void btnExportToPdf_Click(object sender, EventArgs e)
        //{
        //    SqlConnection sc = new SqlConnection(con);
        //    string sqlconnection = "select * from form";
        //    SqlDataAdapter da = new SqlDataAdapter(sqlconnection, sc);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    DataTable dt = ds.Tables[0];

        //    // Set the filename for the downloaded file
        //    string fileName = "form.pdf";

        //    // Clear any content from the response
        //    Response.Clear();
        //    Response.Buffer = true;

        //    // Set the content-disposition header to force download
        //    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        //    Response.Charset = "";
        //    Response.ContentType = "application/pdf";

        //    // Create a StringWriter to render the HTML content into PDF
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);

        //    // Render the HTML table containing the data
        //    GridView GridView1 = new GridView(); // Assuming you have a GridView for display
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //    GridView1.RenderControl(hw);

        //    // Use iTextSharp to convert the HTML to PDF
        //    StringReader sr = new StringReader(sw.ToString());
        //    Document pdfDoc = new Document(PageSize.A4);
        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();

        //    // Write the PDF content to the response
        //    Response.Write(pdfDoc);
        //    Response.Flush();
        //    Response.End();
        //}


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(con);

            sqlconnection = "select * from form";
            SqlDataAdapter da = new SqlDataAdapter(sqlconnection, sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment;filename=form.xls");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            //table
            sw.Write("<table border='1'>");

            //colname
            sw.Write("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sw.Write("<th>" + column.ColumnName + "</th>");

            }
            sw.Write("</tr>");
            // rows
            foreach (DataRow row in dt.Rows)
            {
                sw.Write("<tr>");
                //each data
                foreach (var item in row.ItemArray)
                {
                    sw.Write("<td>" + item.ToString() + "</td>");
                }
                sw.Write("</tr>");
            }
            sw.Write("<table>");


            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}