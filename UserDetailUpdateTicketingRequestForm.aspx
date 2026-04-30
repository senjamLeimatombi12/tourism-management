<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserDetailUpdateTicketingRequestForm.aspx.cs" Inherits="Tourism_Management.UserDetailUpdateTicketingRequestForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>User Update Request Form</h2>
    
    <asp:Panel ID="pnlUpdateRequest" runat="server" CssClass="form-panel">
        <table>
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Full Name:" /></td>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="form-control" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblEmail" runat="server" Text="Email:" /></td>
                <td><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPhone" runat="server" Text="Phone Number:" /></td>
                <td><asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" TextMode="Phone" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblDate" runat="server" Text="Date:" /></td>
                <td><asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblIDUpload" runat="server" Text="Upload ID Document:" /></td>
                <td><asp:FileUpload ID="fileID" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblMessage" runat="server" Text="Message:" /></td>
                <td><asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Columns="30" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right">
                    <asp:Button ID="btnSubmitRequest" runat="server" Text="Submit Request" CssClass="btn btn-primary" OnClick="btnSubmitRequest_Click" />
                </td>
            </tr>
        </table>

        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Green" />
    </asp:Panel>
</asp:Content>


