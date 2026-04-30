<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="superAdminNewfestivalApproval.aspx.cs" Inherits="Tourism_Management.superAdminNewfestivalApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .section-box {
            margin: 20px 0;
            padding: 20px;
            background: #f7f7f7;
            border: 1px solid #ccc;
            border-radius: 10px;
        }

        .section-title {
            font-size: 22px;
            font-weight: bold;
            color: #444;
            margin-bottom: 20px;
        }

        .btn {
            padding: 8px 16px;
            margin: 2px;
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

        .table {
            width: 100%;
            border-collapse: collapse;
        }

        .table th,
        .table td {
            padding: 10px;
            border: 1px solid #ddd;
        }

        .table th {
            background-color: #333;
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Section 1: Festival Name Requests -->
    <div class="section-box">
        <div class="section-title">📨 Festival Name Requests</div>

        <asp:GridView ID="gvFestivalNameRequests" runat="server" AutoGenerateColumns="False" class="table" OnRowCommand="gvFestivalNameRequests_RowCommand">
            <Columns>
                <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                <asp:BoundField DataField="FestivalName" HeaderText="Festival Name" />
                <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnApprove" runat="server" Text="Approve"
                            CommandName="Approve" CommandArgument='<%# Eval("RequestID") %>' CssClass="btn btn-approve" />
                        <asp:Button ID="btnReject" runat="server" Text="Reject"
                            CommandName="Reject" CommandArgument='<%# Eval("RequestID") %>' CssClass="btn btn-reject" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Section 2: Festival Cost Requests -->
    <div class="section-box">
        <div class="section-title">💰 Festival Cost Requests</div>

        <asp:GridView ID="gvFestivalCostRequests" runat="server" AutoGenerateColumns="False" CssClass="table"
            OnRowCommand="gvFestivalCostRequests_RowCommand">
            <Columns>
                <asp:BoundField DataField="FestivalName" HeaderText="Festival Name" />
                <asp:BoundField DataField="RequestedCost" HeaderText="Requested Cost" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="Message" HeaderText="Message" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="RequestDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnApproveCost" runat="server" Text="Approve"
                            CommandName="Approve" CommandArgument='<%# Eval("RequestID") %>' CssClass="btn btn-approve" />
                        <asp:Button ID="btnRejectCost" runat="server" Text="Reject"
                            CommandName="Reject" CommandArgument='<%# Eval("RequestID") %>' CssClass="btn btn-reject" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
