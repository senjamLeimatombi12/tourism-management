<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminFestivalBookingDateSetUp.aspx.cs" Inherits="Tourism_Management.AdminFestivalBookingDateSetUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <h2>Set Date Period For Booking Festival Ticket </h2>
        </center>
<br />

<!-- Centered container -->
<div style="display: flex; justify-content: center; gap: 50px; align-items: flex-start;">
    <!-- Start Date -->
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
<br /><br />
        <!-- Start Date -->
<asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
<asp:Calendar ID="StartDate" runat="server" 
              OnSelectionChanged="StartDate_SelectionChanged" 
              Width="300px"></asp:Calendar>

<!-- End Date -->
<asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
<asp:Calendar ID="EndDate" runat="server" 
              OnSelectionChanged="EndDate_SelectionChanged" 
              Width="300px"></asp:Calendar>

        
     
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Allow Continuous Day Booking" /><br />
<asp:CheckBox ID="CheckBox2" runat="server" Text="Enable Booking" /><br />

<asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
    </div>

    <!-- End Date -->
    <div>
        <br />
        <br />
     
       
    </div>
</div>

<br />


   
        


</asp:Content>
