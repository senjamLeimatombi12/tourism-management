<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserTicketing.aspx.cs" Inherits="Tourism_Management.UserTicketing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container { border: 1px solid #ddd; padding: 20px; margin: 20px auto; max-width: 900px; }
        .section-title { margin-top: 10px; margin-bottom: 10px; font-weight: 600; }
        .booking-section { border: 1px solid #ccc; padding: 15px; border-radius: 8px; }
        label { display:block; margin-top: 8px; }
    </style>

    <!-- jQuery + Bootstrap Datepicker (only one datepicker library to avoid conflicts) -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        function onNationalityChange() {
            var nat = $('#<%= ddlNationality.ClientID %>').val();
            var $passport = $('#<%= txtPassport.ClientID %>');
            var $govt = $('#<%= txtGovtID.ClientID %>');
            if (nat === "Indian") {
                $passport.prop('disabled', true).val('');
                $govt.prop('disabled', false);
            } else if (nat === "Foreigner") {
                $govt.prop('disabled', true).val('');
                $passport.prop('disabled', false);
            } else {
                $passport.prop('disabled', false);
                $govt.prop('disabled', false);
            }
        }

        function initFestivalDatepicker() {
            var start = $('#<%= hdnStartDate.ClientID %>').val() || null;
            var end = $('#<%= hdnEndDate.ClientID %>').val() || null;
            var $date = $('#<%= txtFestivalDate.ClientID %>');

            try { $date.datepicker('destroy'); } catch (e) { }

            $date.datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                startDate: start,
                endDate: end,
                daysOfWeekDisabled: [0] // disable Sundays
            });

            // If server supplied a start and the box is empty, prefill it
            if (start && !$date.val()) {
                $date.val(start);
            }
        }

        // Build the JSON we post in the hidden field
        function prepareBookingData() {
            var festivalId = $('#<%= ddlFestival.ClientID %>').val();
            var festivalName = $('#<%= ddlFestival.ClientID %> option:selected').text();
            var costPerTicket = $('#<%= lblCostPerTicket.ClientID %>').text().trim();
            var selectedDate = $('#<%= txtFestivalDate.ClientID %>').val();

            if (!festivalId) {
                alert("Please select a festival.");
                return false;
            }
            if (!selectedDate) {
                alert("Please select a festival date.");
                return false;
            }

            var booking = [{
                FestivalID: parseInt(festivalId),
                FestivalName: festivalName,
                TicketQuantity: 1,                // default 1 (since we don't show totals)
                TicketCost: parseFloat(costPerTicket || 0),
                SelectedDate: selectedDate        // yyyy-MM-dd
            }];

            $('#<%= hdnBookingData.ClientID %>').val(JSON.stringify(booking));
            return true;
        }

        $(function () {
            // apply initial nationality state
            onNationalityChange();

            // initialize festival datepicker with current hidden start/end
            initFestivalDatepicker();

            // When festival changes, the page does a postback (AutoPostBack=true).
            // After postback completes, server fills hidden start/end + cost,
            // and on document ready we re-init the datepicker with new limits.

            // Also ensure the Date (today date) doesn't exceed the festival range if you like
            // (not mandatory). We keep user's "Date" field independent.
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Festival Booking</h2>

        <!-- User Details -->
        <div class="section-title">User Details</div>
        <label>Name:</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />

        <label>Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />

        <label>Phone:</label>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />

        <label>State:</label>
        <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />

        <label>Country:</label>
        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" />

        <label>Address:</label>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />

        <label>Pincode:</label>
        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" />

        <label>Nationality:</label>
        <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-select" onchange="onNationalityChange()">
            <asp:ListItem Text="--Select--" Value="" />
            <asp:ListItem Text="Indian" Value="Indian" />
            <asp:ListItem Text="Foreigner" Value="Foreigner" />
        </asp:DropDownList>

        <label>Passport Number:</label>
        <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" />

        <label>Govt ID:</label>
        <asp:TextBox ID="txtGovtID" runat="server" CssClass="form-control" />

        <label>Today Date:</label>
        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" />

        <hr />

        <!-- Single Booking Block -->
        <div class="section-title">Booking</div>
        <div class="booking-section">
            <label>Festival:</label>
            <asp:DropDownList
                ID="ddlFestival"
                runat="server"
                CssClass="form-select"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlFestival_SelectedIndexChanged">
            </asp:DropDownList>

            <!-- Hidden fields to carry date limits set server-side -->
            <asp:HiddenField ID="hdnStartDate" runat="server" />
            <asp:HiddenField ID="hdnEndDate" runat="server" />

            <label>Cost Per Ticket:</label>
            <asp:Label ID="lblCostPerTicket" runat="server" CssClass="form-control" />

            <label>Select Date:</label>
            <asp:TextBox ID="txtFestivalDate" runat="server" CssClass="form-control" placeholder="yyyy-mm-dd" />
        </div>

        <!-- Hidden JSON for server -->
        <asp:HiddenField ID="hdnBookingData" runat="server" />

        <div class="mt-3">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                CssClass="btn btn-primary"
                OnClientClick="return prepareBookingData();"
                OnClick="btnSubmit_Click" />
        </div>

        <div class="mt-2">
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red" />
        </div>
    </div>
</asp:Content>
