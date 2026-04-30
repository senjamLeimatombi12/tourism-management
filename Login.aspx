<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tourism_Management.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

    <title>Customer Login</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #74ebd5 0%, #9face6 100%);
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .login-container {
            background: #fff;
            padding: 40px 35px;
            border-radius: 15px;
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.2);
            width: 350px;
            text-align: center;
        }

        .login-container h2 {
            margin-bottom: 25px;
            color: #333;
            font-size: 24px;
            font-weight: 600;
        }

        .login-container label {
            display: block;
            text-align: left;
            font-size: 14px;
            margin-bottom: 6px;
            color: #444;
        }

        .login-container input[type="text"],
        .login-container input[type="password"] {
            width: 100%;
            padding: 10px 12px;
            border-radius: 8px;
            border: 1px solid #ccc;
            margin-bottom: 18px;
            font-size: 14px;
            transition: border 0.3s ease;
        }

        .login-container input[type="text"]:focus,
        .login-container input[type="password"]:focus {
            border-color: #6a5acd;
            outline: none;
        }

        .login-container input[type="submit"],
        .login-container button {
            width: 100%;
            padding: 12px;
            background: #6a5acd;
            border: none;
            border-radius: 8px;
            color: #fff;
            font-size: 15px;
            cursor: pointer;
            transition: background 0.3s ease;
        }

        .login-container input[type="submit"]:hover,
        .login-container button:hover {
            background: #5a4fcf;
        }

        .login-container .error-message {
            margin-top: 15px;
            font-size: 13px;
            color: red;
        }
    </style>

<body>
    
        <div class="login-container">
            <h2>Customer Login</h2>

            <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
            <asp:TextBox ID="txtEmail" runat="server" />

            <asp:Label ID="lblPassword" runat="server" Text="Password:" AssociatedControlID="txtPassword" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />

            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />

            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" />
        </div>

</body>
</html>

</asp:Content>
