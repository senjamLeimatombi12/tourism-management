<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="profile_update.aspx.cs" Inherits="Tourism_Management.profile_update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!DOCTYPE html>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .profile-card {
            max-width: 900px;
            margin: 40px auto;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 6px 20px rgba(0,0,0,0.1);
        }
        .profile-card-header {
            background: linear-gradient(135deg, #007bff, #00c6ff);
            color: white;
            padding: 25px;
            text-align: center;
        }
        .profile-card-header h2 {
            margin: 0;
            font-weight: 600;
        }
        .form-section {
            margin-bottom: 30px;
        }
        .form-section h5 {
            border-bottom: 2px solid #e9ecef;
            padding-bottom: 8px;
            margin-bottom: 20px;
            color: #495057;
            font-weight: 600;
        }
        .error {
            color: #dc3545;
            font-size: 0.85em;
        }
        .btn-primary {
            padding: 12px 25px;
            border-radius: 8px;
            font-weight: 500;
            transition: all 0.3s ease;
        }
        .btn-primary:hover {
            background-color: #0069d9;
            transform: translateY(-2px);
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card profile-card">
        <div class="profile-card-header">
            <h2>Update Profile</h2>
        </div>
        <div class="card-body">
            <asp:Literal ID="SuccessMessage" runat="server" Visible="false" />

            <!-- Account Info -->
            <div class="form-section">
                <h5>Account Information</h5>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="txtName" class="form-label">Full Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                            ErrorMessage="Name is required." CssClass="error" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtUsername" class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                            ErrorMessage="Username is required." CssClass="error" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is required." CssClass="error" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtPassword" class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                        <small class="text-muted">Leave blank to keep existing password</small>
                    </div>
                </div>
            </div>

            <!-- Contact Info -->
            <div class="form-section">
                <h5>Contact Information</h5>
                <div class="row g-3">
                    <div class="col-md-12">
                        <label for="txtAddress" class="form-label">Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtState" class="form-label">State</label>
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label for="ddlCountry" class="form-label">Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-select">
                            <asp:ListItem Value="" Text="Select Country" Selected="True" />
                            <asp:ListItem Value="India" Text="India" />
                            <asp:ListItem Value="Other" Text="Other" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label for="txtPincode" class="form-label">Pincode</label>
                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtPhone" class="form-label">Phone</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtDOB" class="form-label">Date of Birth</label>
                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                </div>
            </div>

            <!-- Identity Info -->
            <div class="form-section">
                <h5>Identity Information</h5>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Nationality</label>
                        <div class="d-flex gap-3">
                            <asp:RadioButton ID="rbIndian" runat="server" GroupName="Nationality" Text="Indian" CssClass="form-check-input" />
                            <asp:RadioButton ID="rbForeigner" runat="server" GroupName="Nationality" Text="Foreigner" CssClass="form-check-input" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label for="ddlIDType" class="form-label">ID Type</label>
                        <asp:DropDownList ID="ddlIDType" runat="server" CssClass="form-select">
                            <asp:ListItem Value="" Text="Select ID Type" Selected="True" />
                            <asp:ListItem Value="Aadhaar" Text="Aadhaar" />
                            <asp:ListItem Value="Voter ID" Text="Voter ID" />
                            <asp:ListItem Value="Passport" Text="Passport" />
                            <asp:ListItem Value="Driver's License" Text="Driver's License" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label for="txtGovtID" class="form-label">Government ID</label>
                        <asp:TextBox ID="txtGovtID" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6" id="passportField" style="display:none;">
                        <label for="txtPassportNumber" class="form-label">Passport Number</label>
                        <asp:TextBox ID="txtPassportNumber" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>

            <!-- Submit -->
            <div class="text-center mt-4">
                <asp:Button ID="btnSubmit" runat="server" Text="Update Profile" CssClass="btn btn-primary px-5" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>

    <!-- Scripts -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(function () {
            // toggle passport field for foreigners
            $('input[name="Nationality"]').on("change", function () {
                if ($(this).val() === "Foreigner") {
                    $("#passportField").slideDown();
                } else {
                    $("#passportField").slideUp();
                }
            });
        });
    </script>

</asp:Content>


