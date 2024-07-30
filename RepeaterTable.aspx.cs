using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class RepeaterTable : System.Web.UI.Page

    {
        string sqlcon = ConfigurationManager.ConnectionStrings["formcreation"].ToString();
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //creating an sql connection
            SqlConnection sc = new SqlConnection(sqlcon);
            //creating an new Datatable
            DataTable dt = new DataTable();
            //sql query
            sql = "select * from form";
            //opening connection
            sc.Open();
            //interact with database execute query and returns the data 
            SqlDataAdapter da = new SqlDataAdapter(sql, sc);
            //fill the data into datatable
            da.Fill(dt);
            repeaterview.DataSource = dt;
            repeaterview.DataBind();
            if (dt.Rows.Count > 0)
            {
                repeaterview.Visible = true;
                NoRecords.Visible = false;
            }
            else
            {
                NoRecords.Visible = true;
                repeaterview.Visible = false;
            }


        
    }
       
    }
}
