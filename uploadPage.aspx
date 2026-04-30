<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPage.aspx.cs" Inherits="Tourism_Management.UploadPage" %>



    

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:400px; margin:50px auto; padding:20px; border:1px solid #ccc; border-radius:8px; text-align:center;">
            <h2>Upload a File</h2>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br /><br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <br /><br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
</body>
</html>
