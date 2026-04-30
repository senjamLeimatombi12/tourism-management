 <%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserSignUp.aspx.cs" Inherits="Tourism_Management.UserSignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function toggleIDFields() {
            var nationality = document.getElementById('<%= ddlNationality.ClientID %>').value;
            var indianDiv = document.getElementById("indianIDSection");
            var foreignDiv = document.getElementById("foreignIDSection");

            if (nationality === "Indian") {
                indianDiv.style.display = "block";
                foreignDiv.style.display = "none";
            } else if (nationality === "Foreigner") {
                indianDiv.style.display = "none";
                foreignDiv.style.display = "block";
            } else {
                indianDiv.style.display = "none";
                foreignDiv.style.display = "none";
            }
        }
    </script>



    <div class="container">
        <div class="row">
            <div class="col-md-7 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col text-center">
                                <img width="150px" src="imgs/11%20(1).jpg" />
                                <h4 class="mt-2">Member Sign Up</h4>
                                <hr />
                            </div>
                        </div>

                        <!-- Full Name / Email -->
                        <div class="row">
                            <div class="col-md-6">
                                <label>Full Name</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Full Name"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Email ID</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Email ID" TextMode="Email"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Contact / DOB -->
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <label>Contact No</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Contact Number" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Date of Birth</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <!-- State / Country / Pin -->
                        <div class="row mt-2">
                            <div class="col-md-4">
                                <label>State</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="State"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>Country</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Country"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>Pin Code</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Pin Code" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Address -->
                        <div class="row mt-2">
                            <div class="col">
                                <label>Full Address</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="Full Address" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Nationality -->
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <label>Nationality</label>
                                <asp:DropDownList CssClass="form-control" ID="ddlNationality" runat="server" onchange="toggleIDFields()">
                                    <asp:ListItem Text="-- Select --" Value="" />
                                    <asp:ListItem Text="Indian" Value="Indian" />
                                    <asp:ListItem Text="Foreigner" Value="Foreigner" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Govt ID for Indians -->
                        <div class="row mt-3" id="indianIDSection" style="display:none;">
                            <div class="col-md-6">
                                <label>Government ID Type</label>
                                <asp:DropDownList CssClass="form-control" ID="ddlGovtIDType" runat="server">
                                    <asp:ListItem Text="Aadhaar" Value="Aadhaar" />
                                    <asp:ListItem Text="PAN" Value="PAN" />
                                    <asp:ListItem Text="Driving License" Value="DrivingLicense" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label>ID Number</label>
                                <asp:TextBox CssClass="form-control" ID="txtGovtID" runat="server" placeholder="Enter Govt ID"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Passport for Foreigners -->
                        <div class="row mt-3" id="foreignIDSection" style="display:none;">
                            <div class="col-md-12">
                                <label>Passport Number</label>
                                <asp:TextBox CssClass="form-control" ID="txtPassport" runat="server" placeholder="Enter Passport Number"></asp:TextBox>
                            </div>
                        </div>

                        <!-- User ID / Password -->
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <label>Username</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Username"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Password</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Submit -->
                        <div class="form-group mt-4">
                            <asp:Button CssClass="btn bg-warning-subtle w-100" ID="Button1" runat="server" Text="Sign Up" OnClick="Button1_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

