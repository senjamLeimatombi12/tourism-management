<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="Tourism_Management.Map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <!-- Dark Maroon Line -->
<!-- Dark Maroon Line -->
<div style="width: 100%; height: 0.5in; background-color: darkred; margin: 0; padding: 0;"></div>

<!-- Main Content with Whitesmoke Background -->
<div style="background-color: whitesmoke; margin: 0; padding-top: 0;">
    <div class="container pt-2"> <!-- Reduced padding from pt-5 to pt-2 -->
        <div class="row">
                  
            <!-- Map Section -->
            <div class="col-12 col-md-6 mb-4 mb-md-0">
                <iframe 
                    title="Map of Kangla in Imphal, Manipur"
                    aria-label="Google Map showing Kangla in Imphal"
                    src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d11355.256424990965!2d93.93238663494816!3d24.80880384740407!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x374927abba7f4ddd%3A0xb07673c3fdf9fc40!2sKangla%2C%20Imphal%2C%20Manipur%20795001!5e1!3m2!1sen!2sin!4v1752720797122!5m2!1sen!2sin"
                    width="100%" 
                    height="350" 
                    style="border:0;" 
                    allowfullscreen 
                    loading="lazy" 
                    referrerpolicy="no-referrer-when-downgrade">
                </iframe>
            </div>

            <!-- Text Section -->
            <div class="col-12 col-md-6">
                <section>
                    <h3>Kangla: The Kingdom of Manipur</h3>
                    <div style="height: 12px;"></div>
                    <p style="margin-bottom: 2px;">Address: Kangla, Imphal, Manipur 795001.</p>
                    <p style="margin-left: 64px; margin-top: 2px; margin-bottom: 4px;">Near Ima Keithel, Bir Tikendrajit Overbridge.</p>
                    <p>Map : Please refer the map on the left to see the route to Kangla</p>
                </section>
            </div>

        </div>
    </div>
</div>



</asp:Content>



