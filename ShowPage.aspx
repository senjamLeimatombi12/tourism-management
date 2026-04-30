<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPage.aspx.cs" Inherits="Tourism_Management.ShowPage" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Tourism Ticket</title>
    <style>
        body {
            background-color: #f4f4f9;
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 40px;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }
        .ticket-container {
            background: url('<%= ResolveUrl("~/imgs/ticket2.png") %>') center/cover no-repeat,
                        linear-gradient(135deg, #f4f4f9, #e0e0e0);
            border: 3px solid #FFD700;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.3);
            width: 420px;
            min-height: 320px;
            position: relative;
            overflow: visible;
            font-family: 'Arial', sans-serif;
        }
        .ticket-overlay {
            background: rgba(255, 255, 255, 0.8);
            padding: 12px;
            border-radius: 10px;
            display: flex;
            flex-direction: column;
        }
        .ticket-header {
            background: linear-gradient(90deg, #FFE400, #FFA000);
            color: white;
            padding: 8px;
            text-align: center;
            border-radius: 8px 8px 0 0;
            font-size: 16px;
            font-weight: bold;
        }
        .ticket-header p {
            font-size: 10px;
            margin: 2px 0 0 0;
        }
        .ticket-body {
            padding: 10px;
            display: flex;
            justify-content: space-between;
            flex-grow: 1;
        }
        .ticket-details {
            flex: 2;
            font-size: 12px;
            line-height: 1.4;
        }
        .ticket-qr {
            flex: 1;
            text-align: center;
            padding-left: 10px;
        }
        .ticket-footer {
            background: #FFB300;
            color: white;
            padding: 6px;
            text-align: center;
            font-size: 10px;
            border-radius: 0 0 8px 8px;
        }
        .destination-image {
            width: 45px;
            height: 45px;
            object-fit: cover;
            border-radius: 50%;
            border: 1px solid #FFD700;
            margin-right: 6px;
            vertical-align: middle;
        }
        .mx-auto { margin-left: auto; margin-right: auto; }
        .mt-1 { margin-top: 4px; }
        .text-gray-700 { color: #4B5563; }
        .text-gray-600 { color: #6B7280; }
        @media print {
            .ticket-container {
                width: 420px;
                min-height: 320px;
            }
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
    <script>
        function downloadTicket() {
            const format = document.getElementById('downloadFormat').value;
            const ticket = document.getElementById('pnlTicket');
            html2canvas(ticket, {
                scale: 2,
                useCORS: true,
                backgroundColor: null
            }).then(canvas => {
                if (format === 'pdf') {
                    const { jsPDF } = window.jspdf;
                    const pdf = new jsPDF({
                        orientation: 'portrait',
                        unit: 'px',
                        format: [canvas.width / 2, canvas.height / 2]
                    });
                    const imgData = canvas.toDataURL('image/png');
                    pdf.addImage(imgData, 'PNG', 0, 0, canvas.width / 2, canvas.height / 2);
                    pdf.save('Tourism_Ticket.pdf');
                } else {
                    const link = document.createElement('a');
                    link.download = `Tourism_Ticket.${format}`;
                    link.href = canvas.toDataURL(`image/${format}`);
                    link.click();
                }
            }).catch(err => {
                alert('Failed to download ticket. Please try again.');
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlTicket" runat="server" CssClass="ticket-container">
            <div class="ticket-overlay">
                <div class="ticket-header">
                    <asp:Label ID="lblFestivalHeading" runat="server" Text="Festival / Tourist Place"></asp:Label>
                    <p>Your Official Tourism Ticket</p>
                </div>
                <div class="ticket-body">
                    <div class="ticket-details">
                        <h2>
                            <img src='<%= ResolveUrl("~/imgs/icon.jpeg") %>' class="destination-image" alt="Tourism" />
                            Traveler: <asp:Label ID="lblTravelerName" runat="server"></asp:Label>
                        </h2>
                        <p class="text-gray-700"><strong>Festival / Place:</strong>
                            <asp:Label ID="lblFestival" runat="server"></asp:Label>
                        </p>
                        <p class="text-gray-700"><strong>Venue:</strong>
                            <asp:Label ID="lblVenue" runat="server"></asp:Label>
                        </p>
                        <p class="text-gray-700"><strong>Date:</strong>
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </p>
                        <p class="text-gray-700"><strong>Seat No:</strong>
                            <asp:Label ID="lblSeatNo" runat="server"></asp:Label>
                        </p>
                        <p class="text-gray-700"><strong>Ticket No:</strong>
                            <asp:Label ID="lblTicketNo" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="ticket-qr">
                        <asp:Image ID="imgQrCode" runat="server" CssClass="mx-auto" style="width: 80px; height: 80px;" AlternateText="QR Code" />
                        <p class="text-gray-600 mt-1">Scan for Details</p>
                    </div>
                </div>
                <div class="ticket-footer">
                    Thank you for booking with us! Contact TourismManipur@gmail.com for queries.
                </div>
            </div>
        </asp:Panel>
        <div class="btn-container" style="text-align:center; margin-top:15px;">
            <select id="downloadFormat">
                <option value="png">PNG</option>
                <option value="jpeg">JPG</option>
                <option value="pdf">PDF</option>
            </select>
            <button type="button" onclick="downloadTicket()">Download Ticket</button>
        </div>
    </form>
</body>
</html>
