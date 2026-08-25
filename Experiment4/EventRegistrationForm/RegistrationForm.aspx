<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="RegistrationForm.aspx.cs" Inherits="EventRegistrationForm.WebForm1"
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Event Registration Portal</title>
    <style type="text/css">
      /* Global Styles */
      body {
        font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
        background-color: #e9ecef;
        color: #333;
      }
      .form-container {
        max-width: 650px;
        margin: 50px auto;
        padding: 35px;
        background: #ffffff;
        border-radius: 10px;
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.15);
      }
      .auto-style1 {
        width: 100%;
        border-collapse: separate;
        border-spacing: 0 18px;
      }
      td {
        vertical-align: middle;
        font-weight: 500;
      }
      td:first-child {
        width: 30%;
        color: #495057;
      }
      input[type="text"],
      select {
        width: 100%;
        padding: 10px 12px;
        border: 1px solid #ced4da;
        border-radius: 5px;
        box-sizing: border-box;
        font-size: 14px;
        transition:
          border-color 0.3s ease,
          box-shadow 0.3s ease;
      }
      input[type="text"]:focus,
      select:focus {
        border-color: #80bdff;
        outline: none;
        box-shadow: 0 0 5px rgba(0, 123, 255, 0.25);
      }
      input[type="radio"] {
        margin-right: 5px;
        margin-left: 10px;
        transform: scale(1.1);
      }
      input[type="radio"]:first-child {
        margin-left: 0;
      }
      label {
        font-weight: normal;
        margin-right: 15px;
      }
      input[type="submit"] {
        background-color: #0d6efd;
        color: white;
        padding: 12px 20px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 16px;
        font-weight: bold;
        width: 100%;
        transition: background-color 0.3s;
      }
      input[type="submit"]:hover {
        background-color: #0b5ed7;
      }
    </style>
  </head>
  <body>
    <form id="form1" runat="server">
      <div class="form-container">
        <h2
          style="
            text-align: center;
            color: #0d6efd;
            margin-bottom: 25px;
            margin-top: 0;
          "
        >
          Event Registration
        </h2>
        <table class="auto-style1">
          <tr>
            <td>Name</td>
            <td>
              <asp:TextBox
                ID="txtName"
                runat="server"
                placeholder="Enter your full name"
              ></asp:TextBox>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1"
                runat="server"
                ErrorMessage="Name is required!"
                ControlToValidate="txtName"
                Display="Dynamic"
                ForeColor="Red"
              ></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator
                ID="RegularExpressionValidator2"
                runat="server"
                ControlToValidate="txtName"
                Display="Dynamic"
                ErrorMessage="Only single space allowed!"
                ForeColor="Red"
                ValidationExpression="^[a-zA-Z]+( [a-zA-Z]+)*$"
              ></asp:RegularExpressionValidator>
            </td>
          </tr>
          <tr>
            <td>Gr. No.</td>
            <td>
              <asp:TextBox
                ID="TextBox2"
                runat="server"
                placeholder="Enter your Gr. No."
              ></asp:TextBox>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator2"
                runat="server"
                ErrorMessage="Gr. No. is required"
                ControlToValidate="TextBox2"
                Display="Dynamic"
                ForeColor="Red"
              ></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator
                ID="RegularExpressionValidator3"
                runat="server"
                ControlToValidate="TextBox2"
                Display="Dynamic"
                ErrorMessage="Gr. No. must be 6 digits!"
                ForeColor="Red"
                ValidationExpression="^\d{6}$"
              ></asp:RegularExpressionValidator>
            </td>
          </tr>
          <tr>
            <td>Enrollment No.</td>
            <td>
              <asp:TextBox
                ID="TextBox3"
                runat="server"
                placeholder="Enter your enrollment no."
              ></asp:TextBox>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator3"
                runat="server"
                ErrorMessage="Enrollment is required!"
                ControlToValidate="TextBox3"
                Display="Dynamic"
                ForeColor="Red"
              ></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator
                ID="RegularExpressionValidator4"
                runat="server"
                ControlToValidate="TextBox3"
                Display="Dynamic"
                ErrorMessage="Enrollment must be 11 digits!"
                ForeColor="Red"
                ValidationExpression="^\d{11}$"
              ></asp:RegularExpressionValidator>
            </td>
          </tr>
          <tr>
            <td>Gender</td>
            <td style="white-space: nowrap">
              <asp:RadioButtonList
                ID="RadioButtonList1"
                runat="server"
                RepeatDirection="Horizontal"
                RepeatLayout="Flow"
              >
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
              </asp:RadioButtonList>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator4"
                runat="server"
                ErrorMessage="Gender is required!"
                ControlToValidate="RadioButtonList1"
                Display="Dynamic"
                ForeColor="Red"
              ></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td>Email Address</td>
            <td>
              <asp:TextBox
                ID="TextBox4"
                runat="server"
                placeholder="Enter your email"
              ></asp:TextBox>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator5"
                runat="server"
                ErrorMessage="Email is required!"
                ControlToValidate="TextBox4"
                Display="Dynamic"
                ForeColor="Red"
              ></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator
                ID="RegularExpressionValidator5"
                runat="server"
                ControlToValidate="TextBox4"
                Display="Dynamic"
                ErrorMessage="Invalid Email Format!"
                ForeColor="Red"
                ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
              ></asp:RegularExpressionValidator>
            </td>
          </tr>
          <tr>
            <td>Phone Number</td>
            <td>
              <asp:TextBox
                ID="TextBox5"
                runat="server"
                placeholder="Enter your phone number"
              ></asp:TextBox>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator6"
                runat="server"
                ErrorMessage="Phone Number is required!"
                ControlToValidate="TextBox5"
                Display="Dynamic"
                ForeColor="Red"
              ></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator
                ID="RegularExpressionValidator6"
                runat="server"
                ControlToValidate="TextBox5"
                Display="Dynamic"
                ErrorMessage="Starts with 6-9, 10 digits!"
                ForeColor="Red"
                ValidationExpression="^[6-9]\d{9}$"
              ></asp:RegularExpressionValidator>
            </td>
          </tr>
          <tr>
            <td>Department</td>
            <td>
              <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>--Select Department--</asp:ListItem>
                <asp:ListItem>CE</asp:ListItem>
                <asp:ListItem>CSE</asp:ListItem>
                <asp:ListItem>CSE-AI</asp:ListItem>
                <asp:ListItem>CSE-AI/ML</asp:ListItem>
                <asp:ListItem>CSE-AI/DS</asp:ListItem>
                <asp:ListItem>ECE</asp:ListItem>
                <asp:ListItem>IT</asp:ListItem>
                <asp:ListItem>ICT</asp:ListItem>
              </asp:DropDownList>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator7"
                runat="server"
                ErrorMessage="Department is required!"
                ControlToValidate="DropDownList1"
                Display="Dynamic"
                ForeColor="Red"
                InitialValue="--Select Department--"
              ></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td>Current Semester</td>
            <td>
              <asp:DropDownList ID="DropDownList2" runat="server">
                <asp:ListItem>--Semester--</asp:ListItem>
                <asp:ListItem>I</asp:ListItem>
                <asp:ListItem>II</asp:ListItem>
                <asp:ListItem>III</asp:ListItem>
                <asp:ListItem>IV</asp:ListItem>
                <asp:ListItem>V</asp:ListItem>
                <asp:ListItem>VI</asp:ListItem>
                <asp:ListItem>VII</asp:ListItem>
                <asp:ListItem>VIII</asp:ListItem>
              </asp:DropDownList>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator8"
                runat="server"
                ErrorMessage="Semester is required!"
                ControlToValidate="DropDownList2"
                Display="Dynamic"
                ForeColor="Red"
                InitialValue="--Semester--"
              ></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td>Select Main Event</td>
            <td>
              <asp:DropDownList ID="DropDownList3" runat="server">
                <asp:ListItem>--Select Event--</asp:ListItem>
                <asp:ListItem>AI Workshop</asp:ListItem>
                <asp:ListItem>Blind Coding</asp:ListItem>
                <asp:ListItem>IPL Auction</asp:ListItem>
                <asp:ListItem>Music Competition</asp:ListItem>
                <asp:ListItem>Drama</asp:ListItem>
              </asp:DropDownList>
            </td>
            <td>
              <asp:RequiredFieldValidator
                ID="RequiredFieldValidator9"
                runat="server"
                ErrorMessage="Event is required!"
                ControlToValidate="DropDownList3"
                Display="Dynamic"
                ForeColor="Red"
                InitialValue="--Select Event--"
              ></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>
              <asp:Button
                ID="Button1"
                runat="server"
                Text="Register Now"
                OnClick="Button1_Click"
              />
            </td>
            <td></td>
          </tr>
        </table>
      </div>
    </form>
  </body>
</html>
