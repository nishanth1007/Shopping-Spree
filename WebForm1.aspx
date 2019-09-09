<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication6.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Login"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Loginbtn" runat="server" Text="Button" OnClick="Loginbtn_Click" />
            <br />
            <br />
            <asp:Label ID="errormessagelbl" runat="server" Text="Invalid Email Address" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
