<%@ Page Title="ticketing" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ticketing.aspx.cs" Inherits="Tourism_Management.ticketing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Festival Booking</title>
    <style>
        .form-row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }
        .form-group {
            flex: 1;
            margin: 0 10px; 
        }
        .form-center {
            text-align: center;
        } 
    </style>
    <script type="text/javascript">
        function addMoreBookingSection() {
            var original = document.getElementById("bookingSection");
            var clone = original.cloneNode(true);
            clone.querySelector(".ddlFestival").selectedIndex = 0;
            clone.querySelector(".txtQty").value = "";
            clone.querySelector(".lblCost").innerText = "0";
            clone.querySelector(".lblTotal").innerText = "0";
            document.getElementById("bookingContainer").appendChild(clone);
        }

        function calculateOverallTotal() {
            var totals = document.querySelectorAll(".lblTotal");
            var overall = 0;
            totals.forEach(function (lbl) {
                overall += parseFloat(lbl.innerText || 0);
            });
            document.getElementById("txtOverallTotal").value = overall;
        }

        function calculateTotal(qtyInput) {
            var section = qtyInput.closest(".bookingBlock");
            var cost = parseFloat(section.querySelector(".lblCost").innerText || 0);
            var qty = parseInt(qtyInput.value || 0);
            section.querySelector(".lblTotal").innerText = cost * qty;
            calculateOverallTotal();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>User/Customer Info</h2>
    <div class="form-row">
        <div class="form-group">
            <asp:TextBox ID="txtName" runat="server" Placeholder="Full Name" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtPhone" runat="server" Placeholder="Phone" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="form-control" />
        </div>
    </div>
    <div class="form-row">
        <div class="form-group">
            <asp:TextBox ID="txtState" runat="server" Placeholder="State (Optional)" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtCountry" runat="server" Placeholder="Country" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtPincode" runat="server" Placeholder="Pincode" CssClass="form-control" />
        </div>
    </div>
    <div class="form-row">
        <div class="form-group">
            <asp:TextBox ID="txtAddress" runat="server" Placeholder="Address" CssClass="form-control" />
        </div>
    </div>
    <div class="form-row">
        <div class="form-group">
            <asp:TextBox ID="txtPassport" runat="server" Placeholder="Passport Number (for foreigner)" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtGovtId" runat="server" Placeholder="Govt ID (PAN/Aadhaar etc. for Indian)" CssClass="form-control" />
        </div>
    </div>

    <h3>Festival Booking</h3>

    <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
        <div class="form-row">
            <div class="form-group">
                <asp:TextBox ID="txtNewFestival" runat="server" Placeholder="Festival Name" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtFestivalCost" runat="server" Placeholder="Cost" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnAddFestival" runat="server" Text="Add Festival"  CssClass="form-control" />
            </div>
        </div>
        <asp:GridView ID="gvFestivals" runat="server" AutoGenerateColumns="false" >
            <Columns>
                <asp:BoundField DataField="FestivalName" HeaderText="Festival" />
                <asp:BoundField DataField="TicketCost" HeaderText="Cost" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteFestival" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <hr />
    </asp:Panel>

   <div id="bookingContainer">
    <div id="bookingSection" class="bookingBlock" style="display: flex; justify-content: space-between; margin-bottom: 20px; gap: 30px;">

        <!-- Left Side -->
        <div style="flex: 1;">
            <label>Festival:</label><br />
            <asp:DropDownList ID="ddlFestival" CssClass="ddlFestival" runat="server" AutoPostBack="true"  Width="100%" />
            <br /><br />

            <label>Quantity:</label><br />
            <input type="number" class="txtQty" onchange="calculateTotal(this)" style="width: 100%;" />
        </div>

        <!-- Right Side -->
        <div style="flex: 1;">
            <label>Cost per Ticket:</label><br />
            ₹<span class="lblCost">0</span><br /><br />

            <label>Total Cost:</label><br />
            ₹<span class="lblTotal">0</span>
        </div>

    </div>
</div>

<!-- Add More Button -->
<!-- Add More Button -->
<input type="button" value="Add More" onclick="addMoreBookingSection()" /><br /><br />

<!-- Overall Total -->
<h4>Total Overall Cost:</h4>
<asp:Label ID="lblOverallTotal" runat="server" Text="0" Font-Bold="true" Font-Size="Large" ForeColor="DarkGreen" />
<br />
    <br />

    <asp:Button ID="btnSubmit" runat="server" Text="Submit Booking" />
</asp:Content>
