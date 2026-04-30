<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SuperAdminTicketing.aspx.cs" Inherits="Tourism_Management.SuperAdminTicketing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2>👤 User Management (Full Access)</h2>
<asp:TextBox ID="txtSearch" runat="server" Placeholder="Search by Name/Email/ID" CssClass="form-control" />
<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
<br /><br />

<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowDeleting="gvUsers_RowDeleting">
    <Columns>
        <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="true" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Phone" HeaderText="Phone" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>
<br /><br />

<h3>🗂 Admin Request Inbox</h3>
<asp:TextBox ID="txtSearchMonth" runat="server" Placeholder="Search by Month (e.g., 07/2025)" CssClass="form-control" />
<asp:Button ID="btnSearchMonth" runat="server" Text="Search" OnClick="btnSearchMonth_Click" CssClass="btn btn-secondary" />
<br /><br />

<asp:Repeater ID="rptMessages" runat="server">
    <HeaderTemplate>
        <table class="table table-bordered"><tr><th>Name</th><th>Email</th><th>Message</th><th>Date</th><th>Status</th><th>Action</th></tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Eval("Name") %></td>
            <td><%# Eval("Email") %></td>
            <td><%# Eval("Message") %></td>
            <td><%# Eval("Date") %></td>
            <td><%# Eval("Status") %></td>
            <td>
                <asp:Button ID="btnApprove" runat="server" CommandArgument='<%# Eval("MessageID") %>' Text="Approve" OnClick="btnApprove_Click" />
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate></table></FooterTemplate>
</asp:Repeater>
<br />

</asp:Content>


