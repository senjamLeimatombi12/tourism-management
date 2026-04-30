<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="Tourism_Management.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  

    <div class="container mt-5">
        <div class="card shadow-sm border-0 rounded-3 bg-light">
<div class="card-header  bg-warning text-dark  text-center rounded-top-3 py-3">

                <h2 class="mb-0">Admin Dashboard</h2>
            </div>

            <div class="card-body p-4">

                <div class="mb-4 text-center">
                    <h4 class="text-dark fw-semibold">Welcome, Admin</h4>
                    <p class="text-muted">Use the controls below to manage users, festivals, and bookings</p>
                </div>

                <div class="row gy-4">
                    <div class="col-md-6">
                        <label class="form-label fw-bold text-secondary">Disable User Login</label>
                        <asp:Button ID="Button4" runat="server" CssClass="btn btn-outline-dark w-100  bg-warning-subtle" 
                            Text="Delete User / Disable Login" OnClick="btnUserRequests_Click" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label fw-bold text-secondary">Add Festival</label>
                        <asp:Button ID="Button5" runat="server" CssClass="btn btn-outline-dark w-100  bg-warning-subtle" 
                            Text="Add Festival" OnClick="btnFestivalRequests_Click" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label fw-bold text-secondary">Festival Booking Dates</label>
                        <asp:Button ID="Button6" runat="server" CssClass="btn btn-outline-dark w-100 bg-warning-subtle" 
                            Text="Set Festival Booking Dates" OnClick="btnTicketRequests_Click" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label fw-bold text-secondary">Ticket Pricing</label>
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-dark w-100  bg-warning-subtle" 
                            Text="Set Price of Ticket" OnClick="setPrice_Click" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label fw-bold text-secondary">Update User Information</label>
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-outline-dark w-100  bg-warning-subtle" 
                            Text="Update User Detail" OnClick="user_update_Click" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label fw-bold text-secondary">Ticket Booking</label>
                        <asp:Button ID="Button3" runat="server" CssClass="btn btn-outline-dark w-100  bg-warning-subtle" 
                            Text="Book Ticket" OnClick="book_ticket_Click" />
                    </div>
                </div>

            </div>

            <div class="card-footer text-center bg-white border-top rounded-bottom-3 py-2">
                <small class="text-muted">© 2025 Tourism Management Admin Control Panel</small>
            </div>
        </div>
    </div>

</asp:Content>







