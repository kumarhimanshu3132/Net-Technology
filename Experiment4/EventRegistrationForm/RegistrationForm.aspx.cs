using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace EventRegistrationForm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string cleanName = Regex.Replace(txtName.Text.Trim(), @"\s+", " ");

                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                cleanName = textInfo.ToTitleCase(cleanName.ToLower());
                txtName.Text = cleanName;

                string enrollmentNo = TextBox3.Text;
                string selectedEvent = DropDownList3.SelectedItem.Text;

                lblmessage.Text = $"Registration Successful! Welcome {cleanName} (Enrollment: {enrollmentNo}) to {selectedEvent}.";
                lblmessage.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}