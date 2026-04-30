<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Tourism_Management.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin - Manage Customers</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 40px;">
        <h2>Delete Customer</h2>

        <asp:Label ID="Label1" runat="server" Text="Customer Email to Delete:" />
        <asp:TextBox ID="txtDeleteEmail" runat="server" />
        <br /><br />

        <asp:Button ID="btnDelete" runat="server" Text="Delete Customer" OnClick="btnDelete_Click" />
        <br /><br />

        <asp:Label ID="lblDeleteMessage" runat="server" ForeColor="Green" />
    </div>
</asp:Content>
