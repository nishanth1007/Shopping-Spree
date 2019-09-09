<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication6.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shooping Cart</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                        <br /> <br />
            <h4> <center> Enter Item Number: </center> </h4>
            <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AppendDataBoundItems="true" >
        </asp:DropDownList>
    
        <p>
            <h4> <center> Enter quantity: </center> </h4>
            <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add to Cart" />
            <asp:Label ID="displayError" runat="server" Text="Invalid quantity" ForeColor="Red" ></asp:Label>
            <asp:Button ID="Button3" runat="server" Text="Cancel Cart" />
            <asp:Button ID="Button2" runat="server" Text="Checkout" />
        </p>
        
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        </div>
    </form>
</body>
</html>
