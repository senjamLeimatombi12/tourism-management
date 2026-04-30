<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="Tourism_Management.CartPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!DOCTYPE html>
<html>

    <title>Download Ticket</title>
</head>
<body>
    
        <div class="text-center mt-10">
            <asp:Button ID="btnDownloadLatest" runat="server" Text="Download My Latest Ticket"
                OnClick="btnDownloadLatest_Click" CssClass="px-4 py-2 bg-yellow-500 text-white rounded" />
        </div>
  
</body>
</html>

</asp:Content>




