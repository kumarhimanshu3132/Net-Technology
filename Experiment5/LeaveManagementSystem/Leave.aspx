<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="LeaveManagementSystem.Leave" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application Form</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f1f5f9;
            color: #1e293b;
            padding: 30px;
        }
        form {
            max-width: 600px;
            margin: 0 auto;
            background: #ffffff;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }
        h2 {
            color: #1e3a8a;
            border-bottom: 2px solid #e2e8f0;
            padding-bottom: 10px;
            margin-top: 0;
        }
        p {
            margin-bottom: 15px;
            font-weight: 600;
        }
        input[type="text"], select {
            width: 100%;
            padding: 8px;
            margin-top: 5px;
            border: 1px solid #cbd5e1;
            border-radius: 5px;
            box-sizing: border-box;
        }
        .btn-submit {
            background-color: #2563eb;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
        }
        .btn-submit:hover {
            background-color: #1d4ed8;
        }
        table {
            width: 100%;
            margin-top: 15px;
            border-collapse: collapse;
        }
        th, td {
            border: 1px solid #cbd5e1;
            padding: 8px;
            text-align: left;
        }
        th {
            background-color: #1e3a8a;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Leave Application Form</h2>
        <p>
            Employee Name
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p>
            Leave Type
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Selected="True" Value="">--Select Leave Type--</asp:ListItem>
                <asp:ListItem>Sick Leave (SL)</asp:ListItem>
                <asp:ListItem>Casual Leave (CL)</asp:ListItem>
                <asp:ListItem>Privilege Leave (PL)</asp:ListItem>
                <asp:ListItem>Leave Without Pay (LWP)</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            From Date
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <p>
            To Date
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
        <p>
            Remarks
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" CssClass="btn-submit" />
        
        <p style="margin-top: 30px;">
            <strong>Leave History Table</strong>
            <asp:GridView ID="GridView1" runat="server" Width="100%">
            </asp:GridView>
        </p>
    </form>
</body>
</html>