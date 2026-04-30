 <%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TicketPreview.aspx.cs" Inherits="Tourism_Management.TicketPreview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="ticket-preview">
    <asp:Image ID="imgTicketTemplate" runat="server" CssClass="ticket-bg" />

    <div class="ticket-details">
        <div><strong>Name:</strong> <asp:Label ID="lblName" runat="server" /></div>
        <div><strong>Festival:</strong> <asp:Label ID="lblFestival" runat="server" /></div>
        <div><strong>Date:</strong> <asp:Label ID="lblDate" runat="server" /></div>
        <div><strong>Ticket ID:</strong> <asp:Label ID="lblTicketID" runat="server" /></div>
        <div><strong>Venue:</strong> <asp:Label ID="lblVenue" runat="server" /></div>
        <div><strong>Cost:</strong> <asp:Label ID="lblCost" runat="server" /></div>
    </div>
</div>

<style>
.ticket-preview {
    position: relative;
    width: 800px;
    height: 500px;
}
.ticket-bg {
    width: 100%;
    height: 100%;
}
.ticket-details {
    position: absolute;
    top: 150px;  /* adjust based on template design */
    left: 200px;
    font-size: 18px;
    color: black;
    font-weight: bold;
}
.ticket-details div {
    margin-bottom: 10px;
}
</style>





</asp:Content>
