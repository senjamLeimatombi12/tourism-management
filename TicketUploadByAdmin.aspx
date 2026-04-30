<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TicketUploadByAdmin.aspx.cs" Inherits="Tourism_Management.TicketUploadByAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Ticket Numbers</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h3>Upload Ticket for Festivals / Places</h3>
    </center>
    <br />

   
    <asp:Label ID="Label1" runat="server" Text="Select Festival/Place: "></asp:Label>
    <asp:DropDownList ID="ddlFestival" runat="server" Style="width:8cm;"></asp:DropDownList>
    <br /><br />

   
    <div style="display:flex; align-items:center; margin-bottom:10px;">
        <asp:TextBox ID="txtFestivalName" runat="server" CssClass="form-control"
            Placeholder="Festival Name" Style="width:8cm; margin-right:5px;"></asp:TextBox>
    </div>

   
   <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />

        <asp:TextBox ID="txtTicketNumber" runat="server" CssClass="form-control"
    Placeholder="Enter Ticket ID" Style="width:6cm; margin-right:5px;"></asp:TextBox>

<asp:Button ID="btnUpload" runat="server" Text="Add Ticket"
    CssClass="btn btn-primary" OnClick="btnUpload_Click" />

<asp:Button ID="btnUpdate" runat="server" Text="Update Ticket"
    CssClass="btn btn-success" OnClick="btnUpdate_Click" />


    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
    <br /><br />

 
    <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
        OnRowCommand="gvTickets_RowCommand">
        <Columns>
            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" ReadOnly="True" />
            <asp:BoundField DataField="FestivalName" HeaderText="Festival Name" />
            <asp:BoundField DataField="FestivalID" HeaderText="Festival ID" />

         
            <asp:TemplateField HeaderText="Preview">
                <ItemTemplate>
                    <asp:Image ID="imgTicket" runat="server" Width="120px"
                        ImageUrl='<%# ResolveUrl(Convert.ToString(Eval("TicketPath"))) %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit"
                        CommandName="EditFestival" CommandArgument='<%# Eval("FestivalID") %>'
                        CssClass="btn btn-primary btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
