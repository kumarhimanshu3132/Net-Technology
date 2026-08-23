<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationForm.aspx.cs" Inherits="EventRegistrationForm.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">Name</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is required!" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Only single space allowed between name and title!" ForeColor="Red" ValidationExpression="^[a-zA-Z]+( [a-zA-Z]+)*$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Gr. No.</td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Gr. No. is required" ControlToValidate="TextBox2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="Gr. No. must be 6 digits!" ForeColor="Red" ValidationExpression="^\d{6}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Enrollment No.</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enrollment No. is required!" ControlToValidate="TextBox3" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="Enrollment No. must be 11 digits!" ForeColor="Red" ValidationExpression="^\d{11}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Gender</td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Gender is required!" ControlToValidate="RadioButtonList1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Email Address</td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Email is required!" ControlToValidate="TextBox4" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox4" Display="Dynamic" ErrorMessage="Invalid Email Format!" ForeColor="Red" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Phone Number</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Phone Number is required!" ControlToValidate="TextBox5" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox5" Display="Dynamic" ErrorMessage="10 digits starting with 6-9 required!" ForeColor="Red" ValidationExpression="^[6-9]\d{9}$"></asp:RegularExpressionValidator>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Department is required!" ControlToValidate="DropDownList1" Display="Dynamic" ForeColor="Red" InitialValue="--Select Department--"></asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Semester is required!" ControlToValidate="DropDownList2" Display="Dynamic" ForeColor="Red" InitialValue="--Semester--"></asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Event is required!" ControlToValidate="DropDownList3" Display="Dynamic" ForeColor="Red" InitialValue="--Select Event--"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
