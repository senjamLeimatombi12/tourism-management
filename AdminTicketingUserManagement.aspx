<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminTicketingUserManagement.aspx.cs" Inherits="Tourism_Management.AdminTicketingUserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <style>
        .section-box {
            padding: 20px;
            margin: 20px auto;
            width: 95%;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f8f9fa;
        }

        .section-title {
            font-size: 22px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .form-row {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            align-items: flex-start;
        }

        .form-group {
            flex: 1 1 30%;
            min-width: 250px;
        }

        .form-group label {
            font-weight: bold;
        }

        .form-group input, .form-group textarea {
            width: 100%;
            padding: 8px;
            margin-top: 5px;
            border-radius: 6px;
            border: 1px solid #ccc;
        }

        .btn {
            padding: 10px 18px;
            margin-top: 15px;
            border: none;
            border-radius: 6px;
            background-color: #007bff;
            color: white;
            cursor: pointer;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 25px;
        }

        .table th, .table td {
            padding: 12px;
            border: 1px solid #ccc;
            text-align: left;
        }

        .table th {
            background-color: #343a40;
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section-box">
        <div class="section-title">👤 User Update Request Panel</div>

        <div class="form-row">
            <div class="form-group">
                <label for="txtUserId">User ID:</label>
                <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" OnTextChanged="txtUserId_TextChanged" />
            </div>

            <div class="form-group">
                <label for="txtNewName">New Name:</label>
                <asp:TextBox ID="txtNewName" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtNewEmail">New Email:</label>
                <asp:TextBox ID="txtNewEmail" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtNewPhone">New Phone Number:</label>
                <asp:TextBox ID="txtNewPhone" runat="server" CssClass="form-control" />
            </div>
        </div>

        <div class="form-group" style="margin-top: 15px;">
            <label for="txtMessage">Message to Super Admin:</label>
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
        </div>

        <asp:Button ID="btnSendUpdateRequest" runat="server" Text="Send Update Request" CssClass="btn" OnClick="btnSendUpdateRequest_Click" />
    </div>

    <div class="section-box">
        <div class="section-title">📋 User List (Editable After Approval)</div>

        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowEditing="gvUsers_RowEditing"
            OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="true" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="EditStatus" HeaderText="Edit Status" />

                <asp:CommandField ShowEditButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
