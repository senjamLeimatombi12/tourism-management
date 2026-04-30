<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminTicketingFestivalManagement.aspx.cs" Inherits="Tourism_Management.AdminTicketingFestivalManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>
        .section-box {
            padding: 15px;
            margin-bottom: 25px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f4f4f4;
        }

        .section-title {
            font-size: 20px;
            font-weight: bold;
            margin-bottom: 12px;
            color: #333;
        }

        .form-group {
            display: flex;
            flex-wrap: wrap;
            gap: 30px;
        }

        .form-column {
            flex: 1;
            min-width: 300px;
        }

        .form-label {
            font-weight: bold;
            display: block;
            margin-top: 10px;
        }

        .btn {
            padding: 8px 16px;
            margin-top: 10px;
            border-radius: 6px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
            border: none;
        }

        .btn-success {
            background-color: #28a745;
            color: white;
            border: none;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }

        .table th,
        .table td {
            border: 1px solid #ccc;
            padding: 10px;
            text-align: left;
        }

        .table th {
            background-color: #444;
            color: #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Section 1: Festival Name Request -->
    <div class="section-box">
        <div class="section-title">🎉 Request New Festival Name</div>

        <div class="form-group">
            <div class="form-column">
                <asp:Label runat="server" AssociatedControlID="txtFestivalName" CssClass="form-label" Text="Festival Name:" />
                <asp:TextBox ID="txtFestivalName" runat="server" Width="100%" />
            </div>

            <div class="form-column">
                <asp:Label runat="server" AssociatedControlID="txtFestivalMessage" CssClass="form-label" Text="Message to Super Admin:" />
                <asp:TextBox ID="txtFestivalMessage" runat="server" Width="100%" TextMode="MultiLine" Rows="3" />
            </div>
        </div>

        <asp:Button ID="btnSubmitFestival" runat="server" Text="Submit Festival Name Request" CssClass="btn btn-primary" OnClick="btnSubmitFestival_Click" />
    </div>

    <!-- Section 2: Festival Cost Request -->
    <div class="section-box">
        <div class="section-title">💰 Request Festival Cost</div>

        <div class="form-group">
            <div class="form-column">
                <asp:Label runat="server" AssociatedControlID="ddlFestivalList" CssClass="form-label" Text="Select Festival:" />
                <asp:DropDownList ID="ddlFestivalList" runat="server" Width="100%" />
            </div>

            <div class="form-column">
                <asp:Label runat="server" AssociatedControlID="txtFestivalCost" CssClass="form-label" Text="Cost Per Ticket (INR):" />
                <asp:TextBox ID="txtFestivalCost" runat="server" Width="100%" />
            </div>

            <div class="form-column" style="flex-basis: 100%;">
                <asp:Label runat="server" AssociatedControlID="txtCostMessage" CssClass="form-label" Text="Message to Super Admin:" />
                <asp:TextBox ID="txtCostMessage" runat="server" Width="100%" TextMode="MultiLine" Rows="3" />
            </div>
        </div>

        <asp:Button ID="btnSubmitCost" runat="server" Text="Submit Cost Request" CssClass="btn btn-success" OnClick="btnSubmitCost_Click" />
    </div>

    <!-- Section 3: Status Grid -->
    <div class="section-box">
        <div class="section-title">📋 All Festival Requests Status</div>

        <asp:GridView ID="gvFestivalRequests" runat="server" CssClass="table" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="Request Type" DataField="RequestType" />
                <asp:BoundField HeaderText="Festival Name" DataField="FestivalName" />
                <asp:BoundField HeaderText="Requested Cost" DataField="RequestedCost" DataFormatString="{0:N2}" />
                <asp:BoundField HeaderText="Message" DataField="Message" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="Date" DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
