using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace WebApplication6
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public string connectionString;
        public SqlConnection sqlConnection;

        protected void Page_Load(object sender, EventArgs e)
        {
            displayError.Visible = false;
            connectionString = @"Data Source=ids520sp19.database.windows.net;Initial Catalog=shopcart ;User ID=admin1234;Password=Shyam1234";
            Button2.Visible = false;
            Button3.Visible = false;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            DropDownList1.DataSource = null;
            DropDownList1.DataBind();
            String sql = "select * from item";

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlConnection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DropDownList1.DataSource = ds;
            //DropDownList1.DataTextField = ds.Tables[0].Columns["itemNumber"].ToString(); // text field name of table dispalyed in dropdown
            DropDownList1.DataValueField = "itemNumber".ToString();             // to retrive specific  textfield name 
                                                                                //assigning datasource to the dropdownlist
            DropDownList1.DataBind();
            //String inv = DataValueField;

            if (!IsPostBack)
            {
                if (Session["DropDownSelection"] != null)
                {
                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(Convert.ToString(Session["DropDownSelection"])));
                }

            }
            sqlConnection.Close();
            //body.Style[HtmlTextWriterStyle.BackgroundColor] = "#E0F4FA";
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            connectionString = @"Data Source=ids520sp19.database.windows.net;Initial Catalog=shopcart ;User ID=admin1234;Password=Shyam1234";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            GridView1.DataSource = null;
            GridView1.DataBind();
            int sbuilder = 0;
            try
            {
                sbuilder = int.Parse(TextBox1.Text);
            }
            catch (Exception error)
            {
                displayError.Visible = true;

                return;
            }
            String inv = DropDownList1.SelectedValue;

            int inventoryid = sbuilder;
            if (inventoryid < 0)
            {
                displayError.Visible = true;

                return;
            }
            String sql = "select * from item where itemNumber =" + inv + " and inventory >=" + inventoryid;

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sql, sqlConnection);
                    DataTable datatbl = new DataTable();
                    sda.Fill(datatbl);
                    if (datatbl.Rows.Count.Equals(0))
                    {
                        displayError.Visible = true;
                    }
                    else
                    {

                        GridView1.DataSource = datatbl;
                        GridView1.DataBind();
                        // GridView1.Visible = true;
                        Button2.Visible = true;
                        Button3.Visible = true;
                        AddToCart(inv, inventoryid, sqlConnection);

                    }

                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
               // MessageBox.Show(err.Message);
                //System.Windows.MessageBox.Show(err.Message);
            }


            sqlConnection.Close();

        }
        protected void AddToCart(String inv, int inventoryid, SqlConnection sqlConn)
        {
            String customerEmail = Session["emailaddress"].ToString();
            // String email = "'"+customerEmail+"'";
            SqlCommand command = sqlConn.CreateCommand();
            SqlTransaction transaction;
            transaction = sqlConn.BeginTransaction("addToCart");
            command.Connection = sqlConn;
            command.Transaction = transaction;
            try
            {
                command.CommandText =
                    "UPDATE item set inventory=inventory-@inventoryid where itemNumber=@inv";
                command.Parameters.AddWithValue("@inventoryid", inventoryid);
                command.Parameters.AddWithValue("@inv", inv);
                command.ExecuteNonQuery();
                command.CommandText =
                  "if (select count(1) from cartitem where customeremail=@emailaddress and itemnumber =@item) > 0 UPDATE cartItem set quantity= quantity + @quantity where customeremail=@emailaddress and itemnumber=@item else insert into cartitem (customerEmail, itemNumber,quantity) values (@emailaddress,@item,@quantity)";
                command.Parameters.Add("@emailaddress", SqlDbType.VarChar, 255).Value = customerEmail;
                command.Parameters.AddWithValue("@item", inv);
                command.Parameters.AddWithValue("@quantity", inventoryid);

                command.ExecuteNonQuery();
                transaction.Commit();
                return;

            }
            catch (Exception e)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (SqlException ex)
                {
                    if (transaction.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                    }
                }

            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["DropDownSelection"] = DropDownList1.SelectedValue;

        }
    }
}