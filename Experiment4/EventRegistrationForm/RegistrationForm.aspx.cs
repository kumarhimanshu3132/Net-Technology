using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.UI;

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
                //Process Name (Space trimming aur Title Case)
                string rawName = txtName.Text.Trim();
                rawName = Regex.Replace(rawName, @"\s+", " ");
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string formattedName = textInfo.ToTitleCase(rawName.ToLower());

                //Generate Pop-up Message (Alert)
                string alertMessage = $"Registration Successful!\\nWelcome {formattedName}.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);

                //Auto-Clear the Form
                txtName.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBox3.Text = string.Empty;
                TextBox4.Text = string.Empty;
                TextBox5.Text = string.Empty;

                RadioButtonList1.ClearSelection();
                DropDownList1.SelectedIndex = 0;
                DropDownList2.SelectedIndex = 0;
                DropDownList3.SelectedIndex = 0;
            }
        }
    }
}