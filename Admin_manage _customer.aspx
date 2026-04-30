<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Admin_manage _customer.aspx.cs" Inherits="Tourism_Management.Admin_manage__customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <h2>Admin Manage Customer List</h2>
   </center>
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col"></div>
    <div style="width: 100%; float: left;">
        <asp:Label ID="lblID" runat="server" Text="Customer ID:" />
        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" OnTextChanged="txtID_TextChanged" /><br />

        <asp:Label ID="lblName" runat="server" Text="Customer Name:" />
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" /><br /><br />

        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-warning" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
    </div>
                    </div>
                    </div>
                </div>
                </div>
            
            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">

    <div style="width: 100%; float: right;">
        <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Placeholder="Search by name..." CssClass="form-control" OnTextChanged="txtSearch_TextChanged" /><br />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Festival ID" />
                <asp:BoundField DataField="Name" HeaderText="Festival Name" />
                <asp:CommandField ShowSelectButton="True" SelectText="Select" />
            </Columns>
        </asp:GridView>
    </div>

    <div style="clear: both;"></div>
        </div>
            </div>
                </div>
            </div>
    </div>
        </div>
</asp:Content>