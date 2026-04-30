<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SuperAdminUserApproval.aspx.cs" Inherits="Tourism_Management.SuperAdminUserApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .section-box {
            padding: 20px;
            margin: 20px auto;
            width: 95%;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
        }

        .section-title {
            font-size: 22px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
        }

        .table th, .table td {
            padding: 12px;
            border: 1px solid #ccc;
        }

        .table th {
            background-color: #343a40;
            color: white;
        }

        .btn {
            padding: 6px 14px;
            border-radius: 5px;
            border: none;
            cursor: pointer;
        }

        .btn-approve {
            background-color: #28a745;
            color: white;
        }

        .btn-reject {
            background-color: #dc3545;
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section-box">
        <div class="section-title">🔐 User Edit Requests – Super Admin</div>

        <asp:GridView ID="gvEditRequests" runat="server" AutoGenerateColumns="False" CssClass="table"
            OnRowCommand="gvEditRequests_RowCommand" OnSelectedIndexChanged="gvEditRequests_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="RequestID" HeaderText="Request ID" ReadOnly="true" />
                <asp:BoundField DataField="UserID" HeaderText="User ID" />
                <asp:BoundField DataField="NewName" HeaderText="New Name" />
                <asp:BoundField DataField="NewEmail" HeaderText="New Email" />
                <asp:BoundField DataField="NewPhone" HeaderText="New Phone" />
                <asp:BoundField DataField="Message" HeaderText="Message" />
                <asp:BoundField DataField="Status" HeaderText="Status" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Eval("RequestID") %>' CssClass="btn btn-approve" />
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("RequestID") %>' CssClass="btn btn-reject" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <form id="form1" runat="server">
        <asp:GridView ID="gvRequests" runat="server" AutoGenerateColumns="False"
            DataKeyNames="UserID" OnRowCommand="gvRequests_RowCommand">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="User ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:ButtonField ButtonType="Button" CommandName="Approve" Text="Approve" />
            </Columns>
        </asp:GridView>
    </form>

</asp:Content>
