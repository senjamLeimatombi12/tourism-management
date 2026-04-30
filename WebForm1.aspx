<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Tourism_Management.WebForm1" %>



<!DOCTYPE html>
<html>
<head runat="server">
    <title>Ticket Generator</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Upload Ticket Template</h2>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnGenerate" runat="server" Text="Generate Ticket" OnClick="btnGenerate_Click" />
            <br /><br />
            <asp:Label ID="lblMessage" runat="server" />
            <br />
            <asp:Image ID="imgPreview" runat="server" />
        </div>
    </form>
</body>
</html>
