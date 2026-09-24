using System;
using System.Web.UI;
namespace LeaveManagementSystem
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray; // Purani dates grey color ki ho jayengi
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate == DateTime.MinValue)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a date from the calendar first!');", true);
                return;
            }
            Session["SelectedDate"] = Calendar1.SelectedDate.ToShortDateString();
            Response.Redirect("Leave.aspx");
        }
    }
}