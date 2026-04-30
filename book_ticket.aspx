<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="book_ticket.aspx.cs" Inherits="Tourism_Management.book_ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <h2 class="mb-4">Book Ticket</h2>
    </center>
    <div class="container">

        <div class="row mb-3">
            <div class="col-md-4">
                <label for="ddlPlaces">Select Tourist Place:</label>
            </div>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlPlaces" class="form-control" runat="server">
                    <asp:ListItem Text="Loktak Lake" Value="Loktak Lake" />
                    <asp:ListItem Text="Shiroy Hill" Value="Shiroy Hill" />
                    <asp:ListItem Text="Konung" Value="Konung" />
                    <asp:ListItem Text="Keibul Lamjao" Value="Keibul Lamjao" />
                </asp:DropDownList>
            </div>
        </div>

       
            
                 <div class="row mb-3">
            <div class="col-md-4">
         <label for="txtEntryFees"> Entry Fee:</label>
     </div>
     <div class="col-md-8">
         <asp:TextBox ID="txtEntryFee" class="form-control" runat="server"  placeholder="Entry Fee:"></asp:TextBox>
     </div>
 </div>
        
        

        <div class="row mb-3">
            <div class="col-md-4">
                <label for="txtPersons">Number of Persons:</label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtPersons" class="form-control" runat="server" placeholder="Enter number of persons"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-4">
                <label for="txtTotalCost">Total Cost: </label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtTotalCost" class="form-control" runat="server" ReadOnly="true" placeholder="Total cost:" OnTextChanged="txtTotalCost_TextChanged"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnCalculate" class=" btn btn-info  " runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
            </div>
        </div>

    </div>

</asp:Content>
