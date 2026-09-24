using System;
using System.Data;
using System.Web.UI;
namespace LeaveManagementSystem
{
    public partial class Leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SelectedDate"] != null)
                {
                    TextBox2.Text = Session["SelectedDate"].ToString();
                }
                if (Session["LeaveTable"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Employee Name");
                    dt.Columns.Add("Leave Type");
                    dt.Columns.Add("From Date");
                    dt.Columns.Add("To Date");
                    dt.Columns.Add("Remarks");
                    Session["LeaveTable"] = dt;
                }
            }
            if (Session["LeaveTable"] != null)
            {
                GridView1.DataSource = (DataTable)Session["LeaveTable"];
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a valid Leave Type!');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox2.Text) || string.IsNullOrWhiteSpace(TextBox3.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please fill all required fields!');", true);
                return;
            }

            DateTime fromDate, toDate;
            if (!DateTime.TryParse(TextBox2.Text, out fromDate) || !DateTime.TryParse(TextBox3.Text, out toDate))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Date Format!');", true);
                return;
            }
            if (fromDate.Date < DateTime.Now.Date || toDate.Date < DateTime.Now.Date)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You cannot apply leave for past dates!');", true);
                return;
            }
            if (toDate < fromDate)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('To Date cannot be earlier than From Date!');", true);
                return;
            }

            DataTable dt = (DataTable)Session["LeaveTable"];
            string empName = TextBox1.Text.Trim();
            foreach (DataRow row in dt.Rows)
            {
                if (row["Employee Name"].ToString().Equals(empName, StringComparison.OrdinalIgnoreCase))
                {
                    DateTime existingFrom, existingTo;
                    if (DateTime.TryParse(row["From Date"].ToString(), out existingFrom) &&
                        DateTime.TryParse(row["To Date"].ToString(), out existingTo))
                    {
                        if (fromDate <= existingTo && toDate >= existingFrom)
                        {
                            string msg = $"alert('Overlap Error! {empName} already has leave from {existingFrom.ToShortDateString()} to {existingTo.ToShortDateString()}');";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", msg, true);
                            return;
                        }
                    }
                }
            }
            dt.Rows.Add(empName, DropDownList1.SelectedValue, fromDate.ToShortDateString(), toDate.ToShortDateString(), TextBox4.Text);
            Session["LeaveTable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            TextBox1.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            DropDownList1.SelectedIndex = 0;
            Response.Redirect("Leave.aspx");
        }
    }
}