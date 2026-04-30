<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SuperAdmin_Login.aspx.cs" Inherits="Tourism_Management.SuperAdmin_Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
    <div class="row">
        <div class="col-md-4 mx-auto">

            <div class= "card">
                <div class="card-body">
                    <div class="row">
                        <div class="col">
                           
                                <center>
                                    <imgwidth="250px" src="imgs/icon-person.jpg" />
                                </center>
                            </div>
                      
                    </div>
                    <div class="row">
                        <div class="col">
                            <h4>Super Admin Login</h4>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr />
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col">
                            <label>Super_Admin ID</label>
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="TextBox1"
                            runat="server" placeholder= "Member ID"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="height: 10px;"></div>

                    <label>Password</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="TextBox2" 
                            runat="server" placeholder="Password" ></asp:TextBox>
                    </div>
                    <div style="height: 15px;"></div>

                    <div>
                        <center>
<asp:Button ID="Button1" runat="server" Text="Login" 
            CssClass="btn btn-primary btn-lg px-3" OnClick="Button1_Click" />
                    </center>
                            </div>

                   




                </div>
            </div>

            <a href="homepage.aspx"> << Back to Home</Back></a><br><br>
        </div>
    </div>
</div>
</asp:Content>
